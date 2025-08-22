using LogisticaApp.Comun.DTOs;
using LogisticaApp.Comun.Logs;
using LogisticaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LogisticaController : ControllerBase
    {
        private readonly ServicioLogistica _servicioLogistica;
        private readonly IServicioLogs _logger;

        public LogisticaController(ServicioLogistica servicioLogistica, IServicioLogs logger)
        {
            _servicioLogistica = servicioLogistica;
            _logger = logger;
        }

        // GET: api/logistica/stock
        [HttpGet("stock")]
        public async Task<IActionResult> ObtenerStockCompleto()
        {
            try
            {
                var stock = await _servicioLogistica.ObtenerStockCompleto();
                return Ok(stock);
            }
            catch (Exception ex)
            {
                _logger.Error("Error al obtener stock completo", ex);
                return StatusCode(500, new { mensaje = "Error al obtener stock", detalle = ex.Message });
            }
        }

        // PUT: api/logistica/stock
        [HttpPut("stock")]
        public async Task<IActionResult> ActualizarStock([FromBody] StockSucursalDTO stockDto)
        {
            try
            {
                await _servicioLogistica.ActualizarStock(stockDto.ProductoId, stockDto.SucursalId, stockDto.CantidadDisponible);
                _logger.Informacion($"Stock actualizado: Producto {stockDto.ProductoId}, Sucursal {stockDto.SucursalId}, Cantidad {stockDto.CantidadDisponible}");
                return Ok(new { mensaje = "Stock actualizado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.Error("Error al actualizar stock", ex);
                return StatusCode(500, new { mensaje = "Error al actualizar stock", detalle = ex.Message });
            }
        }

        // GET: api/logistica/sucursales
        [HttpGet("sucursales")]
        public async Task<IActionResult> ObtenerSucursales()
        {
            try
            {
                var sucursales = await _servicioLogistica.ObtenerSucursales();
                return Ok(sucursales);
            }
            catch (Exception ex)
            {
                _logger.Error("Error al obtener sucursales", ex);
                return StatusCode(500, new { mensaje = "Error al obtener sucursales", detalle = ex.Message });
            }
        }

        [HttpPost("sucursales")]
        public async Task<IActionResult> CrearSucursal([FromBody] SucursalCreacionDTO dto)
        {
            try
            {
                var sucursal = await _servicioLogistica.AgregarSucursal(dto);
                _logger.Informacion($"Sucursal creada: {sucursal.Nombre} ({sucursal.Pais})");
                return Ok(new { mensaje = "Sucursal creada correctamente", sucursal });
            }
            catch (Exception ex)
            {
                _logger.Error("Error al crear sucursal", ex);
                return StatusCode(500, new { mensaje = "Error al crear sucursal", detalle = ex.Message });
            }
        }


    }
}

