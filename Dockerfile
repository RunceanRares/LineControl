FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiem fișierele proiectului și restaurăm dependențele
COPY *.csproj ./
RUN dotnet restore

# Copiem restul fișierelor și compilăm aplicația
COPY . ./
RUN dotnet publish -c Release -o out

# Pasul 2: Creăm imaginea finală, mai mică, doar pentru rulare
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Comanda de pornire a aplicației
ENTRYPOINT ["dotnet", "LineControl.dll"]