IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\NetCore')
BEGIN
    CREATE LOGIN [IIS APPPOOL\NetCore] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
CREATE USER [BookUser] 
  FOR LOGIN [IIS APPPOOL\NetCore]
GO
EXEC sp_addrolemember 'db_owner', 'BookUser'
GO