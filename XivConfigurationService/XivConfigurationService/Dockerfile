FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["XivConfigurationService/XivConfigurationService/XivConfigurationService.csproj", "XivConfigurationService/XivConfigurationService/"]
COPY ["XivConfigurationService/XivConfigurationService.Client/XivConfigurationService.Client.csproj", "XivConfigurationService/XivConfigurationService.Client/"]
RUN dotnet restore "XivConfigurationService/XivConfigurationService/XivConfigurationService.csproj"
COPY . .
WORKDIR "/src/XivConfigurationService/XivConfigurationService"
RUN dotnet build "./XivConfigurationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./XivConfigurationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XivConfigurationService.dll"]
