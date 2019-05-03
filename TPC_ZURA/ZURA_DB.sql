use master
go
CREATE DATABASE ZURA_DB
go
use ZURA_DB
go

CREATE TABLE TiposUsuarios (
	ID	int primary key identity(1, 1) not null,
	Nombre	varchar(50) not null
)

GO

CREATE TABLE Personas (
	ID	int primary key identity(1, 1) not null,
	Nombre	varchar(50) not null,
	Apellido varchar(50) not null,
	FNacimiento	date not null,
	Email	varchar(50) not null,
	Telefono int not null check(Telefono > 0)
)

GO


CREATE TABLE Usuarios (
	ID int primary key identity(1, 1) not null,
	IDPersona	int foreign key references Personas(ID) not null,
	Usuario	varchar(50) not null,
	Pass	varchar(50) not null,
	TipoUsuario	int not null foreign key references TiposUsuarios(ID)
)

go

CREATE TABLE Clientes (
	ID	int not null identity(1, 1) primary key,
	IDUsuario int not null foreign key references Usuarios(ID)
)

go

CREATE TABLE Empleados (
	ID	int primary key identity(1, 1) not null,
	IDUsuario int not null foreign key references Usuarios(ID)
)

go

CREATE TABLE TiposIncidencias (
	ID	int primary key identity(1, 1) not null,
	Nombre	varchar(50) not null
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
	Problematica	text not null,
	IDCreador	int foreign key references Usuarios(ID) not null,
	IDEstado	int foreign key references Estados(ID) not null
)

go

INSERT INTO TiposUsuarios (Nombre)
VALUES ('Administrador'), ('Telefonista'), ('Supervisor'), ('Cliente')

go

INSERT INTO Personas (Nombre, Apellido, FNacimiento, Email, Telefono)
VALUES ('Ivan', 'Zura', '1995/10/20', 'no@no.com', 1125263598), ('Maxi', 'Sar', '01/01/1901', 'prog3@JA.com', 1188888888)

go

INSERT INTO Usuarios (IDPersona, Usuario, Pass, TipoUsuario)
VALUES (1, 'izura', '123', 2), (2, 'msar', '123', 4)

go

INSERT INTO Clientes (IDUsuario)
VALUES (2)

go

INSERT INTO Empleados (IDUsuario)
VALUES (1)

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

INSERT INTO Reclamos (IDCliente, IDIncidencia, IDPrioridad, Problematica, IDCreador, IDEstado)
VALUES (1, 2, 1, 'El usuario comenta que el internet se elevo un 500% en su ultima factura.', 1, 1)