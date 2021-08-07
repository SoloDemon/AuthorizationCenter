#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AuthorizationCenter/AuthorizationCenter.csproj", "AuthorizationCenter/"]
RUN dotnet restore "AuthorizationCenter/AuthorizationCenter.csproj"
COPY . .
WORKDIR "/src/AuthorizationCenter"
RUN dotnet build "AuthorizationCenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthorizationCenter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthorizationCenter.dll"]