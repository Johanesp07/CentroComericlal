CREATE DATABASE Mall
go
Use Mall

CREATE TABLE Locales (
    ID_Local INT PRIMARY KEY IDENTITY(1,1),
    Numero_Local INT NOT NULL UNIQUE,
    Tamaño DECIMAL(10,2) NOT NULL,
    Ubicacion VARCHAR(100) NOT NULL,
    Estado VARCHAR(50) NOT NULL
);

-- 2. Creación de la tabla Personal
CREATE TABLE Personal (
    ID_Personal INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Cargo VARCHAR(50) NOT NULL
);

-- 3. Creación de la tabla Usuarios
CREATE TABLE Usuarios (
    ID_Usuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Usuario VARCHAR(100) NOT NULL UNIQUE,
    Contrasena VARCHAR(100) NOT NULL,
    Rol VARCHAR(50) NOT NULL,  -- Rol puede ser 'administrador', 'inquilino', etc.
    ID_Personal INT NULL,      -- Relación opcional con Personal (si el usuario es empleado)
    FOREIGN KEY (ID_Personal) REFERENCES Personal(ID_Personal)
);

-- 4. Creación de la tabla Alquileres
CREATE TABLE Alquileres (
    ID_Alquiler INT PRIMARY KEY IDENTITY(1,1),
    ID_Local INT NOT NULL,
    ID_Usuario INT NOT NULL,  -- Relación con el usuario que alquila
    Fecha_Inicio DATE NOT NULL,
    Fecha_Fin DATE NOT NULL,
    Monto DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (ID_Local) REFERENCES Locales(ID_Local),
    FOREIGN KEY (ID_Usuario) REFERENCES Usuarios(ID_Usuario)
);

-- 5. Creación de la tabla Pagos
CREATE TABLE Pagos (
    ID_Pago INT PRIMARY KEY IDENTITY(1,1),
    ID_Alquiler INT NOT NULL,
    ID_Usuario INT NOT NULL,  -- Usuario que realiza el pago
    Fecha_Pago DATE NOT NULL,
    Monto_Pagado DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (ID_Alquiler) REFERENCES Alquileres(ID_Alquiler),
    FOREIGN KEY (ID_Usuario) REFERENCES Usuarios(ID_Usuario)
);

CREATE PROCEDURE ValidarLogin
    @Nombre_Usuario VARCHAR(100),
    @Contrasena VARCHAR(256)
AS
BEGIN
    SELECT 
        u.ID_Usuario,
        u.Nombre_Usuario,
        u.Contrasena,
        u.Rol
    FROM 
        Usuarios u
    WHERE 
        u.Nombre_Usuario = @Nombre_Usuario
        AND u.Contrasena = @Contrasena;
END

INSERT INTO Personal (Nombre, Apellido, Cargo)
VALUES ('Luis', 'Chaves', 'TI');
INSERT INTO Personal (Nombre, Apellido, Cargo)
VALUES ('Jarol', 'Chaverri', 'TI');

INSERT INTO Usuarios (Nombre_Usuario, Contrasena, Rol, ID_Personal)
VALUES ('lacm', '1234', 'admin', 1);

select * from Usuarios

CREATE PROCEDURE [dbo].[GestionarUsuarios]
    @accion NVARCHAR(10),
    @ID_Usuario INT = NULL,
    @Nombre_Usuario VARCHAR(100) = NULL,
    @Contrasena VARCHAR(100) = NULL,
    @Rol VARCHAR(50) = NULL,
    @ID_Personal INT = NULL
AS
BEGIN
    IF @accion = 'agregar'
    BEGIN
        INSERT INTO [dbo].[Usuarios]
           ([Nombre_Usuario]
           ,[Contrasena]
           ,[Rol]
           ,[ID_Personal])
        VALUES
           (@Nombre_Usuario, @Contrasena, @Rol, @ID_Personal);
    END
    ELSE IF @accion = 'borrar'
    BEGIN
        DELETE FROM [dbo].[Usuarios] WHERE [ID_Usuario] = @ID_Usuario;
    END
    ELSE IF @accion = 'modificar'
    BEGIN
        UPDATE [dbo].[Usuarios] SET 
            Nombre_Usuario = @Nombre_Usuario,
            Contrasena = @Contrasena,
            Rol = @Rol,
            ID_Personal = @ID_Personal
        WHERE ID_Usuario = @ID_Usuario;
    END
    ELSE IF @accion = 'consultar'
    BEGIN
        SELECT [ID_Usuario], [Nombre_Usuario], [Contrasena], [Rol], [ID_Personal]
        FROM [dbo].[Usuarios]
        WHERE ID_Usuario = @ID_Usuario;
    END
    ELSE
    BEGIN
        SELECT 'Acción no válida';
    END
END;

CREATE PROCEDURE [dbo].[GestionarAlquileres]
    @accion NVARCHAR(10),
    @ID_Alquiler INT = NULL,
    @ID_Local INT = NULL,
    @ID_Usuario INT = NULL,
    @Fecha_Inicio DATE = NULL,
    @Fecha_Fin DATE = NULL,
    @Monto DECIMAL(10,2) = NULL
AS
BEGIN
    IF @accion = 'agregar'
    BEGIN
        INSERT INTO [dbo].[Alquileres]
           ([ID_Local]
           ,[ID_Usuario]
           ,[Fecha_Inicio]
           ,[Fecha_Fin]
           ,[Monto])
        VALUES
           (@ID_Local, @ID_Usuario, @Fecha_Inicio, @Fecha_Fin, @Monto);
    END
    ELSE IF @accion = 'borrar'
    BEGIN
        DELETE FROM [dbo].[Alquileres] WHERE [ID_Alquiler] = @ID_Alquiler;
    END
    ELSE IF @accion = 'modificar'
    BEGIN
        UPDATE [dbo].[Alquileres] SET 
            ID_Local = @ID_Local,
            ID_Usuario = @ID_Usuario,
            Fecha_Inicio = @Fecha_Inicio,
            Fecha_Fin = @Fecha_Fin,
            Monto = @Monto
        WHERE ID_Alquiler = @ID_Alquiler;
    END
    ELSE IF @accion = 'consultar'
    BEGIN
        SELECT [ID_Alquiler], [ID_Local], [ID_Usuario], [Fecha_Inicio], [Fecha_Fin], [Monto]
        FROM [dbo].[Alquileres]
        WHERE ID_Alquiler = @ID_Alquiler;
    END
    ELSE
    BEGIN
        SELECT 'Acción no válida';
    END
END;

INSERT INTO Locales (Numero_Local, Tamaño, Ubicacion, Estado)
VALUES (101, 75.50, 'Primer Piso, Ala Norte', 'Ocupado');

INSERT INTO Locales (Numero_Local, Tamaño, Ubicacion, Estado)
VALUES (102, 120.00, 'Segundo Piso, Ala Sur', 'Disponible');

INSERT INTO Locales (Numero_Local, Tamaño, Ubicacion, Estado)
VALUES (103, 60.75, 'Tercer Piso, Ala Este', 'En Renovación');
