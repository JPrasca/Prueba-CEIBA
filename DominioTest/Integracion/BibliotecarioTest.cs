using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
using BibliotecaRepositorio.Contexto;
using BibliotecaRepositorio.Repositorio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominioTest.TestDataBuilders;
using Microsoft.EntityFrameworkCore;

namespace DominioTest.Integracion
{

    [TestClass]
    public class BibliotecarioTest
    {
        public const String CRONICA_UNA_MUERTE_ANUNCIADA = "Cronica de una muerte anunciada";
        private  BibliotecaContexto contexto;
        private  RepositorioLibroEF repositorioLibro;
        private RepositorioPrestamoEF repositorioPrestamo;

        #region Variables y constantes para test añadidos
        //largos (>=30 caracteres)
        public const String ISBN_30PLUS_PALINDROME = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890987654321ZYXWVUTSRQPONMLKJIHGFEDCBA";
        public const String ISBN_30PLUS_X = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        //cortos (<30 caracteres)
        public const string ISBN_UNDER30_X = "ISBNTESTIN";
        public const string iSBN_UNDER30_PALINDROME = "ISBN00NBSI";

        public const string USUARIO_EJEMPLO = "Karen";
        #endregion


        [TestInitialize]
        public void setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContexto>();
            contexto = new BibliotecaContexto(optionsBuilder.Options);
            repositorioLibro  = new RepositorioLibroEF(contexto);
            repositorioPrestamo = new RepositorioPrestamoEF(contexto, repositorioLibro);
        }

        [TestMethod]
        public void PrestarLibroTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn, "Juan");

            // Assert
            Assert.AreEqual(bibliotecario.EsPrestado(libro.Isbn), true);
            Assert.IsNotNull(repositorioPrestamo.ObtenerLibroPrestadoPorIsbn(libro.Isbn));

        }

        [TestMethod]
        public void PrestarLibroNoDisponibleTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn,"Juan");
            try
            {
                bibliotecario.Prestar(libro.Isbn, "Juan");
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual("El libro no se encuentra disponible", err.Message);
            }
        
        }

        #region Pruebas añadidas

        //2020-12-20 JPrasca: Método para corroborar un ISBN palíndromo
        [TestMethod]
        public void ZPrestarLibroIsbnPalindromoTest()
        {

            // Arrange
            Libro libro = new LibroTestDataBuilder().ConIsbn(ISBN_30PLUS_PALINDROME)
                .Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            try
            {
                // Act
                bibliotecario.Prestar(libro.Isbn, "Juan");
                Assert.Fail();
            }
            catch (Exception ex) 
            {
                //Assert
                Assert.AreEqual("los libros palíndromos solo se pueden utilizar en la biblioteca", ex.Message);
            }
        }

        //2020-12-20 JPrasca: Método para corroborar un ISBN con menos de 30 caracteres
        [TestMethod]
        public void ZPrestarLibroVerificarFechaIsbnCortoTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConIsbn(ISBN_UNDER30_X)
                .Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);


            // Act
            bibliotecario.Prestar(libro.Isbn, "Ana");


            //Assert
            Assert.IsNull(repositorioPrestamo.Obtener(libro.Isbn).FechaEntregaMaxima);//si no sobre pasa los 30 caracteres, guarda la entrega null

        }


        //2020-12-20 JPrasca: Método para corroborar un ISBN con más de 30 caracteres y no-palíndromos
        [TestMethod]
        public void ZPrestarLibroVerificarFechaIsbnLargoTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConIsbn(ISBN_30PLUS_X)
                .Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);


            // Act
            bibliotecario.Prestar(libro.Isbn, "Maria");


            //Assert
            Assert.IsNotNull(repositorioPrestamo.Obtener(libro.Isbn).FechaEntregaMaxima);//si no sobre pasa los 30 caracteres, guarda la entrega null
            Assert.IsNotNull(repositorioPrestamo.Obtener(libro.Isbn).Libro);
        }

        //2020-12-20 JPrasca: Corroborar que la información del préstamo se esá registrando
        [TestMethod]
        public void ZPrestarLibroCorroborarInfoTest() 
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder()
                .ConIsbn(ISBN_30PLUS_X)
                .ConAnio(1997)
                .ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA)
                .Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            //Act
            bibliotecario.Prestar(libro.Isbn, USUARIO_EJEMPLO);

            //Assert
            Libro libroAux = repositorioPrestamo.ObtenerLibroPrestadoPorIsbn(ISBN_30PLUS_X);
            Assert.AreEqual(libroAux.Isbn, ISBN_30PLUS_X);
            Assert.AreEqual(libroAux.Titulo, CRONICA_UNA_MUERTE_ANUNCIADA);
            Assert.AreEqual(libroAux.Anio, 1997);
            Assert.AreEqual(repositorioPrestamo.Obtener(ISBN_30PLUS_X).NombreUsuario, USUARIO_EJEMPLO);
            Assert.IsNotNull(repositorioPrestamo.Obtener(ISBN_30PLUS_X).FechaEntregaMaxima);
        }
        #endregion
    }
}
