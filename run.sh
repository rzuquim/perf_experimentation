#!/bin/bash

# TODO: argument and case when there are more langs

dotnet build --configuration=Release ./dotnet/dotnet.csproj
dotnet ./dotnet/bin/Release/net8.0/dotnet.dll

