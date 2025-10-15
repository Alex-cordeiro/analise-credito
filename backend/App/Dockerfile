#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ANALISECREDITO.API/ANALISECREDITO.API.csproj", "ANALISECREDITO.API/"]
COPY ["ANALISECREDITO.Application/ANALISECREDITO.Application.csproj", "ANALISECREDITO.Application/"]
COPY ["ANALISECREDITO.Core/ANALISECREDITO.Core.csproj", "ANALISECREDITO.Core/"]
COPY ["ANALISECREDITO.Data/ANALISECREDITO.Data.csproj", "ANALISECREDITO.Data/"]
COPY ["ANALISECREDITO.CrossCutting/ANALISECREDITO.CrossCutting.csproj", "ANALISECREDITO.CrossCutting/"]
COPY ["ANALISECREDITO.Domain/ANALISECREDITO.Domain.csproj", "ANALISECREDITO.Domain/"]
COPY ["ANALISECREDITO.Utils/ANALISECREDITO.Utils.csproj", "ANALISECREDITO.Utils/"]

RUN dotnet restore "ANALISECREDITO.API/ANALISECREDITO.API.csproj"
COPY . .
WORKDIR "ANALISECREDITO.API"
RUN dotnet build "ANALISECREDITO.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ANALISECREDITO.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ANALISECREDITO.API.dll"]