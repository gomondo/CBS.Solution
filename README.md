
# Clinic Booking System (CBS)

## 🏥 About the Project
A streamlined healthcare scheduling system built with .NET 8, Blazor WASM, and Clean Architecture.

## 🚀 Getting Started

### Backend Setup (API)
1. **Appsettings**: Update `cbsConnectionString` in `appsettings.Development.json`.
2. **Migrations**: Run `dotnet ef database update`.
3. **NLog**: Ensure `C:/Logs/` directory is created for logging.

### Frontend Setup (Blazor)
1. **OIDC**: Ensure the Authority URL in `appsettings.json` matches your Identity Server.
2. **HTTP Client**: The base address is currently set to `https://localhost:44398/`.

## 🛠 Tech Stack
- **WebAssembly**: Blazor
- **UI Components**: Microsoft FluentUI
- **ORM**: Entity Framework Core
- **Testing**: xUnit & Moq 4.20.72

## ⚖️ License
Internal Use - Department of Health and Wellness.
