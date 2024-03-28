#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConnectedVehicleAPI.Service/ConnectedVehicleAPI.Service.csproj", "ConnectedVehicleAPI.Service/"]
COPY ["ConnectVehicle.Data/ConnectVehicle.Data.csproj", "ConnectVehicle.Data/"]
#COPY ["ConnectVehicle.Utility/ConnectVehicle.Utility.csproj", "ConnectVehicle.Utility/"]
RUN dotnet restore "ConnectedVehicleAPI.Service/ConnectedVehicleAPI.Service.csproj"
COPY . .
WORKDIR "/src/ConnectedVehicleAPI.Service"
RUN dotnet build "ConnectedVehicleAPI.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConnectedVehicleAPI.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConnectedVehicleAPI.Service.dll"]