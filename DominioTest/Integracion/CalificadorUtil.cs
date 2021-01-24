using System;
using System.Collections.Generic;
using System.Text;

namespace DominioTest.Integracion
{
    public class CalificadorUtil
    {
        public CalificadorUtil()
        {

        }

        public static DateTime sumarDiasSinContarDomingo(DateTime fechaAsuma, int diasAsumar)
        {
            int diasOperar = diasAsumar - 1;
            while(diasOperar > 0)
            {
                fechaAsuma.AddDays(1);
                diasOperar = DisminuirDiasNoEsdomingo(fechaAsuma, diasOperar);
            }
            return fechaAsuma;
        }

        public static int DisminuirDiasNoEsdomingo(DateTime fechaAsumar, int diasOperar)
        {
            if(NoEsDomingo(fechaAsumar))
            {
                diasOperar--;
            }
            return diasOperar;
        }

        public static Boolean NoEsDomingo(DateTime fechaAsumar)
        {
            var diasemana = fechaAsumar.DayOfWeek.ToString();
            if (diasemana != "Sunday")
            {
                return true;
            }
            return false;
        }
    }
}
