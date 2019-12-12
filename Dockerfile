FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS base
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY constituentsearch.sln ./
COPY AlsacWebApiCore/*.csproj ./AlsacWebApiCore/
COPY ConstituentSearch/*.csproj ./ConstituentSearch/

RUN dotnet restore
COPY . .
WORKDIR /src/AlsacWebApiCore
RUN dotnet build -c Release -o /app

WORKDIR /src/ConstituentSearch
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ConstituentSearch.dll"]
