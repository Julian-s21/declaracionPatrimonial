
CREATE DATABASE IF NOT EXISTS Declaraciones;
USE Declaraciones;


CREATE TABLE Banco (
    IDBanco INT AUTO_INCREMENT PRIMARY KEY,
    Nombre_Banco VARCHAR(100) NOT NULL
);


CREATE TABLE tipoCuenta (
    IDtipoCuenta INT AUTO_INCREMENT PRIMARY KEY,
    Nombre_tipoCuenta VARCHAR(100) NOT NULL
);


CREATE TABLE Usuarios (
    IDUsuario INT AUTO_INCREMENT PRIMARY KEY,
    Nombre_Usuario VARCHAR(100) NOT NULL,
    Apellido_Usuario VARCHAR(100) NOT NULL,
    Correo_Usuario VARCHAR(150) UNIQUE NOT NULL,
    Contrasena_Usuario VARCHAR(200) NOT NULL,
    Rol_Usuario VARCHAR(50) NOT NULL
);


CREATE TABLE cuentaBancaria (
    IDCuentaBancaria INT AUTO_INCREMENT PRIMARY KEY,
    Numero_cuentaBancaria VARCHAR(50) NOT NULL,
    Saldo_cuentaBancaria DECIMAL(15,2) DEFAULT 0.00,
    IDUsuario INT NOT NULL,
    IDBanco INT NOT NULL,
    IDtipoCuenta INT NOT NULL,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDBanco) REFERENCES Banco(IDBanco),
    FOREIGN KEY (IDtipoCuenta) REFERENCES tipoCuenta(IDtipoCuenta)
);


CREATE TABLE creditosBancarios (
    IDCreditoBancario INT AUTO_INCREMENT PRIMARY KEY,
    Cantidad_AprobadaCredito DECIMAL(15,2) NOT NULL,
    Motivo_creditoBancario VARCHAR(255),
    IDUsuario INT NOT NULL,
    IDBanco INT NOT NULL,
    IDtipoCuenta INT NOT NULL,
    IDCuentaBancaria INT NOT NULL,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDBanco) REFERENCES Banco(IDBanco),
    FOREIGN KEY (IDtipoCuenta) REFERENCES tipoCuenta(IDtipoCuenta),
    FOREIGN KEY (IDCuentaBancaria) REFERENCES cuentaBancaria(IDCuentaBancaria)
);


CREATE TABLE otrosPasivos (
    IDPasivo INT AUTO_INCREMENT PRIMARY KEY,
    Cantidad_aprobadaPasivo DECIMAL(15,2) NOT NULL,
    Motivo_Pasivo VARCHAR(255),
    IDUsuario INT NOT NULL,
    IDCuentaBancaria INT NOT NULL,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDCuentaBancaria) REFERENCES cuentaBancaria(IDCuentaBancaria)
);


CREATE TABLE tipoInmueble (
    IDtipoInmueble INT AUTO_INCREMENT PRIMARY KEY,
    Nombre_tipoInmueble VARCHAR(100) NOT NULL
);


CREATE TABLE tipoPropiedad (
    IDtipoPropiedad INT AUTO_INCREMENT PRIMARY KEY,
    Nombre_tipoPropiedad VARCHAR(100) NOT NULL
);


CREATE TABLE Bienes (
    IDBienes INT AUTO_INCREMENT PRIMARY KEY,
    Nombre_Bienes VARCHAR(100) NOT NULL,
    Fecha_adquisicionBien DATE NOT NULL,
    Descripcion_Bien TEXT,
    Forma_adquisicionBien VARCHAR(100),
    Precio_Bien DECIMAL(15,2),
    IDtipoInmueble INT,
    IDtipoPropiedad INT,
    FOREIGN KEY (IDtipoInmueble) REFERENCES tipoInmueble(IDtipoInmueble),
    FOREIGN KEY (IDtipoPropiedad) REFERENCES tipoPropiedad(IDtipoPropiedad)
);
