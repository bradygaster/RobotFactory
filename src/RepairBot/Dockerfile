FROM glennc/nightly:latest AS base
WORKDIR /src
COPY ["RepairBot.csproj", "./"]
RUN dotnet restore "./RepairBot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RepairBot.csproj" -c Release -o /app

FROM base AS publish
RUN dotnet publish "RepairBot.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RepairBot.dll"]
