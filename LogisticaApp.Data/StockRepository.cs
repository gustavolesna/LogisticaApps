using Dapper;
using LogisticaApp.Entities;
using System.Data;

namespace LogisticaApp.Data
{
    public interface IRepositorioStock
    {
        Task<IEnumerable<Stock>> ObtenerTodos();
        Task<Stock> ObtenerPorProductoYSucursal(int productoId, int sucursalId);
        Task Agregar(Stock stock);
        Task Actualizar(Stock stock);
    }

    public class RepositorioStock : IRepositorioStock
    {
        private readonly IDbConnection _db;
        public RepositorioStock(IDbConnection db) { _db = db; }

        public Task<IEnumerable<Stock>> ObtenerTodos() =>
            _db.QueryAsync<Stock>("SELECT * FROM Stock;");

        public Task<Stock> ObtenerPorProductoYSucursal(int productoId, int sucursalId) =>
            _db.QueryFirstOrDefaultAsync<Stock>(
                "SELECT * FROM Stock WHERE ProductoId = @ProductoId AND SucursalId = @SucursalId;",
                new { ProductoId = productoId, SucursalId = sucursalId });

        public Task Agregar(Stock stock) =>
            _db.ExecuteAsync(
                "INSERT INTO Stock (ProductoId, SucursalId, CantidadDisponible) VALUES (@ProductoId, @SucursalId, @CantidadDisponible);",
                stock);

        public Task Actualizar(Stock stock) =>
            _db.ExecuteAsync(
                "UPDATE Stock SET CantidadDisponible = @CantidadDisponible WHERE Id = @Id;",
                stock);
    }
}
