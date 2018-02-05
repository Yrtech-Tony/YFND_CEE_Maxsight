using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INFI.API.Models;
using INFI.API.Context;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Net.Http.Headers;
using INFI.API.Models.AttachmentMngDto;
using INFI.API.Common;
using Newtonsoft.Json;
using INFI.API.Models.ImprovementDto;

namespace INFI.API.Service
{
    public interface IUploadFileService
    {
        Task<IEnumerable<FruitDto>> FruitQuery();
        Task<APIResult> Create(DocumentInput input);
    }

    public class UploadFileService : IUploadFileService
    {
        public async Task<IEnumerable<FruitDto>> FruitQuery()
        {
            string sqlText = @"Select * from Fruit";
            using (var conn = new SqlConnection(DapperContext.Current.Configuration["Data:DefaultConnection:ConnectionString"]))
            {
                return await conn.QueryAsync<FruitDto>(sqlText);
            }
        }
        private IHostingEnvironment _hostingEnvironment;
        //public UploadFileService()
        //{
        //}
        public UploadFileService(
            IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<APIResult> Create(DocumentInput input)
        {
            var file = input.File;
            input.Name = file.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
            var parsedContentDisposition =
                ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var parsedFilename = HeaderUtilities.RemoveQuotes(parsedContentDisposition.FileName);
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(parsedFilename);
            if (!Directory.Exists("\\\\CNVFCUSTIF01.home.e-kmall.com" + "\\uploads\\"))
            {
                try
                {
                    Directory.CreateDirectory("\\\\CNVFCUSTIF01.home.e-kmall.com" + "\\uploads\\");
                }
                catch
                {

                }
            }

            var fileDestination = Path.Combine("\\\\CNVFCUSTIF01.home.e-kmall.com", "uploads", filename);
            var upload = new AttachDto()
            {
                AttachName = input.Name,
                Url = "http://infi.qa.elandcloud.com/uploads/" + filename,
                SeqNo = input.Id
            };

            using (var fileStream = new FileStream(fileDestination, FileMode.Create))
            {
                var inputStream = file.OpenReadStream();
                await inputStream.CopyToAsync(fileStream); //JsonConvert.SerializeObject(t)
            };
            IEnumerable<AttachDto> resultInfo = new AttachDto[] { upload };
            APIResult result = new APIResult { Body = CommonHelper.EncodeDto<AttachDto>(resultInfo), ResultCode = ResultType.Success, Msg = "" };
            return result;
        }
    }
}
