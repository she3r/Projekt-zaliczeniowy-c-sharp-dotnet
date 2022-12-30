FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ProjektZaliczeniowy.csproj", "./"]
RUN dotnet restore "ProjektZaliczeniowy.csproj"
COPY . .
RUN dotnet publish "ProjektZaliczeniowy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProjektZaliczeniowy.dll"]
