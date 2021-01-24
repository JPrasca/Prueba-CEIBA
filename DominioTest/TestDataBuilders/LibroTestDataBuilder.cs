using BibliotecaDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace DominioTest.TestDataBuilders
{
    public class LibroTestDataBuilder
    {
        private const int ANIO = 2012;
        private const string TITULO = "Cien años de soledad";
        private const String ISBN = "1234";


        public int Anio { get; set; }
        public String Isbn { get; set; }
        public String Titulo { get; set; }

        public LibroTestDataBuilder()
        {
            Anio = ANIO;
            Titulo = TITULO;
            Isbn = ISBN;
        }


        public LibroTestDataBuilder ConTitulo(string titulo)
        {
            Titulo = titulo;
            return this;
        }

        public LibroTestDataBuilder ConIsbn(string isbn)
        {
            Isbn = isbn;
            return this;
        }

        public LibroTestDataBuilder ConAnio(int anio)
        {
            Anio = anio;
            return this;
        }

        public Libro Build()
        {
            return new Libro(Isbn, Titulo, Anio);
        }

    }
}
