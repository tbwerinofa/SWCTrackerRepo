#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SWCTracker/SWCTracker.csproj", "SWCTracker/"]
COPY ["BusinessObject/BusinessObject.csproj", "BusinessObject/"]
RUN dotnet restore "SWCTracker/SWCTracker.csproj"
COPY . .
WORKDIR "/src/SWCTracker"
RUN dotnet build "SWCTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SWCTracker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SWCTracker.dll"]