# SmartCharging

GreenFlux smart charging solution

## Features

- Group can contain multiple charge stations and has capacity in Amps
- Charge station can contain multiple connectors (at least one, but not more than 5).
- Connector has max current in Amps 
- All can be created, updated and removed

## Tech

- SmartCharging is a .NET 6 Web API application with some external libaries used:

	- [AutoMapper] - AutoMapper is a library built to solve a complex problem - getting rid of code that mapped one object to another
	- [MediatR] - Simple mediator implementation in .NET. Supports request/response, commands, queries, notifications and events, synchronous and async with intelligent dispatching via C# generic variance.
	- [Entity Framework Core] - Entity Framework Core is a modern object-database mapper for .NET

## Installation

Solution and the tests are ready to run with VisualStudio

- Solution can be run and debugged with VisualStudio and `SmartCharging` profile
- Also, after compiling the solution `dotnet SmartCharging.dll` command or running `SmartCharging.exe` can run the application
- Swagger is integrated, and the url is https://localhost:5001/swagger/index.html

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job

   [AutoMapper]: <https://automapper.org/>
   [MediatR]: <https://github.com/jbogard/MediatR>
   [Entity Framework Core]: <https://docs.microsoft.com/en-us/ef/core/>