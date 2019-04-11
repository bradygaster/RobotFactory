FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
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
