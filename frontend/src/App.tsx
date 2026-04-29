import { useEffect, useState } from 'react'
import { useQuery } from '@tanstack/react-query'

import './App.css'
import heroImage from './assets/hero.png'
import { fetchJson } from './lib/api'

type HealthResponse = {
  status: string
}

type StudyTopic = {
  id: string
  title: string
  summary: string
  practice: string
}

type StudyTrack = {
  id: string
  title: string
  summary: string
  estimatedHours: number
  topics: StudyTopic[]
}

type Capstone = {
  id: string
  title: string
  goal: string
  deliverables: string[]
}

type StudyWorkflowResponse = {
  title: string
  description: string
  weeklyFocusHours: number
  targetRole: string
  tracks: StudyTrack[]
  capstones: Capstone[]
}

type SavedWikiState = {
  activeTrackId?: string
  activeTopicId?: string
  notes: string
}

type FlowStep = {
  title: string
  description: string
}

const STORAGE_KEY = 'wiki.technical.reference.v1'
const HERO_TITLE = 'Wiki C#/.NET + React'
const HERO_DESCRIPTION =
  'Consulte conceitos, fluxos e relações entre tecnologias para entender como cada tema funciona em sistemas reais.'

const trackFlowSteps: Record<string, FlowStep[]> = {
  'backend-dotnet': [
    { title: 'Request', description: 'Cliente envia uma chamada HTTP para a API.' },
    { title: 'Endpoint', description: 'ASP.NET Core roteia a chamada para a feature correta.' },
    { title: 'Regra', description: 'Validação, caso de uso e política técnica são aplicados.' },
    { title: 'Dados', description: 'Persistência, cache ou integração externa respondem ao fluxo.' },
    { title: 'Response', description: 'A API devolve contrato, status code e telemetria.' },
  ],
  'frontend-react': [
    { title: 'Evento', description: 'Usuário interage com tela, formulário ou navegação.' },
    { title: 'Estado', description: 'React atualiza estado local, cache ou parâmetros.' },
    { title: 'Render', description: 'Componentes recalculam UI de acordo com dados e estados.' },
    { title: 'Feedback', description: 'Loading, erro, vazio e sucesso comunicam o resultado.' },
    { title: 'Ajuste', description: 'Medição e acessibilidade refinam a experiência.' },
  ],
  'architecture-microservices': [
    { title: 'Domínio', description: 'Entenda regras, linguagem e fronteiras do negócio.' },
    { title: 'Contrato', description: 'Defina API, eventos e responsabilidades do serviço.' },
    { title: 'Execução', description: 'Serviços colaboram por chamadas síncronas ou mensagens.' },
    { title: 'Consistência', description: 'Dados evoluem com transações, eventos ou compensações.' },
    { title: 'Operação', description: 'Deploy, observabilidade e ownership sustentam o sistema.' },
  ],
  'messaging-events': [
    { title: 'Publicação', description: 'Um serviço registra um fato ou comando relevante.' },
    { title: 'Broker', description: 'Fila, tópico ou stream distribui a mensagem.' },
    { title: 'Consumo', description: 'Consumers processam com idempotência e controle de erro.' },
    { title: 'Falha', description: 'Retries, DLQ e reprocessamento tratam exceções.' },
    { title: 'Evolução', description: 'Contratos e versões mantêm compatibilidade entre sistemas.' },
  ],
  'observability-sre': [
    { title: 'Sinal', description: 'Aplicação emite logs, métricas e traces.' },
    { title: 'Coleta', description: 'Agentes ou SDKs capturam o contexto técnico.' },
    { title: 'Correlação', description: 'Trace ID, span e labels conectam eventos.' },
    { title: 'Análise', description: 'Dashboards, alertas e SLOs mostram impacto real.' },
    { title: 'Ação', description: 'Runbooks e incidentes guiam correção e aprendizado.' },
  ],
  'cloud-devops': [
    { title: 'Código', description: 'Mudança entra no versionamento com revisão.' },
    { title: 'Pipeline', description: 'Build, testes e análise validam o pacote.' },
    { title: 'Artefato', description: 'Imagem, pacote ou manifesto é versionado.' },
    { title: 'Deploy', description: 'Ambiente recebe a alteração com configuração segura.' },
    { title: 'Operação', description: 'Métricas, rollback e custos fecham o ciclo.' },
  ],
  security: [
    { title: 'Ativo', description: 'Identifique dados, usuários e permissões sensíveis.' },
    { title: 'Ameaça', description: 'Modele abuso, fronteiras de confiança e exposição.' },
    { title: 'Controle', description: 'Valide entrada, proteja acesso e registre auditoria.' },
    { title: 'Verificação', description: 'Testes, revisão e scans reduzem risco residual.' },
    { title: 'Resposta', description: 'Monitoramento e plano de incidente sustentam defesa.' },
  ],
  'data-systems': [
    { title: 'Modelo', description: 'Dados representam entidades, relações e invariantes.' },
    { title: 'Consulta', description: 'Índices, filtros e projeções moldam performance.' },
    { title: 'Concorrência', description: 'Transações e isolamento protegem consistência.' },
    { title: 'Cache', description: 'Leitura frequente pode ser acelerada com invalidação clara.' },
    { title: 'Diagnóstico', description: 'Planos, métricas e locks explicam gargalos.' },
  ],
  'senior-engineering': [
    { title: 'Contexto', description: 'Defina problema, restrições e objetivos.' },
    { title: 'Alternativas', description: 'Compare tradeoffs técnicos e organizacionais.' },
    { title: 'Decisão', description: 'Documente escolha, impacto e critérios.' },
    { title: 'Execução', description: 'Divida mudanças, revise riscos e acompanhe entrega.' },
    { title: 'Evolução', description: 'Aprenda com operação, feedback e manutenção.' },
  ],
}

const defaultFlowSteps: FlowStep[] = [
  { title: 'Contexto', description: 'Entenda onde o tema aparece no sistema.' },
  { title: 'Conceito', description: 'Identifique os termos e responsabilidades principais.' },
  { title: 'Aplicação', description: 'Veja como o conceito entra em um fluxo real.' },
  { title: 'Riscos', description: 'Observe tradeoffs, falhas comuns e pontos de atenção.' },
  { title: 'Evolução', description: 'Conecte o tema com arquitetura e operação.' },
]

function loadWikiState(): SavedWikiState {
  const fallback = { notes: '' }

  try {
    const raw = window.localStorage.getItem(STORAGE_KEY)
    if (!raw) {
      return fallback
    }

    const parsed = JSON.parse(raw) as Partial<SavedWikiState>
    return {
      activeTrackId: parsed.activeTrackId,
      activeTopicId: parsed.activeTopicId,
      notes: typeof parsed.notes === 'string' ? parsed.notes : '',
    }
  } catch {
    return fallback
  }
}

function getKeyConcepts(topic: StudyTopic): string[] {
  return topic.summary
    .split(',')
    .map((concept) => concept.replace(/\.$/, '').trim())
    .filter(Boolean)
    .slice(0, 7)
}

function getTopicFlowSteps(track: StudyTrack, topic: StudyTopic): FlowStep[] {
  const baseSteps = trackFlowSteps[track.id] ?? defaultFlowSteps

  return baseSteps.map((step, index) =>
    index === 1
      ? {
          ...step,
          title: topic.title,
          description: `Aplique este tema considerando: ${getKeyConcepts(topic).slice(0, 3).join(', ')}.`,
        }
      : step,
  )
}

function App() {
  const workflow = useQuery({
    queryKey: ['study-workflow'],
    queryFn: () => fetchJson<StudyWorkflowResponse>('/api/v1/study/workflow'),
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

  const tracks = workflow.data?.tracks ?? []
  const activeTrack = tracks.find((track) => track.id === wikiState.activeTrackId) ?? tracks[0]
  const activeTopic =
    activeTrack?.topics.find((topic) => topic.id === wikiState.activeTopicId) ??
    activeTrack?.topics[0]
  const totalTopics = tracks.reduce((sum, track) => sum + track.topics.length, 0)
  const totalFlows = tracks.length
  const keyConcepts = activeTopic ? getKeyConcepts(activeTopic) : []
  const topicFlowSteps = activeTrack && activeTopic ? getTopicFlowSteps(activeTrack, activeTopic) : []
  const hasWorkflow = workflow.isSuccess && tracks.length > 0
  const hasEmptyWorkflow = workflow.isSuccess && tracks.length === 0
  const healthStatus = health.isSuccess ? health.data.status : health.isError ? 'offline' : 'verificando'
  const healthClassName = health.isSuccess
    ? 'status-ok'
    : health.isError
      ? 'status-error'
      : 'status-wait'
  const loadingCards = Array.from({ length: 4 }, (_, index) => `loading-card-${index}`)
  const loadingTopics = Array.from({ length: 6 }, (_, index) => `loading-topic-${index}`)

  function selectTrack(trackId: string) {
    const nextTrack = tracks.find((track) => track.id === trackId)
    setWikiState((current) => ({
      ...current,
      activeTrackId: trackId,
      activeTopicId: nextTrack?.topics[0]?.id,
    }))
  }

  function selectTopic(topicId: string) {
    setWikiState((current) => ({ ...current, activeTopicId: topicId }))
  }

  return (
    <main className="app-shell">
      <a className="skip-link" href="#wiki-content">
        Pular para o conteúdo
      </a>

      <section className="hero-band" style={{ backgroundImage: `url(${heroImage})` }}>
        <div className="hero-copy">
          <p className="eyebrow">Wiki técnica</p>
          <h1>{HERO_TITLE}</h1>
          <p>{HERO_DESCRIPTION}</p>
        </div>

        <div className="status-strip" aria-label="Resumo da wiki">
          <span>
            Escopo <strong>{workflow.data?.targetRole ?? 'C#/.NET + React'}</strong>
          </span>
          <span>
            API <strong className={healthClassName}>{healthStatus}</strong>
          </span>
          <span>
            Temas <strong>{totalTopics}</strong>
          </span>
          <span>
            Fluxos <strong>{totalFlows}</strong>
          </span>
        </div>
      </section>

      {workflow.isPending && (
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

      {workflow.isError && (
        <section className="feedback feedback-error" role="alert">
          <div>
            <p className="eyebrow">Falha ao carregar</p>
            <h2>Não foi possível buscar a wiki</h2>
            <p>
              {workflow.error instanceof Error ? workflow.error.message : 'Erro desconhecido'}.
              Verifique se a API está rodando e tente novamente.
            </p>
          </div>
          <button className="primary-button" type="button" onClick={() => void workflow.refetch()}>
            Tentar novamente
          </button>
        </section>
      )}

      {hasEmptyWorkflow && (
        <section className="empty-state" id="wiki-content">
          <p className="eyebrow">Wiki vazia</p>
          <h2>Nenhuma trilha disponível</h2>
          <p>A API respondeu sem temas para consulta. Quando houver dados, eles aparecerão aqui.</p>
          <button className="primary-button" type="button" onClick={() => void workflow.refetch()}>
            Atualizar
          </button>
        </section>
      )}

      {hasWorkflow && activeTrack && (
        <>
          <section className="overview-grid" id="wiki-content" aria-label="Indicadores da wiki">
            <article>
              <span className="metric">{tracks.length}</span>
              <p>trilhas de conhecimento</p>
            </article>
            <article>
              <span className="metric">{totalTopics}</span>
              <p>temas consultáveis</p>
            </article>
            <article>
              <span className="metric">{totalFlows}</span>
              <p>fluxos explicativos</p>
            </article>
            <article>
              <span className="metric">{keyConcepts.length}</span>
              <p>conceitos no tema atual</p>
            </article>
          </section>

          <section className="wiki-layout">
            <aside className="track-rail" aria-label="Trilhas da wiki">
              <div className="rail-heading">
                <p className="eyebrow">Trilhas</p>
                <h2>Base de consulta</h2>
              </div>

              <div className="track-list">
                {tracks.map((track) => (
                  <button
                    aria-current={track.id === activeTrack.id ? 'true' : undefined}
                    className={track.id === activeTrack.id ? 'track-tab active' : 'track-tab'}
                    key={track.id}
                    onClick={() => selectTrack(track.id)}
                    type="button"
                  >
                    <span>
                      <strong>{track.title}</strong>
                      <small>{track.topics.length} temas</small>
                    </span>
                  </button>
                ))}
              </div>
            </aside>

            <nav className="topic-index" aria-label={`Temas de ${activeTrack.title}`}>
              <div className="rail-heading">
                <p className="eyebrow">Índice</p>
                <h2>{activeTrack.title}</h2>
                <p>{activeTrack.summary}</p>
              </div>

              {activeTrack.topics.length === 0 ? (
                <div className="empty-panel">
                  <h3>Nenhum tema nesta trilha</h3>
                  <p>Quando a API enviar temas, eles aparecerão aqui.</p>
                </div>
              ) : (
                <div className="topic-link-list">
                  {activeTrack.topics.map((topic) => (
                    <button
                      aria-current={topic.id === activeTopic?.id ? 'page' : undefined}
                      className={topic.id === activeTopic?.id ? 'topic-link active' : 'topic-link'}
                      key={topic.id}
                      onClick={() => selectTopic(topic.id)}
                      type="button"
                    >
                      <strong>{topic.title}</strong>
                      <span>{topic.summary}</span>
                    </button>
                  ))}
                </div>
              )}
            </nav>

            <section className="wiki-article" aria-labelledby="topic-title">
              {activeTopic ? (
                <>
                  <header className="article-heading">
                    <p className="eyebrow">{activeTrack.title}</p>
                    <h2 id="topic-title">{activeTopic.title}</h2>
                    <p>{activeTopic.summary}</p>
                  </header>

                  <section className="article-section" aria-labelledby="concepts-title">
                    <h3 id="concepts-title">Conceitos principais</h3>
                    <div className="concept-grid">
                      {keyConcepts.map((concept) => (
                        <span className="concept-chip" key={concept}>
                          {concept}
                        </span>
                      ))}
                    </div>
                  </section>

                  <section className="article-section" aria-labelledby="how-it-works-title">
                    <h3 id="how-it-works-title">Como entender este tema</h3>
                    <p>
                      {activeTopic.title} deve ser lido como parte da trilha {activeTrack.title}. O
                      ponto central é entender quais responsabilidades o tema resolve, quais
                      decisões ele influencia e em qual etapa de um sistema real ele aparece.
                    </p>
                  </section>

                  <section className="article-section" aria-labelledby="flow-title">
                    <h3 id="flow-title">Fluxograma de funcionamento</h3>
                    <div className="flow-diagram" aria-label={`Fluxo de ${activeTopic.title}`}>
                      {topicFlowSteps.map((step, index) => (
                        <div className="flow-step" key={`${step.title}-${index}`}>
                          <span className="flow-number">{index + 1}</span>
                          <div>
                            <strong>{step.title}</strong>
                            <p>{step.description}</p>
                          </div>
                        </div>
                      ))}
                    </div>
                  </section>

                  <section className="article-section" aria-labelledby="attention-title">
                    <h3 id="attention-title">Pontos de atenção</h3>
                    <ul className="insight-list">
                      <li>Entenda o problema que o conceito resolve antes de escolher ferramenta.</li>
                      <li>Compare tradeoffs: simplicidade, manutenção, performance e operação.</li>
                      <li>Observe como o tema se conecta com testes, segurança e observabilidade.</li>
                    </ul>
                  </section>
                </>
              ) : (
                <div className="empty-panel">
                  <h3>Selecione um tema</h3>
                  <p>Escolha um item do índice para abrir a página da wiki.</p>
                </div>
              )}
            </section>

            <aside className="notes-panel" aria-label="Notas da consulta">
              <div className="notes-heading">
                <div>
                  <h2>Notas</h2>
                  <p>Registre dúvidas, links e relações que encontrar durante a consulta.</p>
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
              <p className="eyebrow">Mapas de referência</p>
              <h2 id="reference-title">Cenários para conectar os temas</h2>
            </div>
            {workflow.data.capstones.length === 0 ? (
              <div className="empty-panel">
                <h3>Nenhum cenário cadastrado</h3>
                <p>Os cenários aparecerão aqui quando forem enviados pela API.</p>
              </div>
            ) : (
              <div className="reference-grid">
                {workflow.data.capstones.map((capstone) => (
                  <article className="reference-card" key={capstone.id}>
                    <h3>{capstone.title}</h3>
                    <p>{capstone.goal}</p>
                    <ul>
                      {capstone.deliverables.map((deliverable) => (
                        <li key={deliverable}>{deliverable}</li>
                      ))}
                    </ul>
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
