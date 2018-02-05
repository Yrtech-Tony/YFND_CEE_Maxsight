using Dapper;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.AttachmentMngDto;
using INFI.API.Models.RecordDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Service
{
    public interface IRecordService
    {
        Task<APIResult> GetRecordInfoList(int userId, string recordType, string recordSDate, string recordEDate, int DisId, string approvalStatus);

        Task<APIResult> RecordInfoReg(RecordInfoDto param);

        Task<APIResult> RecordInfoSearch(int rId);

        Task<APIResult> UpdateRecordInfo(string userId, string id, string approvalStatus, string approvalRemark, List<AttachmentMngDto> approalAttachList);
    }
    public class RecordService : IRecordService
    {
        public async Task<APIResult> GetRecordInfoList(int userId, string recordType, string recordSDate, string recordEDate, int DisId, string approvalStatus)
        {
            try
            {
                string spName = @"up_RMMT_REC_RecordInfo_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@UserId", userId, DbType.Int32);
                dp.Add("@RecordType", recordType, DbType.String);
                dp.Add("@RecordSDate", recordSDate, DbType.String);
                dp.Add("@RecordEDate", recordEDate, DbType.String);
                dp.Add("@DisId", DisId, DbType.Int32);
                dp.Add("@ApprovalStatus", approvalStatus, DbType.String);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<RecordItemDto> list = await conn.QueryAsync<RecordItemDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<RecordItemDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> RecordInfoReg(RecordInfoDto param)
        {
            string AttachmentList = CommonHelper.Serializer(typeof(List<AttachmentMngDto>), param.AttachmentList);
            string spName = @"up_RMMT_REC_RecordInfoReg_C";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@RecordType", param.RecordType, DbType.String);
            dp.Add("@RecordTitle", param.RecordTitle, DbType.String);
            dp.Add("@RecordReason", param.RecordReason, DbType.String);
            dp.Add("@RecordUserId", param.RecordUserId, DbType.Int32);
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

        public async Task<APIResult> RecordInfoSearch(int rId)
        {
            try
            {
                string spName = @"up_RMMT_REC_RecordInfo_Search_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@RId", rId, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    var list = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    var r1 = list.ReadAsync().Result;
                    var r2 = list.ReadAsync().Result;
                    var r3 = list.ReadAsync().Result;

                    List<RecordInfoDto> lst = JsonConvert.DeserializeObject<List<RecordInfoDto>>(JsonConvert.SerializeObject(r1));
                    List<AttachmentMngDto> lst2 = JsonConvert.DeserializeObject<List<AttachmentMngDto>>(JsonConvert.SerializeObject(r2));
                    List<AttachmentMngDto> lst3 = JsonConvert.DeserializeObject<List<AttachmentMngDto>>(JsonConvert.SerializeObject(r3));

                    RecordInfoDto dto = lst.FirstOrDefault<RecordInfoDto>();
                    dto.AttachmentList = lst2;
                    dto.ApproalAttachList = lst3;

                    string message = "";
                    if (lst.Count == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<RecordInfoDto>(dto), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> UpdateRecordInfo(string userId, string id, string approvalStatus, string approvalRemark, List<AttachmentMngDto> approalAttachList)
        {
            string appList = CommonHelper.Serializer(typeof(List<AttachmentMngDto>), approalAttachList);
            string spName = @"up_RMMT_REC_RecordInfo_U";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserId", userId, DbType.Int32);
            dp.Add("@Id", id, DbType.Int32);
            dp.Add("@ApprovalStatus", approvalStatus, DbType.String);
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
    }
}
