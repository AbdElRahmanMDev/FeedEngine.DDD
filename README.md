# FeedEngine.DDD (In Progress)

A **high-scale social timeline (feed) engine** built with **.NET**, **DDD**, and a **Modular Monolith** approach.  
Goal: showcase production-grade backend architecture for feeds, ranking, caching, and real-time updates.

> **Status:** In progress (early stage). Architecture + module boundaries are being built first, then features are implemented incrementally.

---

## Key Ideas

- **Modular Monolith**: clear module boundaries inside a single deployment (easy to evolve, easy to split later if needed).
- **DDD (Domain-Driven Design)**: aggregates, value objects, invariants, domain events.
- **Hybrid feed generation (push/pull)** *(planned)*:
  - Push (fanout-on-write) where it makes sense
  - Pull (fanout-on-read) for flexibility and cost control
- **Caching & read optimization** *(planned)*:
  - Redis for hot timelines / profile feeds
  - Read models / projections for fast queries
- **Real-time updates** *(planned)*:
  - SignalR for live notifications/feed updates

---

## Tech Stack

- **.NET 8 / ASP.NET Core Web API**
- **Entity Framework Core** + **SQL Server**
- **MediatR** (Domain Events / notifications)
- **Redis** *(planned for caching)*
- **Docker** *(planned for local environment)*
- **Swagger / OpenAPI**

---

## Repository Structure (High Level)

