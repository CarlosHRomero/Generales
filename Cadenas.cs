using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generales
{
    public class Cadenas
    {
        public static Boolean ConvertirABoolean(String s)
        {
            int x;
            if (int.TryParse(s, out x))
            {
                if (Convert.ToBoolean(x))
                    return true;
                else
                    return false;
            }
            else
            {
                if (s == Boolean.TrueString)
                    return true;
                else
                    return false;
            }
        }

        public static List<string> DividirCadena(string s, string var)
        {
            List<string> lista = new List<string>();
            string st, izq, der;
            while (!string.IsNullOrEmpty(s))
            {
                s = DividirCadena(s, var, out izq, out der);
                lista.Add(izq.Trim());
            }
            return lista;
        }

        public static string DividirCadena(string s, string var, out string izq)
        {
            string der;
            return DividirCadena(s, var, out izq, out der);
        }

        public static string DividirCadenaDelimitada(string s, string LimiteIzquierdo, string LimiteDerecho)
        {
            try
            {

                if (s == null)
                {
                    return null;
                }
                int izq = s.IndexOf(LimiteIzquierdo);
                if (izq < 0)
                    return null;
                string w_str = s.Substring(izq+ LimiteIzquierdo.Length);
                int der = w_str.IndexOf(LimiteDerecho);
                if(der < 0)
                    return w_str;
                return w_str.Substring(0, der);
            }
            catch (Exception ex)
            {
                Mensajes.msgError(s, ex);
                return null;
            }

        }
        public static String DividirCadena(String s, String var, out String Izq, out String Der)
        {
            return DividirCadena(s, var, out Izq, out Der, true);

        }
        public static String DividirCadena(String s, String var, out  String Izq, out String Der, Boolean opc)
        {
            try
            {

                if (s == null)
                {
                    Izq = null; Der = null; return null;
                }
                int i = s.IndexOf(var);
                int l = var.Length;
                if (i > -1)
                {
                    Izq = s.Substring(0, i);

                    if (i < s.Length)
                        Der = s.Substring(i + l);
                    else
                        Der = null;
                }
                else
                {
                    Izq = s;
                    Der = null;
                }
                if (!opc)
                    return Izq;
                else
                    return Der;

            }
            catch (Exception ex)
            {
                Mensajes.msgError(s, ex);
                Der = null; Izq = null;
                return null;
            }
        }
        public static String RemplazaApostrofos(String s)
        {
            String s2;
            if (s.Contains("\'"))
                s2 = s.Replace("\'", "[\'\'´`]");
            else if (s.Contains("´"))
                s2 = s.Replace("´", "[\'\'´`]");
            else if (s.Contains("`"))
                s2 = s.Replace("`", "[\'\'´`]");
            else
                s2 = s;
            return s2;
        }
        public static String ReemplazarAcentos(String s)
        {
            String s2 = s; ;
            if (s.Contains("á"))
            {
                s2 = s.Replace("á", "[aáàâäAÁÀÂÄ]");
            }
            else if (s.Contains("Á"))
                s2 = s.Replace("Á", "[aáàâäAÁÀÂÄ]");
            s = s2;
            if (s.Contains("é"))
            {
                s2 = s.Replace("é", "[eéèêëEÉÈÊË]");
            }
            else if (s.Contains("É"))
                s2 = s.Replace("É", "[eéèêëEÉÈÊË]");
            s = s2;
            if (s.Contains("í"))
            {
                s2 = s.Replace("í", "[iíìîIÍÌÎ]");
            }
            if (s.Contains("Í"))
                s2 = s.Replace("Í", "[iíìîIÍÌÎ]");
            s = s2;
            if (s.Contains("ó"))
            {
                s2 = s.Replace("ó", "[oóòôöOÓÒÔÖ]");
            }
            else if (s.Contains("Ó"))
                s2 = s.Replace("Ó", "[oóòôöOÓÒÔÖ]");
            s = s2;
            if (s.Contains("ú"))
            {
                s2 = s.Replace("ú", "[uúùûüUÚÙÛÜ]");
            }
            else if (s.Contains("Ú"))
                s2 = s.Replace("Ú", "[uúùûüUÚÙÛÜ]");
            s = s2;
            return s;

        }

        public static string ReemplazarComodines(string s)
        {
            // Reemplaza * por porcentaje
            return s.Replace("*", "%");
        }

        public static string FormatearCadenaInformes(string s)
        {
            string w_s =s;
            w_s =EliminarEspaciosComienzoLinea(w_s);
            w_s = EliminarPuntuacionComienzoLinea(w_s);
            return w_s;
        }
        public static string EliminarEspaciosComienzoLinea(string s)
        {
            string w_str = s;
            while (w_str.Contains("\r\n "))
                w_str=w_str.Replace("\r\n ", "\r\n");
            while (w_str.Contains("\n "))
                w_str=w_str.Replace("\n ", "\n");

            return w_str.Trim();
        }

        public static string EliminarPuntuacionComienzoLinea(string s)
        {
            string w_str = s;
            while (w_str[0] == ',' || w_str[0] == '.' || w_str[0]== ' ')
                w_str = w_str.Substring(1);

            while (w_str.Contains("\r\n,"))
                w_str = w_str.Replace("\r\n,", "\r\n");
            while (w_str.Contains("\n,"))
                w_str = w_str.Replace("\n,", "\n");
            return w_str.Trim(); 

        }


        public static string EliminarSaltosLinea(string w_str)
        {
            while (w_str.Contains("\r\n,"))
                w_str = w_str.Replace("\r\n", "");
            while (w_str.Contains("\n,"))
                w_str = w_str.Replace("\n", "");
            return w_str;
        }
    }
}
