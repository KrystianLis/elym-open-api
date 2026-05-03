---
name: lookup-docs
description: Fetches up-to-date documentation for libraries used in the project via context7
---

# Lookup Docs

## Arguments

The skill accepts two arguments:

```
/lookup-docs <library> <topic>
```

- `<library>` — name of the library or framework (e.g. `efcore`, `aspnetcore`, `testcontainers`, `wiremock`, `helm`)
- `<topic>` — specific question or topic to search for (e.g. `"migrations add column"`, `"minimal api routing"`)

Examples:
```
/lookup-docs efcore "migrations add column nullable"
/lookup-docs aspnetcore "options pattern configuration"
/lookup-docs testcontainers "postgresql fixture lifetime"
/lookup-docs wiremock "request matching body"
/lookup-docs helm "conditional template values"
```

## Libraries in this project

| Alias | Full name for resolve |
|---|---|
| `efcore` | Entity Framework Core |
| `aspnetcore` | ASP.NET Core |
| `testcontainers` | Testcontainers dotnet |
| `wiremock` | WireMock.Net |
| `go-http` | Go net/http |
| `helm` | Helm |
| `docker-compose` | Docker Compose |

## Steps

1. **Read the arguments** — first argument is the library, second is the topic. If either is missing, ask the user.

2. **Resolve the library ID** — call `mcp__context7__resolve-library-id` using the full name from the table above (or the name provided by the user if no alias matches).

3. **Fetch documentation** — call `mcp__context7__query-docs` with the resolved ID, the topic as `query`, and `tokens: 8000`.

4. **Answer** based on the fetched documentation. Quote specific excerpts rather than paraphrasing vaguely.

## Rules

- Always use context7 — do not answer from training memory for questions about API, syntax, or configuration.
- If the library is not in the alias list, still try to find it via `resolve-library-id`.
- If the documentation does not cover the topic, say so explicitly and share what you found.
