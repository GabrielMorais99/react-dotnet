import { useQuery } from '@tanstack/react-query'

import './App.css'
import { fetchJson } from './lib/api'

type HelloResponse = {
  message: string
  utcNow: string
}

function App() {
  const hello = useQuery({
    queryKey: ['hello'],
    queryFn: () => fetchJson<HelloResponse>('/api/v1/hello'),
  })

  return (
    <main style={{ padding: '2rem', fontFamily: 'system-ui, sans-serif', maxWidth: 520 }}>
      <h1>Wiki · monorepo</h1>
      <p>React + Vite + TanStack Query → API minimal .NET 9 (<code>/api/v1</code>).</p>

      <section style={{ marginTop: '1.5rem' }}>
        <h2 style={{ fontSize: '1rem', fontWeight: 600 }}>GET /api/v1/hello</h2>
        {hello.isPending && <p>Carregando…</p>}
        {hello.isError && (
          <p role="alert">
            Erro: {hello.error instanceof Error ? hello.error.message : 'falha desconhecida'}
          </p>
        )}
        {hello.isSuccess && (
          <pre
            style={{
              background: '#0f172a',
              color: '#e2e8f0',
              padding: '1rem',
              borderRadius: 8,
              overflow: 'auto',
            }}
          >
            {JSON.stringify(hello.data, null, 2)}
          </pre>
        )}
      </section>
    </main>
  )
}

export default App
