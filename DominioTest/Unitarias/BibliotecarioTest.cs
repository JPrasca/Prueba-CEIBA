using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
using BibliotecaDominio.IRepositorio;
using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DominioTest.Unitarias
{
    [TestClass]
    public class BibliotecarioTest
    {
        public BibliotecarioTest()
        {

        }
        public Mock<IRepositorioLibro> repositorioLibro;
        public Mock<IRepositorioPrestamo> repositorioPrestamo;

        [TestInitialize]
        public void setup()
        {
            repositorioLibro = new Mock<IRepositorioLibro>();
           repositorioPrestamo = new Mock<IRepositorioPrestamo>();
        }

        [TestMethod]
        public void EsPrestado()
        {
            // Arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();
            
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Returns(libro);

            // Act
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object,repositorioPrestamo.Object);
            var esprestado = bibliotecario.EsPrestado(libro.Isbn);

            // Assert
            Assert.AreEqual(esprestado, true);
        }

        [TestMethod]
        public void LibroNoPrestadoTest()
        {
            // Arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object);
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Equals(null);

            // Act
            var esprestado = bibliotecario.EsPrestado(libro.Isbn);

            // Assert
            Assert.IsFalse(esprestado);
        }

        

    }
}
