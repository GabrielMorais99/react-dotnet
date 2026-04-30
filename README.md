# Wiki - monorepo

Aplicação de wiki técnica para consultar documentação de C#/.NET, React, arquitetura, mensageria, observabilidade, cloud, segurança, dados e engenharia sênior.

A SPA consome a API e apresenta categorias de documentação, índice de páginas, artigos conceituais, links oficiais, páginas relacionadas, mapas de conhecimento e notas locais de consulta. O objetivo é consulta rápida e documentação de referência, não um roteiro sequencial de estudo.

## Estrutura

- `backend/` - ASP.NET Core 9, Minimal APIs, Swagger e rotas em `Features/V1` usando organização por feature.
- `frontend/` - React 19, Vite 8, TypeScript e TanStack Query.

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) LTS

## Desenvolvimento

Use dois terminais.

1. API na porta `5208`:

   ```bash
   dotnet run --project backend/Wiki.Api
   ```

2. SPA com proxy Vite para `/api`:

   ```bash
   cd frontend
   npm install
   npm run dev
   ```

Abra o endereço exibido pelo Vite, normalmente `http://localhost:5173`.

Com a API em `Development`, o Swagger fica em:

- HTTP: `http://localhost:5208/swagger`
- HTTPS: `https://localhost:7030/swagger`

## Experiência da wiki

- **Categorias de documentação:** agrupam referências por área técnica, como backend .NET, frontend React, arquitetura, mensageria, observabilidade, cloud, segurança e dados.
- **Índice de páginas:** permite abrir qualquer assunto diretamente, sem ordem obrigatória.
- **Artigo de referência:** mostra resumo, definição, quando consultar e notas de implementação.
- **Links de conhecimento:** aponta para documentação oficial, guias, padrões e ferramentas relevantes.
- **Páginas relacionadas:** conecta assuntos que normalmente aparecem juntos em decisões técnicas.
- **Mapas de conhecimento:** agrupam páginas por cenários de consulta, como API em produção ou sistemas distribuídos.
- **Notas de consulta:** campo livre salvo no navegador para registrar dúvidas, links e relações entre temas.
- **Estados de interface:** loading, erro, vazio, foco acessível e layout responsivo para mobile/desktop.

## Persistência local

O frontend salva apenas estado de navegação e notas no `localStorage` do navegador.

- Chave atual: `wiki.technical.reference.v2`
- Dados salvos: categoria ativa, página ativa e notas de consulta.
- A API continua sendo a fonte das categorias, páginas, links e mapas.

## Endpoints

- `GET /api/v1/hello` - resposta básica da API.
- `GET /api/v1/health` - status simples usado pela SPA.
- `GET /api/v1/reference/catalog` - catálogo de documentação técnica consumido pela wiki.

## Scripts

Backend:

```bash
dotnet build Wiki.sln
dotnet run --project backend/Wiki.Api
```

Frontend:

```bash
cd frontend
npm install
npm run dev
npm run build
npm run lint
npm run preview
```

## Configuração

- CORS: `backend/Wiki.Api/appsettings.json` em `Cors:AllowedOrigins`.
- Proxy do Vite: `frontend/vite.config.ts`, alvo `http://localhost:5208`.
- URL base da API no browser: `frontend/.env` com `VITE_API_BASE_URL`. Em desenvolvimento, pode ficar vazio para usar o proxy do Vite.

## Validação

Comandos recomendados para validar a aplicação:

```bash
dotnet build Wiki.sln
cd frontend
npm run lint
npm run build
```
