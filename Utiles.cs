using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;

namespace Generales
{

    /*Esta clase se usa para gestionar procedimiento y funciones relativos
     * exclusivamente al tratamiento de los datos y la base de datos.
     * Capa DAL
     * 
     * Autor:
     * Fecha Creacion:
     * Fecha Ult. Modificacion:
     * 
     */ 
    public class Utiles
    {

        #region - Private Properties -

        private System.Globalization.NumberFormatInfo _NFI = null;
        private System.Globalization.NumberFormatInfo NFI
        {
            get
            {
                if (_NFI == null)
                {
                    System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.InstalledUICulture;
                    _NFI = (System.Globalization.NumberFormatInfo)ci.NumberFormat.Clone();
                    _NFI.NumberDecimalSeparator = ".";
                }
                return _NFI;
            }
        }


        #endregion - Private Properties

        public static void RemplazaApostrofos(String s)
        {
            int i;
            if ((i=s.IndexOf("\'")) < 0)
                return;
            s.Replace("\'", "%");
        }
        public static string ConverToSqlDateTime(string p_Fecha)
        {
            string Ret;
            try
            {
                DateTime Fecha = Convert.ToDateTime(p_Fecha);
                Ret = "'" + Fecha.Day.ToString() + "-" + Fecha.Month.ToString() + "-" + Fecha.Year.ToString() + " " + Fecha.Hour.ToString() + ":" + Fecha.Minute.ToString() + ":" + Fecha.Second.ToString() + "'";
            }
            catch
            {
                Ret = "null";
            }
            return Ret;
        }

        public static string ConverToSqlDate(string p_Fecha)
        {
            string Ret;
            try
            {
                DateTime Fecha = Convert.ToDateTime(p_Fecha);
                Ret = "'" + Fecha.Year.ToString() + "-" + Fecha.Month.ToString() + "-" + Fecha.Day.ToString() + "'";
            }
            catch
            {
                Ret = "null";
            }
            return Ret;
        }

        public static string ConverToSqlDate(DateTime Fecha)
        {
            string Ret;
            try
            {                
                Ret = "'" + Fecha.Year.ToString() + "-" + Fecha.Month.ToString() + "-" + Fecha.Day.ToString() + "'";
            }
            catch
            {
                Ret = "null";
            }
            return Ret;
        }

        public static string ConvertToSqlString(string value)
        {
            string ret;
            if (string.IsNullOrEmpty(value))
                ret = "null";
            else
                ret = "'" + value + "'";
            return ret;
        }

        public static string ConvertToSqlString(string value,int Largo)
        {
            string ret;
            if (string.IsNullOrEmpty(value))
                ret = "null";
            else
                ret = "'" + value.Substring(0,Largo-1) + "'";
            return ret;
        }

        public static string ConvertToSqlBool(string value)
        {
            string ret;
            if (string.IsNullOrEmpty(value))
                ret = "null";
            else
            {
                if (value == bool.TrueString)
                    ret = "1";
                else
                    ret = "0";
            }
            return ret;
        }

        public static string ConvertToSqlSINO(string value)
        {
            string ret;
            if (string.IsNullOrEmpty(value))
                ret = "null";
            else
            {
                if ((value == "s") || (value == "S"))
                    ret = "S";
                else
                    ret = "N";
            }
            return ret;
        }

        public static string ConvertToSqlNumber(string value)
        {
            string ret;
            if (string.IsNullOrEmpty(value))
                ret = "null";
            else
            {
                ret = value;
            }
            return ret;
        }

        public static string ConvertToSqlDouble(Double Value)
        {
            string Ret = Value.ToString();
            int Pos = Ret.IndexOf(',');
            if (Pos > 0)
            {
                Ret = Ret.Substring(0, Pos) + "." + Ret.Substring(Pos + 1);
            }
            return Ret;
        }

        public static string ConvertToSqlDouble(Double? Value)
        {
            string Ret = "";
            if (Value != null)
            {
                Ret = Value.ToString();
                int Pos = Ret.IndexOf(',');
                if (Pos > 0)
                {
                    Ret = Ret.Substring(0, Pos) + "." + Ret.Substring(Pos + 1);
                }
            }
            else
            {
                Ret="null";
            }
            return Ret;
        }

        public static string ConvertToSqlDecimal(Decimal Value)
        {
            string Ret = Value.ToString();
            int Pos = Ret.IndexOf(',');
            if (Pos > 0)
            {
                Ret = Ret.Substring(0, Pos) + "." + Ret.Substring(Pos + 1);
            }
            return Ret;
        }

        public static string ConvertToSqlDecimal(decimal? Value)
        {
            string Ret = "";
            if (Value != null)
            {
                Ret = Value.ToString();
                int Pos = Ret.IndexOf(',');
                if (Pos > 0)
                {
                    Ret = Ret.Substring(0, Pos) + "." + Ret.Substring(Pos + 1);
                }
            }
            else
            {
                Ret = "null";
            }
            return Ret;
        }

        public static string ConvertToEspDate(string Fecha)
        {
            /* Conversor para formato yyyy-mm-dd
             
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy-MM-dd";
            dtfi.DateSeparator = "-";
            DateTime sqlfecha = Convert.ToDateTime(Fecha,dtfi);
             
             */
            DateTime sqlfecha = Convert.ToDateTime(Fecha);
            string Ret = sqlfecha.Day.ToString() + "/" + sqlfecha.Month.ToString() + "/" + sqlfecha.Year.ToString();
            return Ret;
        }

        public static string ConvertToEspDateTime(string Fecha)
        {
            DateTime sqlfecha = Convert.ToDateTime(Fecha);
            string Ret = "'" + sqlfecha.Day.ToString() + "/" + sqlfecha.Month.ToString() + "/" + sqlfecha.Year.ToString() + " " + sqlfecha.Hour + ":" + sqlfecha.Minute + ":" + sqlfecha.Second + "'";
            return Ret;
        }

        public static string ConvertToEspDateTime(DateTime sqlfecha)
        {
            string Ret = "'" + sqlfecha.Day.ToString() + "/" + sqlfecha.Month.ToString() + "/" + sqlfecha.Year.ToString() + " " + sqlfecha.Hour + ":" + sqlfecha.Minute + ":" + sqlfecha.Second + "'";
            return Ret;
        }

        public static string ConvertToEspDateTime(DateTime? sqlfecha)
        {
            string Ret = "null";
            if (sqlfecha != null)
            {
                Ret = "'" + sqlfecha.Value.Day.ToString() + "/" + sqlfecha.Value.Month.ToString() + "/" + sqlfecha.Value.Year.ToString() + " " + sqlfecha.Value.Hour + ":" + sqlfecha.Value.Minute + ":" + sqlfecha.Value.Second + "'";
            }
            return Ret;
        }

        /// <summary>
        /// Escribe en el Archivo de Log
        /// </summary>
        /// <param name="Mensaje"></param>
        public static void WriteErrorLog(string ErrMensaje)
        {
            StreamWriter strStreamWriter = null;
            try
            {

                //string Archivo = HttpContext.Current.Server.MapPath("/") + clsCtrlApplication.LogErrorFile;
                string Archivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Hemodinamia.log";

                //strStreamWriter = new StreamWriter(strStreamW, System.Text.Encoding.UTF8);                                 

                //Se abre el archivo y si este no existe se crea               
                if (File.Exists(Archivo))
                    strStreamWriter = File.AppendText(Archivo);
                else
                    strStreamWriter = File.CreateText(Archivo);
                string Linea = "< FECHA='" + DateTime.Now.ToString() + "' ERROR='" + ErrMensaje + "' />";
                //Escribimos la línea en el achivo de texto
                strStreamWriter.WriteLine(Linea);
                strStreamWriter.Flush();
            }
            catch
            {
                MessageBox.Show("ERROR DE ESCRITURA EN DISCO (ErrorVerifExe.log): " + ErrMensaje);
            }
            finally
            {
                if (strStreamWriter != null)
                {
                    strStreamWriter.Close();
                }
            }
        }


        public static void WriteErrorLogMigracionEstensta(string ErrMensaje)
        {
            StreamWriter strStreamWriter = null;
            try
            {

                //string Archivo = HttpContext.Current.Server.MapPath("/") + clsCtrlApplication.LogErrorFile;
                string Archivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MigracionEstensa.log";

                //strStreamWriter = new StreamWriter(strStreamW, System.Text.Encoding.UTF8);                                 

                //Se abre el archivo y si este no existe se crea               
                if (File.Exists(Archivo))
                    strStreamWriter = File.AppendText(Archivo);
                else
                    strStreamWriter = File.CreateText(Archivo);
                string Linea = "< FECHA='" + DateTime.Now.ToString() + "' ERROR='" + ErrMensaje + "' />";
                //Escribimos la línea en el achivo de texto
                strStreamWriter.WriteLine(Linea);
                strStreamWriter.Flush();
            }
            catch
            {
                MessageBox.Show("ERROR DE ESCRITURA EN DISCO (ErrorVerifExe.log): " + ErrMensaje);
            }
            finally
            {
                if (strStreamWriter != null)
                {
                    strStreamWriter.Close();
                }
            }
        }


        public static string RecortarStr(string Texto, int LargoMax, int LargoTitulo)
        {
            string Ret = Texto;
            int TotCorte = LargoMax - LargoTitulo - 4;
            if (Texto.Length > TotCorte)
            {
                Ret = Texto.Substring(0, TotCorte) + "...";
            }

            return Ret;
        }

        public static string TruncarStr(string Texto, int LargoMax)
        {
            string Ret = Texto;
            if (Texto.Length > LargoMax)
            {
                Ret = Texto.Substring(0, LargoMax);
            }

            return Ret;
        }
    }
}
