using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace ARM_Clinic_API_Helper
{
    public class Helper
    {
        public DataTable ToDataTable<T>(List<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }
        public string IsNull(string sNull)
        {
            string c = string.Empty;
            try
            {

                if (sNull == null)
                {
                    c = string.Empty;
                }
                else
                {
                    c = sNull;

                }
            }
            catch (Exception)
            {

                c = string.Empty;
            }
            return c;

        }
        public bool CheckNull(string sNull)
        {

            bool c = true;
            if (sNull == null)
                c = true;
            else
            {
                if (sNull.Trim() == "" || sNull.Trim() == "0" || sNull.Trim() == "0.0" || sNull.Trim() == "0.00")
                    c = true;
                else
                    c = false;
            }
            return c;
        }
        public bool IsNull<T>(IList<T> List)
        {
            return (List == null || List.Count < 1);
        }

        public void DeleteOldFile(string destinationpath)
        {
            string strMessage = string.Empty;
            try
            {
                if (!File.Exists(destinationpath))
                {
                    using (FileStream fs = File.Create(destinationpath)) { }
                }

                if (File.Exists(destinationpath))
                    File.Delete(destinationpath);

                strMessage = "Delete file successful.";

            }
            catch (Exception e)
            {
                strMessage = e.Message;
            }
        }

        public string ConvertstringtoDatetime(string date)
        {
            string sdate = string.Empty;
            try
            {
                if (date != null && date.Trim() != string.Empty)
                {
                    if (date.IndexOf('/') > 0)
                    {
                        DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", new CultureInfo("en-US"));
                        sdate = String.Format("{0:0000}", dt.Year) + "-" + String.Format("{0:00}", dt.Month) + "-" + String.Format("{0:00}", dt.Day);
                    }
                    else
                    {
                        sdate = date;
                    }
                }
                else if (date.Trim() == string.Empty)
                {
                    sdate = null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return sdate;
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public  string ReadFileToBase64(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            byte[] bytes;
            using (var fileStream = File.OpenRead(filePath))
            {
                bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, (int)fileStream.Length);
            }

            return Convert.ToBase64String(bytes);
        }
    }
}
