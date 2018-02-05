using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Dapper;

namespace NetMidApi.Service
{
    public interface IExcelService
    {
        Task<string> ParseOne(string filePath, string sheetName);
        Task<string> ParseList(string filePath, IEnumerable<string> sheetNameList);
    }
    public class ExcelService : IExcelService
    {
        public async Task<string> ParseOne(string fullFileName, string sheetName)
        {
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + fullFileName + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            string strExcel = string.Format("select * from [{0}$]", sheetName);
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                var result = await conn.QueryAsync(strExcel);
                return await Task.Factory.StartNew(() => JsonConvert.SerializeObject(result));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <param name="sheetNameList"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public async Task<string> ParseList(string fullFileName, IEnumerable<string> sheetNameList)
        {
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + fullFileName + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            string sqlText = string.Empty;

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                Dictionary<string, object> sheetListJson = new Dictionary<string, object>();
                foreach (var sheetName in sheetNameList)
                {
                    sqlText = $"select * from [{sheetName}$]";
                    var result = await conn.QueryAsync(sqlText);
                    sheetListJson.Add(sheetName, result);
                }
                return await Task.Factory.StartNew(() => JsonConvert.SerializeObject(sheetListJson));
            }
        }


    }
}