using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio
{
    public class Prestamo
    {
        public DateTime FechaSolicitud { get;}
        public Libro Libro { get;}
        public DateTime? FechaEntregaMaxima { get; }//2020-12-20 JPrasca: Se permite valor nulo para la fecha de entrega
        public string NombreUsuario { get;}

       
        //2020-12-20 JPrasca: añade valor nulo para fechaEntregaMaxima dado el caso
        public Prestamo(DateTime fechaSolicitud, Libro libro, DateTime? fechaEntregaMaxima, string nombreUsuario)
        {
            this.FechaSolicitud = fechaSolicitud;
            this.Libro = libro;
            this.FechaEntregaMaxima = fechaEntregaMaxima;
            this.NombreUsuario = nombreUsuario;
        }
    }
}
