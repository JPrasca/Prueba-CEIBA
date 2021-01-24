using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DominioTest.Unitarias
{
    public class MetedoFecha
    {
        public MetedoFecha()
        {

        }

        public DateTime generarFechaEntregaMaxima(DateTime fechaIngreso, int diasSumar)
        {
            var fehcaEntregaMaxima = sumarDiasSinContarEsDomingos(fechaIngreso, diasSumar);
            return fehcaEntregaMaxima;
        }

        public DateTime sumarDiasSinContarEsDomingos(DateTime fehsaAsumar, int diasumar)
        {
            var diasAoperar = diasumar - 1;
            var fechaEntrega = new DateTime();
            while (diasAoperar > 0)
            {
                if (EsDomingo(fehsaAsumar))
                {
                    fehsaAsumar = fehsaAsumar.AddDays(1);
                    fechaEntrega = fehsaAsumar;
                    diasAoperar--;
                }
                else
                {
                    fehsaAsumar = fehsaAsumar.AddDays(1);
                    fechaEntrega = fehsaAsumar;
                }
            }
            return fechaEntrega;
        }

        public bool EsDomingo(DateTime fechaOperar)
        {
            return fechaOperar.DayOfWeek.ToString() != "Sunday" ?  true :  false;
        }
    }

    //[TestClass]
    //public class testFEcha
    //{
    //    [TestMethod]
    //    public void tetFechaEntrega()
    //    {
    //        // Arragne
    //        var fechasolicitud = DateTime.Now;
    //        var diasSumar = 15;
    //        MetedoFecha mf = new MetedoFecha();

    //        //Act
    //        var resultadoFecha = mf.sumarDiasSinContarEsDomingos(fechasolicitud, diasSumar);

    //        // Assert
    //        Assert.AreEqual("04", resultadoFecha.Day.ToString());
    //        Assert.AreEqual("08", resultadoFecha.Month.ToString());
    //        Assert.AreEqual("2018", resultadoFecha.Year.ToString());

    //    }
    //}
}

