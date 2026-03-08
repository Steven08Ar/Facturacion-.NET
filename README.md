# Sistema de Facturación Enterprise

Plataforma empresarial de facturación, desarrollada en **.NET 9** utilizando **Clean Architecture**, **CQRS con MediatR** y **Domain-Driven Design (DDD)**. 

El sistema está diseñado para ser altamente escalable y agnóstico a la UI, ofreciendo una Web API sólida lista para su integración con el frontend.

## 🏗️ Stack Tecnológico

**Backend:**
- **Framework:** .NET 9 (LTS)
- **APIs:** ASP.NET Core Web API (RESTful minimal/controllers)
- **Persistencia:** Entity Framework Core 9 (SQL Server)
- **Arquitectura:** Clean Architecture, CQRS (MediatR), Repository Pattern + UoW
- **Mapeo y Validación:** AutoMapper, FluentValidation
- **Autenticación y Seguridad:** ASP.NET Core Identity, JWT Bearer tokens
- **Logging:** Serilog
- **Manejo de Errores:** Result Pattern base y Middleware global
- **Base de Datos:** SQL Server 2022 (con Soft Delete e interceptores automáticos)

## 📁 Estructura del Proyecto (Clean Architecture)

- **`InvoiceApp.Domain`**: Contiene la lógica central del negocio: Entidades (Companies, Users, Invoices, etc.), Value Objects y Enums. Aislado de todo framework externo.
- **`InvoiceApp.Application`**: Contiene los casos de uso (Commands/Queries de MediatR), DTOs, interfaces de abstracción de datos (DbContext, Repositories).
- **`InvoiceApp.Infrastructure`**: Implementa las abstracciones de infraestructura: Entity Framework Core (`ApplicationDbContext`), Repositorios genéricos, Identity, y servicios externos.
- **`InvoiceApp.API`**: Punto de entrada del host web. Configura el contenedor DI, Middlewares, Swagger, CORS, y expone los Endpoints HTTP.
- **`InvoiceApp.Web`**: Proyecto Frontend en Blazor (.NET 9 Auto Interactivity) [Próximamente].
- **Pruebas (`UnitTests`, `IntegrationTests`, `E2ETests`)**: Capas de pruebas dedicadas para validación de la lógica de negocio y endpoints.

## 🚀 Inicio Rápido

1. **Requisitos previos**:
   - [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
   - SQL Server (LocalDB disponible mediante Visual Studio o un contenedor local).
2. **Restaurar paquetes e iniciar base de datos**:
   ```bash
   dotnet restore
   dotnet ef database update -s src/InvoiceApp.API -p src/InvoiceApp.Infrastructure
   ```
3. **Ejecutar la API**:
   ```bash
   cd src/InvoiceApp.API
   dotnet run
   ```
4. Navegar a `https://localhost:5001/swagger` para ver y probar los endpoints.

## 📋 Módulos Implementados

- [x] **Módulo 1: Fundación del Proyecto**. Estructura en capas, EF Core, Entidades principales, Migración Inicial.
- [ ] **Módulo 2: Autenticación**. JWT, Roles, Identity.
- [ ] **Módulo 3: Dashboard**. KPIs, SignalR, Redis Caching.
- [ ] **Módulo 4: Clientes**. CRUD y exportación.
- [ ] **Módulo 5: Productos/Servicios**. Catálogo.
- [ ] **Módulo 6: Cotizaciones**. Creación y envío a Clientes.
- [ ] **Módulo 7: Facturación (Core)**. Wizard de 3 pasos, QuestPdf, estado automático.
- [ ] **Módulo 8: Pagos**. Registro y seguimiento.
- [ ] **Módulo 9: Reportes**.
- [ ] **Módulo 10: Configuración y Auditoría**.