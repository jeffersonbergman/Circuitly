# Circuitly — Roadmap e Decisões de Arquitetura

> Motor genérico de gestão de ligas/torneios (esports), construído como projeto de estudo aplicado, usando Clean Architecture em C#/.NET + Angular.

## Objetivo do projeto

Aprender conceitos de programação implementando um sistema real, em vez de estudar teoria isolada. Cada fase introduz conceitos novos conforme a necessidade aparece, seguindo o método de "ditado" (pair programming guiado, sem código pronto).

---

## Decisões de arquitetura (log)

| Decisão | Escolha | Motivo |
|---|---|---|
| Estilo de arquitetura | Clean Architecture (4 projetos) | Separação de responsabilidades, testabilidade, expõe conceitos de nível sênior |
| Acesso a dados | EF Core | Produtivo, ainda ensina os conceitos certos (migrations, tracking, LINQ to SQL) |
| Autenticação | ASP.NET Identity completo | Robusto, cobre hashing, roles, tokens sem reinventar a roda |
| Padrão de repositório | Específico por entidade (não genérico `IRepository<T>`) | Evita abstração vazada; queries específicas ficam explícitas e testáveis |
| Multi-tenancy | Adiado — single-tenant na Fase 1 | Evita complexidade prematura; migração pra multi-tenant vira exercício deliberado depois |
| Nome do projeto | Circuitly | Genérico o suficiente pra não amarrar a uma liga específica (Colosseum é só o primeiro tenant) |
| Banco de dados (dev) | SQL Server via Docker container | Não interfere no uso do SSMS; já introduz Docker sem risco |
| Hosting atual | IIS (produção atual no trabalho) → meta futura: Docker | Migração de infra vem só na Fase 4 |

---

## Estrutura da solution

```
Circuitly/
├── src/
│   ├── Circuitly.Domain/          → Entidades, regras de negócio puras (zero dependências)
│   ├── Circuitly.Application/     → Casos de uso, DTOs, interfaces de repositório
│   ├── Circuitly.Infrastructure/  → EF Core, DbContext, Identity, implementações concretas
│   └── Circuitly.Api/             → Controllers, Program.cs, autenticação JWT
├── tests/
│   └── Circuitly.Tests/           → Reservado para a Fase 3
└── frontend/
    └── circuitly-web/             → Angular, projeto separado
```

Regra de dependência: `Domain` não referencia nada. `Application` referencia `Domain`. `Infrastructure` referencia `Domain` + `Application`. `Api` referencia `Application` + `Infrastructure`.

---

## Entidades centrais (domínio)

- **Liga/Torneio** — nome, formato, temporada
- **Time**
- **Jogador** — com vínculo Jogador↔Time (histórico de transferências)
- **Partida/Confronto**
- **Evento de pontuação** — reaproveitando os 21 tipos já modelados pro bot do WayGaming
- **Usuário/Papel** — admin, capitão de time, jogador, espectador (via ASP.NET Identity + Roles)

---

## Fases

### ✅ Fase 0 — Setup (concluída)
- [x] Solution + 4 projetos criados com referências corretas
- [x] Repositório GitHub criado (público)
- [x] Docker + SQL Server em container funcionando
- [x] SSMS conectado ao container

### 🔵 Fase 1 — MVP (em andamento)
- [ ] Entidade `Liga` no Domain
- [ ] Entidade `Time` no Domain
- [ ] Entidade `Jogador` no Domain
- [ ] `DbContext` + configuração do Identity no Infrastructure
- [ ] Primeira migration + banco criado
- [ ] Interfaces de repositório no Application (`ILigaRepository`, `ITimeRepository`, `IJogadorRepository`)
- [ ] Implementação dos repositórios no Infrastructure
- [ ] DTOs de entrada/saída no Application
- [ ] Controllers REST no Api (CRUD Liga/Time/Jogador)
- [ ] Autenticação JWT + Identity funcionando (registro/login)
- [ ] Angular: módulo de auth (login/registro)
- [ ] Angular: telas de CRUD consumindo a API

### ⚪ Fase 2 — Regras de negócio
- [ ] Sistema de pontuação (eventos, cálculo de ranking)
- [ ] Papéis e permissões (admin / capitão / jogador) aplicados nos endpoints
- [ ] Fluxo de aprovação de eventos (reaproveitando lógica do bot)
- [ ] Histórico de transferências de jogador entre times

### ⚪ Fase 3 — Conceitos modernos
- [ ] Testes automatizados (unit + integration) — projeto `Circuitly.Tests`
- [ ] SignalR (standings em tempo real)
- [ ] Cache (Redis)
- [ ] CI/CD pipeline (Jenkins ou GitHub Actions)

### ⚪ Fase 4 — Infraestrutura
- [ ] Dockerizar API
- [ ] Dockerizar Angular
- [ ] Sair do IIS
- [ ] docker-compose orquestrando tudo (API + SQL Server + Angular)

### ⚪ Fase 5 — Multi-tenancy (futuro)
- [ ] `ITenantResolver` / `ITenantConnectionProvider`
- [ ] Resolução de tenant por subdomínio ou claim do token
- [ ] Provisionamento de banco por tenant

---

## Como usar este documento

Atualize os checkboxes conforme avança. Decisões novas entram na tabela do topo com o motivo — isso vira sua própria documentação de "por que fiz assim", útil daqui a 6 meses quando esquecer o raciocínio original.
