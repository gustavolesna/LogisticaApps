using LogisticaApp.Data;
using LogisticaApp.Entities;

namespace LogisticaApp.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _repo;

        public ClienteService(ClienteRepository repo)
        {
            _repo = repo;
        }

        public Task<int> CrearCliente(Cliente cliente)
        {
            return _repo.Add(cliente);
        }

        public Task<IEnumerable<Cliente>> ObtenerClientes()
        {
            return _repo.GetAll();
        }

        public Task<Cliente?> ObtenerClientePorId(int id)
        {
            return _repo.GetById(id);
        }

        public Task<bool> ActualizarCliente(Cliente cliente)
        {
            return _repo.Update(cliente);
        }

        public Task<bool> EliminarCliente(int id)
        {
            return _repo.Delete(id);
        }
    }
}
