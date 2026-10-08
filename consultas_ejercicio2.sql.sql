-- ============================================================================
-- EJERCICIO 2: CONSULTAS SQL Y OPTIMIZACION
-- ============================================================================

-- 1. Usuario que MAS tiempo ha estado logueado
WITH Sesiones AS (
    SELECT 
        l.User_id,
        l.fecha AS FechaLogin,
        LEAD(l.fecha) OVER (PARTITION BY l.User_id ORDER BY l.fecha) AS FechaLogout
    FROM ccloglogin l
    WHERE l.TipoMov IN (0, 1)
),
DuracionSesiones AS (
    SELECT 
        User_id,
        SUM(DATEDIFF(SECOND, FechaLogin, FechaLogout)) AS TotalSegundos
    FROM Sesiones
    WHERE FechaLogout IS NOT NULL
    GROUP BY User_id
)
SELECT TOP 1 
    User_id,
    CONCAT(
        TotalSegundos / 86400, ' días, ',
        (TotalSegundos % 86400) / 3600, ' horas, ',
        (TotalSegundos % 3600) / 60, ' minutos, ',
        TotalSegundos % 60, ' segundos'
    ) AS TiempoTotal
FROM DuracionSesiones
ORDER BY TotalSegundos DESC;


-- 2. Usuario que MENOS tiempo ha estado logueado
WITH Sesiones AS (
    SELECT 
        l.User_id,
        l.fecha AS FechaLogin,
        LEAD(l.fecha) OVER (PARTITION BY l.User_id ORDER BY l.fecha) AS FechaLogout
    FROM ccloglogin l
    WHERE l.TipoMov IN (0, 1)
),
DuracionSesiones AS (
    SELECT 
        User_id,
        SUM(DATEDIFF(SECOND, FechaLogin, FechaLogout)) AS TotalSegundos
    FROM Sesiones
    WHERE FechaLogout IS NOT NULL
    GROUP BY User_id
)
SELECT TOP 1 
    User_id,
    CONCAT(
        TotalSegundos / 86400, ' días, ',
        (TotalSegundos % 86400) / 3600, ' horas, ',
        (TotalSegundos % 3600) / 60, ' minutos, ',
        TotalSegundos % 60, ' segundos'
    ) AS TiempoTotal
FROM DuracionSesiones
ORDER BY TotalSegundos ASC;


-- 3. Promedio de logueo por mes por usuario
WITH Sesiones AS (
    SELECT 
        l.User_id,
        YEAR(l.fecha) AS Anio,
        MONTH(l.fecha) AS Mes,
        DATENAME(MONTH, l.fecha) AS NombreMes,
        DATEDIFF(SECOND, l.fecha, LEAD(l.fecha) OVER (PARTITION BY l.User_id ORDER BY l.fecha)) AS SegundosSesion
    FROM ccloglogin l
    WHERE l.TipoMov IN (0, 1)
),
Promedios AS (
    SELECT 
        User_id,
        Anio,
        Mes,
        NombreMes,
        CAST(AVG(CAST(SegundosSesion AS FLOAT)) AS BIGINT) AS PromedioSegundos
    FROM Sesiones
    WHERE SegundosSesion IS NOT NULL
    GROUP BY User_id, Anio, Mes, NombreMes
)
SELECT 
    User_id,
    CONCAT(NombreMes, ' ', Anio) AS Periodo,
    CONCAT(
        PromedioSegundos / 86400, ' días, ',
        (PromedioSegundos % 86400) / 3600, ' horas, ',
        (PromedioSegundos % 3600) / 60, ' minutos, ',
        PromedioSegundos % 60, ' segundos'
    ) AS PromedioTiempoLogueo
FROM Promedios
ORDER BY User_id, Anio, Mes;