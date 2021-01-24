using BibliotecaRepositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaRepositorio.Repositorio
{
    public interface IRepositorioLibroEF
    {
        /// <summary>
        /// Permite obtener un libro entity por un isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        LibroEntidad ObtenerLibroEntidadPorIsbn(string isbn);
    }
}
