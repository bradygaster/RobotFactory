FROM glennc/nightly:latest AS base
WORKDIR /src
COPY ["MonitorBot.csproj", "./"]
RUN dotnet restore "./MonitorBot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MonitorBot.csproj" -c Release -o /app

FROM base AS publish
RUN dotnet publish "MonitorBot.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MonitorBot.dll"]
