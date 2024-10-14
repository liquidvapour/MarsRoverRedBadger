#!/usr/bin/env bash
dotnet test
dotnet run --project src/MarsRover/MarsRover.csproj < test.txt