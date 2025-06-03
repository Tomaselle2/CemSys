CREATE DATABASE CemSys;
GO
USE CemSys;
GO

-- Tablas de configuración inicial
CREATE TABLE TipoCategoriaPersona (
    idCategoriaPersona INT PRIMARY KEY IDENTITY(1,1),
    tipo VARCHAR(25) NOT NULL
);

CREATE TABLE TipoNumeracionParcela (
    idTipoNumeracionParcela INT PRIMARY KEY IDENTITY(1,1),
    tipoNumeracion VARCHAR(25) NOT NULL
);

CREATE TABLE TipoNicho (
    idTipoNicho INT PRIMARY KEY IDENTITY(1,1),
    tipoNicho VARCHAR(15) NOT NULL,
    porDefecto BIT NOT NULL DEFAULT 0
);

CREATE TABLE EstadoDifunto (
    idEstadoDifunto INT PRIMARY KEY IDENTITY(1,1),
    estado VARCHAR(20) NOT NULL
);

CREATE TABLE TipoUsuario (
    idTipoUsuario INT PRIMARY KEY IDENTITY(1,1),
    tipoUsuario VARCHAR(20) NOT NULL
);

CREATE TABLE NumeroCuenta (
    idNumeroCuenta INT PRIMARY KEY,
    numero INT UNIQUE NOT NULL
);

-- Tablas de secciones
CREATE TABLE SeccionesFosas (
    idSeccionFosa INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE,
    fosas INT NOT NULL,
    visibilidad BIT NOT NULL
);

CREATE TABLE SeccionesNichos (
    idSeccionNicho INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE,
    filas INT NOT NULL,
    nichos INT NOT NULL,
    visibilidad BIT NOT NULL,
    tipoNumeracion INT NOT NULL,
    FOREIGN KEY (tipoNumeracion) REFERENCES TipoNumeracionParcela(idTipoNumeracionParcela)
);

CREATE TABLE SeccionesPanteones (
    idSeccionPanteones INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    panteones INT NOT NULL,
    visibilidad BIT NOT NULL
);

-- Tablas de personas y usuarios
CREATE TABLE Personas (
    idPersona INT PRIMARY KEY IDENTITY(1,1),
    visibilidad BIT NOT NULL,
    nombre VARCHAR(60) NOT NULL,
    apellido VARCHAR(60) NOT NULL,
    dni VARCHAR(8),
    email VARCHAR(60),
    celular VARCHAR(15),
    fechaNacimiento DATE,
    domicilio VARCHAR(255),
    categoriaPersona INT NOT NULL,
    FOREIGN KEY (categoriaPersona) REFERENCES TipoCategoriaPersona(idCategoriaPersona)
);

CREATE TABLE Usuarios (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    correo VARCHAR(100) NOT NULL UNIQUE,
    nombre VARCHAR(50) NOT NULL,
    clave VARCHAR(50) NOT NULL,
    tipoUsuario INT NOT NULL,
    FOREIGN KEY (tipoUsuario) REFERENCES TipoUsuario(idTipoUsuario)
);

-- Tablas de estructuras funerarias
CREATE TABLE Nicho (
    idNicho INT PRIMARY KEY IDENTITY(1,1),
    visibilidad BIT NOT NULL,
    difuntos INT NOT NULL,
    seccion INT NOT NULL,
    tipoNicho INT NOT NULL,
    nroNicho INT NOT NULL,
    nroFila INT NOT NULL,
    FOREIGN KEY (seccion) REFERENCES SeccionesNichos(idSeccionNicho),
    FOREIGN KEY (tipoNicho) REFERENCES TipoNicho(idTipoNicho)
);

CREATE TABLE Fosas (
    idFosa INT PRIMARY KEY IDENTITY(1,1),
    visibilidad BIT NOT NULL,
    difuntos INT NOT NULL,
    seccion INT NOT NULL,
    nroFosa INT NOT NULL,
    FOREIGN KEY (seccion) REFERENCES SeccionesFosas(idSeccionFosa)
);

CREATE TABLE Panteones (
    idPanteon INT PRIMARY KEY IDENTITY(1,1),
    visibilidad BIT NOT NULL,
    difuntos INT NOT NULL,
    descripcion NVARCHAR(100) NULL,
    nroLote INT NOT NULL,
    idSeccionPanteon INT,
    FOREIGN KEY (idSeccionPanteon) REFERENCES SeccionesPanteones(idSeccionPanteones)
);

ALTER TABLE Panteones
ALTER COLUMN idSeccionPanteon INT NOT NULL;

-- Tablas de difuntos y actas
CREATE TABLE ActaDefuncion (
    idActaDefuncion INT PRIMARY KEY IDENTITY(1,1),
    nroActa INT,
    tomo INT,
    folio INT,
    serie VARCHAR(4),
    age INT
);

CREATE TABLE Difunto (
    idDifunto INT PRIMARY KEY IDENTITY(1,1),
    visibilidad BIT NOT NULL,
    nombre VARCHAR(60) NOT NULL,
    apellido VARCHAR(60) NOT NULL,
    dni VARCHAR(8),
    fechaNacimiento DATE,
    fechaDefuncion DATE,
    fechaIngreso DATE,
    actaDefuncion INT,
    estado INT NOT NULL,
    FOREIGN KEY (actaDefuncion) REFERENCES ActaDefuncion(idActaDefuncion),
    FOREIGN KEY (estado) REFERENCES EstadoDifunto(idEstadoDifunto)
);

alter table difunto
drop column fechaIngreso

ALTER TABLE Difunto
ADD descripcion NVARCHAR(MAX) NULL;

-- Tablas de relación difuntos-estructuras
CREATE TABLE NichosDifuntos (
    idNichosDifuntos INT PRIMARY KEY IDENTITY(1,1),
    difuntoId INT NOT NULL,
    nichoId INT NOT NULL,
    visibilidad BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (difuntoId) REFERENCES Difunto(idDifunto),
    FOREIGN KEY (nichoId) REFERENCES Nicho(idNicho)
);

-- Agregar las nuevas columnas
ALTER TABLE NichosDifuntos
ADD fechaIngreso DATETIME NULL,
    empresa VARCHAR(50) NULL,
    usuario INT NULL;

-- Agregar la restricción de clave foránea
ALTER TABLE NichosDifuntos
ADD CONSTRAINT FK_NichosDifuntos_Usuarios
FOREIGN KEY (usuario) REFERENCES Usuarios(idUsuario); 

CREATE TABLE FosasDifuntos (
    idFosasDifuntos INT PRIMARY KEY IDENTITY(1,1),
    difuntoId INT NOT NULL,
    fosaId INT NOT NULL,
    visibilidad BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (difuntoId) REFERENCES Difunto(idDifunto),
    FOREIGN KEY (fosaId) REFERENCES Fosas(idFosa)
);

ALTER TABLE FosasDifuntos
ADD fechaIngreso DATETIME NULL,
    empresa VARCHAR(50) NULL,
    usuario INT NULL;

-- Agregar la restricción de clave foránea
ALTER TABLE FosasDifuntos
ADD CONSTRAINT FK_FosasDifuntos_Usuarios
FOREIGN KEY (usuario) REFERENCES Usuarios(idUsuario); 

CREATE TABLE PanteonDifuntos (
    idPanteonDifuntos INT PRIMARY KEY IDENTITY(1,1),
    difuntoId INT NOT NULL,
    panteonId INT NOT NULL,
    visibilidad BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (difuntoId) REFERENCES Difunto(idDifunto),
    FOREIGN KEY (panteonId) REFERENCES Panteones(idPanteon)
);

ALTER TABLE PanteonDifuntos
ADD fechaIngreso DATETIME NULL,
    empresa VARCHAR(50) NULL,
    usuario INT NULL;

-- Agregar la restricción de clave foránea
ALTER TABLE PanteonDifuntos
ADD CONSTRAINT FK_PanteonDifuntos_Usuarios
FOREIGN KEY (usuario) REFERENCES Usuarios(idUsuario); 

-- Tablas de contratos y concesiones
CREATE TABLE cuotas (
    idCuota INT PRIMARY KEY IDENTITY(1,1), 
    cuota INT
);

CREATE TABLE CantidadAniosConcesion (
    idCantidadAnios INT PRIMARY KEY IDENTITY(1,1), 
    cantidad INT
);

CREATE TABLE ContratoConcesion (
    idContrato INT PRIMARY KEY IDENTITY(1,1), 
    precio DECIMAL(18,2) NOT NULL, 
    creacionContrato DATE NOT NULL, 
    vencimiento DATE NOT NULL, 
    cantidadAniosId INT NOT NULL,
    cantidadCuotas INT NOT NULL,
    nichoDifuntoId INT NULL,
    fosaDifuntoId INT NULL,
    panteonDifuntoId INT NULL,
    descripcion TEXT NULL,
    numeroCuentaId INT,
    FOREIGN KEY (cantidadAniosId) REFERENCES CantidadAniosConcesion(idCantidadAnios),
    FOREIGN KEY (cantidadCuotas) REFERENCES cuotas(idCuota),
    FOREIGN KEY (nichoDifuntoId) REFERENCES NichosDifuntos(idNichosDifuntos),
    FOREIGN KEY (fosaDifuntoId) REFERENCES FosasDifuntos(idFosasDifuntos),
    FOREIGN KEY (panteonDifuntoId) REFERENCES PanteonDifuntos(idPanteonDifuntos),
    FOREIGN KEY (numeroCuentaId) REFERENCES NumeroCuenta(idNumeroCuenta)
);

CREATE TABLE PersonaContrato (
    personaId INT, 
    contratoId INT, 
    PRIMARY KEY (personaId, contratoId),
    FOREIGN KEY (personaId) REFERENCES Personas(idPersona),
    FOREIGN KEY (contratoId) REFERENCES ContratoConcesion(idContrato)
);

create table Tramites(
	idTramite INT PRIMARY KEY IDENTITY(1,1),
	idNichosDifuntosFK int,
	idFosasDifuntosFK int,
	idPanteonesDifuntos int,
	foreign key (idNichosDifuntosFK) references NichosDifuntos(idNichosDifuntos),
	foreign key (idFosasDifuntosFK) references FosasDifuntos(idFosasDifuntos),
	foreign key (idPanteonesDifuntos) references PanteonDifuntos(idPanteonDifuntos),
);

-- Configuración FILESTREAM para archivos
EXEC sp_configure filestream_access_level, 2;
RECONFIGURE;

CREATE TABLE ContratoArchivo (
    contratoId INT PRIMARY KEY,
    nombreArchivo NVARCHAR(255),
    contenido VARBINARY(MAX) FILESTREAM NULL,
    rowGuid UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL UNIQUE,
    extension CHAR(4),
    FOREIGN KEY (contratoId) REFERENCES ContratoConcesion(idContrato)
);

-- Datos iniciales
INSERT INTO TipoUsuario(tipoUsuario) VALUES ('Empleado'), ('Encargado');
INSERT INTO TipoNicho(tipoNicho, porDefecto) VALUES ('Féretro', 0), ('Urnario', 0), ('Especial', 0);
INSERT INTO TipoCategoriaPersona(tipo) VALUES ('Titular'), ('Familiar');
INSERT INTO TipoNumeracionParcela(tipoNumeracion) VALUES ('Nueva (nichos repetidos)'), ('Antigua (sin repetir)');
INSERT INTO EstadoDifunto(estado) VALUES ('Cuerpo completo'), ('Reducido'), ('Cremado');
INSERT INTO cuotas(cuota) VALUES (1), (2), (3), (4), (5), (6);
INSERT INTO CantidadAniosConcesion(cantidad) VALUES (1), (5), (10), (15), (25);
insert into Usuarios(correo, nombre, clave, tipoUsuario) values('tomaselle2@gmail.com', 'Tomas Carreras', '12345', 2);

GO



select * from TipoUsuario
