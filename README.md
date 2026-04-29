# Wiki - monorepo

Aplicação de wiki técnica para consultar conceitos de C#/.NET, React, arquitetura, mensageria, observabilidade, cloud, segurança, dados e senioridade técnica.

A SPA consome a API e apresenta trilhas de conhecimento, índice de temas, artigos conceituais, conceitos principais, pontos de atenção e fluxogramas visuais para explicar como cada tecnologia ou prática funciona em um fluxo real.

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

- **Trilhas de conhecimento:** agrupam temas por área técnica, como backend .NET, frontend React, arquitetura, mensageria, observabilidade, cloud, segurança e dados.
- **Índice por trilha:** permite selecionar um tema específico sem tratar a tela como checklist de estudos.
- **Artigo conceitual:** mostra resumo do tema, conceitos principais e como interpretar o assunto dentro da trilha.
- **Fluxograma de funcionamento:** apresenta etapas visuais para entender como o tema aparece em um sistema real.
- **Pontos de atenção:** destaca decisões, tradeoffs e conexões com testes, segurança e operação.
- **Notas de consulta:** campo livre salvo no navegador para registrar dúvidas, links e relações entre temas.
- **Estados de interface:** loading, erro, vazio, foco acessível e layout responsivo para mobile/desktop.

## Persistência local

O frontend salva apenas estado de navegação e notas no `localStorage` do navegador.

- Chave atual: `wiki.technical.reference.v1`
- Dados salvos: trilha ativa, tema ativo e notas de consulta.
- A API continua sendo a fonte dos temas, trilhas e cenários.

## Endpoints

- `GET /api/v1/hello` - resposta básica da API.
- `GET /api/v1/health` - status simples usado pela SPA.
- `GET /api/v1/study/workflow` - base de trilhas, temas e cenários consumida pela wiki.

Observação: o endpoint ainda usa o caminho `/study/workflow` por compatibilidade com a primeira versão do projeto. A interface atual usa esses dados como base de wiki técnica, não como checklist de progresso.

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

Comandos usados para validar a aplicação:

```bash
cd frontend
npm run lint
npx tsc -b
npx vite build --configLoader native --outDir dist-verify-run --emptyOutDir false
```

Para validar a API sem conflitar com um processo `Wiki.Api` já em execução:

```bash
dotnet build backend/Wiki.Api/Wiki.Api.csproj -o backend/Wiki.Api/bin/verify
```
