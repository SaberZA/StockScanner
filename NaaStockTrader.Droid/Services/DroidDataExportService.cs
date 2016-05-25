using System;
using System.Collections.Generic;
using Android.Content;
using NaaStockScanner.Core.Interfaces;
using NaaStockTrader.Core.Services.ExportData;
using Android.OS;
using Java.IO;
using System.Text;
using System.Diagnostics;

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
            String root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            File myDir = new File(root + "/NaaStockScanner");
            myDir.Mkdirs();
            String fname = "Stock-" + DateTime.Now.ToString("yyyy-mm-dd-HH-MM-ss") + ".csv";
            File file = new File(myDir, fname);

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

    }
}