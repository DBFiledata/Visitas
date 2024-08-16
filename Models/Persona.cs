using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace paginaOficial.Models
{
    public class Persona
    {
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Nacionalidad { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Foto { get; set; }
        public string FechaExpiracion { get; set; }
    }
}