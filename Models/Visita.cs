using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace paginaOficial.Models
{
    public class Visita
    {
        public string Motivo { get; set; }
        public int Lugar_id { get; set; }
        public string Descripcion { get; set; }
        public int Persona_id { get; set; }
    }
}