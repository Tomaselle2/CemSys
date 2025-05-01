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
CREATE TABLE PersonasNichos (
    idPersonaNicho INT PRIMARY KEY IDENTITY(1,1),
    personalId INT NOT NULL,
    nichoId INT NOT NULL,
    FOREIGN KEY (personalId) REFERENCES Personas(idPersona),
    FOREIGN KEY (nichoId) REFERENCES Nicho(idNicho)
);
go
CREATE TABLE PersonasFosas (
    idPersonaFosas INT PRIMARY KEY IDENTITY(1,1),
    personalId INT NOT NULL,
    fosaId INT NOT NULL,
    FOREIGN KEY (personalId) REFERENCES Personas(idPersona),
    FOREIGN KEY (fosaId) REFERENCES Fosas(idFosa)
);
go
CREATE TABLE PersonasPanteones (
    idPersonaFosas INT PRIMARY KEY IDENTITY(1,1),
    personalId INT NOT NULL,
    panteonSeId INT NOT NULL,
    FOREIGN KEY (personalId) REFERENCES Personas(idPersona),
    FOREIGN KEY (panteonSeId) REFERENCES Panteones(idPanteon)
);
go
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

ALTER TABLE TipoNicho ADD porDefecto BIT NOT NULL DEFAULT 0;
select * from TipoNicho