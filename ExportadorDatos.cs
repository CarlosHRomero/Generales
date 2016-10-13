using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Generales
{
    public class ExportadorDatos
    {
        public static bool ExportarListaAExcel<T>(List<T> lista)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);
                Type tipo = typeof(T);
                IList<PropertyInfo> campos = new List<PropertyInfo>(tipo.GetProperties());
                int columnIndex = 0;

                foreach (PropertyInfo campo in campos)
                {
                    columnIndex++;
                    excel.Cells[1, columnIndex] = campo.Name;
                }
                int rowIndex = 0;
                foreach (T row in lista)
                {
                    rowIndex++;
                    columnIndex = 0;
                    foreach (PropertyInfo campo in campos)
                    {
                        columnIndex++;
                        object obj = campo.GetValue(row, null);

                        if (obj == null)
                        {
                            continue;
                        }
                        string dato = obj.ToString();
                        DateTime val;
                        if (DateTime.TryParse(dato, out val) == true)
                            dato = val.ToShortDateString();
                        Microsoft.Office.Interop.Excel.Range range = excel.Cells[rowIndex + 1, columnIndex];
                        range.Value = dato;
                        range.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                        //excel.Cells[rowIndex + 1, ColumnIndex]
                    }
                }
                excel.Visible = true;
                return true;

            }
            catch (Exception ex)
            {
                Mensajes.msgError(ex);
                throw;
                return false;
            }
        }

        public static bool ExportarDataGridAExcel(DataGridView dgv)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Workbooks.Add(true);
                int columnIndex = 0;
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    columnIndex++;
                    excel.Cells[1, columnIndex] = column.HeaderText;
                }
                int rowIndex = 0;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    rowIndex++;
                    columnIndex = 0;
                    for(columnIndex= 0; columnIndex < dgv.Columns.Count; columnIndex++)
                    {
                        //columnIndex++;
                        object obj = row.Cells[columnIndex].Value;
                         
                        if (obj == null)
                        {
                            continue;
                        }
                        string dato = obj.ToString();
                        DateTime val;
                        if (DateTime.TryParse(dato, out val) == true)
                            dato = val.ToShortDateString();
                        Microsoft.Office.Interop.Excel.Range range = excel.Cells[rowIndex + 1, columnIndex+1];
                        range.Value = dato;
                        range.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                        //excel.Cells[rowIndex + 1, ColumnIndex]
                    }
                }
                excel.Visible = true;
                return true;

            }
            catch (Exception ex)
            {                
                Utiles.WriteErrorLog(ex.Message);
                Mensajes.msgErrorExcel();
                return false;
            }
        }

        
    }
}
