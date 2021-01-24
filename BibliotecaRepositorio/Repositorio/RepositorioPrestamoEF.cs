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
    public class RepositorioPrestamoEF : IRepositorioPrestamo
    {
        private readonly BibliotecaContexto bibliotecaContexto;
        private readonly IRepositorioLibroEF libroRepositorio;

        public RepositorioPrestamoEF(BibliotecaContexto bibliotecaContexto, IRepositorioLibro repositorioLibro)
        {
            this.bibliotecaContexto = bibliotecaContexto;
            this.libroRepositorio = (IRepositorioLibroEF) repositorioLibro;
        }

        public void Agregar(Prestamo prestamo)
        {
            PrestamoEntidad prestamoEntidad = BuildPrestamoEntidad(prestamo);
            bibliotecaContexto.Prestamos.Add(prestamoEntidad);
            bibliotecaContexto.SaveChanges();
            
        }

        public Libro ObtenerLibroPrestadoPorIsbn(string isbn)
        {
            PrestamoEntidad prestamoEntidad = ObtenerPrestamoEntidadPorIsbn(isbn);
            return LibroBuilder.ConvertirADominio(prestamoEntidad != null ? prestamoEntidad.LibroEntidad : null);
        }

        private PrestamoEntidad ObtenerPrestamoEntidadPorIsbn(string isbn)
        {
            var resultList = bibliotecaContexto.Prestamos.Where(prestamo => prestamo.LibroEntidad.Isbn == isbn).ToList();

            return resultList.Count != 0 ? resultList.FirstOrDefault() : null;
        }

        private PrestamoEntidad BuildPrestamoEntidad(Prestamo prestamo)
        {
            LibroEntidad libroEntidad = libroRepositorio.ObtenerLibroEntidadPorIsbn(prestamo.Libro.Isbn);

            PrestamoEntidad prestamoEntidad = new PrestamoEntidad
            {
                LibroEntidad = libroEntidad,
                FechaSolicitud = prestamo.FechaSolicitud,
                FechaEntregaMaxima = prestamo.FechaEntregaMaxima,
                NombreUsuario = prestamo.NombreUsuario
            };

            return prestamoEntidad;
        }

        public Prestamo Obtener(string isbn)
        {
            PrestamoEntidad prestamoEntidad = ObtenerPrestamoEntidadPorIsbn(isbn);

            return new Prestamo(prestamoEntidad.FechaSolicitud, LibroBuilder.ConvertirADominio(prestamoEntidad.LibroEntidad), prestamoEntidad.FechaEntregaMaxima, prestamoEntidad.NombreUsuario);
        }                
    }
}
