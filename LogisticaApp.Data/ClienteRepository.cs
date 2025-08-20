using Dapper;
using LogisticaApp.Entities;
using System.Data;

public class ClienteRepository
{
    private readonly IDbConnection _db;
    public ClienteRepository(IDbConnection db) => _db = db;

    public async Task<int> Add(Cliente cliente)
    {
        var sql = @"INSERT INTO Clientes (Nombre, Email, Telefono) 
                    VALUES (@Nombre, @Email, @Telefono) RETURNING Id;";

        var clienteId = await _db.ExecuteScalarAsync<int>(sql, new
        {
            cliente.Nombre,
            cliente.Email,
            cliente.Telefono
        });

        return clienteId;
    }

    public async Task<IEnumerable<Cliente>> GetAll()
    {
        var sql = "SELECT * FROM Clientes;";
        var clientes = await _db.QueryAsync<Cliente>(sql);
        return clientes;
    }

    public async Task<Cliente?> GetById(int id)
    {
        var sql = "SELECT * FROM Clientes WHERE Id=@Id;";
        return await _db.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
    }

    public async Task<bool> Update(Cliente cliente)
    {
        var sql = @"UPDATE Clientes 
                    SET Nombre=@Nombre, Email=@Email, Telefono=@Telefono 
                    WHERE Id=@Id;";

        var rows = await _db.ExecuteAsync(sql, cliente);
        return rows > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var sql = "DELETE FROM Clientes WHERE Id=@Id;";
        var rows = await _db.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }
}
