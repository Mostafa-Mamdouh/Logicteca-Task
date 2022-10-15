using Aspose.Cells;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using System.Data;
using System.Reflection;

namespace API.Helper
{
    public class ExcelHelper
    {

        public static List<DataRow> GetDataRows(Worksheet worksheet)
        {
    
                worksheet.Cells.DeleteBlankRows();
                worksheet.Cells.DeleteBlankColumns();
                //Get the farthest (last) row's index (zero-based) which contains data.
                int lastRowIndex = worksheet.Cells.GetLastDataRow(3);
                //Get the farthest (last) column's index (zero-based) which contains data.
                int lastColIndex = worksheet.Cells.MaxDataColumn + 1;
                //Exporting the contents to DataTable.
                DataTable dt = worksheet.Cells.ExportDataTable(1, 0, lastRowIndex, lastColIndex, true);
                return dt.Rows.OfType<DataRow>().ToList();
            

        }
    }
}
