USE master
GO

--------------------------------------------------------------------------------------------------------------------------
------------------------------------------ CREACION DE LA BASE DE DATOS --------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------

IF exists(SELECT * FROM SysDataBases WHERE name='CrediYi')
BEGIN
	DROP DATABASE CrediYi
END
GO

CREATE DATABASE CrediYi
GO

USE CrediYi
GO

--------------------------------------------------------------------------------------------------------------------------
----------------------------------------------- CREACION DE TABLAS -------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------

CREATE TABLE Usuarios (
	Usu VARCHAR(10) NOT NULL  PRIMARY KEY,
	Pass VARCHAR(8) NOT NULL  CHECK (Pass like '[a-z][a-z][a-z][a-z][a-z][0-9][0-9][^A-Za-z0-9 ]'),
	NomUsu varchar (30) NOT NULL 
)
GO

CREATE TABLE TiposDocumentos (
	IdTipoDoc INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	DscTipoDoc VARCHAR(25) NOT NULL
)
GO

CREATE TABLE Clientes (
	IdCliente INT NOT NULL PRIMARY KEY,
	IdTipoDoc INT NOT NULL FOREIGN KEY REFERENCES TiposDocumentos (IdTipoDoc), 
	NumDoc INT NOT NULL, -- CI, RUC , ETC
	Nombre VARCHAR(30) NOT NULL,
	Email VARCHAR(50) CHECK ((Email) LIKE '%_@__%.__%'),
	Tel VARCHAR(9) NOT NULL CHECK ( Tel like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
)
GO

CREATE TABLE Productos (
	IdProd INT NOT NULL PRIMARY KEY ,
	DscProd VARCHAR(25) NOT NULL ,
	Importe MONEY NOT NULL 
) 
GO

CREATE TABLE CompraCabezal(
	Serie VARCHAR(3) NOT NULL ,
	Numero INT NOT NULL, 
	IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(IdCliente),
	Fecha DATETIME NOT NULL DEFAULT getdate(),
	PRIMARY KEY (Serie, Numero)	
	
)
GO

CREATE TABLE LineaCabezal (
	Serie VARCHAR(3) NOT NULL,
	Numero INT NOT NULL,
	IdLinea INT NOT NULL, 
	IdProd INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProd),
	Cantidad INT NOT NULL,
	PRIMARY KEY (Serie, Numero, IdLinea),
	CONSTRAINT IdCompra FOREIGN KEY (Serie, Numero) REFERENCES CompraCabezal(Serie, Numero)
)
GO



--------------------------------------------------------------------------------------------------------
----------------------------- INSERCION DATOS DE PRUEBA ------------------------------------------------
--------------------------------------------------------------------------------------------------------

INSERT Usuarios VALUES ('leticia', 'letit18$', 'Leticia Torres' );
GO

INSERT TiposDocumentos VALUES ('CI' );
INSERT TiposDocumentos VALUES ('RUC' );
INSERT TiposDocumentos VALUES ('Pasaporte' );
INSERT TiposDocumentos VALUES ('Otro' );
GO

INSERT Clientes VALUES (100,01,49165910, 'Leticia Torres', 'lb.torresgarcia@gmail.com', '094586166' ); 
INSERT Clientes VALUES (101,01,51002004, 'Javier Garcia', 'jgarcia@gmail.com', '095786251' ); 
INSERT Clientes VALUES (102,01,42005006, 'Mercedes Acosta', 'macosta@gmail.com', '094704520' ); 
INSERT Clientes VALUES (103,01,49006007, 'Victor Louro', 'vlouro@gmail.com', '093106870' ); 
GO

INSERT Productos VALUES (100, 'Harina', 120.00);
INSERT Productos VALUES (101, 'Aceite', 90.00);
INSERT Productos VALUES (102, 'Leche', 80.00);
INSERT Productos VALUES (103, 'Café', 139.90);
INSERT Productos VALUES (104, 'Pan', 69.90);
INSERT Productos VALUES (105, 'Manteca', 85.00);
INSERT Productos VALUES (106, 'Azúcar', 110.00);
INSERT Productos VALUES (107, 'Alfajor' , 60.00);
INSERT Productos VALUES (108, 'Jugo en polvo', 40.00 );
INSERT Productos VALUES (109, 'Galletas', 89.99);
GO

INSERT CompraCabezal VALUES ('AAA',1,100, getdate());
INSERT CompraCabezal VALUES ('BBB',1,103, getdate());
INSERT CompraCabezal VALUES ('CCC',1,102, getdate());
GO

INSERT LineaCabezal VALUES ('AAA',1,1,103, 10);
INSERT LineaCabezal VALUES ('AAA',1,2,104, 5);
INSERT LineaCabezal VALUES ('BBB',1,1,105, 15);
INSERT LineaCabezal VALUES ('CCC',1,1,107, 9);
INSERT LineaCabezal VALUES ('CCC',1,2,109, 4);
INSERT LineaCabezal VALUES ('CCC',1,3,102, 2);
GO

