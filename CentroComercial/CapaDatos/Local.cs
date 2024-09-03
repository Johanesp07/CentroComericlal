using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaDatos
{
    public class Local
    {
        public int ID_Local { get; set; }
        public int Numero_Local { get; set; }
        public decimal Tamaño { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
    }
}