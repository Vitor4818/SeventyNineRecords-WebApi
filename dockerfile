# Usar a imagem oficial do SDK para build (build stage)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copia os arquivos do projeto para o container
COPY . .

# Restaura os pacotes NuGet
RUN dotnet restore

# Build da aplicação em Release
RUN dotnet publish -c Release -o /app/publish

# Imagem final - runtime (menor, só roda a aplicação)
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Criar usuário não-root (UID 1001, GID 1001) e grupo
RUN groupadd -g 1001 appgroup && \
    useradd -u 1001 -g appgroup -m appuser

# Diretório de trabalho para rodar a aplicação
WORKDIR /app

# Copiar arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Dar propriedade dos arquivos ao usuário criado
RUN chown -R appuser:appgroup /app

# Definir usuário não-root para rodar a aplicação
USER appuser

# Variável de ambiente exemplo (você pode usar suas próprias ENV no docker run)
ENV ASPNETCORE_ENVIRONMENT=Production

# Expor a porta padrão da aplicação ASP.NET Core
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "SeventyNineRecordsApi.dll"]
