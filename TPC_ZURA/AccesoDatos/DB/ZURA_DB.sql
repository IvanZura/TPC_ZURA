use master
go
DROP DATABASE ZURA_DB
go
CREATE DATABASE ZURA_DB
go
use ZURA_DB
go

--CREATE TABLE TiposUsuarios (
--	ID	int primary key identity(1, 1) not null,
--	Nombre	varchar(50) not null
--)

--GO

CREATE TABLE Personas (
	ID	int primary key identity(1, 1) not null,
	Nombre	varchar(50) not null,
	Apellido varchar(50) not null,
	FNacimiento	date not null,
	Email	varchar(50) null,
	Telefono int not null check(Telefono > 0),
	DNI	varchar(8) not null,
	FechaAlta	date not null default(getdate())
)

GO

CREATE TABLE Puestos (
	ID	int primary key not null identity(1, 1),
	nombre	varchar(30) not null
)

GO

CREATE TABLE Clientes (
	ID	int	not null identity(1, 1) primary key,
	IDPersona	int not null foreign key references Personas(ID),
	FechaAlta	date not null default(getdate()),
	Activo bit not null default(1)
)

GO

CREATE TABLE Empleados (
	ID	int	not null identity(1, 1) primary key,
	IDPersona	int not null foreign key references Personas(ID),
	IDPuesto	int foreign key references Puestos(ID),
	FechaAlta	date not null default(getdate()),
	Activo bit not null default(1)
)

GO

CREATE TABLE Usuarios (
	ID int primary key identity(1, 1) not null,
	IDPersona	int not null foreign key references Personas(ID),
	Usuario	varchar(50) not null,
	Pass	varchar(50) not null,
	FechaAlta	date not null default(getdate()),
	Activo	bit	not null
)

GO

CREATE TABLE TiposIncidencias (
	ID	int primary key identity(1, 1) not null,
	Nombre	varchar(50) not null,
	Activo bit not null default(1)
)

go

CREATE TABLE Prioridades (
	ID	int primary key not null identity(1, 1),
	Nombre	varchar(50)
)

go

CREATE TABLE Estados (
	ID	int primary key not null identity(1, 1),
	Nombre	varchar(50) not null
)

go

CREATE TABLE Reclamos (
	ID	int primary key identity(1, 1) not null,
	IDCliente	int foreign key references Clientes(ID) not null,
	IDIncidencia	int foreign key references TiposIncidencias(ID) not null,
	IDPrioridad	int foreign key references Prioridades(ID) not null,
	Titulo	text not null,
	Problematica	text not null,
	IDCreador	int foreign key references Usuarios(ID) not null,
	IDEstado	int foreign key references Estados(ID) not null,
	Solucion	text null,
	IDReabrio	int not null default(0),
	IDAsignado	int not null default(0),
	IDCerro		int not null default(0),
	FechaHora	datetime not null default(GETDATE()),
	FechaHoraCerrado datetime not null default('1901-01-01'),
	FechaHoraReAbierto datetime not null default('1901-01-01'),
)

go


INSERT INTO Personas (Nombre, Apellido, FNacimiento, Email, Telefono, DNI)
VALUES ('Ivan', 'Zura', '1995/10/20', 'no@no.com', 1125263598, '39148492'), ('Maxi', 'Sar', '01/01/1901', 'prog3@JA.com', 1188888888, '35123123')

go

INSERT INTO Puestos (Nombre)
values ('Administrador'),('Telefonista'),('Supervisor')

go

INSERT INTO Clientes (IDPersona)
VALUES (2)

go

INSERT INTO Empleados (IDPersona, IDPuesto)
VALUES (1, 2)

go

INSERT INTO Usuarios (Usuario, Pass, Activo, IDPersona)
VALUES ('izura', '123', 1, 1), ('msar', '123', 1, 2)

go

INSERT INTO Prioridades (Nombre)
VALUES ('Urgente'), ('Alta'), ('Normal'), ('Baja')

go

INSERT INTO TiposIncidencias (Nombre)
VALUES ('Administrativa'), ('Soporte'), ('Pagos')

go

INSERT INTO Estados (Nombre)
VALUES ('Abierto'), ('En Analisis'), ('Cerrado'), ('Reabierto'), ('Asignado'), ('Resuelto')

go

INSERT INTO Reclamos (IDCliente, IDIncidencia, IDPrioridad, Titulo, Problematica, IDCreador, IDEstado, IDAsignado)
VALUES (1, 2, 1, 'Aumento desmedido', 'El usuario comenta que el internet se elevo un 500% en su ultima factura.', 1, 1, 1)