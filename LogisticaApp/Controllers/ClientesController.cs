using LogisticaApp.Entities;
using LogisticaApp.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteService _service;
    public ClientesController(ClienteService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente)
    {
        try
        {
            var id = await _service.CrearCliente(cliente);
            return CreatedAtAction(nameof(ObtenerClientes), new { id }, cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Mensaje = "Error al crear cliente", Detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerClientes()
    {
        var clientes = await _service.ObtenerClientes();
        return Ok(clientes);
    }
}
