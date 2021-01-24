using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
namespace DominioTest.Unitarias
{
    [TestClass]
    public class LibroTest
    {
        private const int ANIO = 2012;
        private const string TITULO = "Cien años de soledad";
        private const string ISBN = "1234";
        public LibroTest()
        {

        }

        [TestMethod]
        public void CrearLibroTest()
        {
            // Arrange
            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().ConTitulo(TITULO).
                ConAnio(ANIO).ConIsbn(ISBN);


            // Act
            Libro libro = libroTestBuilder.Build();

            // Assert
            Assert.AreEqual(TITULO, libro.Titulo);
            Assert.AreEqual(ISBN, libro.Isbn);
            Assert.AreEqual(ANIO, libro.Anio);
        }
    }
}
