# blazor_sample

A small ASP.NET Core Blazor Web App sample with interactive server and WebAssembly rendering.

## Technology

- .NET 10 (LTS)
- ASP.NET Core Blazor Web App
- Interactive Server and Interactive WebAssembly render modes
- Bootstrap

## Repository layout

The solution is located in `blazor_sample/blazor_sample.sln`.

- `blazor_sample/blazor_sample/blazor_sample/`: ASP.NET Core host application
- `blazor_sample/blazor_sample/blazor_sample.Client/`: Blazor WebAssembly client

## Requirements

- .NET 10 SDK
- An IDE with .NET 10 support (optional)

The repository-level `global.json` selects the .NET 10 SDK feature band and allows later feature bands within .NET 10. Install a .NET 10 SDK before running the commands below.

## Build and run

From the repository root:

```bash
dotnet restore blazor_sample/blazor_sample.sln
dotnet build blazor_sample/blazor_sample.sln --no-restore
dotnet run --project blazor_sample/blazor_sample/blazor_sample/blazor_sample.csproj
```

Open the local URL printed by ASP.NET Core. The sample includes Home, Counter, and Weather pages.

## Maintenance notes

### .NET and NuGet version alignment

The original project targeted `.NET 8`, referenced `Microsoft.AspNetCore.Components.WebAssembly` at `8.0.29`, and referenced `Microsoft.AspNetCore.Components.WebAssembly.Server` at `11.0.0`. This mixed framework/package combination was inconsistent. .NET 8 reached end of support in November 2026.

The project has been aligned to .NET 10 LTS:

1. Changed both projects' `TargetFramework` to `net10.0`.
2. Added `blazor_sample/Directory.Packages.props` to centrally manage the Blazor WebAssembly package versions at `10.0.15`.
3. Kept `Microsoft.AspNetCore.Components.WebAssembly.Server` only in the ASP.NET Core host project. The WebAssembly client references only `Microsoft.AspNetCore.Components.WebAssembly`.
4. Added repository-level `global.json` to select the .NET 10 SDK (`10.0.100` baseline, rolling forward to later .NET 10 feature bands).
5. Added a root `.gitignore` for .NET build output, IDE metadata, local environment overrides, and OS-generated files.

### Validation

Run the restore and build commands above after installing the .NET 10 SDK. A successful remote file update does not itself prove that the solution compiles; build validation should be performed in a .NET 10 environment.
