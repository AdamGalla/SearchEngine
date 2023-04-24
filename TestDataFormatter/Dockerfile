#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestDataFormatter/TestDataFormatter.csproj", "TestDataFormatter/"]
COPY ["DataFormater/DataFormatter.csproj", "DataFormater/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "TestDataFormatter/TestDataFormatter.csproj"
COPY . .
WORKDIR "/src/TestDataFormatter"
RUN dotnet build "TestDataFormatter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestDataFormatter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "TestDataFormatter.dll"]