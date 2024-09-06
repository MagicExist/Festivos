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

	

	SET @A = @Year % 19;
	SET @B = @Year % 4;
	SET @C = @Year % 7;
	SET @D = (19*@A+24) % 30

	SET @RESULT = 15 + (@D + (2*@B+4*@C+6*@D+5) % 7) 

	SET @PASCUADATE = CAST(@Year AS VARCHAR(4)) + '-03-' + RIGHT('00' + CAST(@RESULT AS VARCHAR(2)), 2);
	SET @PASCUADATE = DATEADD(DAY,7,@PascuaDate);
END