using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio
{
    public class Libro
    {
        public string Isbn { get; }
        public string Titulo { get; }        
        public int Anio { get; }
        

        public Libro(string isbn, string titulo, int anio)
        {
            this.Isbn = isbn;
            this.Titulo = titulo;
            this.Anio = anio;
        }
    }
}
