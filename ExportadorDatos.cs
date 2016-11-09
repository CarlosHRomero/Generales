using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using System.Data;

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

        static public bool ExportarExcel(DataSet ds, string archivo)
        {
            try
            {
                using (var workbook = SpreadsheetDocument.Create(archivo, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
                {
                    var workbookPart = workbook.AddWorkbookPart();

                    workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                    workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();

                    foreach (System.Data.DataTable table in ds.Tables)
                    {

                        var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                        var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                        sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

                        DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                        string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                        uint sheetId = 1;
                        if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Count() > 0)
                        {
                            sheetId =
                                sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                        }

                        DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet() { Id = relationshipId, SheetId = sheetId, Name = table.TableName };
                        sheets.Append(sheet);

                        DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                        List<String> columns = new List<string>();
                        foreach (System.Data.DataColumn column in table.Columns)
                        {
                            columns.Add(column.ColumnName);

                            DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName);
                            headerRow.AppendChild(cell);
                        }


                        sheetData.AppendChild(headerRow);

                        foreach (System.Data.DataRow dsrow in table.Rows)
                        {
                            DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            DateTime val;
                            string dato;
                            foreach (String col in columns)
                            {
                                DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                dato = dsrow[col].ToString();

                                if (DateTime.TryParse(dato, out val) == true)
                                    dato = val.ToString("yyyy/MM/dd");
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dato); //
                                newRow.AppendChild(cell);
                            }

                            sheetData.AppendChild(newRow);
                        }
                    }
                }
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                excel.Workbooks.Open(archivo);
                excel.Visible = true;
                return true;
            }
            catch (Exception ex)
            {
                Utiles.WriteErrorLog(ex.Message);
                return false;
            }
        }
    }
}
