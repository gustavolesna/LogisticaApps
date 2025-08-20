using Dapper;
using LogisticaApp.Entities;

using System.Data;


namespace LogisticaApp.Data
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbConnection _db;

        public ProductoRepository(IDbConnection db)
        {
            _db = db;
        }

        public Task<IEnumerable<Producto>> GetAll() =>
            _db.QueryAsync<Producto>("SELECT * FROM Productos;");

        public Task<Producto> GetById(int id) =>
            _db.QueryFirstOrDefaultAsync<Producto>("SELECT * FROM Productos WHERE Id=@Id;", new { Id = id });

        public async Task Add(Producto producto)
        {
            var sql = @"
        INSERT INTO Productos (Nombre, Cantidad, Precio, Ubicacion)
        VALUES (@Nombre, @Cantidad, @Precio, @Ubicacion)
        RETURNING Id;";  // devuelve el Id generado

            producto.Id = await _db.ExecuteScalarAsync<int>(sql, producto);
        }

        public Task Update(Producto producto) =>
            _db.ExecuteAsync("UPDATE Productos SET Nombre=@Nombre,Cantidad=@Cantidad,Precio=@Precio,Ubicacion=@Ubicacion WHERE Id=@Id;", producto);

        public Task Delete(int id) =>
            _db.ExecuteAsync("DELETE FROM Productos WHERE Id=@Id;", new { Id = id });
    }
}
