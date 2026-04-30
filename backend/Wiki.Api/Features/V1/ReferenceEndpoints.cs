namespace Wiki.Api.Features.V1;

public static class ReferenceEndpoints
{
    public static IEndpointRouteBuilder MapReferenceEndpointsV1(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/reference")
            .WithTags("reference");

        group.MapGet("/catalog", () =>
            Results.Ok(new ReferenceCatalogResponse(
                Title: "Wiki C#/.NET + React",
                Description: "Base de consulta para linguagens, frameworks, arquitetura e práticas de engenharia usadas em sistemas reais.",
                Scope: "Documentação técnica C#/.NET, React, arquitetura, mensageria, observabilidade, cloud, segurança e dados",
                Categories:
                [
                    new ReferenceCategoryResponse(
                        Id: "backend-dotnet",
                        Title: "Backend C#/.NET",
                        Summary: "Referência para APIs, C# moderno, persistência, autenticação, testes e performance em .NET.",
                        Links:
                        [
                            new ReferenceLinkResponse("Documentação .NET", "https://learn.microsoft.com/en-us/dotnet/", "Oficial"),
                            new ReferenceLinkResponse("ASP.NET Core", "https://learn.microsoft.com/en-us/aspnet/core/", "Oficial"),
                            new ReferenceLinkResponse("C# Guide", "https://learn.microsoft.com/en-us/dotnet/csharp/", "Oficial")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse(
                                Id: "csharp-modern",
                                Title: "C# moderno",
                                Summary: "Records, pattern matching, async/await, LINQ, nullable reference types, generics e coleções.",
                                Definition: "Conjunto de recursos da linguagem que ajuda a modelar dados, expressar regras e escrever código seguro com menos ruído.",
                                UseCases:
                                [
                                    "Modelar contratos imutáveis com records.",
                                    "Reduzir condicionais complexas com pattern matching.",
                                    "Escrever código assíncrono sem bloquear threads da aplicação."
                                ],
                                ImplementationNotes:
                                [
                                    "Ative nullable reference types para tornar nullabilidade explícita.",
                                    "Use async/await ponta a ponta em fluxos de I/O.",
                                    "Prefira LINQ para consultas legíveis, mas meça consultas críticas."
                                ],
                                RelatedPageIds: ["aspnet-core", "testing-dotnet", "performance-dotnet"],
                                Links:
                                [
                                    new ReferenceLinkResponse("C# documentation", "https://learn.microsoft.com/en-us/dotnet/csharp/", "Oficial"),
                                    new ReferenceLinkResponse("Nullable reference types", "https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references", "Guia"),
                                    new ReferenceLinkResponse("Async programming", "https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/", "Guia")
                                ]),
                            new ReferencePageResponse(
                                Id: "aspnet-core",
                                Title: "APIs com ASP.NET Core",
                                Summary: "Minimal APIs, controllers, middleware, filtros, OpenAPI, versionamento, health checks e configuração.",
                                Definition: "Framework HTTP do .NET para construir APIs, aplicações web e serviços com pipeline configurável.",
                                UseCases:
                                [
                                    "Expor contratos REST para frontend, integrações e serviços internos.",
                                    "Centralizar autenticação, CORS, logs, validação e tratamento de erros.",
                                    "Publicar documentação OpenAPI para consumidores da API."
                                ],
                                ImplementationNotes:
                                [
                                    "Mantenha endpoints finos e mova regras para serviços ou casos de uso.",
                                    "Padronize erros, status codes e contratos de request/response.",
                                    "Use health checks e OpenAPI como parte do contrato operacional."
                                ],
                                RelatedPageIds: ["auth-api", "api-design", "structured-logging"],
                                Links:
                                [
                                    new ReferenceLinkResponse("ASP.NET Core fundamentals", "https://learn.microsoft.com/en-us/aspnet/core/fundamentals/", "Oficial"),
                                    new ReferenceLinkResponse("Minimal APIs", "https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis", "Guia"),
                                    new ReferenceLinkResponse("Health checks", "https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks", "Guia")
                                ]),
                            new ReferencePageResponse(
                                Id: "auth-api",
                                Title: "Autenticação e autorização",
                                Summary: "JWT, OAuth2/OIDC, policies, roles, claims, refresh tokens e proteção de rotas.",
                                Definition: "Camada que identifica usuários e serviços, valida credenciais e decide quais ações cada identidade pode executar.",
                                UseCases:
                                [
                                    "Proteger APIs públicas e internas por escopo, policy ou papel.",
                                    "Integrar login com provedores OIDC.",
                                    "Auditar ações sensíveis por usuário, serviço e tenant."
                                ],
                                ImplementationNotes:
                                [
                                    "Separe autenticação de autorização no desenho da solução.",
                                    "Evite regras de permissão espalhadas diretamente em componentes de UI.",
                                    "Revise expiração, rotação e armazenamento de tokens."
                                ],
                                RelatedPageIds: ["identity-access", "owasp", "secure-coding"],
                                Links:
                                [
                                    new ReferenceLinkResponse("ASP.NET Core security", "https://learn.microsoft.com/en-us/aspnet/core/security/", "Oficial"),
                                    new ReferenceLinkResponse("Authentication overview", "https://learn.microsoft.com/en-us/aspnet/core/security/authentication/", "Guia"),
                                    new ReferenceLinkResponse("Authorization policies", "https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies", "Guia")
                                ]),
                            new ReferencePageResponse(
                                Id: "data-access",
                                Title: "Persistência",
                                Summary: "EF Core, migrations, relacionamentos, queries eficientes, Dapper quando fizer sentido e transações.",
                                Definition: "Conjunto de decisões para gravar, consultar e evoluir dados preservando consistência e desempenho.",
                                UseCases:
                                [
                                    "Mapear entidades e relacionamentos no banco.",
                                    "Criar migrations versionadas.",
                                    "Escolher entre ORM, SQL direto e transações explícitas."
                                ],
                                ImplementationNotes:
                                [
                                    "Projete consultas para retornar apenas o necessário.",
                                    "Revise índices junto com filtros e ordenações reais.",
                                    "Use transações para mudanças que precisam ser atômicas."
                                ],
                                RelatedPageIds: ["sql-modeling", "transactions", "caching"],
                                Links:
                                [
                                    new ReferenceLinkResponse("EF Core documentation", "https://learn.microsoft.com/en-us/ef/core/", "Oficial"),
                                    new ReferenceLinkResponse("EF Core performance", "https://learn.microsoft.com/en-us/ef/core/performance/", "Guia"),
                                    new ReferenceLinkResponse("Dapper repository", "https://github.com/DapperLib/Dapper", "Referência")
                                ]),
                            new ReferencePageResponse(
                                Id: "testing-dotnet",
                                Title: "Testes backend",
                                Summary: "Unitários, integração, Testcontainers, WebApplicationFactory, contratos e regressão.",
                                Definition: "Estratégia para validar regras, contratos e integração da API sem depender apenas de testes manuais.",
                                UseCases:
                                [
                                    "Garantir comportamento de casos de uso.",
                                    "Validar endpoints com infraestrutura realista.",
                                    "Evitar regressão em contratos usados por consumidores."
                                ],
                                ImplementationNotes:
                                [
                                    "Teste unidade para regra pura e integração para contrato HTTP.",
                                    "Use dados controlados por teste para evitar dependência de ordem.",
                                    "Inclua casos de erro, autorização e validação."
                                ],
                                RelatedPageIds: ["aspnet-core", "cicd", "code-review"],
                                Links:
                                [
                                    new ReferenceLinkResponse("Integration tests in ASP.NET Core", "https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests", "Guia"),
                                    new ReferenceLinkResponse("Testcontainers for .NET", "https://dotnet.testcontainers.org/", "Ferramenta"),
                                    new ReferenceLinkResponse("xUnit", "https://xunit.net/", "Ferramenta")
                                ]),
                            new ReferencePageResponse(
                                Id: "performance-dotnet",
                                Title: "Performance .NET",
                                Summary: "Profiling, alocações, caching, pooling, async correto, paginação e streaming.",
                                Definition: "Diagnóstico e otimização de latência, throughput, uso de memória e custo operacional em aplicações .NET.",
                                UseCases:
                                [
                                    "Investigar endpoint lento.",
                                    "Reduzir alocações e contenção.",
                                    "Definir paginação, streaming ou cache para leituras pesadas."
                                ],
                                ImplementationNotes:
                                [
                                    "Meça antes de otimizar e registre o impacto da mudança.",
                                    "Evite carregar coleções grandes sem paginação.",
                                    "Observe CPU, memória, GC, dependências externas e banco."
                                ],
                                RelatedPageIds: ["metrics", "caching", "data-access"],
                                Links:
                                [
                                    new ReferenceLinkResponse(".NET diagnostics", "https://learn.microsoft.com/en-us/dotnet/core/diagnostics/", "Oficial"),
                                    new ReferenceLinkResponse("Performance best practices", "https://learn.microsoft.com/en-us/aspnet/core/performance/performance-best-practices", "Guia"),
                                    new ReferenceLinkResponse("BenchmarkDotNet", "https://benchmarkdotnet.org/", "Ferramenta")
                                ])
                        ]),
                    new ReferenceCategoryResponse(
                        Id: "frontend-react",
                        Title: "Frontend React/TypeScript",
                        Summary: "Referência para componentes, estado, data fetching, formulários, performance, UX e acessibilidade.",
                        Links:
                        [
                            new ReferenceLinkResponse("React", "https://react.dev/", "Oficial"),
                            new ReferenceLinkResponse("TypeScript", "https://www.typescriptlang.org/docs/", "Oficial"),
                            new ReferenceLinkResponse("TanStack Query", "https://tanstack.com/query/latest/docs/framework/react/overview", "Oficial")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse(
                                Id: "typescript",
                                Title: "TypeScript forte",
                                Summary: "Tipos utilitários, generics, narrowing, discriminated unions, contratos de API e modelagem de estado.",
                                Definition: "Uso de tipos para tornar contratos explícitos e reduzir estados impossíveis no frontend.",
                                UseCases:
                                [
                                    "Tipar respostas da API e estados de tela.",
                                    "Modelar fluxos com unions discriminadas.",
                                    "Evitar any em fronteiras críticas."
                                ],
                                ImplementationNotes:
                                [
                                    "Crie tipos próximos da fronteira de dados.",
                                    "Use narrowing para tratar estados carregando, erro, vazio e sucesso.",
                                    "Não use type assertion para esconder incerteza de runtime."
                                ],
                                RelatedPageIds: ["react-state", "api-design", "frontend-quality"],
                                Links:
                                [
                                    new ReferenceLinkResponse("TypeScript handbook", "https://www.typescriptlang.org/docs/handbook/intro.html", "Oficial"),
                                    new ReferenceLinkResponse("React with TypeScript", "https://react.dev/learn/typescript", "Guia"),
                                    new ReferenceLinkResponse("Utility types", "https://www.typescriptlang.org/docs/handbook/utility-types.html", "Referência")
                                ]),
                            new ReferencePageResponse(
                                Id: "react-state",
                                Title: "Estado e data fetching",
                                Summary: "Hooks, estado local, TanStack Query, cache, invalidação, optimistic updates e erros.",
                                Definition: "Separação entre estado de UI, estado de servidor e efeitos assíncronos para manter telas previsíveis.",
                                UseCases:
                                [
                                    "Consultar dados de APIs com cache e revalidação.",
                                    "Manter filtros, seleção e estado temporário da tela.",
                                    "Exibir loading, erro, vazio e sucesso sem duplicação."
                                ],
                                ImplementationNotes:
                                [
                                    "Não duplique no estado local dados que pertencem ao cache de servidor.",
                                    "Modele query keys de forma estável.",
                                    "Trate erros perto da experiência do usuário."
                                ],
                                RelatedPageIds: ["typescript", "react-performance", "a11y-ux"],
                                Links:
                                [
                                    new ReferenceLinkResponse("React hooks reference", "https://react.dev/reference/react/hooks", "Oficial"),
                                    new ReferenceLinkResponse("TanStack Query overview", "https://tanstack.com/query/latest/docs/framework/react/overview", "Oficial"),
                                    new ReferenceLinkResponse("Query keys", "https://tanstack.com/query/latest/docs/framework/react/guides/query-keys", "Guia")
                                ]),
                            new ReferencePageResponse(
                                Id: "react-forms",
                                Title: "Formulários",
                                Summary: "Validação, estados de erro, acessibilidade, máscaras, submit seguro e feedback ao usuário.",
                                Definition: "Fluxo de entrada de dados que precisa comunicar restrições, validar com clareza e proteger ações do usuário.",
                                UseCases:
                                [
                                    "Criar e editar entidades.",
                                    "Validar campos antes do envio.",
                                    "Exibir erros próximos dos campos afetados."
                                ],
                                ImplementationNotes:
                                [
                                    "Use labels reais e mensagens associadas ao campo.",
                                    "Desabilite submit durante envio para evitar duplicidade.",
                                    "Mantenha validação de cliente alinhada com validação da API."
                                ],
                                RelatedPageIds: ["a11y-ux", "secure-coding", "api-design"],
                                Links:
                                [
                                    new ReferenceLinkResponse("React forms", "https://react.dev/reference/react-dom/components/input", "Referência"),
                                    new ReferenceLinkResponse("MDN forms", "https://developer.mozilla.org/en-US/docs/Learn/Forms", "Referência"),
                                    new ReferenceLinkResponse("WAI forms tutorial", "https://www.w3.org/WAI/tutorials/forms/", "Acessibilidade")
                                ]),
                            new ReferencePageResponse(
                                Id: "react-performance",
                                Title: "Performance React",
                                Summary: "Renderização, memoização quando medida, code splitting, Suspense, transitions e profiling.",
                                Definition: "Conjunto de técnicas para manter interações fluidas e renderizações proporcionais à mudança real.",
                                UseCases:
                                [
                                    "Investigar componentes que renderizam demais.",
                                    "Separar bundles por rota ou funcionalidade.",
                                    "Reduzir trabalho em listas grandes."
                                ],
                                ImplementationNotes:
                                [
                                    "Meça com profiler antes de adicionar memoização.",
                                    "Use chaves estáveis em listas.",
                                    "Evite recalcular dados caros em toda renderização."
                                ],
                                RelatedPageIds: ["metrics", "frontend-quality", "react-state"],
                                Links:
                                [
                                    new ReferenceLinkResponse("React performance", "https://react.dev/reference/react/useMemo", "Referência"),
                                    new ReferenceLinkResponse("Profiler", "https://react.dev/reference/react/Profiler", "Ferramenta"),
                                    new ReferenceLinkResponse("web.dev performance", "https://web.dev/learn/performance/", "Referência")
                                ]),
                            new ReferencePageResponse(
                                Id: "frontend-quality",
                                Title: "Qualidade frontend",
                                Summary: "Componentização, design system, testes com Testing Library, E2E e revisão visual.",
                                Definition: "Práticas que tornam interfaces fáceis de alterar, testar, revisar e evoluir.",
                                UseCases:
                                [
                                    "Criar componentes reutilizáveis com props claras.",
                                    "Cobrir fluxos críticos com testes.",
                                    "Padronizar estados e estilos."
                                ],
                                ImplementationNotes:
                                [
                                    "Teste comportamento observável, não detalhes internos.",
                                    "Extraia componentes quando há repetição real.",
                                    "Mantenha estados vazios e de erro como parte do design."
                                ],
                                RelatedPageIds: ["testing-dotnet", "code-review", "a11y-ux"],
                                Links:
                                [
                                    new ReferenceLinkResponse("Testing Library", "https://testing-library.com/docs/react-testing-library/intro/", "Ferramenta"),
                                    new ReferenceLinkResponse("Playwright", "https://playwright.dev/docs/intro", "Ferramenta"),
                                    new ReferenceLinkResponse("React compiler docs", "https://react.dev/learn/react-compiler", "Referência")
                                ]),
                            new ReferencePageResponse(
                                Id: "a11y-ux",
                                Title: "Acessibilidade e UX",
                                Summary: "Semântica HTML, foco, teclado, contraste, estados vazios, responsividade e feedback.",
                                Definition: "Qualidade da interface para ser compreensível, navegável e utilizável por diferentes pessoas e dispositivos.",
                                UseCases:
                                [
                                    "Garantir navegação por teclado.",
                                    "Evitar texto sem contraste ou controles sem nome.",
                                    "Projetar estados previsíveis para erros e loading."
                                ],
                                ImplementationNotes:
                                [
                                    "Use HTML semântico antes de recriar controles com div.",
                                    "Mantenha foco visível.",
                                    "Valide mobile em larguras pequenas, não apenas desktop."
                                ],
                                RelatedPageIds: ["react-forms", "frontend-quality", "secure-coding"],
                                Links:
                                [
                                    new ReferenceLinkResponse("WAI guides", "https://www.w3.org/WAI/fundamentals/accessibility-intro/", "Acessibilidade"),
                                    new ReferenceLinkResponse("MDN accessibility", "https://developer.mozilla.org/en-US/docs/Web/Accessibility", "Referência"),
                                    new ReferenceLinkResponse("WCAG quick reference", "https://www.w3.org/WAI/WCAG22/quickref/", "Referência")
                                ])
                        ]),
                    new ReferenceCategoryResponse(
                        Id: "architecture",
                        Title: "Arquitetura",
                        Summary: "Referência para DDD, camadas, contratos, resiliência, dados distribuídos e limites de serviço.",
                        Links:
                        [
                            new ReferenceLinkResponse("Azure Architecture Center", "https://learn.microsoft.com/en-us/azure/architecture/", "Oficial"),
                            new ReferenceLinkResponse("Microservices architecture", "https://learn.microsoft.com/en-us/azure/architecture/microservices/", "Guia"),
                            new ReferenceLinkResponse("Martin Fowler", "https://martinfowler.com/architecture/", "Referência")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse("ddd", "DDD e bounded contexts", "Linguagem ubíqua, entidades, value objects, agregados, invariantes e context mapping.", "Abordagem para alinhar modelo de software com o domínio e separar responsabilidades por contexto.", ["Entender fronteiras de domínio.", "Organizar modelos com regras próprias.", "Reduzir acoplamento entre áreas do negócio."], ["Comece pela linguagem do negócio.", "Não transforme todo objeto em entidade.", "Use bounded context para explicar diferenças de significado."], ["clean-architecture", "service-boundaries", "system-design"], [new ReferenceLinkResponse("Bounded Context", "https://martinfowler.com/bliki/BoundedContext.html", "Referência"), new ReferenceLinkResponse("DDD reference", "https://domainlanguage.com/ddd/reference/", "Referência")]),
                            new ReferencePageResponse("clean-architecture", "Arquitetura em camadas", "Clean Architecture, vertical slices, casos de uso, domínio, infraestrutura e fronteiras.", "Separação entre regra de negócio, entrada/saída e detalhes de infraestrutura.", ["Evitar regra de negócio presa em controller.", "Trocar detalhes externos sem reescrever domínio.", "Organizar features com baixo acoplamento."], ["Use camadas quando houver complexidade real.", "Mantenha dependências apontando para regras estáveis.", "Evite abstrações vazias sobre bibliotecas simples."], ["aspnet-core", "ddd", "legacy"], [new ReferenceLinkResponse("Common web app architectures", "https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures", "Guia")]),
                            new ReferencePageResponse("api-design", "Design de APIs", "REST, gRPC, contratos, versionamento, idempotência, paginação, erros e compatibilidade.", "Prática de criar contratos claros e evolutivos para consumidores humanos e sistemas.", ["Publicar endpoints consumidos por frontend ou parceiros.", "Definir erros previsíveis.", "Evoluir contrato sem quebrar clientes."], ["Documente exemplos de sucesso e erro.", "Padronize paginação e filtros.", "Use idempotência em operações repetíveis."], ["aspnet-core", "react-state", "service-boundaries"], [new ReferenceLinkResponse("REST API guidelines", "https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design", "Guia"), new ReferenceLinkResponse("gRPC for .NET", "https://learn.microsoft.com/en-us/aspnet/core/grpc/", "Oficial")]),
                            new ReferencePageResponse("resilience", "Resiliência", "Timeouts, retries, circuit breakers, bulkheads, rate limiting, fallback e degradação graciosa.", "Técnicas para uma aplicação continuar previsível quando dependências ficam lentas ou falham.", ["Chamar APIs externas.", "Controlar retry sem amplificar incidente.", "Definir fallback para funções não críticas."], ["Todo retry precisa de timeout.", "Use circuit breaker quando falha repetida deve cortar tráfego.", "Meça impacto no usuário, não só exceções."], ["metrics", "slo-alerting", "service-boundaries"], [new ReferenceLinkResponse("Cloud design patterns", "https://learn.microsoft.com/en-us/azure/architecture/patterns/", "Referência"), new ReferenceLinkResponse("Polly", "https://www.pollydocs.org/", "Ferramenta")]),
                            new ReferencePageResponse("distributed-data", "Dados distribuídos", "Consistência eventual, sagas, outbox, inbox, CQRS, read models e compensação.", "Estratégias para coordenar dados quando não existe uma única transação cobrindo todos os serviços.", ["Publicar eventos confiáveis após gravação.", "Projetar compensação entre serviços.", "Criar modelos de leitura para consulta."], ["Use outbox para alinhar banco e publicação de evento.", "Modele idempotência no consumidor.", "Explique consistência eventual no contrato do produto."], ["event-design", "reliability-messaging", "transactions"], [new ReferenceLinkResponse("Transactional outbox", "https://microservices.io/patterns/data/transactional-outbox.html", "Padrão"), new ReferenceLinkResponse("CQRS pattern", "https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs", "Padrão")]),
                            new ReferencePageResponse("service-boundaries", "Limites de serviço", "API gateway, service discovery, configuração, deploy independente, ownership e contratos.", "Critérios para decidir quando um módulo deve ser separado e como ele se integra aos demais.", ["Avaliar modular monolith vs microsserviços.", "Definir ownership de dados.", "Evitar serviços distribuídos demais."], ["Separe serviço por capacidade de negócio, não por tabela.", "Garanta observabilidade antes de distribuir.", "Considere custo operacional como parte da decisão."], ["ddd", "distributed-data", "cloud-architecture"], [new ReferenceLinkResponse("Microservices on Azure", "https://learn.microsoft.com/en-us/azure/architecture/microservices/", "Guia"), new ReferenceLinkResponse("Service per team", "https://martinfowler.com/articles/microservice-trade-offs.html", "Referência")])
                        ]),
                    new ReferenceCategoryResponse(
                        Id: "messaging-events",
                        Title: "Mensageria e eventos",
                        Summary: "Referência para filas, tópicos, brokers, confiabilidade, contratos de eventos e processamento de streams.",
                        Links:
                        [
                            new ReferenceLinkResponse("RabbitMQ docs", "https://www.rabbitmq.com/docs", "Oficial"),
                            new ReferenceLinkResponse("Apache Kafka docs", "https://kafka.apache.org/documentation/", "Oficial"),
                            new ReferenceLinkResponse("Azure Service Bus", "https://learn.microsoft.com/en-us/azure/service-bus-messaging/", "Oficial")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse("queues-topics", "Filas e tópicos", "Point-to-point, publish/subscribe, competing consumers, ordering, sessions e throughput.", "Modelos de distribuição assíncrona usados para desacoplar produtores e consumidores.", ["Processar trabalho em background.", "Notificar múltiplos sistemas sobre fatos.", "Controlar concorrência de consumidores."], ["Fila distribui trabalho; tópico distribui cópias por assinatura.", "Ordering reduz throughput e aumenta acoplamento.", "Defina TTL e política de retry."], ["brokers", "reliability-messaging", "event-design"], [new ReferenceLinkResponse("Service Bus queues and topics", "https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-queues-topics-subscriptions", "Guia"), new ReferenceLinkResponse("RabbitMQ tutorial", "https://www.rabbitmq.com/tutorials", "Guia")]),
                            new ReferencePageResponse("brokers", "Brokers", "RabbitMQ, Azure Service Bus, Kafka, AMQP, particionamento, consumer groups e tradeoffs.", "Infraestrutura que recebe, armazena e distribui mensagens ou eventos.", ["Escolher broker para comandos, eventos ou streaming.", "Definir particionamento e retenção.", "Integrar serviços com garantia operacional."], ["Compare semântica antes de comparar marca.", "Kafka não substitui fila tradicional em todos os casos.", "Service Bus resolve bem cenários enterprise na Azure."], ["queues-topics", "stream-processing", "cloud-architecture"], [new ReferenceLinkResponse("RabbitMQ docs", "https://www.rabbitmq.com/docs", "Oficial"), new ReferenceLinkResponse("Kafka documentation", "https://kafka.apache.org/documentation/", "Oficial"), new ReferenceLinkResponse("Azure Service Bus", "https://learn.microsoft.com/en-us/azure/service-bus-messaging/", "Oficial")]),
                            new ReferencePageResponse("reliability-messaging", "Confiabilidade", "Retries, backoff, DLQ, poison messages, idempotência, deduplicação e reprocessamento.", "Conjunto de práticas para processar mensagens sem perder dados e sem duplicar efeitos indevidos.", ["Tratar falhas temporárias.", "Isolar mensagens inválidas.", "Reprocessar com rastreabilidade."], ["Consumidor deve ser idempotente.", "DLQ precisa de dono e processo de análise.", "Retry infinito sem backoff cria incidente."], ["distributed-data", "structured-logging", "slo-alerting"], [new ReferenceLinkResponse("Azure Service Bus dead-letter queues", "https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dead-letter-queues", "Guia"), new ReferenceLinkResponse("Idempotent consumer", "https://microservices.io/patterns/communication-style/idempotent-consumer.html", "Padrão")]),
                            new ReferencePageResponse("event-design", "Design de eventos", "Eventos de domínio, eventos de integração, schema evolution, versionamento e contratos.", "Modelagem de fatos publicados por um sistema para comunicar mudança sem expor detalhes internos.", ["Descrever mudança de estado relevante.", "Criar integração assíncrona entre contextos.", "Evoluir schema sem quebrar consumidores."], ["Evento deve representar fato já ocorrido.", "Não exponha entidade inteira por conveniência.", "Versione contrato e mantenha compatibilidade."], ["ddd", "distributed-data", "brokers"], [new ReferenceLinkResponse("Event-driven architecture", "https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/event-driven", "Guia"), new ReferenceLinkResponse("Schema Registry", "https://docs.confluent.io/platform/current/schema-registry/index.html", "Ferramenta")]),
                            new ReferencePageResponse("stream-processing", "Processamento de streams", "Eventos ordenados, janelas, reprocessamento, lag, exactly-once como tradeoff e auditoria.", "Processamento contínuo de eventos com retenção, offsets e análise temporal.", ["Calcular agregações em tempo quase real.", "Reprocessar histórico de eventos.", "Monitorar atraso de consumidores."], ["Particionamento define paralelismo e ordenação.", "Exactly-once tem custo e limites.", "Lag deve virar métrica operacional."], ["brokers", "metrics", "search-analytics"], [new ReferenceLinkResponse("Kafka Streams", "https://kafka.apache.org/documentation/streams/", "Oficial"), new ReferenceLinkResponse("Kafka consumer groups", "https://kafka.apache.org/documentation/#consumerconfigs", "Referência")])
                        ]),
                    new ReferenceCategoryResponse(
                        Id: "observability-sre",
                        Title: "Observabilidade e SRE",
                        Summary: "Referência para logs, métricas, traces, OpenTelemetry, SLOs, alertas e resposta a incidentes.",
                        Links:
                        [
                            new ReferenceLinkResponse("OpenTelemetry docs", "https://opentelemetry.io/docs/", "Oficial"),
                            new ReferenceLinkResponse(".NET observability", "https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-with-otel", "Oficial"),
                            new ReferenceLinkResponse("Google SRE books", "https://sre.google/books/", "Referência")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse("structured-logging", "Logs estruturados", "Correlation ID, trace ID, contexto, níveis, PII, amostragem e padrão de eventos.", "Logs em formato consultável, com campos consistentes e contexto suficiente para diagnóstico.", ["Investigar erro em produção.", "Correlacionar request entre serviços.", "Auditar ações relevantes."], ["Não registre segredo ou PII sem necessidade.", "Inclua correlation ID e identificadores técnicos.", "Padronize nomes de campos."], ["distributed-tracing", "secure-coding", "reliability-messaging"], [new ReferenceLinkResponse("Logging in .NET", "https://learn.microsoft.com/en-us/dotnet/core/extensions/logging", "Oficial"), new ReferenceLinkResponse("Serilog", "https://serilog.net/", "Ferramenta")]),
                            new ReferencePageResponse("metrics", "Métricas", "Golden signals, RED/USE, latência, erro, saturação, throughput e métricas de negócio.", "Séries temporais que indicam saúde, capacidade, comportamento e impacto de um sistema.", ["Criar dashboards operacionais.", "Medir SLOs.", "Detectar regressão de performance."], ["Métrica sem ação associada vira ruído.", "Separe métrica técnica de métrica de produto.", "Use percentis para latência."], ["performance-dotnet", "slo-alerting", "stream-processing"], [new ReferenceLinkResponse("OpenTelemetry metrics", "https://opentelemetry.io/docs/concepts/signals/metrics/", "Oficial"), new ReferenceLinkResponse("RED method", "https://grafana.com/blog/2018/08/02/the-red-method-how-to-instrument-your-services/", "Referência")]),
                            new ReferencePageResponse("distributed-tracing", "Tracing distribuído", "Spans, traces, propagação de contexto, dependências e gargalos entre serviços.", "Rastreamento de uma operação atravessando processos, serviços, filas e bancos.", ["Entender latência de ponta a ponta.", "Localizar dependência lenta.", "Correlacionar erros em arquitetura distribuída."], ["Propague contexto entre HTTP e mensageria.", "Nomeie spans de forma estável.", "Combine trace com logs e métricas."], ["opentelemetry", "resilience", "structured-logging"], [new ReferenceLinkResponse("OpenTelemetry traces", "https://opentelemetry.io/docs/concepts/signals/traces/", "Oficial"), new ReferenceLinkResponse(".NET distributed tracing", "https://learn.microsoft.com/en-us/dotnet/core/diagnostics/distributed-tracing", "Oficial")]),
                            new ReferencePageResponse("opentelemetry", "OpenTelemetry", "SDKs, auto-instrumentação, Collector, exporters e vendor neutrality.", "Padrão aberto para instrumentar, coletar e exportar logs, métricas e traces.", ["Instrumentar APIs e workers.", "Enviar telemetria para múltiplos backends.", "Padronizar observabilidade entre stacks."], ["Use Collector para desacoplar aplicação de vendor.", "Configure sampling conscientemente.", "Instrumente dependências críticas primeiro."], ["distributed-tracing", "metrics", "aspire"], [new ReferenceLinkResponse("OpenTelemetry docs", "https://opentelemetry.io/docs/", "Oficial"), new ReferenceLinkResponse("OpenTelemetry .NET", "https://opentelemetry.io/docs/languages/dotnet/", "Oficial"), new ReferenceLinkResponse("Collector", "https://opentelemetry.io/docs/collector/", "Oficial")]),
                            new ReferencePageResponse("slo-alerting", "SLOs e alertas", "SLI, SLO, error budget, alertas acionáveis, runbooks e postmortems.", "Acordos mensuráveis de confiabilidade e alertas baseados em impacto real.", ["Definir nível de serviço de endpoint crítico.", "Priorizar incidentes por impacto.", "Reduzir alertas ruidosos."], ["Alerta deve ter ação clara.", "Use error budget para discutir risco.", "Postmortem documenta aprendizado, não culpados."], ["metrics", "resilience", "system-design"], [new ReferenceLinkResponse("SRE books", "https://sre.google/books/", "Referência"), new ReferenceLinkResponse("Alerting on SLOs", "https://sre.google/workbook/alerting-on-slos/", "Referência")])
                        ]),
                    new ReferenceCategoryResponse(
                        Id: "cloud-devops",
                        Title: "Cloud, DevOps e plataforma",
                        Summary: "Referência para containers, Kubernetes, CI/CD, infraestrutura como código, arquitetura cloud e .NET Aspire.",
                        Links:
                        [
                            new ReferenceLinkResponse("Docker docs", "https://docs.docker.com/", "Oficial"),
                            new ReferenceLinkResponse("Kubernetes docs", "https://kubernetes.io/docs/home/", "Oficial"),
                            new ReferenceLinkResponse(".NET Aspire", "https://learn.microsoft.com/en-us/dotnet/aspire/", "Oficial")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse("containers", "Docker e imagens", "Dockerfile, multi-stage builds, imagens pequenas, variáveis, secrets e health checks.", "Empacotamento da aplicação com runtime, dependências e configuração de execução.", ["Rodar API e frontend em ambientes reproduzíveis.", "Publicar artefatos versionados.", "Reduzir diferença entre dev e produção."], ["Use multi-stage build.", "Não grave secrets na imagem.", "Defina health check e usuário não root quando possível."], ["cicd", "kubernetes", "aspnet-core"], [new ReferenceLinkResponse("Docker build", "https://docs.docker.com/build/", "Oficial"), new ReferenceLinkResponse(".NET container images", "https://learn.microsoft.com/en-us/dotnet/core/docker/build-container", "Guia")]),
                            new ReferencePageResponse("kubernetes", "Kubernetes", "Deployments, services, ingress, config maps, secrets, probes, HPA e troubleshooting.", "Plataforma de orquestração para executar workloads containerizados com escala e resiliência.", ["Publicar serviços com réplicas.", "Configurar readiness/liveness probes.", "Gerenciar configuração por ambiente."], ["Kubernetes não elimina necessidade de observabilidade.", "Comece por manifests simples.", "Defina recursos e limites."], ["containers", "opentelemetry", "cloud-architecture"], [new ReferenceLinkResponse("Kubernetes docs", "https://kubernetes.io/docs/home/", "Oficial"), new ReferenceLinkResponse("AKS docs", "https://learn.microsoft.com/en-us/azure/aks/", "Oficial")]),
                            new ReferencePageResponse("cicd", "CI/CD", "Build, testes, análise estática, artefatos, ambientes, migrations e rollback.", "Pipeline automatizado para validar, empacotar e publicar mudanças com rastreabilidade.", ["Executar testes em pull requests.", "Publicar imagem versionada.", "Promover artefato entre ambientes."], ["Pipeline deve falhar cedo.", "Separe build de deploy quando houver promoção.", "Planeje rollback e migrations."], ["testing-dotnet", "frontend-quality", "containers"], [new ReferenceLinkResponse("GitHub Actions", "https://docs.github.com/en/actions", "Oficial"), new ReferenceLinkResponse("Azure Pipelines", "https://learn.microsoft.com/en-us/azure/devops/pipelines/", "Oficial")]),
                            new ReferencePageResponse("iac", "Infra as Code", "Terraform/Bicep, ambientes, estado, módulos, revisão e drift.", "Definição versionada de infraestrutura para criar e alterar ambientes de forma reprodutível.", ["Criar recursos cloud revisáveis.", "Separar ambientes.", "Detectar drift."], ["Proteja o estado remoto.", "Use módulos apenas quando reduzem repetição real.", "Revise plano antes de aplicar."], ["cloud-architecture", "identity-access", "cicd"], [new ReferenceLinkResponse("Terraform docs", "https://developer.hashicorp.com/terraform/docs", "Oficial"), new ReferenceLinkResponse("Bicep docs", "https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/", "Oficial")]),
                            new ReferencePageResponse("cloud-architecture", "Arquitetura cloud", "Azure App Service, Container Apps, AKS, Functions, Service Bus, Key Vault e custos.", "Escolha de serviços cloud e topologia considerando operação, escala, segurança e custo.", ["Escolher runtime para API.", "Integrar banco, cache e broker.", "Definir segregação de ambientes."], ["Serviço gerenciado reduz operação, mas impõe limites.", "Custo deve entrar no desenho inicial.", "Use Key Vault ou equivalente para segredos."], ["service-boundaries", "identity-access", "brokers"], [new ReferenceLinkResponse("Azure Architecture Center", "https://learn.microsoft.com/en-us/azure/architecture/", "Oficial"), new ReferenceLinkResponse("Azure Well-Architected Framework", "https://learn.microsoft.com/en-us/azure/well-architected/", "Guia")]),
                            new ReferencePageResponse("aspire", ".NET Aspire", "Orquestração local, dashboard, service discovery, health checks, telemetria e integrações locais.", "Stack para desenvolver aplicações distribuídas .NET com orquestração local e defaults operacionais.", ["Rodar API, banco, cache e broker juntos no desenvolvimento.", "Padronizar service defaults.", "Visualizar logs, métricas e traces em dashboard local."], ["Use quando houver mais de um serviço ou dependência local relevante.", "Não confunda orquestração local com plataforma de produção.", "Aproveite integração com OpenTelemetry."], ["opentelemetry", "containers", "aspnet-core"], [new ReferenceLinkResponse(".NET Aspire docs", "https://learn.microsoft.com/en-us/dotnet/aspire/", "Oficial"), new ReferenceLinkResponse("Aspire service defaults", "https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/service-defaults", "Guia")])
                        ]),
                    new ReferenceCategoryResponse(
                        Id: "security",
                        Title: "Segurança aplicada",
                        Summary: "Referência para OWASP, threat modeling, código seguro, identidade e controle de acesso.",
                        Links:
                        [
                            new ReferenceLinkResponse("OWASP Top 10", "https://owasp.org/Top10/", "Oficial"),
                            new ReferenceLinkResponse("OWASP Cheat Sheet Series", "https://cheatsheetseries.owasp.org/", "Oficial"),
                            new ReferenceLinkResponse("Microsoft identity platform", "https://learn.microsoft.com/en-us/entra/identity-platform/", "Oficial")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse("owasp", "OWASP e ameaças comuns", "Injection, XSS, CSRF, SSRF, broken access control, secrets e supply chain.", "Referência de riscos comuns para revisar design, implementação e operação de aplicações web.", ["Priorizar revisão de segurança.", "Criar critérios de revisão de PR.", "Explicar risco técnico para produto."], ["Use OWASP como mapa, não como cobertura completa.", "Broken access control costuma ser mais crítico que validação isolada.", "Revise dependências e supply chain."], ["secure-coding", "threat-modeling", "auth-api"], [new ReferenceLinkResponse("OWASP Top 10", "https://owasp.org/Top10/", "Oficial"), new ReferenceLinkResponse("OWASP ASVS", "https://owasp.org/www-project-application-security-verification-standard/", "Padrão")]),
                            new ReferencePageResponse("threat-modeling", "Threat modeling", "Assets, trust boundaries, STRIDE, mitigações e riscos residuais.", "Processo de identificar ameaças antes da implementação ou mudança arquitetural.", ["Revisar API com dados sensíveis.", "Avaliar integração externa.", "Definir controles de mitigação."], ["Modele fluxos e fronteiras de confiança.", "Registre risco aceito explicitamente.", "Conecte ameaça a controle verificável."], ["system-design", "owasp", "identity-access"], [new ReferenceLinkResponse("Microsoft Threat Modeling Tool", "https://learn.microsoft.com/en-us/azure/security/develop/threat-modeling-tool", "Ferramenta"), new ReferenceLinkResponse("STRIDE", "https://learn.microsoft.com/en-us/azure/security/develop/threat-modeling-tool-threats", "Referência")]),
                            new ReferencePageResponse("secure-coding", "Código seguro", "Validação, output encoding, criptografia correta, proteção de dados e auditoria.", "Práticas de implementação que reduzem superfície de ataque e vazamento de dados.", ["Validar entradas externas.", "Proteger dados sensíveis.", "Registrar auditoria sem expor segredo."], ["Validação de cliente não substitui validação no servidor.", "Use bibliotecas maduras para criptografia.", "Faça output encoding no contexto correto."], ["react-forms", "aspnet-core", "structured-logging"], [new ReferenceLinkResponse("OWASP Cheat Sheets", "https://cheatsheetseries.owasp.org/", "Oficial"), new ReferenceLinkResponse(".NET cryptography", "https://learn.microsoft.com/en-us/dotnet/standard/security/cryptography-model", "Oficial")]),
                            new ReferencePageResponse("identity-access", "Identidade e acesso", "RBAC, ABAC, least privilege, managed identities, rotação de secrets e Key Vault.", "Modelo de permissões para usuários, serviços, ambientes e recursos cloud.", ["Definir matriz de acesso.", "Conceder permissão mínima para serviços.", "Remover secrets estáticos quando houver identidade gerenciada."], ["Least privilege precisa ser revisado continuamente.", "Permissão de serviço não deve depender de usuário humano.", "Audite mudanças em papéis críticos."], ["auth-api", "cloud-architecture", "iac"], [new ReferenceLinkResponse("Microsoft identity platform", "https://learn.microsoft.com/en-us/entra/identity-platform/", "Oficial"), new ReferenceLinkResponse("Azure Key Vault", "https://learn.microsoft.com/en-us/azure/key-vault/", "Oficial")])
                        ]),
                    new ReferenceCategoryResponse(
                        Id: "data-systems",
                        Title: "Dados, cache e performance",
                        Summary: "Referência para modelagem SQL, transações, cache, busca, analytics e diagnóstico.",
                        Links:
                        [
                            new ReferenceLinkResponse("PostgreSQL docs", "https://www.postgresql.org/docs/current/", "Oficial"),
                            new ReferenceLinkResponse("SQL Server docs", "https://learn.microsoft.com/en-us/sql/sql-server/", "Oficial"),
                            new ReferenceLinkResponse("Redis docs", "https://redis.io/docs/latest/", "Oficial")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse("sql-modeling", "Modelagem SQL", "Normalização, índices, constraints, planos de execução, locks e isolamento.", "Desenho de estruturas relacionais para preservar integridade e permitir consultas eficientes.", ["Criar tabelas e relacionamentos.", "Escolher índices.", "Ler plano de execução."], ["Constraint protege regra que não pode depender só da aplicação.", "Índice acelera leitura, mas custa escrita.", "Analise cardinalidade antes de otimizar."], ["data-access", "transactions", "search-analytics"], [new ReferenceLinkResponse("PostgreSQL indexes", "https://www.postgresql.org/docs/current/indexes.html", "Oficial"), new ReferenceLinkResponse("SQL Server indexes", "https://learn.microsoft.com/en-us/sql/relational-databases/indexes/", "Oficial")]),
                            new ReferencePageResponse("transactions", "Transações", "ACID, isolamento, concorrência, deadlocks, optimistic concurrency e consistência.", "Unidade de trabalho que protege consistência quando múltiplas operações precisam confirmar ou falhar juntas.", ["Atualizar múltiplas tabelas.", "Evitar race conditions.", "Tratar concorrência de edição."], ["Escolha isolamento pelo risco real.", "Deadlock precisa de diagnóstico e ordem consistente.", "Optimistic concurrency funciona bem para edição por usuário."], ["distributed-data", "data-access", "sql-modeling"], [new ReferenceLinkResponse("EF Core transactions", "https://learn.microsoft.com/en-us/ef/core/saving/transactions", "Oficial"), new ReferenceLinkResponse("PostgreSQL transactions", "https://www.postgresql.org/docs/current/tutorial-transactions.html", "Oficial")]),
                            new ReferencePageResponse("caching", "Cache", "Redis, cache-aside, expiração, invalidação, stampede e cache distribuído.", "Armazenamento temporário para reduzir latência e carga, com estratégia clara de atualização.", ["Acelerar leituras frequentes.", "Reduzir chamadas a dependências caras.", "Compartilhar estado temporário entre instâncias."], ["Invalidação é parte do desenho, não detalhe posterior.", "Evite cache de dados sensíveis sem controles.", "Defina fallback quando cache falha."], ["performance-dotnet", "resilience", "cloud-architecture"], [new ReferenceLinkResponse("Azure Cache for Redis", "https://learn.microsoft.com/en-us/azure/azure-cache-for-redis/", "Oficial"), new ReferenceLinkResponse("Redis docs", "https://redis.io/docs/latest/", "Oficial")]),
                            new ReferencePageResponse("search-analytics", "Busca e analytics", "Projeções, read models, Elasticsearch/OpenSearch, eventos para BI e auditoria.", "Modelos otimizados para consulta, busca textual e análise que não cabem bem no transacional puro.", ["Criar busca por texto.", "Montar read model para dashboard.", "Enviar eventos para BI."], ["Read model pode ser eventualmente consistente.", "Evite sobrecarregar banco transacional com analytics.", "Documente origem e atualização dos dados."], ["stream-processing", "distributed-data", "metrics"], [new ReferenceLinkResponse("Elasticsearch docs", "https://www.elastic.co/guide/index.html", "Oficial"), new ReferenceLinkResponse("OpenSearch docs", "https://opensearch.org/docs/latest/", "Oficial")])
                        ]),
                    new ReferenceCategoryResponse(
                        Id: "senior-engineering",
                        Title: "Arquitetura e engenharia sênior",
                        Summary: "Referência para system design, ADRs, code review, legado, mentoria e decisões técnicas.",
                        Links:
                        [
                            new ReferenceLinkResponse("C4 model", "https://c4model.com/", "Referência"),
                            new ReferenceLinkResponse("ADR GitHub", "https://adr.github.io/", "Referência"),
                            new ReferenceLinkResponse("Google engineering practices", "https://google.github.io/eng-practices/", "Referência")
                        ],
                        Pages:
                        [
                            new ReferencePageResponse("system-design", "System design", "Requisitos, capacidade, tradeoffs, diagramas C4, riscos e plano de evolução.", "Processo de transformar requisitos e restrições em arquitetura compreensível e evolutiva.", ["Avaliar nova feature grande.", "Discutir escala e confiabilidade.", "Comunicar arquitetura para times."], ["Comece por requisitos funcionais e não funcionais.", "Mostre alternativas descartadas.", "Inclua riscos e plano de evolução."], ["ddd", "cloud-architecture", "slo-alerting"], [new ReferenceLinkResponse("C4 model", "https://c4model.com/", "Referência"), new ReferenceLinkResponse("Azure architecture guide", "https://learn.microsoft.com/en-us/azure/architecture/guide/", "Guia")]),
                            new ReferencePageResponse("adrs", "ADRs e documentação", "Contexto, decisão, alternativas, consequências e revisão futura.", "Registro curto de decisões arquiteturais para preservar raciocínio e tradeoffs ao longo do tempo.", ["Justificar escolha de broker.", "Registrar mudança de arquitetura.", "Evitar repetir discussões antigas sem contexto."], ["ADR deve ter contexto e consequência, não só decisão.", "Marque decisões substituídas.", "Use linguagem objetiva."], ["api-design", "service-boundaries", "code-review"], [new ReferenceLinkResponse("ADR GitHub", "https://adr.github.io/", "Referência"), new ReferenceLinkResponse("MADR template", "https://adr.github.io/madr/", "Template")]),
                            new ReferencePageResponse("code-review", "Code review sênior", "Riscos, legibilidade, testes, contratos, segurança, performance e feedback objetivo.", "Revisão focada em bugs, regressões, manutenção, contratos e risco real da mudança.", ["Revisar pull requests críticas.", "Orientar padrões técnicos.", "Detectar falhas de contrato e segurança."], ["Priorize risco antes de estilo.", "Peça teste quando o comportamento mudou.", "Comente com referência concreta ao código."], ["testing-dotnet", "frontend-quality", "secure-coding"], [new ReferenceLinkResponse("Google code review", "https://google.github.io/eng-practices/review/", "Referência"), new ReferenceLinkResponse("Code review guide", "https://google.github.io/eng-practices/review/reviewer/", "Guia")]),
                            new ReferencePageResponse("legacy", "Modernização de legado", "Strangler fig, refatoração incremental, cobertura de testes e redução de risco.", "Estratégias para melhorar sistemas existentes sem reescrita arriscada e sem parar entrega.", ["Migrar módulo antigo.", "Criar cobertura antes de alterar regra crítica.", "Separar fronteiras gradualmente."], ["Reescrita total raramente reduz risco.", "Use testes de caracterização.", "Escolha fatias pequenas com valor verificável."], ["clean-architecture", "testing-dotnet", "system-design"], [new ReferenceLinkResponse("Strangler fig pattern", "https://martinfowler.com/bliki/StranglerFigApplication.html", "Padrão"), new ReferenceLinkResponse("Refactoring", "https://refactoring.com/", "Referência")]),
                            new ReferencePageResponse("mentoring", "Mentoria e alinhamento", "Pareamento, comunicação, estimativas, negociação técnica e compartilhamento de contexto.", "Práticas para multiplicar clareza técnica e reduzir dependência de conhecimento isolado.", ["Apoiar devs em crescimento.", "Alinhar decisões entre times.", "Compartilhar contexto de arquitetura."], ["Mentoria precisa de objetivo observável.", "Pareamento resolve transferência de contexto melhor que documento isolado.", "Explique tradeoffs, não apenas preferência."], ["code-review", "adrs", "system-design"], [new ReferenceLinkResponse("Engineering practices", "https://google.github.io/eng-practices/", "Referência")])
                        ])
                ],
                KnowledgeMaps:
                [
                    new KnowledgeMapResponse(
                        Id: "api-production",
                        Title: "API em produção",
                        Summary: "Conecta ASP.NET Core, autenticação, persistência, logs, métricas, CI/CD e cloud.",
                        PageIds: ["aspnet-core", "auth-api", "data-access", "structured-logging", "metrics", "cicd", "cloud-architecture"]),
                    new KnowledgeMapResponse(
                        Id: "distributed-systems",
                        Title: "Sistemas distribuídos",
                        Summary: "Conecta DDD, limites de serviço, mensageria, dados distribuídos, resiliência e tracing.",
                        PageIds: ["ddd", "service-boundaries", "event-design", "distributed-data", "resilience", "distributed-tracing"]),
                    new KnowledgeMapResponse(
                        Id: "frontend-product",
                        Title: "Frontend de produto",
                        Summary: "Conecta TypeScript, React, formulários, performance, acessibilidade, contratos e testes.",
                        PageIds: ["typescript", "react-state", "react-forms", "react-performance", "a11y-ux", "api-design", "frontend-quality"]),
                    new KnowledgeMapResponse(
                        Id: "security-review",
                        Title: "Revisão de segurança",
                        Summary: "Conecta OWASP, threat modeling, identidade, código seguro, logs e code review.",
                        PageIds: ["owasp", "threat-modeling", "identity-access", "secure-coding", "structured-logging", "code-review"])
                ])));

        return app;
    }

    private sealed record ReferenceCatalogResponse(
        string Title,
        string Description,
        string Scope,
        IReadOnlyList<ReferenceCategoryResponse> Categories,
        IReadOnlyList<KnowledgeMapResponse> KnowledgeMaps);

    private sealed record ReferenceCategoryResponse(
        string Id,
        string Title,
        string Summary,
        IReadOnlyList<ReferenceLinkResponse> Links,
        IReadOnlyList<ReferencePageResponse> Pages);

    private sealed record ReferencePageResponse(
        string Id,
        string Title,
        string Summary,
        string Definition,
        IReadOnlyList<string> UseCases,
        IReadOnlyList<string> ImplementationNotes,
        IReadOnlyList<string> RelatedPageIds,
        IReadOnlyList<ReferenceLinkResponse> Links);

    private sealed record ReferenceLinkResponse(string Label, string Url, string Kind);

    private sealed record KnowledgeMapResponse(
        string Id,
        string Title,
        string Summary,
        IReadOnlyList<string> PageIds);
}
