
#if DEBUG
using Dapper;
using LogisticaApp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[ApiController]
[Route("api/[controller]")]
public class DevSqlController : ControllerBase
{
    private readonly IDbConnection _db;
    private readonly IWebHostEnvironment _env;

    public DevSqlController(IDbConnection db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpPost("EjecutarArchivo")]
    public async Task<IActionResult> EjecutarArchivo([FromBody] SqlFileRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FileName))
            return BadRequest(new { Mensaje = "Debe indicar el nombre del archivo" });

        try
        {
            var path = Path.Combine(_env.ContentRootPath, "Scripts", request.FileName);

            if (!System.IO.File.Exists(path))
                return NotFound(new { Mensaje = "Archivo no encontrado", File = request.FileName });

            var sql = await System.IO.File.ReadAllTextAsync(path);

            await _db.ExecuteAsync(sql); // Dapper ejecuta todo el script

            return Ok(new { Mensaje = $"Script {request.FileName} ejecutado correctamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Mensaje = "Error al ejecutar el script", Detalle = ex.Message });
        }
    }
}
#endif



//#if DEBUG
//using Dapper;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;

//[ApiController]
//[Route("api/[controller]")]
//public class DevSqlController : ControllerBase
//{
//    private readonly IDbConnection _db;

//    public DevSqlController(IDbConnection db)
//    {
//        _db = db;
//    }

//    [HttpPost("Ejecutar")]
//    public async Task<IActionResult> EjecutarScript([FromBody] SqlScriptRequest request)
//    {
//        if (string.IsNullOrWhiteSpace(request.Sql))
//            return BadRequest(new { Mensaje = "El script no puede estar vacío" });

//        try
//        {
//            await _db.ExecuteAsync(request.Sql);
//            return Ok(new { Mensaje = "Script ejecutado correctamente" });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Mensaje = "Error al ejecutar el script", Detalle = ex.Message });
//        }
//    }
//}

//public class SqlScriptRequest
//{
//    public string Sql { get; set; }
//}
//#endif
