using Dapper;
using LogisticaApp.Entities;
using System.Data;

namespace LogisticaApp.Data
{
    // PedidoRepository.cs
    public class PedidoRepository
    {
        private readonly IDbConnection _db;
        public PedidoRepository(IDbConnection db) => _db = db;

        public async Task<int> Add(Pedido pedido)
        {
            var sql = @"INSERT INTO Pedidos (ClienteId, Fecha) VALUES (@ClienteId, @Fecha) RETURNING Id;";
            var pedidoId = await _db.ExecuteScalarAsync<int>(sql, new { pedido.ClienteId, pedido.Fecha });

            foreach (var p in pedido.Productos)
            {
                await _db.ExecuteAsync(
                    "INSERT INTO PedidoProductos (PedidoId, ProductoId, Cantidad) VALUES (@PedidoId,@ProductoId,@Cantidad);",
                    new { PedidoId = pedidoId, ProductoId = p.ProductoId, p.Cantidad });
            }

            return pedidoId;
        }

        public async Task<IEnumerable<Pedido>> GetAll()
        {
            var sql = "SELECT * FROM Pedidos;";
            var pedidos = await _db.QueryAsync<Pedido>(sql);

            foreach (var pedido in pedidos)
            {
                var productos = await _db.QueryAsync<PedidoProducto>(
                    "SELECT * FROM PedidoProductos WHERE PedidoId=@PedidoId;", new { pedido.Id });
                pedido.Productos = productos.ToList();
            }

            return pedidos;
        }
    }
}
