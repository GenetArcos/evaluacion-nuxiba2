USE CCenterRIA;
GO

-- =============================================
-- 1. Crear Tabla de Áreas (Catálogo)
-- =============================================
IF OBJECT_ID('dbo.ccRIACat_Areas', 'U') IS NOT NULL DROP TABLE dbo.ccRIACat_Areas;
CREATE TABLE dbo.ccRIACat_Areas (
    IdArea INT IDENTITY(1,1) PRIMARY KEY,
    NombreArea VARCHAR(100) NOT NULL
);

SET IDENTITY_INSERT dbo.ccRIACat_Areas ON;
INSERT INTO dbo.ccRIACat_Areas (IdArea, NombreArea) VALUES
(1, 'Sistemas'),
(2, 'Recursos Humanos'),
(3, 'Ventas');
SET IDENTITY_INSERT dbo.ccRIACat_Areas OFF;
GO

-- =============================================
-- 2. Crear Tabla de Usuarios
-- =============================================
IF OBJECT_ID('dbo.ccUsers', 'U') IS NOT NULL DROP TABLE dbo.ccUsers;
CREATE TABLE dbo.ccUsers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    User_id INT NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    ApellidoPaterno VARCHAR(100) NOT NULL,
    ApellidoMaterno VARCHAR(100) NOT NULL,
    Login VARCHAR(50) NOT NULL,
    idArea INT NULL,
    CONSTRAINT FK_ccUsers_ccRIACat_Areas FOREIGN KEY (idArea) REFERENCES dbo.ccRIACat_Areas(IdArea)
);

INSERT INTO dbo.ccUsers (User_id, Nombres, ApellidoPaterno, ApellidoMaterno, Login, idArea) VALUES
(1, 'Juan', 'Perez', 'Lopez', 'jperez', 1),
(2, 'Maria', 'Gomez', 'Hernandez', 'mgomez', 1),
(3, 'Carlos', 'Sanchez', 'Diaz', 'csanchez', 2),
(4, 'Ana', 'Torres', 'Ramirez', 'atorres', 2),
(5, 'Luis', 'Morales', 'Castro', 'lmorales', 3);
GO

-- =============================================
-- 3. Crear Tabla de Registros de Logins
-- =============================================
IF OBJECT_ID('dbo.ccloglogin', 'U') IS NOT NULL DROP TABLE dbo.ccloglogin;
CREATE TABLE dbo.ccloglogin (
    id INT IDENTITY(1,1) PRIMARY KEY,
    User_id INT NOT NULL,
    Extension INT NOT NULL,
    TipoMov INT NOT NULL, -- 1 = Login, 0 = Logout
    fecha DATETIME NOT NULL
);

INSERT INTO dbo.ccloglogin (User_id, Extension, TipoMov, fecha) VALUES
(1, 101, 1, '2026-10-01 08:00:00'),
(1, 101, 0, '2026-10-01 17:00:00'),
(2, 102, 1, '2026-10-01 09:15:00'),
(2, 102, 0, '2026-10-01 18:00:00'),
(1, 101, 1, '2026-10-02 08:05:00'),
(1, 101, 0, '2026-10-02 16:50:00'),
(3, 103, 1, '2026-10-02 10:00:00'),
(4, 104, 1, '2026-10-03 08:30:00'),
(4, 104, 0, '2026-10-03 15:30:00'),
(5, 105, 1, '2026-10-03 09:00:00'),
(5, 105, 0, '2026-10-03 17:30:00');
GO