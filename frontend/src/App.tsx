import { useEffect, useMemo, useState } from 'react'
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

type SavedStudyState = {
  activeTrackId?: string
  completedTopics: string[]
  notes: string
}

const STORAGE_KEY = 'wiki.study.curriculum.v1'

function createTopicId(trackId: string, topicId: string) {
  return `${trackId}:${topicId}`
}

function loadStudyState(): SavedStudyState {
  const fallback = { completedTopics: [], notes: '' }

  try {
    const raw = window.localStorage.getItem(STORAGE_KEY)
    if (!raw) {
      return fallback
    }

    const parsed = JSON.parse(raw) as Partial<SavedStudyState>
    return {
      activeTrackId: parsed.activeTrackId,
      completedTopics: Array.isArray(parsed.completedTopics) ? parsed.completedTopics : [],
      notes: typeof parsed.notes === 'string' ? parsed.notes : '',
    }
  } catch {
    return fallback
  }
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

  const [studyState, setStudyState] = useState<SavedStudyState>(() => loadStudyState())

  useEffect(() => {
    window.localStorage.setItem(STORAGE_KEY, JSON.stringify(studyState))
  }, [studyState])

  const completedTopicIds = useMemo(
    () => new Set(studyState.completedTopics),
    [studyState.completedTopics],
  )

  const tracks = workflow.data?.tracks ?? []
  const activeTrack = tracks.find((track) => track.id === studyState.activeTrackId) ?? tracks[0]
  const totalTopics = tracks.reduce((sum, track) => sum + track.topics.length, 0)
  const completedTopics = tracks.reduce(
    (sum, track) =>
      sum +
      track.topics.filter((topic) => completedTopicIds.has(createTopicId(track.id, topic.id)))
        .length,
    0,
  )
  const totalHours = tracks.reduce((sum, track) => sum + track.estimatedHours, 0)
  const progress = totalTopics > 0 ? Math.round((completedTopics / totalTopics) * 100) : 0
  const activeTrackProgress = activeTrack
    ? Math.round(
        (activeTrack.topics.filter((topic) =>
          completedTopicIds.has(createTopicId(activeTrack.id, topic.id)),
        ).length /
          activeTrack.topics.length) *
          100,
      )
    : 0

  function selectTrack(trackId: string) {
    setStudyState((current) => ({ ...current, activeTrackId: trackId }))
  }

  function toggleTopic(trackId: string, topicId: string) {
    setStudyState((current) => {
      const completed = new Set(current.completedTopics)
      const id = createTopicId(trackId, topicId)

      if (completed.has(id)) {
        completed.delete(id)
      } else {
        completed.add(id)
      }

      return { ...current, completedTopics: Array.from(completed) }
    })
  }

  function resetProgress() {
    setStudyState((current) => ({ ...current, completedTopics: [], notes: '' }))
  }

  return (
    <main className="app-shell">
      <section className="hero-band" style={{ backgroundImage: `url(${heroImage})` }}>
        <div className="hero-copy">
          <p className="eyebrow">Plano de estudos</p>
          <h1>{workflow.data?.title ?? 'Roadmap para sênior C# + React'}</h1>
          <p>{workflow.data?.description ?? 'Carregando seu plano de estudos...'}</p>
        </div>

        <div className="status-strip" aria-label="Resumo do plano de estudos">
          <span>
            Perfil <strong>{workflow.data?.targetRole ?? 'Sênior'}</strong>
          </span>
          <span>
            API{' '}
            <strong className={health.isSuccess ? 'status-ok' : 'status-wait'}>
              {health.isSuccess ? health.data.status : 'verificando'}
            </strong>
          </span>
          <span>
            Ritmo <strong>{workflow.data?.weeklyFocusHours ?? 0} h/semana</strong>
          </span>
          <span>
            Progresso <strong>{progress}%</strong>
          </span>
        </div>
      </section>

      {workflow.isPending && <p className="feedback">Carregando roadmap...</p>}

      {workflow.isError && (
        <section className="feedback feedback-error" role="alert">
          <p>
            Não foi possível carregar o roadmap:{' '}
            {workflow.error instanceof Error ? workflow.error.message : 'erro desconhecido'}.
          </p>
          <button type="button" onClick={() => void workflow.refetch()}>
            Tentar novamente
          </button>
        </section>
      )}

      {workflow.isSuccess && activeTrack && (
        <>
          <section className="overview-grid" aria-label="Indicadores do roadmap">
            <article>
              <span className="metric">{tracks.length}</span>
              <p>trilhas técnicas</p>
            </article>
            <article>
              <span className="metric">{totalTopics}</span>
              <p>tópicos de estudo</p>
            </article>
            <article>
              <span className="metric">{totalHours}</span>
              <p>horas estimadas</p>
            </article>
            <article>
              <span className="metric">{completedTopics}</span>
              <p>tópicos concluídos</p>
            </article>
          </section>

          <section className="study-layout">
            <aside className="track-rail" aria-label="Trilhas do roadmap">
              <div className="progress-panel">
                <div>
                  <span className="metric">{progress}%</span>
                  <span className="metric-label">do roadmap concluído</span>
                </div>
                <div className="progress-track" aria-hidden="true">
                  <div className="progress-value" style={{ width: `${progress}%` }} />
                </div>
              </div>

              <div className="track-list">
                {tracks.map((track) => {
                  const done = track.topics.filter((topic) =>
                    completedTopicIds.has(createTopicId(track.id, topic.id)),
                  ).length

                  return (
                    <button
                      className={track.id === activeTrack.id ? 'track-tab active' : 'track-tab'}
                      key={track.id}
                      onClick={() => selectTrack(track.id)}
                      type="button"
                    >
                      <span>
                        <strong>{track.title}</strong>
                        <small>
                          {done}/{track.topics.length} tópicos · {track.estimatedHours} h
                        </small>
                      </span>
                    </button>
                  )
                })}
              </div>
            </aside>

            <section className="work-panel" aria-labelledby="active-track-title">
              <div className="panel-heading">
                <div>
                  <p className="eyebrow">Trilha selecionada</p>
                  <h2 id="active-track-title">{activeTrack.title}</h2>
                  <p>{activeTrack.summary}</p>
                </div>
                <span className="time-pill">{activeTrackProgress}%</span>
              </div>

              <div className="topic-list">
                {activeTrack.topics.map((topic) => {
                  const topicId = createTopicId(activeTrack.id, topic.id)
                  const checked = completedTopicIds.has(topicId)

                  return (
                    <article className={checked ? 'topic-card done' : 'topic-card'} key={topic.id}>
                      <label>
                        <input
                          checked={checked}
                          onChange={() => toggleTopic(activeTrack.id, topic.id)}
                          type="checkbox"
                        />
                        <span>{topic.title}</span>
                      </label>
                      <p>{topic.summary}</p>
                      <div className="practice-box">
                        <strong>Prática:</strong> {topic.practice}
                      </div>
                    </article>
                  )
                })}
              </div>
            </section>

            <aside className="notes-panel" aria-label="Notas da sessão">
              <div className="notes-heading">
                <div>
                  <h2>Notas</h2>
                  <p>Registre dúvidas, decisões, links e próximos passos.</p>
                </div>
                <button type="button" onClick={resetProgress}>
                  Reiniciar
                </button>
              </div>
              <textarea
                aria-label="Notas de estudo"
                onChange={(event) =>
                  setStudyState((current) => ({ ...current, notes: event.target.value }))
                }
                placeholder="Ex.: estudar outbox com MassTransit, comparar Azure Service Bus com RabbitMQ, revisar OpenTelemetry Collector..."
                value={studyState.notes}
              />
            </aside>
          </section>

          <section className="capstone-section" aria-labelledby="capstone-title">
            <div className="section-heading">
              <p className="eyebrow">Projetos práticos</p>
              <h2 id="capstone-title">Capstones para consolidar senioridade</h2>
            </div>
            <div className="capstone-grid">
              {workflow.data.capstones.map((capstone) => (
                <article className="capstone-card" key={capstone.id}>
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
          </section>
        </>
      )}
    </main>
  )
}

export default App
