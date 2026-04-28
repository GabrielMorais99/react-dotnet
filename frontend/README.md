# React + TypeScript + Vite

Este diretório faz parte do monorepo Wiki; para subir **API + SPA** juntos, consulte o `README.md` na raiz do repositório.

O template oferece uma configuração mínima para React com Vite, **HMR** e regras do **ESLint**.

Hoje existem dois plugins oficiais:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) usa [Oxc](https://oxc.rs)
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) usa [SWC](https://swc.rs/)

## React Compiler

O React Compiler **não** vem habilitado neste template por causa do impacto no desempenho em **dev** e no **build**. Para adicionar, veja [esta documentação](https://react.dev/learn/react-compiler/installation).

## Expandindo a configuração do ESLint

Se você estiver desenvolvendo uma aplicação em produção, recomendamos ajustar a configuração para habilitar regras de lint orientadas a tipos:

```js
export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Outras configs...

      // Remova tseslint.configs.recommended e use isto
      tseslint.configs.recommendedTypeChecked,
      // Ou regras mais estritas
      tseslint.configs.strictTypeChecked,
      // Opcional: regras estilísticas
      tseslint.configs.stylisticTypeChecked,

      // Outras configs...
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // outras opções...
    },
  },
])
```

Você também pode instalar [eslint-plugin-react-x](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-x) e [eslint-plugin-react-dom](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-dom) para regras específicas de React:

```js
// eslint.config.js
import reactX from 'eslint-plugin-react-x'
import reactDom from 'eslint-plugin-react-dom'

export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Outras configs...
      // Habilita regras de lint para React
      reactX.configs['recommended-typescript'],
      // Habilita regras de lint para React DOM
      reactDom.configs.recommended,
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // outras opções...
    },
  },
])
```
