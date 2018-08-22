
using System.Threading.Tasks;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Linq;
using INFI.API.Models;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models.AttachmentMngDto;
using INFI.API.Models.AppealMngDto;
using INFI.API.Models.NotifiMngDto;

namespace INFI.API.Service
{
    public interface IAppealMngService
    {
        Task<APIResult> AppealInfoReg(AppealInfo param);
        Task<APIResult> AppealInfoSearch(int aPId);
        Task<APIResult> SearchApplealInfoList(string sdate, string edate, string sourceType, string carId, string areaId, string zoneId, string disId, string appealResult);
        Task<APIResult> UpdateApplealInfo(string userId, string id, string appealResult, string approvalRemark, List<AttachmentMngDto> approalAttachList);
        Task<APIResult> SearchChapterType();

    }
    public class AppealMngService : IAppealMngService
    {
        public async Task<APIResult> AppealInfoReg(AppealInfo param)
        {
            string AttachmentList = CommonHelper.Serializer(typeof(List<AttachmentMngDto>), param.AttachmentList);
            string spName = @"up_MBMS_APP_AppealInfoReg_C";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@APId", param.Id, DbType.Int32);
            dp.Add("@TPId", param.TPId, DbType.Int32);
            dp.Add("@TIId", param.TIId, DbType.Int32);
            dp.Add("@AppealContent", param.AppealContent, DbType.String);
            dp.Add("@ApprealUserId", param.ApprealUserId, DbType.Int32);

            dp.Add("@AttachmentList", AttachmentList, DbType.Xml);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                    }
                    finally
                    {
                        tran.Dispose();
                    }
                }
                return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
            }

        }

        public async Task<APIResult> AppealInfoSearch(int aPId)
        {
            try
            {
                string spName = @"up_MBMS_APP_AppealInfoSearch_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@APId", aPId, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    var list = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    var r1 = list.ReadAsync().Result;
                    var r2 = list.ReadAsync().Result;
                    var r3 = list.ReadAsync().Result;

                    List<AppealInfo> lst = JsonConvert.DeserializeObject<List<AppealInfo>>(JsonConvert.SerializeObject(r1));
                    List<AttachmentMngDto> lst2 = JsonConvert.DeserializeObject<List<AttachmentMngDto>>(JsonConvert.SerializeObject(r2));
                    List<AttachmentMngDto> lst3 = JsonConvert.DeserializeObject<List<AttachmentMngDto>>(JsonConvert.SerializeObject(r3));

                    AppealInfo dto = lst.FirstOrDefault<AppealInfo>();
                    dto.AttachmentList = lst2;
                    dto.ApproalAttachList = lst3;

                    string message = "";
                    if (lst.Count == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<AppealInfo>(dto), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }
        public async Task<APIResult> SearchApplealInfoList(string sdate, string edate, string sourceType, string carId, string areaId, string zoneId, string disId, string appealResult)
        {
            string spName = @"up_MBMS_APP_AppealInfo_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@SDate", sdate, DbType.String);
            dp.Add("@EDate", edate, DbType.String);
            dp.Add("@SourceTypeCode", sourceType, DbType.String);
            dp.Add("@AreaId", areaId, DbType.Int32);
            dp.Add("@ZoneId", zoneId, DbType.Int32);
            dp.Add("@DisId", disId, DbType.Int32);
            dp.Add("@AppealResult", appealResult, DbType.String);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<AppealListDto> list = await conn.QueryAsync<AppealListDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if ((list as List<AppealListDto>).Count == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<AppealListDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }
        }
        public async Task<APIResult> UpdateApplealInfo(string userId, string id, string appealResult, string approvalRemark, List<AttachmentMngDto> approalAttachList)
        {
            string appList = CommonHelper.Serializer(typeof(List<AttachmentMngDto>), approalAttachList);
            string spName = @"up_MBMS_APP_AppealInfo_U";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserId", userId, DbType.Int32);
            dp.Add("@Id", id, DbType.Int32);
            dp.Add("@AppealResult", appealResult, DbType.String);
            dp.Add("@ApprovalRemark", approvalRemark, DbType.String);
            dp.Add("@ApproalAttachList", appList, DbType.Xml);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                    }
                    finally
                    {
                        tran.Dispose();
                    }

                }
                return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
            }
        }
        /// <summary>
        /// add by moujunsheng
        /// </summary>
        /// <returns></returns>
        public async Task<APIResult> SearchChapterType()
        {
            string spName = @"up_MBMS_APP_ChapterType_R";

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                try
                {
                    IEnumerable<ChapterTypeDto> ChapterTypeList = await conn.QueryAsync<ChapterTypeDto>(spName, null, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ChapterTypeDto>(ChapterTypeList), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }
        }
    }
}
