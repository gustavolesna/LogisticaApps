using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaApp.Entities
{
    // Pedido.cs
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public List<PedidoProducto> Productos { get; set; } = new();
    }
}
