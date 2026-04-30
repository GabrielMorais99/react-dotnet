import { useEffect, useMemo, useState } from 'react'
import { useQuery } from '@tanstack/react-query'

import './App.css'
import heroImage from './assets/hero.png'
import { fetchJson } from './lib/api'

type HealthResponse = {
  status: string
}

type ReferenceLink = {
  label: string
  url: string
  kind: string
}

type ReferencePage = {
  id: string
  title: string
  summary: string
  definition: string
  useCases: string[]
  implementationNotes: string[]
  relatedPageIds: string[]
  links: ReferenceLink[]
}

type ReferenceCategory = {
  id: string
  title: string
  summary: string
  links: ReferenceLink[]
  pages: ReferencePage[]
}

type KnowledgeMap = {
  id: string
  title: string
  summary: string
  pageIds: string[]
}

type ReferenceCatalogResponse = {
  title: string
  description: string
  scope: string
  categories: ReferenceCategory[]
  knowledgeMaps: KnowledgeMap[]
}

type SavedWikiState = {
  activeCategoryId?: string
  activePageId?: string
  notes: string
}

type PageLocation = {
  category: ReferenceCategory
  page: ReferencePage
}

const STORAGE_KEY = 'wiki.technical.reference.v2'
const EMPTY_CATEGORIES: ReferenceCategory[] = []

function loadWikiState(): SavedWikiState {
  const fallback = { notes: '' }

  try {
    const raw = window.localStorage.getItem(STORAGE_KEY)
    if (!raw) {
      return fallback
    }

    const parsed = JSON.parse(raw) as Partial<SavedWikiState>
    return {
      activeCategoryId: parsed.activeCategoryId,
      activePageId: parsed.activePageId,
      notes: typeof parsed.notes === 'string' ? parsed.notes : '',
    }
  } catch {
    return fallback
  }
}

function countLinks(categories: ReferenceCategory[]): number {
  return categories.reduce(
    (total, category) =>
      total + category.links.length + category.pages.reduce((sum, page) => sum + page.links.length, 0),
    0,
  )
}

function buildPageIndex(categories: ReferenceCategory[]): Map<string, PageLocation> {
  const index = new Map<string, PageLocation>()

  categories.forEach((category) => {
    category.pages.forEach((page) => {
      index.set(page.id, { category, page })
    })
  })

  return index
}

function App() {
  const catalog = useQuery({
    queryKey: ['reference-catalog'],
    queryFn: () => fetchJson<ReferenceCatalogResponse>('/api/v1/reference/catalog'),
  })

  const health = useQuery({
    queryKey: ['health'],
    queryFn: () => fetchJson<HealthResponse>('/api/v1/health'),
    retry: 1,
  })

  const [wikiState, setWikiState] = useState<SavedWikiState>(() => loadWikiState())

  useEffect(() => {
    window.localStorage.setItem(STORAGE_KEY, JSON.stringify(wikiState))
  }, [wikiState])

  const categories = catalog.data?.categories ?? EMPTY_CATEGORIES
  const pageIndex = useMemo(() => buildPageIndex(categories), [categories])
  const activeCategory =
    categories.find((category) => category.id === wikiState.activeCategoryId) ?? categories[0]
  const activePage =
    activeCategory?.pages.find((page) => page.id === wikiState.activePageId) ??
    activeCategory?.pages[0]
  const totalPages = categories.reduce((sum, category) => sum + category.pages.length, 0)
  const totalLinks = countLinks(categories)
  const relatedPages = activePage
    ? activePage.relatedPageIds
        .map((pageId) => pageIndex.get(pageId))
        .filter((location): location is PageLocation => Boolean(location))
    : []
  const hasCatalog = catalog.isSuccess && categories.length > 0
  const hasEmptyCatalog = catalog.isSuccess && categories.length === 0
  const healthStatus = health.isSuccess ? health.data.status : health.isError ? 'offline' : 'verificando'
  const healthClassName = health.isSuccess
    ? 'status-ok'
    : health.isError
      ? 'status-error'
      : 'status-wait'
  const loadingCards = Array.from({ length: 4 }, (_, index) => `loading-card-${index}`)
  const loadingTopics = Array.from({ length: 6 }, (_, index) => `loading-topic-${index}`)

  function selectCategory(categoryId: string) {
    const nextCategory = categories.find((category) => category.id === categoryId)
    setWikiState((current) => ({
      ...current,
      activeCategoryId: categoryId,
      activePageId: nextCategory?.pages[0]?.id,
    }))
  }

  function selectPage(pageId: string, categoryId = activeCategory?.id) {
    setWikiState((current) => ({
      ...current,
      activeCategoryId: categoryId,
      activePageId: pageId,
    }))
  }

  return (
    <main className="app-shell">
      <a className="skip-link" href="#wiki-content">
        Pular para o conteúdo
      </a>

      <section className="hero-band" style={{ backgroundImage: `url(${heroImage})` }}>
        <div className="hero-copy">
          <p className="eyebrow">Wiki técnica</p>
          <h1>{catalog.data?.title ?? 'Wiki C#/.NET + React'}</h1>
          <p>
            {catalog.data?.description ??
              'Consulte conceitos, decisões, links e relações entre tecnologias.'}
          </p>
        </div>

        <div className="status-strip" aria-label="Resumo da wiki">
          <span>
            Escopo <strong>{catalog.data?.scope ?? 'Documentação técnica'}</strong>
          </span>
          <span>
            API <strong className={healthClassName}>{healthStatus}</strong>
          </span>
          <span>
            Categorias <strong>{categories.length}</strong>
          </span>
          <span>
            Páginas <strong>{totalPages}</strong>
          </span>
        </div>
      </section>

      {catalog.isPending && (
        <section className="loading-state" aria-label="Carregando wiki" aria-live="polite">
          <div className="overview-grid">
            {loadingCards.map((cardId) => (
              <article className="metric-skeleton" key={cardId}>
                <span />
                <p />
              </article>
            ))}
          </div>

          <section className="wiki-layout skeleton-layout">
            <aside className="track-rail skeleton-panel">
              <span className="skeleton-line wide" />
              <span className="skeleton-line" />
              <span className="skeleton-line" />
              <span className="skeleton-line" />
            </aside>

            <section className="topic-index skeleton-panel">
              {loadingTopics.map((topicId) => (
                <span className="skeleton-line wide" key={topicId} />
              ))}
            </section>

            <section className="wiki-article skeleton-panel">
              <span className="skeleton-line title" />
              <span className="skeleton-line wide" />
              <span className="skeleton-box" />
            </section>
          </section>
        </section>
      )}

      {catalog.isError && (
        <section className="feedback feedback-error" role="alert">
          <div>
            <p className="eyebrow">Falha ao carregar</p>
            <h2>Não foi possível buscar a wiki</h2>
            <p>
              {catalog.error instanceof Error ? catalog.error.message : 'Erro desconhecido'}.
              Verifique se a API está rodando e tente novamente.
            </p>
          </div>
          <button className="primary-button" type="button" onClick={() => void catalog.refetch()}>
            Tentar novamente
          </button>
        </section>
      )}

      {hasEmptyCatalog && (
        <section className="empty-state" id="wiki-content">
          <p className="eyebrow">Wiki vazia</p>
          <h2>Nenhuma categoria disponível</h2>
          <p>A API respondeu sem páginas de referência. Quando houver dados, eles aparecerão aqui.</p>
          <button className="primary-button" type="button" onClick={() => void catalog.refetch()}>
            Atualizar
          </button>
        </section>
      )}

      {hasCatalog && activeCategory && (
        <>
          <section className="overview-grid" id="wiki-content" aria-label="Indicadores da wiki">
            <article>
              <span className="metric">{categories.length}</span>
              <p>categorias de documentação</p>
            </article>
            <article>
              <span className="metric">{totalPages}</span>
              <p>páginas consultáveis</p>
            </article>
            <article>
              <span className="metric">{totalLinks}</span>
              <p>links de conhecimento</p>
            </article>
            <article>
              <span className="metric">{relatedPages.length}</span>
              <p>páginas relacionadas</p>
            </article>
          </section>

          <section className="wiki-layout">
            <aside className="track-rail" aria-label="Categorias da wiki">
              <div className="rail-heading">
                <p className="eyebrow">Categorias</p>
                <h2>Documentação</h2>
              </div>

              <div className="track-list">
                {categories.map((category) => (
                  <button
                    aria-current={category.id === activeCategory.id ? 'true' : undefined}
                    className={category.id === activeCategory.id ? 'track-tab active' : 'track-tab'}
                    key={category.id}
                    onClick={() => selectCategory(category.id)}
                    type="button"
                  >
                    <span>
                      <strong>{category.title}</strong>
                      <small>{category.pages.length} páginas</small>
                    </span>
                  </button>
                ))}
              </div>
            </aside>

            <nav className="topic-index" aria-label={`Páginas de ${activeCategory.title}`}>
              <div className="rail-heading">
                <p className="eyebrow">Índice</p>
                <h2>{activeCategory.title}</h2>
                <p>{activeCategory.summary}</p>
              </div>

              <div className="category-links" aria-label="Links da categoria">
                {activeCategory.links.map((link) => (
                  <a href={link.url} key={link.url} rel="noreferrer" target="_blank">
                    <span>{link.kind}</span>
                    <strong>{link.label}</strong>
                  </a>
                ))}
              </div>

              {activeCategory.pages.length === 0 ? (
                <div className="empty-panel">
                  <h3>Nenhuma página nesta categoria</h3>
                  <p>Quando a API enviar páginas, elas aparecerão aqui.</p>
                </div>
              ) : (
                <div className="topic-link-list">
                  {activeCategory.pages.map((page) => (
                    <button
                      aria-current={page.id === activePage?.id ? 'page' : undefined}
                      className={page.id === activePage?.id ? 'topic-link active' : 'topic-link'}
                      key={page.id}
                      onClick={() => selectPage(page.id)}
                      type="button"
                    >
                      <strong>{page.title}</strong>
                      <span>{page.summary}</span>
                    </button>
                  ))}
                </div>
              )}
            </nav>

            <section className="wiki-article" aria-labelledby="page-title">
              {activePage ? (
                <>
                  <header className="article-heading">
                    <p className="eyebrow">{activeCategory.title}</p>
                    <h2 id="page-title">{activePage.title}</h2>
                    <p>{activePage.summary}</p>
                  </header>

                  <section className="article-section" aria-labelledby="definition-title">
                    <h3 id="definition-title">Definição</h3>
                    <p>{activePage.definition}</p>
                  </section>

                  <section className="article-section" aria-labelledby="use-cases-title">
                    <h3 id="use-cases-title">Quando consultar</h3>
                    <ul className="insight-list">
                      {activePage.useCases.map((useCase) => (
                        <li key={useCase}>{useCase}</li>
                      ))}
                    </ul>
                  </section>

                  <section className="article-section" aria-labelledby="notes-title">
                    <h3 id="notes-title">Notas de implementação</h3>
                    <ul className="insight-list">
                      {activePage.implementationNotes.map((note) => (
                        <li key={note}>{note}</li>
                      ))}
                    </ul>
                  </section>

                  <section className="article-section" aria-labelledby="knowledge-links-title">
                    <h3 id="knowledge-links-title">Links de conhecimento</h3>
                    <div className="knowledge-link-grid">
                      {activePage.links.map((link) => (
                        <a href={link.url} key={link.url} rel="noreferrer" target="_blank">
                          <span>{link.kind}</span>
                          <strong>{link.label}</strong>
                          <small>{link.url.replace(/^https?:\/\//, '')}</small>
                        </a>
                      ))}
                    </div>
                  </section>

                  <section className="article-section" aria-labelledby="related-title">
                    <h3 id="related-title">Páginas relacionadas</h3>
                    {relatedPages.length === 0 ? (
                      <p>Nenhuma relação cadastrada para esta página.</p>
                    ) : (
                      <div className="related-page-list">
                        {relatedPages.map(({ category, page }) => (
                          <button
                            className="related-page-link"
                            key={`${category.id}-${page.id}`}
                            onClick={() => selectPage(page.id, category.id)}
                            type="button"
                          >
                            <span>{category.title}</span>
                            <strong>{page.title}</strong>
                          </button>
                        ))}
                      </div>
                    )}
                  </section>
                </>
              ) : (
                <div className="empty-panel">
                  <h3>Selecione uma página</h3>
                  <p>Escolha um item do índice para abrir a documentação.</p>
                </div>
              )}
            </section>

            <aside className="notes-panel" aria-label="Notas da consulta">
              <div className="notes-heading">
                <div>
                  <h2>Notas</h2>
                  <p>Registre dúvidas, links e relações encontradas durante a consulta.</p>
                </div>
              </div>
              <textarea
                aria-label="Notas da consulta"
                onChange={(event) =>
                  setWikiState((current) => ({ ...current, notes: event.target.value }))
                }
                placeholder="Ex.: relacionar outbox com mensageria, revisar OpenTelemetry Collector, comparar Service Bus e RabbitMQ..."
                value={wikiState.notes}
              />
            </aside>
          </section>

          <section className="reference-section" aria-labelledby="reference-title">
            <div className="section-heading">
              <p className="eyebrow">Mapas de conhecimento</p>
              <h2 id="reference-title">Relações úteis entre páginas</h2>
            </div>
            {catalog.data.knowledgeMaps.length === 0 ? (
              <div className="empty-panel">
                <h3>Nenhum mapa cadastrado</h3>
                <p>Os mapas aparecerão aqui quando forem enviados pela API.</p>
              </div>
            ) : (
              <div className="reference-grid">
                {catalog.data.knowledgeMaps.map((knowledgeMap) => (
                  <article className="reference-card" key={knowledgeMap.id}>
                    <h3>{knowledgeMap.title}</h3>
                    <p>{knowledgeMap.summary}</p>
                    <div className="map-page-list">
                      {knowledgeMap.pageIds
                        .map((pageId) => pageIndex.get(pageId))
                        .filter((location): location is PageLocation => Boolean(location))
                        .map(({ category, page }) => (
                          <button
                            key={`${knowledgeMap.id}-${page.id}`}
                            onClick={() => selectPage(page.id, category.id)}
                            type="button"
                          >
                            {page.title}
                          </button>
                        ))}
                    </div>
                  </article>
                ))}
              </div>
            )}
          </section>
        </>
      )}
    </main>
  )
}

export default App
