using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaDatos
{
    public class Alquiler
    {
        public int ID_Alquiler { get; set; }
        public int ID_Local { get; set; }
        public int ID_Usuario { get; set; }
        public string Inquilino { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public decimal Monto { get; set; }
    }
}