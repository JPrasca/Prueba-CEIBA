using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio.IRepositorio
{
    public interface IRepositorioLibro
    {
        /// <summary>
        /// Permite obtener un libro dado un isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        Libro ObtenerPorIsbn(string isbn);

        /// <summary>
        /// Permite agregar un libro al repositorio
        /// </summary>
        /// <param name="libro"></param>
        void Agregar(Libro libro);
    }
}
