using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generales
{
    public class Fechas
    {
        public static String CalcularEdad(DateTime FNac)
        {            
            int edad = ((DateTime.Today -FNac).Days) / 365;
            return Convert.ToString(edad);
        }
        public static String CalcularEdad(DateTime? FNac)
        {
            if (FNac.HasValue)
            {
                int edad = ((DateTime.Today - (DateTime)FNac).Days) / 365;
                return Convert.ToString(edad);
            }
            else
                return null;
        }
    }
}
