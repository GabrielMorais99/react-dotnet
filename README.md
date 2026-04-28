# Wiki — monorepo

- **backend/** — ASP.NET Core 9, Minimal APIs, rotas em `Features/V1` (vertical slice).
- **frontend/** — React 19, Vite 8, TypeScript, TanStack Query.

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (LTS)

## Desenvolvimento

Em dois terminais:

1. API (porta **5208** por padrão):

   ```bash
   dotnet run --project backend/Wiki.Api
   ```

2. SPA (Vite encaminha `/api` para a API):

   ```bash
   cd frontend && npm install && npm run dev
   ```

Abra o endereço do Vite (geralmente `http://localhost:5173`). A página chama `GET /api/v1/hello` via proxy.

Com a API rodando em **Development**, documentação Swagger UI: `http://localhost:5208/swagger` (HTTPS: `https://localhost:7030/swagger` conforme seu perfil).

### Build

```bash
dotnet build Wiki.sln
cd frontend && npm run build
```

## Configuração

- CORS: `backend/Wiki.Api/appsettings.json` → `Cors:AllowedOrigins`
- Proxy do Vite: `frontend/vite.config.ts` → alvo `http://localhost:5208`
- URL base da API no browser: `frontend/.env` com `VITE_API_BASE_URL` (vazio em dev com proxy)
