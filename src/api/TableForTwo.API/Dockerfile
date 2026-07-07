FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/api/TableForTwo.API/TableForTwo.API.csproj", "src/api/TableForTwo.API/"]
RUN dotnet restore "src/api/TableForTwo.API/TableForTwo.API.csproj"
COPY . .
WORKDIR "/src/src/api/TableForTwo.API"
RUN dotnet build "./TableForTwo.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TableForTwo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TableForTwo.API.dll"]
