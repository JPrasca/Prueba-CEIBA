using BibliotecaDominio;
using BibliotecaDominio.IRepositorio;
using BibliotecaRepositorio.Builder;
using BibliotecaRepositorio.Contexto;
using BibliotecaRepositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibliotecaRepositorio.Repositorio
{
    public class RepositorioLibroEF : IRepositorioLibro, IRepositorioLibroEF
    {
        private readonly BibliotecaContexto bibliotecaContexto;

        public RepositorioLibroEF(BibliotecaContexto bibliotecaContexto)
        {
            this.bibliotecaContexto = bibliotecaContexto;
        }
        
        public Libro ObtenerPorIsbn(string isbn)
        {
            LibroEntidad libroEntidad = ObtenerLibroEntidadPorIsbn(isbn);
            return LibroBuilder.ConvertirADominio(libroEntidad);
        }

        public void Agregar(Libro libro)
        {
            bibliotecaContexto.Libros.Add(LibroBuilder.ConvertirAEntidad(libro));
            bibliotecaContexto.SaveChanges();
        }

        public LibroEntidad ObtenerLibroEntidadPorIsbn(string isbn)
        {
            var libroEntidad = bibliotecaContexto.Libros.FirstOrDefault(libro => libro.Isbn == isbn);
            return libroEntidad;
        }
    }
}
