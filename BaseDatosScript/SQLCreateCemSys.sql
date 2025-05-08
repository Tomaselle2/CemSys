create database CemSys;
go
use CemSys;
go
CREATE TABLE TipoCategoriaPersona (
    idCategoriaPersona INT PRIMARY KEY IDENTITY(1,1),
    tipo VARCHAR(25) NOT NULL
);
go
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
go
CREATE TABLE SeccionesFosas (
    idSeccionFosa INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL unique,
    fosas INT NOT NULL,
    visibilidad BIT NOT NULL
);
go
CREATE TABLE TipoNumeracionParcela (
    idTipoNumeracionParcela INT PRIMARY KEY IDENTITY(1,1),
    tipoNumeracion VARCHAR(25) NOT NULL
);
go
CREATE TABLE SeccionesNichos (
    idSeccionNicho INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL unique,
    filas INT NOT NULL,
    nichos INT NOT NULL,
    visibilidad BIT NOT NULL,
    tipoNumeracion INT NOT NULL,
    FOREIGN KEY (tipoNumeracion) REFERENCES TipoNumeracionParcela(idTipoNumeracionParcela)
);
go
CREATE TABLE TipoNicho (
    idTipoNicho INT PRIMARY KEY IDENTITY(1,1),
    tipoNicho VARCHAR(15) NOT NULL
);
go
CREATE TABLE NumeroCuenta (
    idNumeroCuenta INT PRIMARY KEY,
    numero INT UNIQUE NOT NULL  -- Esto garantiza que el número no se repite
);
go
CREATE TABLE Nicho (
    idNicho INT PRIMARY KEY IDENTITY(1,1),
    visibilidad BIT NOT NULL,
    difuntos INT NOT NULL,
    seccion INT NOT NULL,
    nroCuenta INT,
    tipoNicho INT NOT NULL,
	nroNicho INT NOT NULL,
	nroFila INT NOT NULL,
    FOREIGN KEY (seccion) REFERENCES SeccionesNichos(idSeccionNicho),
    FOREIGN KEY (nroCuenta) REFERENCES NumeroCuenta(idNumeroCuenta),
    FOREIGN KEY (tipoNicho) REFERENCES TipoNicho(idTipoNicho)
);
go
CREATE TABLE Fosas (
    idFosa INT PRIMARY KEY IDENTITY(1,1),
    visibilidad BIT NOT NULL,
    difuntos INT NOT NULL,
    seccion INT NOT NULL,
	nroFosa INT not null,
    nroCuenta INT,
    FOREIGN KEY (seccion) REFERENCES SeccionesFosas(idSeccionFosa),
    FOREIGN KEY (nroCuenta) REFERENCES NumeroCuenta(idNumeroCuenta)
);
go
CREATE TABLE Panteones (
    idPanteon INT PRIMARY KEY IDENTITY(1,1),
    visibilidad BIT NOT NULL,
    difuntos INT NOT NULL,
    nroCuenta INT,
    FOREIGN KEY (nroCuenta) REFERENCES NumeroCuenta(idNumeroCuenta)
);
go
--CREATE TABLE PersonasNichos (
--    idPersonaNicho INT PRIMARY KEY IDENTITY(1,1),
--    personalId INT NOT NULL,
--    nichoId INT NOT NULL,
--    FOREIGN KEY (personalId) REFERENCES Personas(idPersona),
--    FOREIGN KEY (nichoId) REFERENCES Nicho(idNicho)
--);
--go
--CREATE TABLE PersonasFosas (
--    idPersonaFosas INT PRIMARY KEY IDENTITY(1,1),
--    personalId INT NOT NULL,
--    fosaId INT NOT NULL,
--    FOREIGN KEY (personalId) REFERENCES Personas(idPersona),
--    FOREIGN KEY (fosaId) REFERENCES Fosas(idFosa)
--);
--go
--CREATE TABLE PersonasPanteones (
--    idPersonaFosas INT PRIMARY KEY IDENTITY(1,1),
--    personalId INT NOT NULL,
--    panteonSeId INT NOT NULL,
--    FOREIGN KEY (personalId) REFERENCES Personas(idPersona),
--    FOREIGN KEY (panteonSeId) REFERENCES Panteones(idPanteon)
--);
CREATE TABLE EstadoDifunto (
    idEstadoDifunto INT PRIMARY KEY IDENTITY(1,1),
    estado VARCHAR(20) NOT NULL
);
go
CREATE TABLE ActaDefuncion (
    idActaDefuncion INT PRIMARY KEY IDENTITY(1,1),
    nroActa INT,
    tomo INT,
    folio INT,
    serie VARCHAR(4),
    age INT
);
go
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
go
CREATE TABLE NichosDifuntos (
    idNichosDifuntos INT PRIMARY KEY IDENTITY(1,1),
    difuntoId INT NOT NULL,
    nichoId INT NOT NULL,
    FOREIGN KEY (difuntoId) REFERENCES Difunto(idDifunto),
    FOREIGN KEY (nichoId) REFERENCES Nicho(idNicho)
);
go
CREATE TABLE FosasDifuntos (
    idFosasDifuntos INT PRIMARY KEY IDENTITY(1,1),
    difuntoId INT NOT NULL,
    fosaId INT NOT NULL,
    FOREIGN KEY (difuntoId) REFERENCES Difunto(idDifunto),
    FOREIGN KEY (fosaId) REFERENCES Fosas(idFosa)
);
go
CREATE TABLE PanteonDifuntos (
    idPanteonDifuntos INT PRIMARY KEY IDENTITY(1,1),
    difuntoId INT NOT NULL,
    panteonId INT NOT NULL,
    FOREIGN KEY (difuntoId) REFERENCES Difunto(idDifunto),
    FOREIGN KEY (panteonId) REFERENCES Panteones(idPanteon)
);
go
CREATE TABLE TipoUsuario (
    idTipoUsuario INT PRIMARY KEY IDENTITY(1,1),
    tipoUsuario VARCHAR(20) NOT NULL
);
go
CREATE TABLE Usuarios (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    correo VARCHAR(100) NOT NULL UNIQUE,  -- Restricción única integrada
	nombre VARCHAR(50) NOT NULL,
    clave VARCHAR(50) NOT NULL,
    tipoUsuario INT NOT NULL,
    FOREIGN KEY (tipoUsuario) REFERENCES TipoUsuario(idTipoUsuario)
);
go
ALTER TABLE TipoNicho ADD porDefecto BIT NOT NULL DEFAULT 0;
go
insert into TipoUsuario(tipoUsuario) values ('Empleado'), ('Encargado');
go
insert into TipoNicho(tipoNicho, porDefecto) values ('Féretro', 0), ('Urnario', 0), ('Especial', 0);
go
insert into TipoCategoriaPersona(tipo) values ('Titular'), ('Familiar');
go
insert into TipoNumeracionParcela(tipoNumeracion) values ('Nueva (nichos repetidos)'), ('Antigua (sin repetir)');
go
insert into EstadoDifunto(estado) values ('Cuerpo completo'), ('Reducido'), ('Cremado');
go
--select * from EstadoDifunto;
--insert into Usuarios(correo, nombre, clave, tipoUsuario) values('tomaselle2@gmail.com', 'Tomas Carreras', '12345', 2);

--a tener en cuenta, son los id hardcodeados, revisar en controllers Nicho, y en vista _layout 
--select * from TipoNumeracionParcela
--select * from Usuarios
--DROP TABLE PersonasNichos;
--go
--DROP TABLE PersonasFosas;
--go
--DROP TABLE PersonasPanteones;
go
EXEC sp_configure filestream_access_level, 2--para el fileStream
go
RECONFIGURE
go
create table cuotas (idCuota INT PRIMARY KEY IDENTITY(1,1), cuota int);
go
insert into cuotas(cuota) values (1), (2), (3), (4), (5), (6);
go
create table CantidadAniosConcesion (idCantidadAnios INT PRIMARY KEY IDENTITY(1,1), cantidad int);
go
insert into CantidadAniosConcesion (cantidad) values (1), (5), (10), (15), (25);
go
create table ContratoConcesion(idContrato INT PRIMARY KEY IDENTITY(1,1), precio decimal(18,2) not null, creacionContrato date not null, vencimiento date not null, cantidadAniosId int not null,
cantidadCuotas int not null, nichoId int, fosaId int, panteonId int,
foreign key (cantidadAniosId) references CantidadAniosConcesion(idCantidadAnios),
foreign key (cantidadCuotas) references cuotas(idCuota),
foreign key (nichoId) references Nicho(idNicho),
foreign key (fosaId) references Fosas(idFosa),
foreign key (panteonId) references Panteones(idPanteon));
go
CREATE TABLE PersonaContrato (personaId int, contratoId int, 
	Primary key (personaId, contratoId),
	foreign key (personaId) references Personas(idPersona),
	foreign key (contratoId) references ContratoConcesion(idContrato));
go
CREATE TABLE ContratoArchivo (
    contratoId INT PRIMARY KEY,
    nombreArchivo NVARCHAR(255),
    contenido VARBINARY(MAX) FILESTREAM NULL,
    rowGuid UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL UNIQUE,
	extension char(4),
    FOREIGN KEY (ContratoId) REFERENCES ContratoConcesion(idContrato)
);
go
--modificaciones del 06/05/2025
-- Eliminar claves foráneas
ALTER TABLE Fosas DROP CONSTRAINT FK__Fosas__nroCuenta__4F7CD00D;
go
ALTER TABLE Panteones DROP CONSTRAINT FK__Panteones__nroCu__52593CB8;
go
ALTER TABLE Nicho DROP CONSTRAINT FK__Nicho__nroCuenta__4AB81AF0;
go
-- Eliminar columnas
ALTER TABLE Fosas DROP COLUMN nroCuenta;
go
ALTER TABLE Panteones DROP COLUMN nroCuenta;
go
ALTER TABLE Nicho DROP COLUMN nroCuenta;
go
ALTER TABLE NichosDifuntos
ADD visibilidad BIT NOT NULL DEFAULT 1;
go
ALTER TABLE FosasDifuntos
ADD visibilidad BIT NOT NULL DEFAULT 1;
go
ALTER TABLE PanteonDifuntos
ADD visibilidad BIT NOT NULL DEFAULT 1;
go
-- Eliminar claves foráneas 
ALTER TABLE ContratoConcesion DROP CONSTRAINT FK__ContratoC__nicho__08B54D69;
go
ALTER TABLE ContratoConcesion DROP CONSTRAINT FK__ContratoC__fosaI__09A971A2;
go
ALTER TABLE ContratoConcesion DROP CONSTRAINT FK__ContratoC__pante__0A9D95DB;
go
-- Eliminar columnas
ALTER TABLE ContratoConcesion DROP COLUMN nichoId;
go
ALTER TABLE ContratoConcesion DROP COLUMN fosaId;
go
ALTER TABLE ContratoConcesion DROP COLUMN panteonId;
go
ALTER TABLE ContratoConcesion ADD 
    nichoDifuntoId INT NULL,
    fosaDifuntoId INT NULL,
    panteonDifuntoId INT NULL;
go
ALTER TABLE ContratoConcesion
ADD CONSTRAINT FK_ContratoConcesion_NichoDifunto
FOREIGN KEY (nichoDifuntoId) REFERENCES NichosDifuntos(idNichosDifuntos);
go
ALTER TABLE ContratoConcesion
ADD CONSTRAINT FK_ContratoConcesion_FosaDifunto
FOREIGN KEY (fosaDifuntoId) REFERENCES FosasDifuntos(idFosasDifuntos);
go
ALTER TABLE ContratoConcesion
ADD CONSTRAINT FK_ContratoConcesion_PanteonDifunto
FOREIGN KEY (panteonDifuntoId) REFERENCES PanteonDifuntos(idPanteonDifuntos);
go
ALTER TABLE ContratoConcesion
ADD descripcion text NULL;
go
ALTER TABLE ContratoConcesion
ADD numeroCuentaId INT;
go
ALTER TABLE ContratoConcesion
ADD CONSTRAINT FK_ContratoConcesion_NumeroCuenta
FOREIGN KEY (numeroCuentaId) REFERENCES NumeroCuenta(idNumeroCuenta);
go
CREATE TABLE SeccionesPanteones (
    idSeccionPanteones INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    panteones INT not null,
    visibilidad BIT not null
);
go
-- Agregar las nuevas columnas
ALTER TABLE Panteones
ADD descripcion NVARCHAR(100) NULL,
    nroLote INT NOT NULL,
    idSeccionPanteon INT;
go
-- Agregar la restricción de clave foránea a SeccionesPanteones
ALTER TABLE Panteones
ADD CONSTRAINT FK_Panteones_SeccionesPanteones
FOREIGN KEY (idSeccionPanteon) REFERENCES SeccionesPanteones(idSeccionPanteones);
--fin modificaciones del 06/05/2025