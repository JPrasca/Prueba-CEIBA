using BibliotecaDominio.IRepositorio;
using BibliotecaDominio.Utils;
using System;

namespace BibliotecaDominio
{
    public class Bibliotecario
    {
        public const string EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE = "El libro no se encuentra disponible";
        private  IRepositorioLibro libroRepositorio;
        private  IRepositorioPrestamo prestamoRepositorio;

        #region variables y consantes añadidas

        //se define un número de días
        private const int DIAS_DE_PRESTAMO_DEFAULT = 0;
        public const string ES_PALINDROMO = "los libros palíndromos solo se pueden utilizar en la biblioteca";
        #endregion

        public Bibliotecario(IRepositorioLibro libroRepositorio, IRepositorioPrestamo prestamoRepositorio)
        {
            this.libroRepositorio = libroRepositorio;
            this.prestamoRepositorio = prestamoRepositorio;
        }

        public void Prestar(string isbn, string nombreUsuario)
        {
            //variable auxiliar para obtener los caracteres del ISBN
            char[] auxISBN = isbn.ToCharArray();

            //se invierte el orden de los caracteres
            Array.Reverse(auxISBN);

            //se tiene en cuenta lo mencionado en la regla de negocio #5 (se añadie una cantidad de días por defecto para los códigos con menos de 30)
            int diasPrestamo = (isbn.Length >= 30) ? 15: DIAS_DE_PRESTAMO_DEFAULT;

            //fecha de entrega que toma valor si lleva 30+ caracteres
            DateTime? fechaEntrega = null;

            try
            {
                //se verifica si corresponde a un palíndrome
                if (isbn == new string(auxISBN)) 
                {
                    throw new Exception(ES_PALINDROMO);
                }

                //se verifica si esta prestado
                if (this.EsPrestado(isbn)) 
                {
                    throw new Exception(EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE);
                }

                //se calcula la fecha de entrega en caso dado
                if (diasPrestamo != 0)
                {
                    fechaEntrega = CalificadorUtil.sumarDiasSinContarDomingo(DateTime.Now, diasPrestamo);
                }

                //registro del préstamo
                this.prestamoRepositorio.Agregar(
                    new Prestamo(
                        DateTime.Now,
                        libroRepositorio.ObtenerPorIsbn(isbn),
                        fechaEntrega,
                        nombreUsuario
                    )
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool EsPrestado(string isbn)
        {            
            //se verifica que el préstamo para el ISBN dado no exista en BD
            return (this.prestamoRepositorio.ObtenerLibroPrestadoPorIsbn(isbn) != null);
        }
    }
}
