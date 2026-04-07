# Trinity Jiu-Jitsu API

API para gerenciamento de academia de Jiu-Jitsu — Clean Architecture com .NET 9 + EF Core + SQLite.

## Configuração de ambiente (Docker Compose + credenciais JSON)

1. Copie os arquivos de exemplo:

```bash
cp .env.example .env
cp credentials/db-credentials.json.example credentials/db-credentials.json
```

2. Ajuste as credenciais no `.env` e no `credentials/db-credentials.json`.

3. Suba o SQL Server com Docker Compose:

```bash
docker compose up -d sqlserver
```

O arquivo `credentials/db-credentials.json` é montado no container em:

`/run/secrets/db-credentials.json`

## Rodar

```bash
cd src/TrinityJiuJitsu.Api
dotnet run
```

Acesse o Swagger em: **https://localhost:5001/swagger** (ou a porta exibida no terminal).

## Dados Seed

O banco SQLite (`trinity.db`) é criado automaticamente com:

| Entidade | Nome |
|----------|------|
| Gym | Trinity IBF |
| Branch | Sede Fortaleza |
| Class | Fundamentos - Segunda 19h |
| Student | Alan (Faixa Azul) |

## IDs Seed (para testar no Swagger)

- Gym: `11111111-1111-1111-1111-111111111111`
- Branch: `22222222-2222-2222-2222-222222222222`
- Class: `33333333-3333-3333-3333-333333333333`
- Student: `44444444-4444-4444-4444-444444444444`

## Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/gyms | Listar academias |
| POST | /api/gyms | Criar academia |
| GET | /api/branches/by-gym/{gymId} | Listar filiais |
| POST | /api/branches | Criar filial |
| GET | /api/classes/by-branch/{branchId} | Listar aulas |
| POST | /api/classes | Criar aula |
| GET | /api/students | Listar alunos |
| POST | /api/students | Criar aluno |
| POST | /api/attendances/check-in | Check-in aluno |
| GET | /api/attendances/by-class/{classId} | Presenças da aula |

## Arquitetura

```
Domain        → Entidades + Interfaces (sem dependências)
Application   → DTOs + Services (depende de Domain)
Infrastructure→ EF Core + Repositories (depende de Domain)
Api           → Controllers + DI (depende de tudo)
```
