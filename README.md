# aoc-2022-csharp

## Prerequisites

- .NET 7.0 SDK
- A GitHub account
- An Advent of Code account

Usage:

Use `dotnet user-secrets set InputRetrievers:Http:SessionCookie <session:supersecretvalue;>` to authenticate yourself. Log in and get the cookie out with developer tools, it lasts a month according to the website.

To run a given day, navigate to [AOCRunner](./src/AOCRunner/) and execute `dotnet run -- <day>` - the output will be printed to console.

To use the Sample input retriever, set the environment variable `AOC2022_InputRetrievers__UseSampleInputRetriever` to `true`.
