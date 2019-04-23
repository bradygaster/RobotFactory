FROM glennc/nightly:latest AS base
WORKDIR /src
COPY ["UniBot.csproj", "./"]
RUN dotnet restore "./UniBot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "UniBot.csproj" -c Release -o /app

FROM base AS publish
RUN dotnet publish "UniBot.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UniBot.dll"]
