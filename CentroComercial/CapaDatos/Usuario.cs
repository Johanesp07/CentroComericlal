using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaDatos
{
    public class Usuario
    {
        public int ID_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public int? ID_Personal { get; set; }
    }
}