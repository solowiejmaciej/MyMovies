FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["backend/MyMovies.csproj", "backend/"]
RUN dotnet restore "backend/MyMovies.csproj"
COPY . .
WORKDIR "/src/backend"
RUN dotnet build "MyMovies.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyMovies.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyMovies.dll"]
