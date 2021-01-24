using BibliotecaDominio;
using BibliotecaRepositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaRepositorio.Builder
{
    public class LibroBuilder
    {
        private LibroBuilder() { }

        internal static Libro ConvertirADominio(LibroEntidad libroEntidad)
        {
            Libro libro = null;
            if (libroEntidad != null)
            {
                libro = new Libro(libroEntidad.Isbn, libroEntidad.Titulo, libroEntidad.Anio);
            }
            return libro;
        }

        public static LibroEntidad ConvertirAEntidad(Libro libro)
        {
            LibroEntidad libroEntidad = new LibroEntidad
            {
                Anio = libro.Anio,
                Isbn = libro.Isbn,
                Titulo = libro.Titulo
            };
            return libroEntidad;
        }
    }
}
