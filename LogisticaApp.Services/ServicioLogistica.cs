using LogisticaApp.Comun.DTOs;
using LogisticaApp.Data;
using LogisticaApp.Entities;


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

        //public async Task ActualizarStock(int productoId, int sucursalId, int cantidad)
        //{
        //    var stock = await _repositorioStock.ObtenerPorProductoYSucursal(productoId, sucursalId);
        //    if (stock == null)
        //        throw new Exception("Stock no encontrado para ese producto y sucursal.");

        //    stock.CantidadDisponible = cantidad;
        //    await _repositorioStock.Actualizar(stock);
        //}
        public async Task ActualizarStock(int productoId, int sucursalId, int cantidad)
        {
            // Buscar stock existente
            var stockExistente = await _repositorioStock.ObtenerPorProductoYSucursal(productoId, sucursalId);

            if (stockExistente != null)
            {
                // Actualizar
                stockExistente.CantidadDisponible = cantidad;
                await _repositorioStock.Actualizar(stockExistente);
            }
            else
            {
                // Crear nuevo registro de stock
                var nuevoStock = new Stock
                {
                    ProductoId = productoId,
                    SucursalId = sucursalId,
                    CantidadDisponible = cantidad
                };
                await _repositorioStock.Agregar(nuevoStock);
            }
        }
        public async Task<IEnumerable<Sucursal>> ObtenerSucursales()
        {
            try
            {
                return await _repositorioSucursal.ObtenerTodas();
            }
            catch (Exception ex)
            {
                // Podés loguear aquí si querés centralizar errores
                throw new Exception("Error al obtener sucursales desde el repositorio.", ex);
            }
        }

        public async Task<Sucursal> AgregarSucursal(SucursalCreacionDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre de la sucursal es obligatorio.");

            var nuevaSucursal = new Sucursal
            {
                Nombre = dto.Nombre,
                Pais = dto.Pais
            };

            await _repositorioSucursal.Agregar(nuevaSucursal);
            return nuevaSucursal;
        }
    }
}
