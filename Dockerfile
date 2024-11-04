FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Testestefanini.csproj", "."]
RUN dotnet restore "./Testestefanini.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Testestefanini.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Testestefanini.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Testestefanini.dll"]
