using LogisticaApp.Data;
using LogisticaApp.Services;
using System.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Conexión PostgreSQL
var connectionString = "Host=switchback.proxy.rlwy.net;Port=32187;Username=postgres;Password=xPcyrLEjtKRxSxobhrxDjdrVxCqJMIqf;Database=railway;SSL Mode=Require;Trust Server Certificate=true";
builder.Services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(connectionString));

// Inyección de dependencias
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ClienteService>();


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
