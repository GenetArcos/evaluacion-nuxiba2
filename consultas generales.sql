SELECT 
    u.idUser,
    u.Username,
    u.FirstName + ' ' + u.LastName AS NombreCompleto,
    u.Email,
    a.NombreArea
FROM dbo.ccUsers u
INNER JOIN dbo.ccRIACat_Areas a ON u.idArea = a.idArea;

SELECT 
    u.idUser,
    u.Username,
    COUNT(l.idLog) AS TotalLogins
FROM dbo.ccUsers u
LEFT JOIN dbo.ccloglogin l ON u.idUser = l.idUser
GROUP BY u.idUser, u.Username
ORDER BY TotalLogins DESC;

SELECT 
    u.idUser,
    u.Username,
    COUNT(l.idLog) AS TotalLogins
FROM dbo.ccUsers u
LEFT JOIN dbo.ccloglogin l ON u.idUser = l.idUser
GROUP BY u.idUser, u.Username
ORDER BY TotalLogins DESC;

SELECT 
    u.Username,
    l.FechaIngreso
FROM dbo.ccloglogin l
INNER JOIN dbo.ccUsers u ON l.idUser = u.idUser
WHERE l.FechaSalida IS NULL;