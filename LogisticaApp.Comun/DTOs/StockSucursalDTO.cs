using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaApp.Comun.DTOs
{
    public class StockSucursalDTO
    {
        public int ProductoId { get; set; }
        public int SucursalId { get; set; }
        public string SucursalNombre { get; set; }
        public int CantidadDisponible { get; set; }
    }
}
