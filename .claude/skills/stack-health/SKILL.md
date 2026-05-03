---
name: stack-health
description: Diagnoses the state of the local dev stack — Docker containers and Postgres database
---

# Stack Health Check

Execute the following steps in order and display a concise summary at the end.

## 1. Docker — container status

Use the Docker MCP to check whether the following containers are running:
- `open-api`
- `users-service`
- `postgres`

For each container note: status (running/stopped/missing), uptime, and the last 10 log lines if the container is running.

## 2. Postgres — database state

Use the Postgres MCP to:
1. List tables in the `public` schema
2. For the `User` table (if it exists) — provide the row count and the date of the latest entry (`MAX(created_at)`)
3. Check the `__EFMigrationsHistory` table — provide the number of applied migrations and the name of the last one

## 3. Summary

Display the result in the following format:

```
=== STACK HEALTH ===

DOCKER
  open-api       [RUNNING | STOPPED | MISSING]  uptime: ...
  users-service  [RUNNING | STOPPED | MISSING]  uptime: ...
  postgres       [RUNNING | STOPPED | MISSING]  uptime: ...

POSTGRES
  Tables:        list of tables
  Users:         N rows, latest: YYYY-MM-DD HH:MM
  Migrations:    N applied, latest: <name>

STATUS: OK | DEGRADED | DOWN
```

If any container is not running or the Postgres MCP cannot connect — set STATUS to DEGRADED or DOWN and suggest a specific remediation command (e.g. `docker-compose up -d db`).
