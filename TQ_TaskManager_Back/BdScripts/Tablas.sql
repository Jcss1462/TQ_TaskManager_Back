--CREATE DATABASE tq_taskmanagement;

--Drop Table Tareas;
--Drop Table Estados;
--Drop Table Usuarios;
--Drop Table Roles;

CREATE TABLE Roles (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE
);


CREATE TABLE Usuarios (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) NOT NULL UNIQUE,
    Contrasena TEXT NOT NULL, -- Guardamos el hash de la contraseña
    RolId INT NOT NULL REFERENCES Roles(Id)
);


CREATE TABLE Estados (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE
);


CREATE TABLE Tareas (
    Id SERIAL PRIMARY KEY,
    Titulo VARCHAR(200) NOT NULL,
    Descripcion TEXT,
    EstadoId INT NOT NULL REFERENCES Estados(Id),
    UsuarioAsignadoId INT REFERENCES Usuarios(Id) ON DELETE SET NULL,
    FechaCreacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaActualizacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
