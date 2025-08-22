using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaApp.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int SucursalId { get; set; }
        public int CantidadDisponible { get; set; }
    }
}
