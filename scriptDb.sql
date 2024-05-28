
CREATE DATABASE [Magnetron]

USE [Magnetron]
GO

--CREAR TABLAS 
CREATE TABLE [dbo].[Producto](
[Prod_Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Prod_Descripcion] [varchar](200) NOT NULL,
	[Prod_Precio] [decimal](18, 2) NOT NULL,
	[Prod_Costo] [decimal](18, 2) NOT NULL,
	[Prod_UM] [varchar](200) NOT NULL,
	[Prod_FechaCreacion] [date] NOT NULL DEFAULT getdate(),
	[Prod_UsuarioCreacion] [varchar](50) NOT NULL DEFAULT ('ADMIN'),
	);

	CREATE TABLE [dbo].[Persona](
	[Per_Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Per_Nombre] [varchar](200) NOT NULL,
	[Per_Apellido] [varchar](200) NOT NULL,
	[Per_TipoDocumento] [varchar](50) NOT NULL,
	[Per_Documento] [int] NOT NULL,
	[Per_FechaCreacion] [date] NOT NULL DEFAULT getdate(),
	[Per_UsuarioCreacion] [varchar](50) NOT NULL DEFAULT ('ADMIN'),
	)

	CREATE TABLE [dbo].[FacturaEncabezado](
	[FEnc_Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[FEnc_Numero] [int] NOT NULL,
	[FEnc_Fecha] [date] NOT NULL,
	[Per_Id] [int] NOT NULL ,
	[FE_FechaCreacion] [date] NOT NULL DEFAULT getdate(),
	[FE_UsuarioCreacion] [varchar](50) NOT NULL DEFAULT ('ADMIN'),
	CONSTRAINT FK_FacturaEncabezado_Persona FOREIGN KEY (Per_Id) REFERENCES dbo.Persona (Per_Id)
	)

	CREATE TABLE [dbo].[FacturaDetalle](
	[FDet_Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[FDet_Linea] [int] NOT NULL,
	[FDet_Cantidad] [int] NOT NULL,
	[Prod_Id] [int] NOT NULL,
	[FEnc_Id] [int] NOT NULL,
	[FEnc_FechaCreacion] [date] NOT NULL DEFAULT getdate(),
	[FEnc_UsuarioCreacion] [varchar](50) NOT NULL  DEFAULT ('ADMIN'),
	CONSTRAINT FK_FacturaDetalle_FacturaEncabezado FOREIGN KEY(FEnc_Id) REFERENCES dbo.FacturaEncabezado ([FEnc_Id]),
	CONSTRAINT FK_FacturaDetalle_Producto FOREIGN KEY(Prod_Id) REFERENCES dbo.Producto (Prod_Id)
	)

--****************Insertar Registros****************************************
INSERT INTO Producto (Prod_Descripcion, Prod_Precio, Prod_Costo, Prod_UM)
VALUES 
('Camiseta', 15.99, 5.00, 'Unidad'),
('Pantalón', 29.99, 10.00, 'Unidad'),
('Zapatos', 49.99, 20.00, 'Par'),
('Sombrero', 12.99, 3.50, 'Unidad'),
('Bufanda', 9.99, 4.00, 'Unidad'),
('Guantes', 7.99, 2.50, 'Par'),
('Vestido', 39.99, 15.00, 'Unidad'),
('Gafas de sol', 19.99, 8.00, 'Unidad'),
('Reloj', 59.99, 25.00, 'Unidad'),
('Bolso', 34.99, 12.00, 'Unidad');

INSERT INTO Persona (Per_Nombre, Per_Apellido, Per_TipoDocumento, Per_Documento)
VALUES 
('Juan', 'Pérez', 'CC', 12345678),
('María', 'Gómez', 'CC', 23456789),
('Pedro', 'López', 'CC', 34567890),
('Ana', 'Martínez', 'CC', 45678901),
('Luis', 'Sánchez', 'CC', 56789012),
('Laura', 'Hernández', 'CC', 67890123),
('Carlos', 'Díaz', 'CC', 78901234),
('Sofía', 'Rodríguez', 'CC', 89012345),
('David', 'García', 'CC', 90123456),
('Elena', 'Fernández', 'CC', 12344321);

INSERT INTO FacturaEncabezado (FEnc_Numero, FEnc_Fecha, Per_Id)
VALUES 
(1001, '2024-05-01', 1),
(1002, '2024-05-02', 2),
(1003, '2024-05-03', 3),
(1004, '2024-05-04', 4),
(1005, '2024-05-05', 5),
(1006, '2024-05-06', 6),
(1007, '2024-05-07', 7),
(1008, '2024-05-08', 8),
(1009, '2024-05-09', 9),
(1010, '2024-05-10', 10);

INSERT INTO FacturaDetalle (FDet_Linea, FDet_Cantidad, Prod_Id, FEnc_Id)
VALUES 
(1, 2, 1, 1),
(2, 1, 2, 1),
(3, 3, 3, 2),
(4, 2, 4, 2),
(5, 1, 5, 3),
(6, 4, 6, 3),
(7, 2, 7, 4),
(8, 3, 8, 4),
(9, 1, 9, 5),
(10, 2, 10, 5);
--**************** Crear Vistas*********************************************
CREATE VIEW VistaPersonaFacturado AS
	SELECT p.Per_Id,
       p.Per_Nombre,
       p.Per_Apellido,
       ISNULL(SUM(ISNULL(fd.FDet_Cantidad * pr.Prod_Precio, 0)), 0) AS TotalFacturado
		FROM Persona p
		LEFT JOIN FacturaEncabezado fe ON p.Per_Id = fe.Per_Id
		LEFT JOIN FacturaDetalle fd ON fe.FEnc_Id = fd.FEnc_Id
		LEFT JOIN Producto pr ON fd.Prod_Id = pr.Prod_Id
		GROUP BY p.Per_Id, p.Per_Nombre, p.Per_Apellido;

CREATE VIEW VistaPersonaProductoMasCaro AS
	SELECT p.Per_Id,
       p.Per_Nombre,
       p.Per_Apellido,
       ISNULL(MAX(pr.Prod_Precio), 0) AS PrecioProductoMasCaro
		FROM Persona p
		JOIN FacturaEncabezado fe ON p.Per_Id = fe.Per_Id
		JOIN FacturaDetalle fd ON fe.FEnc_Id = fd.FEnc_Id
		JOIN Producto pr ON fd.Prod_Id = pr.Prod_Id
		GROUP BY p.Per_Id, p.Per_Nombre, p.Per_Apellido
		order by PrecioProductoMasCaro desc;


CREATE VIEW VistaProductosPorCantidad AS
	SELECT p.Prod_Id,
       p.Prod_Descripcion,
       p.Prod_Precio,
       p.Prod_Costo,
       p.Prod_UM,
       ISNULL(SUM(fd.FDet_Cantidad), 0) AS CantidadFacturada
		FROM Producto p
		LEFT JOIN FacturaDetalle fd ON p.Prod_Id = fd.Prod_Id
		GROUP BY p.Prod_Id, p.Prod_Descripcion, p.Prod_Precio, p.Prod_Costo, p.Prod_UM
		ORDER BY CantidadFacturada DESC;

CREATE VIEW VistaProductosPorUtilidad AS
		SELECT p.Prod_Id,
			   p.Prod_Descripcion,
			   p.Prod_Precio,
			   p.Prod_Costo,
			   p.Prod_UM,
			   ISNULL(SUM((fd.FDet_Cantidad * p.Prod_Precio) - (fd.FDet_Cantidad * p.Prod_Costo)), 0) AS UtilidadGenerada
		FROM Producto p
		LEFT JOIN FacturaDetalle fd ON p.Prod_Id = fd.Prod_Id
		GROUP BY p.Prod_Id, p.Prod_Descripcion, p.Prod_Precio, p.Prod_Costo, p.Prod_UM;

CREATE VIEW VistaProductosPorMargenGanancia AS
		SELECT p.Prod_Id,
			   p.Prod_Descripcion,
			   p.Prod_Precio,
			   p.Prod_Costo,
			   p.Prod_UM,
			   ISNULL(AVG(((p.Prod_Precio - p.Prod_Costo) / p.Prod_Costo) * 100), 0) AS MargenGananciaPromedio
		FROM Producto p
		LEFT JOIN FacturaDetalle fd ON p.Prod_Id = fd.Prod_Id
		GROUP BY p.Prod_Id, p.Prod_Descripcion, p.Prod_Precio, p.Prod_Costo, p.Prod_UM;

