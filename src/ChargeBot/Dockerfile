FROM glennc/nightly:latest AS base
WORKDIR /src
COPY ["ChargeBot.csproj", "./"]
RUN dotnet restore "./ChargeBot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ChargeBot.csproj" -c Release -o /app

FROM base AS publish
RUN dotnet publish "ChargeBot.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChargeBot.dll"]
