# EVALUACIÓN TÉCNICA NUXIBA - DESARROLLADOR JR .NET 8 & SQL SERVER

**Nombre del participante:** Mariana Genet Castrejón Arcos

---

## Requisitos Previos e Instalación

### 1. Levantamiento del Contenedor de SQL Server con Docker

Para iniciar la base de datos SQL Server mediante Docker, ejecuta el siguiente comando en la terminal:

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong!Passw0rd' -p 1433:1433 --name sqlserver -d [mcr.microsoft.com/mssql/server:2019-latest](https://mcr.microsoft.com/mssql/server:2019-latest)