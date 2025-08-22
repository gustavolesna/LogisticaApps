using Dapper;
using LogisticaApp.Entities;
using System.Data;


namespace LogisticaApp.Data
{
    public interface IRepositorioSucursal
    {
        Task<IEnumerable<Sucursal>> ObtenerTodas();
        Task<Sucursal> ObtenerPorId(int id);
        Task Agregar(Sucursal sucursal);
    }

    public class RepositorioSucursal : IRepositorioSucursal
    {
        private readonly IDbConnection _db;
        public RepositorioSucursal(IDbConnection db) { _db = db; }

        public Task<IEnumerable<Sucursal>> ObtenerTodas() =>
            _db.QueryAsync<Sucursal>("SELECT * FROM Sucursales;");

        public Task<Sucursal> ObtenerPorId(int id) =>
            _db.QueryFirstOrDefaultAsync<Sucursal>(
                "SELECT * FROM Sucursales WHERE Id = @Id;", new { Id = id });

        public Task Agregar(Sucursal sucursal) =>
            _db.ExecuteAsync(
                "INSERT INTO Sucursales (Nombre, Pais) VALUES (@Nombre, @Pais);",
                sucursal);
    }

}
