using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel;
using LumenWorks.Framework.IO.Csv;

namespace DataIntegrationTool.HelperMethods
{
    public static class OpenAndImportFiles
    {
        public static string GetFile()
        {
            var ofd = new OpenFileDialog { Title = @"Select file to Import", Filter = @"*.xls, *.xlsm,*.xlsx,*.csv |*.xls;*.xlsm;*.xlsx;*.csv" };
            var fileName = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : string.Empty;

            return fileName;
        }

        public static DataView ImportCSV(string fileName)
        {
            var table = new DataTable();

            using (var csv = new CsvReader(new StreamReader(fileName, Encoding.Default),true))
            {
          
                csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;
                table.Load(csv);
            }

            return table.DefaultView;
        }

        public static DataView ImportExcel(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                var excelReader = fileName.EndsWith(".xlsx") || fileName.EndsWith(".xlsm") ? ExcelReaderFactory.CreateOpenXmlReader(stream) :
                                                                ExcelReaderFactory.CreateBinaryReader(stream);
                excelReader.IsFirstRowAsColumnNames = true;
                return excelReader.AsDataSet().Tables[0].DefaultView;
            }
        }
    }
}
