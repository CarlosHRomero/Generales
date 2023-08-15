using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

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
                    excel.Columns[columnIndex].ColumnWidth = column.Width/7;
                    var ran = excel.Columns[columnIndex];

                }
                int rowIndex = 1;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    rowIndex++;
                    columnIndex = 0;
                    for (columnIndex = 0; columnIndex < dgv.Columns.Count; columnIndex++)
                    {
                        //columnIndex++;
                        object obj = row.Cells[columnIndex].Value;
                        DataGridViewCell cell = row.Cells[columnIndex];
                        Microsoft.Office.Interop.Excel.Range range = excel.Cells[rowIndex + 1, columnIndex + 1];
                        int c;
                        if ((c = System.Drawing.ColorTranslator.ToOle(row.DefaultCellStyle.BackColor)) != 0)
                        {
                            range.Interior.Color = c;
                            range.Font.Color = System.Drawing.ColorTranslator.ToOle(row.DefaultCellStyle.ForeColor);
                        }
                        if (row.DefaultCellStyle.Font != null)
                        {
                            if (row.DefaultCellStyle.Font.Bold)
                            {
                                range.Font.FontStyle = "Bold";
                            }
                        }
                        if (obj == null)
                        {
                            continue;
                        }
                        string dato = obj.ToString();
                        DateTime val;
                        if (dato.Length >= 10 && DateTime.TryParse(dato, out val) == true)
                            dato = val.ToString("yyyy/MM/dd");
                        range.Value = dato;
                        if (dgv.Columns[columnIndex].DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                            range.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        else
                            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                        //excel.Cells[rowIndex + 1, ColumnIndex]
                    }
                }
                excel.Visible = true;
                return true;

            }
            catch (Exception ex)
            {
                Utiles.WriteErrorLogLocal(ex.Message);
                Mensajes.msgErrorExcel();
                return false;
            }
        }


        static public bool ExportaListaAExcel2<T>(List<T> lista, string archivo, string hoja)
        {
            try
            {
                using (var workbook = SpreadsheetDocument.Create(archivo, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
                {
                    var workbookPart = workbook.AddWorkbookPart();
                    workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
                    workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();
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

                    DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet() { Id = relationshipId, SheetId = sheetId, Name = hoja};
                    sheets.Append(sheet);

                    DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    List<String> columns = new List<string>();
                    IList<PropertyInfo> campos = new List<PropertyInfo>(typeof(T).GetProperties());
                    foreach (PropertyInfo campo in campos)
                    {
                        columns.Add(campo.Name);
                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(campo.Name);
                        headerRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(headerRow);
                    foreach (T row in lista)
                    {
                        DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                        DateTime val;
                        string dato;
                        foreach (String col in columns)
                        {
                            PropertyInfo campo = campos.ToList().Find(x => x.Name == col);
                            DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            object obj = campo.GetValue(row, null);
                            if (obj == null)
                                dato= "";
                            else
                                dato = obj.ToString();

                            if (dato.Length >= 10 && DateTime.TryParse(dato, out val) == true)
                                dato = val.ToString("yyyy/MM/dd");
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dato); //
                            newRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(newRow);
                    }
                    
                }
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Workbooks.Open(archivo);
                excel.Visible = true;
                return true;
            }
            catch (Exception ex)
            {
                Utiles.WriteErrorLogLocal(ex.Message);
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

                                if (dato.Length >= 10 && DateTime.TryParse(dato, out val) == true)
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
                Utiles.WriteErrorLogLocal(ex.Message);
                return false;
            }
        }


        public static bool LargeExport(DataTable dt, string filename)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook))
            {
                //this list of attributes will be used when writing a start element
                List<OpenXmlAttribute> attributes;
                OpenXmlWriter writer;
                int columnNum = 0;
                document.AddWorkbookPart();
                WorksheetPart workSheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();

                writer = OpenXmlWriter.Create(workSheetPart);
                writer.WriteStartElement(new Worksheet());
                writer.WriteStartElement(new SheetData());
                List<String> columns = new List<string>();
                attributes = new List<OpenXmlAttribute>();
                attributes.Add(new OpenXmlAttribute("r", null, "1"));
                writer.WriteStartElement(new Row(), attributes);

                foreach (System.Data.DataColumn column in dt.Columns)
                {
                    columns.Add(column.ColumnName);
                    //create a new list of attributes
                    // add the row index attribute to the list
                    attributes = new List<OpenXmlAttribute>();
                    // add data type attribute - in this case inline string (you might want to look at the shared strings table)
                    attributes.Add(new OpenXmlAttribute("t", null, "str"));
                    //add the cell reference attribute
                    string cName = GetColumnName(columnNum);
                    attributes.Add(new OpenXmlAttribute("r", "", string.Format("{0}{1}", cName, "1")));
                    writer.WriteStartElement(new Cell(), attributes);
                    writer.WriteElement(new CellValue(column.ColumnName));
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                int rowNum = 1;
                foreach (System.Data.DataRow dsrow in dt.Rows)
                {
                    rowNum++;
                    //create a new list of attributes
                    attributes = new List<OpenXmlAttribute>();
                    // add the row index attribute to the list
                    attributes.Add(new OpenXmlAttribute("r", null, rowNum.ToString()));

                    //write the row start element with the row index attribute
                    writer.WriteStartElement(new Row(), attributes);
                    columnNum = 0;
                    foreach (String col in columns)
                    {
                        columnNum++;
                        //reset the list of attributes
                        attributes = new List<OpenXmlAttribute>();
                        // add data type attribute - in this case inline string (you might want to look at the shared strings table)
                        attributes.Add(new OpenXmlAttribute("t", null, "str"));
                        //add the cell reference attribute
                        string cName=GetColumnName(columnNum);
                        //if (columnNum == 360)
                        //    throw new Exception();
                        attributes.Add(new OpenXmlAttribute("r", "", string.Format("{0}{1}", cName, rowNum)));

                        //write the cell start element with the type and reference attributes
                        writer.WriteStartElement(new Cell(), attributes);
                        //write the cell value
                        string dato = dsrow[col].ToString();
                        //if (string.IsNullOrEmpty(dato))
                        //    throw new Exception();
                        DateTime val;
                        if (dato.Length >= 10 && DateTime.TryParse(dato, out val) == true)
                        {
                            string colname = dt.Columns[col].ColumnName;
                            if (colname.Substring(colname.Length -2) == "_F")
                                dato = val.ToString("yyyy/MM/dd");
                            if (colname.Substring(colname.Length - 2) == "_H")
                                dato = val.ToString("HH:mm");
                        }
                        char c = Convert.ToChar(0x1D);
                       
                        dato=dato.Replace(c, ' ');
                        c = Convert.ToChar(0x1C);
                        dato = dato.Replace(c, ' ');
                        dato=dato.Replace('\r', ' ');
                        dato=dato.Replace('\n', ' ');

                        try
                        {
                            writer.WriteElement(new CellValue(dato));
                        }
                        catch(Exception ex)
                        {
                            continue;
                        }
                        // write the end cell element
                        writer.WriteEndElement();
                    }

                    // write the end row element
                    writer.WriteEndElement();
                }

                // write the end SheetData element
                writer.WriteEndElement();
                // write the end Worksheet element
                writer.WriteEndElement();
                writer.Close();

                writer = OpenXmlWriter.Create(document.WorkbookPart);
                writer.WriteStartElement(new Workbook());
                writer.WriteStartElement(new Sheets());

                writer.WriteElement(new Sheet()
                {
                    Name = "Large Sheet",
                    SheetId = 1,
                    Id = document.WorkbookPart.GetIdOfPart(workSheetPart)
                });

                // End Sheets
                writer.WriteEndElement();
                // End Workbook
                writer.WriteEndElement();
                writer.Close();
                document.Close();
            }
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Workbooks.Open(filename);
            excel.Visible = true;
            return true;
        }
        private static string GetColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modifier;

            while (dividend > 0)
            {
                modifier = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modifier).ToString() + columnName;
                dividend = (int)((dividend - modifier) / 26);
            }

            return columnName;
        }
    }
}
