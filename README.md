# YakSE

Yak Stock Exchange application.

## Description

This application is built using:

- `Backend` .NET 5.0
- `Frontend` Angular 11

Persistence is managed using an in-memory store and is tied to the lifecycle of the application. As a result, all data is lost when the application is stopped.

### Components

#### Yakse.Core
The core domain of the application containing logic to create orders and manage stock prices.

#### Yakse.Infrastructure
Contains the implementation for persistence. This is currently a very simple in-memory repository.

#### Yakse.Web
This project contains the ASP.NET API and Angular application. It was built using the `Angular` dotnet template. Frontend code can be found in the `ClientApp` folder of this project.

#### Yakse.Core.Tests
Test project containing a very limited set of tests.

## Running The Application

1. Download the source code from github
1. Open a terminal window at the root of the source folder
1. Run `dotnet build`
1. Run `dotnet run --project src/Yakse.Web/Yakse.Web.csproj`
1. Open a browser window to [https://localhost:5001/](https://localhost:5001/)
