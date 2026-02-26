# Clinic Booking System (CBS)

## 🏥 About the Project
A streamlined healthcare scheduling system built with **.NET 8**, **Blazor WebAssembly**, and **Clean Architecture**.  
The system enables patients to book appointments, view available time slots, and receive confirmations, while ensuring scalability, security, and maintainability.

---

## 📌 Requirements

### Key Stakeholders
- **Patients** – book, cancel, and reschedule appointments.
- **Clinic Staff** – manage schedules and availability.
- **System Admin** – oversee users, clinics, and system configuration.

### Functional Requirements
- Appointment booking, cancellation, and rescheduling.
- Time slot availability checks.
- Confirmation messages for patients.
- Validation to prevent double bookings.

### Non-Functional Requirements
- **Security**: Authentication & authorization via ASP.NET Identity or Azure AD B2C.
- **Performance**: Optimized queries with EF Core and optional Dapper.
- **Scalability**: Modular architecture with clean separation of concerns.
- **Logging & Monitoring**: NLog or Serilog integration.

---

## 🏗️ System Design

### Architecture
- **Frontend**: Blazor WebAssembly with FluentUI/MudBlazor.
- **Backend**: ASP.NET Core Web API with layered architecture.
- **Database**: SQL Server or PostgreSQL with EF Core migrations.
- **Communication**: HttpClient for API calls, AutoMapper for DTO mapping.

### Data Model
- **Patients** – personal details, contact info.
- **Clinics** – clinic details, location, services.
- **Appointments** – patient, clinic, time slot, status.
- **Time Slots** – availability linked to clinic schedules.

---

## 🚀 Getting Started

### Backend Setup (API)
1. **Appsettings**: Update `cbsConnectionString` in `appsettings.Development.json`.
2. **Migrations**: Run `dotnet ef database update`.
3. **Logging**: Ensure `C:/Logs/` directory exists for NLog/Serilog.

### Frontend Setup (Blazor)
1. **OIDC**: Ensure the Authority URL in `appsettings.json` matches your Identity Server.
2. **HTTP Client**: Base address defaults to `https://localhost:44398/`.

---

## 🛠 Tech Stack

- **Frontend**: Blazor WebAssembly  
  - UI: FluentUI or MudBlazor  
  - Validation: FluentValidation  
  - Testing: bUnit, Playwright  

- **Backend**: ASP.NET Core Web API  
  - ORM: Entity Framework Core  
  - Mapping: AutoMapper  
  - Validation: FluentValidation  
  - Logging: NLog or Serilog  

- **Database**: SQL Server / PostgreSQL  
  - EF Core Migrations  
  - Optional: Dapper for performance-critical queries  

- **Authentication & Authorization**  
  - ASP.NET Identity or Azure AD B2C  
  - JWT Tokens for secure API access  

- **Testing Frameworks**  
  - xUnit / NUnit  
  - Moq for mocking dependencies  

- **DevOps & CI/CD**  
  - GitHub Actions or Azure DevOps Pipelines  
  - Swagger/OpenAPI for API documentation  

---

## 🌟 Optional Enhancements
- **Appointment Reminders**: Twilio or SendGrid  
- **Calendar Sync**: Outlook API or Google Calendar API  
- **Real-time Updates**: SignalR  
- **Specific Data Retrieval**: GraphQL  

---

## ⚖️ License
Internal Use - Department of Health and Wellness.
