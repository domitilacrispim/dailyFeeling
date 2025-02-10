# Use a imagem base oficial do .NET 8 para construir e rodar a aplicação
# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie os arquivos do projeto e restaure as dependências
COPY . ./
RUN dotnet restore

# Compile a aplicação em Release
RUN dotnet publish -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Expõe a porta 8080 (porta personalizada que você configurou)
EXPOSE 8080

RUN apt update
RUN apt-get install -y curl

# Comando de inicialização
ENTRYPOINT ["dotnet", "DailyFeeling.dll"]
