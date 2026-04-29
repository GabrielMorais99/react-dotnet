namespace Wiki.Api.Features.V1;

public static class StudyEndpoints
{
    public static IEndpointRouteBuilder MapStudyEndpointsV1(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/study")
            .WithTags("study");

        group.MapGet("/workflow", () =>
            Results.Ok(new StudyWorkflowResponse(
                Title: "Wiki C#/.NET + React",
                Description: "Base de consulta para entender conceitos, relações e fluxos de backend .NET, React, arquitetura, microsserviços, mensageria, observabilidade, cloud e liderança técnica.",
                WeeklyFocusHours: 10,
                TargetRole: "Referência técnica C#/.NET + React",
                Tracks:
                [
                    new StudyTrackResponse(
                        Id: "backend-dotnet",
                        Title: "Backend C#/.NET",
                        Summary: "Dominar ASP.NET Core, C# moderno, APIs, persistência, performance e testes.",
                        EstimatedHours: 48,
                        Topics:
                        [
                            new StudyTopicResponse("csharp-modern", "C# moderno", "Records, pattern matching, async/await, LINQ, nullable reference types, generics e coleções.", "Implementar uma pequena biblioteca com validações, testes e benchmarks simples."),
                            new StudyTopicResponse("aspnet-core", "APIs com ASP.NET Core", "Minimal APIs, controllers, middleware, filtros, OpenAPI, versionamento, health checks e configuração.", "Criar uma API versionada com Swagger, validação, logs e health endpoint."),
                            new StudyTopicResponse("auth-api", "Autenticação e autorização", "JWT, OAuth2/OIDC, policies, roles, claims, refresh tokens e proteção de rotas.", "Proteger endpoints por policy e documentar o fluxo de autenticação."),
                            new StudyTopicResponse("data-access", "Persistência", "EF Core, migrations, relacionamentos, queries eficientes, Dapper quando fizer sentido e transações.", "Modelar um módulo com SQL, índices, migrations e testes de repositório."),
                            new StudyTopicResponse("testing-dotnet", "Testes backend", "Unitários, integração, Testcontainers, WebApplicationFactory, contratos e testes de regressão.", "Cobrir uma feature completa com testes de unidade e integração."),
                            new StudyTopicResponse("performance-dotnet", "Performance .NET", "Profiling, alocações, caching, pooling, async correto, paginação e streaming.", "Medir um endpoint lento, otimizar e registrar o antes e o depois.")
                        ]),
                    new StudyTrackResponse(
                        Id: "frontend-react",
                        Title: "Frontend React/TypeScript",
                        Summary: "Construir interfaces robustas, acessíveis, testáveis e performáticas.",
                        EstimatedHours: 42,
                        Topics:
                        [
                            new StudyTopicResponse("typescript", "TypeScript forte", "Tipos utilitários, generics, narrowing, discriminated unions, contratos de API e modelagem de estado.", "Tipar um fluxo de ponta a ponta sem any desnecessário."),
                            new StudyTopicResponse("react-state", "Estado e data fetching", "Hooks, estado local, TanStack Query, cache, invalidação, optimistic updates e erros.", "Criar uma tela CRUD com loading, erro, cache e revalidação."),
                            new StudyTopicResponse("react-forms", "Formulários", "Validação, estados de erro, acessibilidade, máscaras, submit seguro e feedback ao usuário.", "Implementar um formulário de criação e edição com validação completa."),
                            new StudyTopicResponse("react-performance", "Performance React", "Renderização, memoização quando medida, code splitting, Suspense, transitions e profiling.", "Identificar e corrigir uma interação lenta usando medição."),
                            new StudyTopicResponse("frontend-quality", "Qualidade frontend", "Componentização, design system, testes com Testing Library, E2E e revisão visual.", "Criar uma suíte de testes para um fluxo crítico."),
                            new StudyTopicResponse("a11y-ux", "Acessibilidade e UX", "Semântica HTML, foco, teclado, contraste, estados vazios, responsividade e feedback.", "Auditar uma tela e corrigir problemas de navegação por teclado.")
                        ]),
                    new StudyTrackResponse(
                        Id: "architecture-microservices",
                        Title: "Arquitetura e microsserviços",
                        Summary: "Projetar sistemas distribuídos com limites claros, resiliência e evolução segura.",
                        EstimatedHours: 56,
                        Topics:
                        [
                            new StudyTopicResponse("ddd", "DDD e bounded contexts", "Linguagem ubíqua, entidades, value objects, agregados, invariantes e context mapping.", "Desenhar os contextos de um domínio e justificar os limites."),
                            new StudyTopicResponse("clean-architecture", "Arquitetura em camadas", "Clean Architecture, vertical slices, casos de uso, domínio, infraestrutura e fronteiras.", "Refatorar uma feature para separar regra de negócio de detalhes externos."),
                            new StudyTopicResponse("api-design", "Design de APIs", "REST, gRPC, contratos, versionamento, idempotência, paginação, erros e compatibilidade.", "Criar contrato de API com exemplos de sucesso e erro."),
                            new StudyTopicResponse("resilience", "Resiliência", "Timeouts, retries, circuit breakers, bulkheads, rate limiting, fallback e degradação graciosa.", "Adicionar policies de resiliência em chamadas HTTP externas."),
                            new StudyTopicResponse("distributed-data", "Dados distribuídos", "Consistência eventual, sagas, outbox, inbox, CQRS, read models e compensação.", "Implementar outbox conceitual ou documentar fluxo de saga."),
                            new StudyTopicResponse("service-boundaries", "Operação de microsserviços", "API gateway, service discovery, configuração, deploy independente, ownership e contratos.", "Criar um desenho de deploy com responsabilidades por serviço.")
                        ]),
                    new StudyTrackResponse(
                        Id: "messaging-events",
                        Title: "Mensageria e eventos",
                        Summary: "Usar filas, tópicos e streaming para desacoplar sistemas com confiabilidade.",
                        EstimatedHours: 38,
                        Topics:
                        [
                            new StudyTopicResponse("queues-topics", "Filas e tópicos", "Point-to-point, publish/subscribe, competing consumers, ordering, sessions e throughput.", "Modelar quando usar fila, tópico ou chamada síncrona."),
                            new StudyTopicResponse("brokers", "Brokers", "RabbitMQ, Azure Service Bus, Kafka, AMQP, particionamento, consumer groups e tradeoffs.", "Comparar brokers para três cenários de produto."),
                            new StudyTopicResponse("reliability-messaging", "Confiabilidade", "Retries, backoff, DLQ, poison messages, idempotência, deduplicação e reprocessamento.", "Criar estratégia para tratar mensagens com falha."),
                            new StudyTopicResponse("event-design", "Design de eventos", "Eventos de domínio, eventos de integração, schema evolution, versionamento e contratos.", "Desenhar eventos versionados para uma jornada de pedido."),
                            new StudyTopicResponse("stream-processing", "Processamento de streams", "Eventos ordenados, janelas, reprocessamento, lag, exactly-once como tradeoff e auditoria.", "Definir métricas de atraso e plano de reprocessamento.")
                        ]),
                    new StudyTrackResponse(
                        Id: "observability-sre",
                        Title: "Observabilidade e SRE",
                        Summary: "Instrumentar, monitorar e operar sistemas com logs, métricas, traces e SLOs.",
                        EstimatedHours: 36,
                        Topics:
                        [
                            new StudyTopicResponse("structured-logging", "Logs estruturados", "Correlation ID, trace ID, contexto, níveis, PII, amostragem e padrão de eventos.", "Padronizar logs para uma request atravessando serviços."),
                            new StudyTopicResponse("metrics", "Métricas", "Golden signals, RED/USE, latência, erro, saturação, throughput e métricas de negócio.", "Criar dashboard de saúde de API."),
                            new StudyTopicResponse("distributed-tracing", "Tracing distribuído", "Spans, traces, propagação de contexto, dependências e gargalos entre serviços.", "Instrumentar uma chamada API -> broker -> worker no desenho da arquitetura."),
                            new StudyTopicResponse("opentelemetry", "OpenTelemetry", "SDKs, auto-instrumentação, Collector, exporters e vendor neutrality.", "Definir pipeline OTel para ambiente local e produção."),
                            new StudyTopicResponse("slo-alerting", "SLOs e alertas", "SLI, SLO, error budget, alertas acionáveis, runbooks e postmortems.", "Escrever um SLO para endpoint crítico e um runbook de incidente.")
                        ]),
                    new StudyTrackResponse(
                        Id: "cloud-devops",
                        Title: "Cloud, DevOps e plataforma",
                        Summary: "Entregar software em produção com containers, pipelines, segurança e automação.",
                        EstimatedHours: 44,
                        Topics:
                        [
                            new StudyTopicResponse("containers", "Docker e imagens", "Dockerfile, multi-stage builds, imagens pequenas, variáveis, secrets e health checks.", "Containerizar API e frontend com builds reproduzíveis."),
                            new StudyTopicResponse("kubernetes", "Kubernetes", "Deployments, services, ingress, config maps, secrets, probes, HPA e troubleshooting.", "Criar manifesto de deploy para uma API .NET."),
                            new StudyTopicResponse("cicd", "CI/CD", "Build, testes, análise estática, artefatos, ambientes, migrations e rollback.", "Montar pipeline com build, testes e publicação de imagem."),
                            new StudyTopicResponse("iac", "Infra as Code", "Terraform/Bicep, ambientes, estado, módulos, revisão e drift.", "Descrever infra mínima para API, banco, cache e broker."),
                            new StudyTopicResponse("cloud-architecture", "Arquitetura cloud", "Azure App Service, Container Apps, AKS, Functions, Service Bus, Key Vault e custos.", "Escolher uma topologia cloud e defender os tradeoffs."),
                            new StudyTopicResponse("aspire", ".NET Aspire", "Orquestração local, service defaults, dashboard, integrações e apps distribuídos observáveis.", "Criar um plano para migrar o monorepo para Aspire quando houver mais serviços.")
                        ]),
                    new StudyTrackResponse(
                        Id: "security",
                        Title: "Segurança aplicada",
                        Summary: "Projetar e revisar software com segurança desde o design até a operação.",
                        EstimatedHours: 30,
                        Topics:
                        [
                            new StudyTopicResponse("owasp", "OWASP e ameaças comuns", "Injection, XSS, CSRF, SSRF, broken access control, secrets e supply chain.", "Revisar uma feature contra riscos OWASP."),
                            new StudyTopicResponse("threat-modeling", "Threat modeling", "Assets, trust boundaries, STRIDE, mitigações e riscos residuais.", "Criar threat model de uma API autenticada."),
                            new StudyTopicResponse("secure-coding", "Código seguro", "Validação, output encoding, criptografia correta, proteção de dados e auditoria.", "Adicionar validação e logs de auditoria em fluxo sensível."),
                            new StudyTopicResponse("identity-access", "Identidade e acesso", "RBAC, ABAC, least privilege, managed identities, rotação de secrets e Key Vault.", "Definir matriz de permissões para usuários e serviços.")
                        ]),
                    new StudyTrackResponse(
                        Id: "data-systems",
                        Title: "Dados, cache e performance",
                        Summary: "Projetar acesso a dados com consistência, escalabilidade e diagnóstico.",
                        EstimatedHours: 34,
                        Topics:
                        [
                            new StudyTopicResponse("sql-modeling", "Modelagem SQL", "Normalização, índices, constraints, planos de execução, locks e isolamento.", "Otimizar uma query e explicar o plano de execução."),
                            new StudyTopicResponse("transactions", "Transações", "ACID, isolamento, concorrência, deadlocks, optimistic concurrency e consistência.", "Resolver conflito de atualização concorrente."),
                            new StudyTopicResponse("caching", "Cache", "Redis, cache-aside, expiração, invalidação, stampede e cache distribuído.", "Adicionar estratégia de cache com invalidação clara."),
                            new StudyTopicResponse("search-analytics", "Busca e analytics", "Projeções, read models, Elasticsearch/OpenSearch, eventos para BI e auditoria.", "Projetar read model para consulta pesada.")
                        ]),
                    new StudyTrackResponse(
                        Id: "senior-engineering",
                        Title: "Senioridade e liderança técnica",
                        Summary: "Atuar como sênior: clareza técnica, autonomia, mentoria e decisões bem documentadas.",
                        EstimatedHours: 28,
                        Topics:
                        [
                            new StudyTopicResponse("system-design", "System design", "Requisitos, capacidade, tradeoffs, diagramas C4, riscos e plano de evolução.", "Produzir um desenho de sistema para um produto realista."),
                            new StudyTopicResponse("adrs", "ADRs e documentação", "Contexto, decisão, alternativas, consequências e revisão futura.", "Escrever ADR para broker, banco ou arquitetura."),
                            new StudyTopicResponse("code-review", "Code review sênior", "Riscos, legibilidade, testes, contratos, segurança, performance e feedback objetivo.", "Revisar uma PR focando bugs e riscos reais."),
                            new StudyTopicResponse("legacy", "Modernização de legado", "Strangler fig, refatoração incremental, cobertura de testes e redução de risco.", "Planejar migração incremental de módulo legado."),
                            new StudyTopicResponse("mentoring", "Mentoria e alinhamento", "Pareamento, comunicação, estimativas, negociação técnica e compartilhamento de contexto.", "Criar plano de mentoria para um dev pleno.")
                        ])
                ],
                Capstones:
                [
                    new CapstoneResponse(
                        Id: "learning-platform",
                        Title: "Plataforma de estudos distribuída",
                        Goal: "Evoluir este projeto para matérias, tópicos, sessões, progresso, revisões e notificações.",
                        Deliverables:
                        [
                            "API .NET com autenticação e persistência",
                            "Frontend React com CRUD, filtros, progresso e testes",
                            "Worker de revisão usando mensageria",
                            "OpenTelemetry com logs, métricas e traces",
                            "Pipeline CI/CD e containerização"
                        ]),
                    new CapstoneResponse(
                        Id: "commerce-orders",
                        Title: "Pedidos e pagamentos",
                        Goal: "Simular um sistema de pedidos com eventos, consistência eventual e observabilidade.",
                        Deliverables:
                        [
                            "Serviços de catálogo, pedidos e pagamento",
                            "Outbox e consumidores idempotentes",
                            "DLQ e estratégia de reprocessamento",
                            "Dashboard de SLO e runbook",
                            "ADR justificando limites de microsserviços"
                        ])
                ])));

        return app;
    }

    private sealed record StudyWorkflowResponse(
        string Title,
        string Description,
        int WeeklyFocusHours,
        string TargetRole,
        IReadOnlyList<StudyTrackResponse> Tracks,
        IReadOnlyList<CapstoneResponse> Capstones);

    private sealed record StudyTrackResponse(
        string Id,
        string Title,
        string Summary,
        int EstimatedHours,
        IReadOnlyList<StudyTopicResponse> Topics);

    private sealed record StudyTopicResponse(
        string Id,
        string Title,
        string Summary,
        string Practice);

    private sealed record CapstoneResponse(
        string Id,
        string Title,
        string Goal,
        IReadOnlyList<string> Deliverables);
}
