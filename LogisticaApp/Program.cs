using LogisticaApp.Comun.Logs;
using LogisticaApp.Data;
using LogisticaApp.Services;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Conexión PostgreSQL
var connectionString = "Host=switchback.proxy.rlwy.net;Port=32187;Username=postgres;Password=xPcyrLEjtKRxSxobhrxDjdrVxCqJMIqf;Database=railway;SSL Mode=Require;Trust Server Certificate=true";
builder.Services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(connectionString));

// Inyección de dependencias
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ClienteService>();
// Registro de repositorios
builder.Services.AddScoped<IRepositorioSucursal, RepositorioSucursal>();
builder.Services.AddScoped<IRepositorioStock, RepositorioStock>();

// Registro del servicio principal
builder.Services.AddScoped<ServicioLogistica>();

// Registro del logger
builder.Services.AddSingleton<IServicioLogs, ServicioLogs>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
