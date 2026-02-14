# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copier tout le projet
COPY . .

# Restaurer les dépendances
RUN dotnet restore "./CheckMyStar.Apis.csproj"

# Compiler et publier
RUN dotnet publish "./CheckMyStar.Apis.csproj" -c Release -o /app/publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Exposer le port de ton API
EXPOSE 8080

# Copier les fichiers publiés
COPY --from=build /app/publish .

# Lancer l’API
ENTRYPOINT ["dotnet", "CheckMyStar.Apis.dll"]
