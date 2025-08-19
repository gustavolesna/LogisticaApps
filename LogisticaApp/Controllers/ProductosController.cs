using LogisticaApp.Entities;
using LogisticaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<IEnumerable<Producto>> Get() => _service.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await _service.GetById(id);
            if (producto == null) return NotFound();
            return producto;
        }

        [HttpPost]
        public Task<IActionResult> Post([FromBody] Producto producto)
        {
            return _service.Add(producto).ContinueWith(_ => (IActionResult)Ok());
        }

        [HttpPut("{id}")]
        public Task<IActionResult> Put(int id, [FromBody] Producto producto)
        {
            producto.Id = id;
            return _service.Update(producto).ContinueWith(_ => (IActionResult)NoContent());
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(int id) =>
            _service.Delete(id).ContinueWith(_ => (IActionResult)NoContent());


        [HttpGet("prueba")]
        public async Task<IActionResult> PruebaConexion()
        {
            try
            {
                var productos = await _service.GetAll();
                return Ok(new { Mensaje = "Conexión exitosa!", Productos = productos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error de conexión", Detalle = ex.Message });
            }
        }
    }
}
