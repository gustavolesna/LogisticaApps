using LogisticaApp.Data;
using LogisticaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaApp.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _repo;
        public PedidoService(PedidoRepository repo) => _repo = repo;

        public Task<int> CrearPedido(Pedido pedido) => _repo.Add(pedido);

        public Task<IEnumerable<Pedido>> ObtenerPedidos() => _repo.GetAll();
    }
}
