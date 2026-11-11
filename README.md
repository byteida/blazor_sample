# blazor_sample

A layered ASP.NET Core Blazor Web App sample targeting .NET 10.

## Architecture

The solution uses a small Clean Architecture-style modular structure. Dependencies point inward: the Domain has no project dependencies, Application depends on Domain, Infrastructure implements Application contracts, and the ASP.NET Core host composes the layers.

```text
blazor_sample/
├── blazor_sample/                 # ASP.NET Core host, UI and composition root
│   └── Components/Pages/          # Razor pages (Weather uses Application contract)
├── blazor_sample.Client/          # WebAssembly UI (Counter)
├── BlazorSample.Domain/           # Core domain types and rules
├── BlazorSample.Application/      # Use-case contracts and application logic
├── BlazorSample.Infrastructure/   # External adapters and data providers
└── BlazorSample.Tests/            # Automated tests
```

### Dependency direction

- **Domain**: framework-independent domain model. It owns `WeatherForecast`.
- **Application**: defines `IWeatherForecastService`, the contract used by the UI.
- **Infrastructure**: provides `DemoWeatherForecastService`, a deterministic sample-data adapter.
- **Web host**: registers the adapter in dependency injection and renders the Razor pages.
- **Client**: remains the WebAssembly UI project. Its Counter page uses `InteractiveAuto` and does not depend on server-only infrastructure.

The Weather page no longer generates data itself. It depends on the Application interface, while the concrete demo implementation lives in Infrastructure. A real API, database, or third-party weather provider can later replace the demo adapter without changing the page or application contract.

This is intentionally a modest layered foundation rather than an enterprise framework: no database, repository abstraction, CQRS/MediatR, or authentication system is added before the sample has requirements for them.

## Technology

- .NET 10 (LTS)
- ASP.NET Core Blazor Web App
- Interactive Server and Interactive WebAssembly render modes
- Central NuGet package management
- xUnit tests

## Requirements

- .NET 10 SDK

The root `global.json` selects the .NET 10 SDK feature band. Install a .NET 10 SDK before running the commands below.

## Build, test and run

From the repository root:

```bash
dotnet restore blazor_sample/blazor_sample.sln
dotnet build blazor_sample/blazor_sample.sln --no-restore
dotnet test blazor_sample/blazor_sample.sln --no-build
dotnet run --project blazor_sample/blazor_sample/blazor_sample.csproj
```

Open the local URL printed by ASP.NET Core. The sample includes Home, Counter, and Weather pages.

## Version management

NuGet versions are centrally managed in `blazor_sample/Directory.Packages.props`. Keep package versions there rather than specifying versions in individual project files.

## Refactoring notes

- Extracted the forecast model from the Razor component into the Domain project.
- Introduced an Application service contract for forecast retrieval.
- Moved deterministic demo forecast generation into Infrastructure.
- Registered the Infrastructure implementation in the Web host composition root.
- Added an xUnit test project covering forecast count/date range, deterministic output, input validation, cancellation, and temperature conversion.
- Updated the solution to include all layers and tests.
