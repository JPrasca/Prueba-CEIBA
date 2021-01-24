using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaRepositorio.Entidades
{
    public class LibroEntidad
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Anio { get; set; }        
    }
}
