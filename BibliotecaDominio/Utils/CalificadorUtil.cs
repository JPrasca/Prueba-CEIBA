using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio.Utils
{
    public class CalificadorUtil
    {
        public CalificadorUtil()
        {

        }

        //2020-12-20 JPrasca: Se realiza asignación a fechaAsuma del valor retornado por AddDays
        public static DateTime sumarDiasSinContarDomingo(DateTime fechaAsuma, int diasAsumar)
        {
            int diasOperar = diasAsumar - 1;
            while(diasOperar > 0)
            {
                //la fecha va actualizando su valor mientras los días van decrementando
                fechaAsuma = fechaAsuma.AddDays(1);
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
