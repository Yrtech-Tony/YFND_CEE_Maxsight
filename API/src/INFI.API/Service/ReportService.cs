using Dapper;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.PlanTaskMngDto;
using INFI.API.Models.ReportDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Service
{
    public interface IReportService
    {
        Task<APIResult> GetAttachmentByUserId(int userId, string sourceType, string sDate, string eDate);
        Task<APIResult> SaveReportAttachment(SaveReportAttachdto param);
        Task<APIResult> UpdateAttachmentDownloadCnt(int id);
        Task<APIResult> GetPlansListForExcelDownload(string SDate, string EDate, string UserId, string DisId);
        Task<APIResult> GetRegionByUserId(int inuserid, string usertype, string zonetype);
        Task<APIResult> GetAreaByRegionId(int inuserid, int regionid, string usertype);
    }
    public class ReportService : IReportService
    {
        public async Task<APIResult> GetAttachmentByUserId(int userId, string sourceType, string sDate, string eDate)
        {
            try
            {
                string spName = @"up_RMMT_REP_GetAttachmentByUserId_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@UserId", userId, DbType.Int32);
                dp.Add("@SourceType", sourceType, DbType.String);
                dp.Add("@SDate", sDate, DbType.String);
                dp.Add("@Edate", eDate, DbType.String);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<ReportAttachmentDto> list = await conn.QueryAsync<ReportAttachmentDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ReportAttachmentDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> SaveReportAttachment(SaveReportAttachdto param)
        {
            try
            {
                string AttachXml = CommonHelper.Serializer(typeof(List<ReportAttachmentDto>), param.AttachList);
                string spName = @"up_RMMT_REP_Attachment_C";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@AttachXml", AttachXml, DbType.Xml);
                dp.Add("@UserId", param.UserId, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {

                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
                }

            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> UpdateAttachmentDownloadCnt(int id)
        {
            try
            {
                string spName = @"up_RMMT_REP_AttachmentDownloadCnt_C";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Id", id, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {

                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
                }

            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> GetPlansListForExcelDownload(string SDate, string EDate, string UserId, string DisId)
        {
            try
            {
                string spName = @"up_RMMT_TAS_GetPlansListFroExcelDownLoad_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@SDate", SDate, DbType.String);
                dp.Add("@Edate", EDate, DbType.String);
                dp.Add("@UserId", UserId, DbType.Int32);
                dp.Add("@DisId", DisId, DbType.Int32);
               
                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<PlansListDto> list = await conn.QueryAsync<PlansListDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<PlansListDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }


        public async Task<APIResult> GetRegionByUserId(int inuserid, string usertype, string zonetype)
        {
            try
            {
                string spName = @"up_MBMS_REP_GetRegionByUserId_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@UserId", inuserid, DbType.Int32);
                dp.Add("@UserType", usertype, DbType.String);
                dp.Add("@ZoneType", zonetype, DbType.String);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<StatusDto> list = await conn.QueryAsync<StatusDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<StatusDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }
        public async Task<APIResult> GetAreaByRegionId(int inuserid, int regionid, string usertype)
        {
            try
            {
                string spName = @"up_MBMS_REP_GetAreaByRegionId_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@InUserId", inuserid, DbType.Int32);
                dp.Add("@RegionId", regionid, DbType.Int32);
                dp.Add("@UserType", usertype, DbType.String);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<StatusDto> list = await conn.QueryAsync<StatusDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<StatusDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }
    }
}
