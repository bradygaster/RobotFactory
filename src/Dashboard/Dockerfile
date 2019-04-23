FROM glennc/nightly AS base
WORKDIR /app
EXPOSE 80

FROM glennc/nightly AS build
WORKDIR /src
COPY ["Dashboard.csproj", "./"]
RUN dotnet restore "./Dashboard.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Dashboard.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Dashboard.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dashboard.dll"]
