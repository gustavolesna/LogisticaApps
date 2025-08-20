using LogisticaApp.Entities;
using LogisticaApp.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _service;
    public PedidosController(PedidoService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> CrearPedido([FromBody] Pedido pedido)
    {
        try
        {
            var id = await _service.CrearPedido(pedido);
            return CreatedAtAction(nameof(ObtenerPedidos), new { id }, pedido);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Mensaje = "Error al crear pedido", Detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerPedidos()
    {
        var pedidos = await _service.ObtenerPedidos();
        return Ok(pedidos);
    }
}
