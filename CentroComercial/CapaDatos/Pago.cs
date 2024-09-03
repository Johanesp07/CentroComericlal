using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaDatos
{
    public class Pago
    {
        public int ID_Pago { get; set; }
        public int ID_Alquiler { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public decimal Monto_Pagado { get; set; }
    }
}