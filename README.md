# EVALUACIÓN TÉCNICA NUXIBA - DESARROLLADOR JR .NET 8 & SQL SERVER

**Nombre del participante:** Mariana Genet Castrejón Arcos

---

# Nuxiba API - Control de Asistencia y Reportes

API desarrollada en **.NET 8** y **SQL Server** para la gestión de logons/logoffs de usuarios y la generación de reportes de horas trabajadas.

---

## Requisitos Previos

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (opcional para levantar SQL Server en contenedor) o [SQL Server Express / Developer](https://www.microsoft.com/es-mx/sql-server/sql-server-downloads)
* [Git](https://git-scm.com/)

---

## 1. Levantar la Base de Datos (SQL Server)

### Opción A: Mediante Docker (Recomendado)

1. Ejecuta el siguiente comando para levantar un contenedor de SQL Server:
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=TuPassword123!" -p 1433:1433 --name sqlserver_nuxiba -d [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)

2. Conéctate con SQL Server Management Studio (SSMS) o Azure Data Studio:

Servidor: localhost,1433

Usuario: sa

Contraseña: TuPassword123!

3. Conectar y Configurar la Base de Datos
Paso 1: Abrir el Explorador de Objetos de SQL Server
Abre tu proyecto en Visual Studio.

En el menú superior, ve a Ver (View) -> Explorador de objetos de SQL Server (SQL Server Object Explorer).

Paso 2: Conectar la instancia de Docker
En el panel del Explorador de Objetos, haz clic derecho sobre el nodo SQL Server y selecciona Agregar SQL Server... (Add SQL Server...).

En la ventana de conexión configura los credenciales que definiste al levantar el Docker:

Nombre del servidor (Server Name): localhost,1433 (o 127.0.0.1,1433)

Autenticación (Authentication): SQL Server Authentication

Inicio de sesión (Login): sa

Contraseña (Password): TuPassword123! (la contraseña configurada en tu comando docker run)

Cifrado (Trust server certificate): Marca la casilla Trust server certificate (Confiar en el certificado del servidor).

Haz clic en Conectar (Connect).

Paso 3: Crear la Base de Datos CCenterRIA y Ejecutar el Script
Despliega el nodo de tu servidor en la lista.

Haz clic derecho sobre la carpeta Bases de datos (Databases) y selecciona Agregar nueva base de datos (Add New Database).

Escribe el nombre: CCenterRIA y presiona Enter.

Paso 4: Ejecutar el Script database.sql
Despliega la conexión localhost,1433 que acaba de aparecer.

Haz clic derecho sobre la carpeta Bases de datos (Databases) -> Agregar nueva base de datos....

Escribe CCenterRIA y presiona Enter.

Haz clic derecho sobre la base de datos CCenterRIA recién creada y selecciona Nueva consulta... (New Query...).

Copia y pega el contenido completo del archivo script ejecutar.sql en la ventana de consulta.

Haz clic en el botón de reproducción verde Ejecutar (Execute) o presiona F5 / Ctrl + Shift + E.

Paso 5: Verificar la creación de las tablas
Despliega la base de datos CCenterRIA -> carpeta Tablas (Tables).

Haz clic derecho en Tablas y selecciona Actualizar (Refresh).

Confirmarás que aparecen las tres tablas:

dbo.ccRIACat_Areas

dbo.ccUsers

dbo.ccloglogin


4. Asegúrate de ajustar la cadena de conexión en el archivo appsettings.json de la API:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=CCenterRIA;User Id=sa;Password=TuPassword123!;TrustServerCertificate=True;"
}

5. Ejecutar la API
Clona el repositorio e ingresa a la carpeta del proyecto:


git clone <URL_DE_TU_REPOSITORIO>
cd NuxibaApi

6. Restaura las dependencias del proyecto:
dotnet restore

7. Ejecutar la aplicación 
dotnet run

8. Abre la interfaz interactiva de Swagger en tu navegador:

Swagger UI: https://localhost:7114/swagger (o el puerto configurado al iniciar)

 9. Endpoints y Uso
Usuarios
GET /api/Users: Obtiene el catálogo de usuarios junto con el nombre del área asociada.

 Registros de Sesión (Logins)
GET /api/Logins: Consulta el historial completo de movimientos registrados.

POST /api/Logins: Registra una nueva marca de inicio (tipoMov: 1) o cierre de sesión (tipoMov: 0).

Reglas de negocio aplicadas:

Fecha automática: Asigna DateTime.Now si el campo fecha no es enviado en la petición.

Alternancia: Bloquea dobles inicios o dobles cierres de sesión consecutivos.

Validación de usuario: Verifica la existencia previa del user_id en la base de datos.

PUT /api/Logins/{id}: Actualiza un registro existente especificando su ID.

DELETE /api/Logins/{id}: Elimina un registro de log por su ID.

Ejemplo de payload:

JSON
{
  "user_id": 1,
  "extension": 101,
  "tipoMov": 1
}

10. Reportes
GET /api/Reportes/export-csv: Calcula el total de horas trabajadas para cada usuario basándose en sus registros de login/logout y descarga automáticamente un archivo CSV llamado ReporteHorasTrabajadas.csv.

11.  Descargar el Reporte CSV
Entra a Swagger UI (https://localhost:7114/swagger).

Despliega la sección Reportes -> GET /api/Reportes/export-csv.

Haz clic en Try it out y luego en Execute.

Haz clic en el botón Download file en la respuesta HTTP 200 para obtener el archivo generado..

