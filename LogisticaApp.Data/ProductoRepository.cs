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

        public Task Add(Producto producto) =>
            _db.ExecuteAsync("INSERT INTO Productos (Nombre,Cantidad,Precio,Ubicacion) VALUES (@Nombre,@Cantidad,@Precio,@Ubicacion);", producto);

        public Task Update(Producto producto) =>
            _db.ExecuteAsync("UPDATE Productos SET Nombre=@Nombre,Cantidad=@Cantidad,Precio=@Precio,Ubicacion=@Ubicacion WHERE Id=@Id;", producto);

        public Task Delete(int id) =>
            _db.ExecuteAsync("DELETE FROM Productos WHERE Id=@Id;", new { Id = id });
    }
}
