CREATE TABLE IF NOT EXISTS Productos (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(100),
    Cantidad INT,
    Precio NUMERIC,
    Ubicacion VARCHAR(100)
);
CREATE TABLE IF NOT EXISTS Clientes (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(100),
    Email VARCHAR(100)
);
CREATE TABLE IF NOT EXISTS Pedidos (
    Id SERIAL PRIMARY KEY,
    ClienteId INT REFERENCES Clientes(Id),
    Fecha TIMESTAMP
);
CREATE TABLE IF NOT EXISTS PedidoProductos (
    PedidoId INT REFERENCES Pedidos(Id),
    ProductoId INT REFERENCES Productos(Id),
    Cantidad INT,
    PRIMARY KEY (PedidoId, ProductoId)
);
