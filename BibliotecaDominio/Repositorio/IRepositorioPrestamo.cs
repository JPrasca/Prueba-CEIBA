using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio.IRepositorio
{
    public interface IRepositorioPrestamo
    {
        /// <summary>
        /// Permite obtener un libro prestado dado un isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        Libro ObtenerLibroPrestadoPorIsbn(string isbn);

        /// <summary>
        /// Permite agregar un prestamo al repositorio de prestamos
        /// </summary>
        /// <param name="prestamo"></param>
        void Agregar(Prestamo prestamo);

        /// <summary>
        /// Permite obtener un prestamo por el ISBN del libro
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        Prestamo Obtener(string isbn);
    }
}
