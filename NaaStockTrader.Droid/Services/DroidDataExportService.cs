using System;
using System.Collections.Generic;
using Android.Content;
using NaaStockScanner.Core.Interfaces;
using NaaStockTrader.Core.Services.ExportData;
using Android.OS;
using Java.IO;
using System.Text;
using System.Diagnostics;
//using OfficeOpenXml;
using System.Data;
using System.IO;

namespace NaaStockScanner.Droid
{
    public class DroidDataExportService : IExportDataService
    {
        private Context applicationContext;
        
        public DroidDataExportService(Context applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public void SaveCsvToDevice(List<StockItem> stockItems)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Stock Code,Bar Code,Stock Description,Retail Price Incl.,Count,Date Updated");
            foreach (var item in stockItems)
            {
                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", item.StockCode, item.BarCode, item.StockDescription, item.StockPrice,item.StockQuantity, item.DateUpdated));
            }
            SaveCsvToDevice(sb.ToString());
        }

        private void SaveCsvToDevice(string csvData)
        {
            var canWrite = isExternalStorageWritable();
            var canRead = isExternalStorageReadable();
            Java.IO.File file = CreateFile("csv");

            if (file.Exists()) file.Delete();
            try
            {
                FileOutputStream outputStream = new FileOutputStream(file);
                outputStream.Write(Encoding.ASCII.GetBytes(csvData));
                outputStream.Flush();
                outputStream.Close();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        

        /* Checks if external storage is available for read and write */
        public bool isExternalStorageWritable()
        {
            String state = Android.OS.Environment.ExternalStorageState;
            if (Android.OS.Environment.MediaMounted.Equals(state))
            {
                return true;
            }
            return false;
        }

        /* Checks if external storage is available to at least read */
        public bool isExternalStorageReadable()
        {
            String state = Android.OS.Environment.ExternalStorageState;
            if (Android.OS.Environment.MediaMounted.Equals(state) ||
                Android.OS.Environment.MediaMountedReadOnly.Equals(state))
            {
                return true;
            }
            return false;
        }

        //private DataColumn StockCodeColumn = new DataColumn("StockCode", typeof(string));
        //private DataColumn BarCodeColumn = new DataColumn("BarCode", typeof(string));
        //private DataColumn StockDescriptionColumn = new DataColumn("StockDescription", typeof(string));
        //private DataColumn StockQuantityColumn = new DataColumn("StockQuantity", typeof(string));
        //private DataColumn StockPriceColumn = new DataColumn("StockPrice", typeof(int));
        //private DataColumn DateUpdatedColumn = new DataColumn("DateUpdated", typeof(string));

        //public void SaveXlsxToDevice(List<StockItem> stockItems, string sheetName)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    var excelPackage = new ExcelPackage(ms);
        //    excelPackage.Workbook.Worksheets.Add(sheetName);

        //    ExcelWorksheet ws = excelPackage.Workbook.Worksheets[1];
        //    ws.Name = sheetName; //Setting Sheet's name
        //    ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
        //    ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet


        //    var table = new DataTable();
        //    table.Columns.Add("StockCode", typeof(string));
        //    table.Columns.Add("BarCode", typeof(string));
        //    table.Columns.Add("StockDescription", typeof(string));
        //    table.Columns.Add("StockQuantity", typeof(string));
        //    table.Columns.Add("StockPrice", typeof(string));
        //    table.Columns.Add("DateUpdated", typeof(string));

        //    foreach (var stockItem in stockItems)
        //    {
        //        var row = table.NewRow();
        //        row[StockCodeColumn] = stockItem.StockCode;
        //        row[BarCodeColumn] = stockItem.BarCode;
        //        row[StockDescriptionColumn] = stockItem.StockDescription;
        //        row[StockQuantityColumn] = stockItem.StockQuantity;
        //        row[StockPriceColumn] = stockItem.StockPrice;
        //        row[DateUpdatedColumn] = stockItem.DateUpdated;

        //        table.Rows.Add(row);
        //    }
            

        //    var cellRange = ws.Cells[1, 1, stockItems.Count, 6];
        //    var newCellRange = cellRange.LoadFromDataTable(table,true);

        //    var file = CreateFile("xlsx");

        //    if (file.Exists()) file.Delete();

        //    excelPackage.Save();
        //    ms.Position = 0;
        //    //return ms;

        //    //var existingFile = new FileInfo(@"c:\temp\temp.xlsx");
        //    //if (existingFile.Exists)
        //    //    existingFile.Delete();
            
        //    try
        //    {
        //        ms.WriteTo(new FileStream(file.AbsolutePath, FileMode.Create));
        //        ms.Flush();
        //        ms.Close();

        //        //FileOutputStream outputStream = new FileOutputStream(file);
        //        //outputStream.Write(Encoding.ASCII.GetBytes(csvData));
        //        //outputStream.Flush();
        //        //outputStream.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        System.Diagnostics.Debug.WriteLine(e.Message);
        //    }
        //}

        private static Java.IO.File CreateFile(string extension)
        {
            String root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            Java.IO.File myDir = new Java.IO.File(root + "/NaaStockScanner");
            myDir.Mkdirs();
            String fname = "Stock-" + DateTime.Now.ToString("yyyy-mm-dd-HH-MM-ss") + "." + extension;
            Java.IO.File file = new Java.IO.File(myDir, fname);
            return file;
        }
    }
}