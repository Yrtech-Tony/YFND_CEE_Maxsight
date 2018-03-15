using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using NetMidApi.Common.ApiPack;
using NetMidApi.Models;
using NetMidApi.Service;
using Newtonsoft.Json;
using Aliyun.OSS;
using System;
using System.IO;
using System.Net.Http;
using System.Configuration;
using NetMidApi.Common;

namespace NetMidApi.Controllers
{
    public class ExcelController : CommonController
    {
        IExcelService _excelService;
        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }
        [HttpGet]
        public async Task<IHttpActionResult> One()
        {
            string fullFileName = HttpContext.Current.Request.QueryString["fullFileName"];
            string sheetName = HttpContext.Current.Request.QueryString["sheetName"];
            if (string.IsNullOrWhiteSpace(fullFileName))
            {
                return BadRequestEx("FullFileName");
            }
            else if (string.IsNullOrWhiteSpace(sheetName))
            {
                return BadRequestEx("SheetName");
            }
            return OkEx(await _excelService.ParseOne(fullFileName, sheetName));
        }
        public async Task<IHttpActionResult> GetList(string FullFileName)
        {
            Utils.log("start list");
            Utils.log("excelDto.FullFileName:" + FullFileName);
            return null;
        }

        [HttpPost]
        public async Task<IHttpActionResult> List([FromBody]ExcelDto excelDto)
        {
            Utils.log("start list");
            Utils.log("excelDto.FullFileName:"+excelDto.FullFileName);
            try
            {
                if (excelDto == null)
                {
                    return BadRequestEx("FullFileName,SheetNameList");
                }
                else if (string.IsNullOrWhiteSpace(excelDto.FullFileName))
                {
                    return BadRequestEx("FullFileName");
                }
                else if (string.IsNullOrWhiteSpace(excelDto.SheetNameList))
                {
                    return BadRequestEx("SheetNameList");
                }

                IEnumerable<string> sheetList = JsonConvert.DeserializeObject<IEnumerable<string>>(excelDto.SheetNameList);

                if (sheetList == null)
                {
                    return BadRequestEx("SheetNameList");
                }
                return OkEx(await _excelService.ParseList(excelDto.FullFileName, sheetList));
            }catch(Exception ex){
                Utils.log(ex.ToString());
                return ErrorEx(ex.ToString());
            }            
        }
        [HttpPost]
        public string UploadToOss([FromBody]OssForIosDto obj)
        {
            //using (FileStream fs = new FileStream(@"D:\minmin2016\"+obj.Filename, FileMode.Create))
            //{
            //    fs.Write(obj.Bytes,0,obj.Bytes.Length);
            //    fs.Close();
            //}
            Stream stream= new MemoryStream(obj.Bytes);
            string accessKeyId = ConfigurationManager.AppSettings["AccessKeyId"];//"3JkljJxvXgjLz80X";
            string accessKeySecret = ConfigurationManager.AppSettings["AccessKeySecret"];//"L2ERHORPk3WkjqfGUb27RlxvT8x5f3";
            string endpoint = ConfigurationManager.AppSettings["Endpoint"];//"oss-cn-beijing.aliyuncs.com";
            string dir = ConfigurationManager.AppSettings["OssKey"];//"AreaTool/";
            string bucket = ConfigurationManager.AppSettings["Bucket"];//"vgic";
            string domain = ConfigurationManager.AppSettings["Domain"];// "http://vgic.oss-cn-beijing.aliyuncs.com/";
            OssClient ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
            string key = string.Empty;
            if (obj.ExtraParam.ToUpper() == "L")
            {
                key = dir + obj.Filename;
            }
            else
            {
                key = dir + DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + obj.Filename.Substring(obj.Filename.LastIndexOf("."));
            }
            ossClient.PutObject(bucket, key, stream);
            return domain + key;

        }
        
        public class OssForIosDto
        {
            public byte[] Bytes { get; set; }
            public string Filename { get; set; }
            public string ExtraParam { get; set; }
        }
    }
}
