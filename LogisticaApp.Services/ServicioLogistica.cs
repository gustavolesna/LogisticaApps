using LogisticaApp.Comun.DTOs;
using LogisticaApp.Data;


namespace LogisticaApp.Services
{
    public class ServicioLogistica
    {
        private readonly IRepositorioSucursal _repositorioSucursal;
        private readonly IRepositorioStock _repositorioStock;

        public ServicioLogistica(IRepositorioSucursal repositorioSucursal, IRepositorioStock repositorioStock)
        {
            _repositorioSucursal = repositorioSucursal;
            _repositorioStock = repositorioStock;
        }

        public async Task<IEnumerable<StockSucursalDTO>> ObtenerStockCompleto()
        {
            var sucursales = await _repositorioSucursal.ObtenerTodas();
            var stocks = await _repositorioStock.ObtenerTodos();

            var resultado = from s in sucursales
                            join st in stocks on s.Id equals st.SucursalId
                            select new StockSucursalDTO
                            {
                                SucursalId = s.Id,
                                SucursalNombre = s.Nombre,
                                ProductoId = st.ProductoId,
                                CantidadDisponible = st.CantidadDisponible
                            };

            return resultado;
        }

        public async Task ActualizarStock(int productoId, int sucursalId, int cantidad)
        {
            var stock = await _repositorioStock.ObtenerPorProductoYSucursal(productoId, sucursalId);
            if (stock == null)
                throw new Exception("Stock no encontrado para ese producto y sucursal.");

            stock.CantidadDisponible = cantidad;
            await _repositorioStock.Actualizar(stock);
        }
    }
}
