# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Structure

Two independently-built services live in this repo plus a Helm chart that deploys both with Postgres:

- `OpenApi/` — .NET 8 minimal-API gateway. Single endpoint `GET /latest-users` aggregates data from `UsersService`, persists it to Postgres via EF Core, then returns the most recent N users (N is `Configuration:LastLimit` from config).
  - `src/OpenApi/Core/` — Domain layer. Entities (`User`), interface contracts (`IUsersServiceClient`, `IUserRepository`), `IClock` abstraction. **No external dependencies.**
  - `src/OpenApi/Application/` — Use cases. `IDataAggregatorService` orchestrates upstream call → persist → return latest. DTOs (`UserDto`) live here.
  - `src/OpenApi/Infrastructure/` — Concrete implementations: `ApplicationDbContext` (Npgsql), `UserRepository`, typed `HttpClient` `UsersServiceClient`, options (`ConfOptions`, `DatabaseOptions`), `ErrorHandlerMiddleware`.
  - `src/OpenApi/Migrations/` — Auto-generated EF Core migrations, applied on startup.
  - `tests/OpenApi.Tests.Integration/` — In-process tests via `WebApplicationFactory<IApiMaker>` with Testcontainers Postgres + WireMock upstream.
  - `tests/E2E.OpenApiTests/` — Black-box tests against a running deployment, configured via `testConfiguration.json`.
- `UsersService/` — Go 1.21.5 HTTP server on port 8080 exposing `GET /api/users` and `GET /api/user`. Generates fake user data via `github.com/go-faker/faker/v4`. No persistence.
  - `cmd/server/main.go` — Entry point, registers `http.HandleFunc` routes.
  - `internal/handlers.go` / `logic.go` / `models.go` — Flat package: handlers, faker logic, the `User` struct.
- `helm/` — Helm chart `elym-app` deploying both services + Postgres as `NodePort` services (open-api on 30081, users-service on 30080, db on 30532).
- `docker-compose.yml` — Local dev stack (open-api on 5241, users-service on 8080, Postgres on 5432).
- `integration-tests-script.sh` / `e2e-tests-script.sh` — CI wrappers around the `dotnet test` invocations.

## Workflow

### Build
```bash
# .NET — also performs full type/nullable analysis
cd OpenApi && dotnet build -c Release

# Go — also performs type checking
cd UsersService && go build ./...
```

### Test
```bash
# .NET integration tests (Testcontainers Postgres + WireMock — requires Docker)
cd OpenApi && dotnet test tests/OpenApi.Tests.Integration -c Release

# .NET E2E tests (requires the Helm release running on localhost:30081)
cd OpenApi && dotnet test tests/E2E.OpenApiTests -c Release

# Single test class / method
cd OpenApi && dotnet test tests/OpenApi.Tests.Integration --filter "FullyQualifiedName~GetLatestUsersTests"

# Helper scripts (run from repo root)
./integration-tests-script.sh
./e2e-tests-script.sh
```

### Type / static checks
```bash
# .NET — `dotnet build` runs the C# compiler with <Nullable>enable</Nullable>; treat any CS8xxx warning as a blocker.
dotnet build -c Release

# Go
cd UsersService && go vet ./...
```

### Run the stack
```bash
# Local
docker-compose -f docker-compose.yml build
docker-compose -f docker-compose.yml up -d

# Kubernetes
helm install elympics ./helm/
helm uninstall elympics
```

### EF Core migrations
```bash
dotnet ef migrations add <Name> --project OpenApi/src/OpenApi
```
Migrations are auto-applied on startup in `Program.cs` — no manual `database update` step.

## Code Style

1. **C# uses file-scoped namespaces** (`namespace OpenApi.Application;`), `<Nullable>enable</Nullable>`, and `<ImplicitUsings>enable</ImplicitUsings>`. Don't add `using` directives that are already implicit, and don't add a block-scoped namespace.
2. **DTOs are `record`s, entities are `class`es.** `UserDto` is a positional record (immutable transport), `User` (Core entity) is a mutable class with EF-friendly properties. Don't mix the two roles.
3. **Wire DI through layer extension methods.** New services register via `AddCore` / `AddApplication` / `AddInfrastructure` in the corresponding `Extension(s).cs` — never call `services.Add*` directly from `Program.cs`.
4. **Cross-layer types live in `Core/Interfaces/`.** Infrastructure depends inward on Core; Core never references Application or Infrastructure. New repository or HTTP client → interface in `Core/Interfaces/Repositories/` or `Core/Interfaces/Clients/`, implementation in `Infrastructure/`.
5. **Bind config via strongly-typed options.** Use `services.Configure<TOptions>(configuration.GetRequiredSection("..."))` (see `ConfOptions`, `DatabaseOptions`) and inject `IOptions<T>` — don't read `IConfiguration` from business code.
6. **Async methods accept and forward `CancellationToken`** all the way to EF Core / `HttpClient` calls (the `IDataAggregatorService` → `IUserRepository` chain is the reference pattern).
7. **Go code keeps `cmd/` thin and business logic in `internal/`.** Handlers go in `internal/handlers.go`, pure logic in `internal/logic.go`, types in `internal/models.go`. The `internal` package is intentionally flat — don't introduce sub-packages without a strong reason.
8. **Configuration keys use `__` for nesting in env vars** (`Database__ConnectionString`, `Configuration__LastLimit`, `UsersService__BasePath`). Match this in `docker-compose.yml` and Helm `env:` blocks.

## Constraints

1. **Don't violate the inward dependency rule.** `Core/` must not reference `Application/` or `Infrastructure/`. `Application/` must not reference `Infrastructure/`. If a Core class needs infra behaviour, add an interface in `Core/Interfaces/` and implement it in `Infrastructure/`.
2. **Don't delete `OpenApi/src/OpenApi/IApiMaker.cs`.** It's an empty marker interface used solely as the type parameter for `WebApplicationFactory<IApiMaker>` in integration tests. Removing it breaks the test host.
3. **Never set `User.CreatedAt` manually.** `ApplicationDbContext.SaveChangesAsync` stamps it from the injected `IClock` for every newly-added `User`. Setting it in a repository or service will be overwritten and breaks `IClock`-based test determinism.
4. **Keep `LastLimit` synchronized across deployment + E2E config.** `helm/templates/open-api-deployment.yaml` sets `Configuration__LastLimit=7` and `OpenApi/tests/E2E.OpenApiTests/testConfiguration.json` asserts `LastLimit: 7`. Change one and the E2E suite will fail against the deployed stack — update both.
5. **EF Core migrations auto-apply on startup.** Any migration committed to `OpenApi/src/OpenApi/Migrations/` will run against the target database the moment a new build boots — irreversible migrations need extra care.
6. **`ErrorHandlerMiddleware` only leaks stack traces in `Development`.** Don't add error details to responses outside that environment, and don't bypass the middleware with per-endpoint try/catch — let it handle exceptions globally.
