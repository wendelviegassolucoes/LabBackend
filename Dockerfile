FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY . ./

RUN dotnet build "LabBackend/LabBackend.csproj" -c Release -o /app/build
RUN dotnet publish "LabBackend/LabBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LabBackend.dll"]