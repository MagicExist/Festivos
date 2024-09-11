--Crear la base de datos
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_CATALOG='Festivos')
	CREATE DATABASE Festivos
ELSE
	PRINT 'Ya existe la BD [Festivos]'
GO

--Abrir la base de datos
USE Festivos
GO
--Para las siguientes instrucciones, se debe cambiar la conexión

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
					WHERE TABLE_NAME='Tipo')

	--Crear la tabla TIPO
	CREATE TABLE Tipo(
		Id int IDENTITY(1,1) NOT NULL,
		CONSTRAINT pkTipo_Id PRIMARY KEY (Id),
		Tipo VARCHAR(100) NOT NULL
		);
ELSE
	PRINT 'Ya existe la tabla [Tipo]'

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
					WHERE TABLE_NAME='Festivo')
	--Crear la tabla FESTIVO
	CREATE TABLE Festivo(
		Id int IDENTITY(1,1) NOT NULL,
		CONSTRAINT pkFestivo_Id PRIMARY KEY (Id),
		Nombre VARCHAR(100) NOT NULL,
		Dia INT NOT NULL,
		Mes INT NOT NULL,
		DiasPascua INT NOT NULL,
		IdTipo INT NOT NULL,
		CONSTRAINT fkFestivo_Tipo FOREIGN KEY (IdTipo) REFERENCES Tipo(Id)
		);
ELSE
	PRINT 'Ya existe la tabla [Festivo]'


GO
-- Get SundayPascua Date
CREATE PROC SundayPascua
    @Year INT,
    @PASCUADATE DATE OUTPUT
AS
BEGIN
    DECLARE @A INT;
    DECLARE @B INT;
    DECLARE @C INT;
    DECLARE @D INT;
    DECLARE @RESULT INT;

    -- Cálculos
    SET @A = @Year % 19;
    SET @B = @Year % 4;
    SET @C = @Year % 7;
    SET @D = (19 * @A + 24) % 30;
    
    -- Calcular el número de días para el día de Pascua
    SET @RESULT = 15 + (@D + (2 * @B + 4 * @C + 6 * @D + 5) % 7);

    -- Asegúrate de que el día es válido
    IF @RESULT <= 31
    BEGIN
        -- Si el resultado está en marzo
        SET @PASCUADATE = DATEFROMPARTS(@Year, 3, @RESULT);
    END
    ELSE
    BEGIN
        -- Si el resultado está en abril
        SET @PASCUADATE = DATEFROMPARTS(@Year, 4, @RESULT - 31);
    END
    
    -- Ajusta al siguiente domingo (siete días después)
    SET @PASCUADATE = DATEADD(DAY, 7, @PASCUADATE);
END

--Move date to the next monday
GO
CREATE PROCEDURE ObtenerProximoLunes
    @fecha DATE
AS
BEGIN
    -- Calcular el número de días hasta el próximo lunes
    SELECT DATEADD(DAY, CASE 
                           WHEN (9- DATEPART(WEEKDAY, @fecha)) % 7 = 0 
                           THEN 7 
                           ELSE (9 - DATEPART(WEEKDAY, @fecha)) % 7 
                        END, @fecha) AS FechaProximoLunes;
END