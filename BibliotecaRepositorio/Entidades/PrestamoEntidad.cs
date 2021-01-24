using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaRepositorio.Entidades
{
    public class PrestamoEntidad
    {
        public int Id { get; set; }
        public int LibroEntidadId { get; set; }
        public LibroEntidad LibroEntidad { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaEntregaMaxima { get; set; }//2020-12-20 JPrasca: se permite valor nulo para la fecha de entrega
        public string NombreUsuario { get; set; }
    }
}
