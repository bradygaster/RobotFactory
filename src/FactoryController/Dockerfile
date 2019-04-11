FROM glennc/nightly:latest AS base
WORKDIR /app

FROM glennc/nightly:latest AS build
WORKDIR /src
COPY ["FactoryController.csproj", "./"]
RUN dotnet restore "./FactoryController.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FactoryController.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FactoryController.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FactoryController.dll"]
