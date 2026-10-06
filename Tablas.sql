USE CCenterRIA;
GO

-- 1. Tabla de Áreas
IF OBJECT_ID('dbo.ccRIACat_Areas', 'U') IS NOT NULL DROP TABLE dbo.ccRIACat_Areas;
CREATE TABLE dbo.ccRIACat_Areas (
    idArea INT PRIMARY KEY IDENTITY(1,1),
    NombreArea NVARCHAR(100) NOT NULL
);

-- 2. Tabla de Usuarios
IF OBJECT_ID('dbo.ccUsers', 'U') IS NOT NULL DROP TABLE dbo.ccUsers;
CREATE TABLE dbo.ccUsers (
    idUser INT PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NULL,
    idArea INT NULL,
    FOREIGN KEY (idArea) REFERENCES dbo.ccRIACat_Areas(idArea)
);

-- 3. Tabla de Logs de Sesión (Login)
IF OBJECT_ID('dbo.ccloglogin', 'U') IS NOT NULL DROP TABLE dbo.ccloglogin;
CREATE TABLE dbo.ccloglogin (
    idLog INT PRIMARY KEY IDENTITY(1,1),
    idUser INT NOT NULL,
    FechaIngreso DATETIME NOT NULL,
    FechaSalida DATETIME NULL,
    FOREIGN KEY (idUser) REFERENCES dbo.ccUsers(idUser)
);
GO