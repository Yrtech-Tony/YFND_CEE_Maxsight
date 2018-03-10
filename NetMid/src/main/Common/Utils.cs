using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetMidApi.Common
{
    public class Utils
    {
        public static string StrToMD5(string str)
        {
            byte[] data = Encoding.GetEncoding("GB2312").GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);

            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }
            return OutString.ToLower();
        }

        public static DateTime String2Time(string value, DateTime defalut)
        {
            try
            {
                return DateTime.Parse(value);
            }
            catch
            {
                return defalut;
            }
        }

        //图片 转为    base64编码的文本
        public static string ImgToBase64String(string filename)
        {
            String strbaser64 = "";
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open);
                byte[] arr = new byte[fs.Length];
                fs.Read(arr, 0, (int)fs.Length);
                fs.Close();
                strbaser64 = Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ImgToBase64String 转换失败\nException:" + ex.Message);
            }
            return strbaser64;
        }

        public static void log(string message)
        {
            string appDomainPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = appDomainPath + @"\" + "Log" + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            //File.Create(fileName);
            if (!Directory.Exists(appDomainPath + @"\" + "Log"))
            {
                Directory.CreateDirectory(appDomainPath + @"\" + "Log");
            }
            if (!Directory.Exists(appDomainPath + @"\" + "Log" + @"\" + DateTime.Now.ToString("yyyyMMdd")))
            {
                Directory.CreateDirectory(appDomainPath + @"\" + "Log" + @"\" + DateTime.Now.ToString("yyyyMMdd"));
            }
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                byte[] by = WriteStringToByte(message, fs);
                fs.Flush();
                fs.Close();
            }
        }
        public static byte[] WriteStringToByte(string str, FileStream fs)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(str);
            fs.Write(info, 0, info.Length);
            return info;
        }

        public static double String2Double(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            try
            {
                return double.Parse(value);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static double Decimal2Double(decimal? value)
        {
            if (value.HasValue)
            {
                return (double)value.Value;
            }
            else
            {
                return 0;
            }
        }

        public static string DoubleFormat(double value)
        {
            return string.Format("{0:0.00}", value);
        }
    }
}
