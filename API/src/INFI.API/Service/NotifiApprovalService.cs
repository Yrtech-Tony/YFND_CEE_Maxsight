using INFI.API.Models.NoticeApproal;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using INFI.API.Context;
using INFI.API.Common;
using INFI.API.Models;
using Newtonsoft.Json;
using INFI.API.Models.ImprovementDto;
using System.Linq;
using INFI.API.Models.NotifiMngDto;

namespace INFI.API.Service
{
    public interface INotifiApprovalService
    {

        Task<APIResult> GetNeedApprovalDtoList(string approvalStatus, string sDate, string eDate, string noticeNo, string noticeDepartment, int userId);
        Task<APIResult> GetNoticeApprovalLog(int noticeReaderId);

        Task<APIResult> GetNoticeApprovalDetail(int noticeReaderId);

        Task<APIResult> NoticeApprovalS(NeedApproalParams param);

        Task<APIResult> GetNoticeDepartments();

        Task<APIResult> GetDistributorListByUserId(int userId, string disCode, string disName);

        Task<APIResult> GetApprovalStatus();

        Task<APIResult> GetNeedApprovalDtoList2(int userId);

        Task<APIResult> GetApprovalNoticeList(int UserId, string status, string sDate, string eDate, string noticeNo);
        Task<APIResult> GetApprovalReaderList(int noticeId, int userId);


    }
    public class NotifiApprovalService : INotifiApprovalService
    {
        public async Task<APIResult> GetNeedApprovalDtoList(string approvalStatus, string sDate, string eDate, string noticeNo, string noticeDepartment, int userId)
        {
            noticeNo = noticeNo == null ? "" : noticeNo;
            noticeDepartment = noticeDepartment == null ? "" : noticeDepartment;
            string spName = @"up_RMMT_NOT_NoticeApprovalList_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@ApprovalStatus", approvalStatus, DbType.String);
            dp.Add("@SDate", sDate, DbType.String);
            dp.Add("@EDate", eDate, DbType.String);
            dp.Add("@NoticeNo", noticeNo, DbType.String);
            dp.Add("@NoticeDepartment", noticeDepartment, DbType.Int16);
            dp.Add("@UserId", userId, DbType.Int16);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<NeedApprovalDto> NeedApprovalDtolist = await conn.QueryAsync<NeedApprovalDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NeedApprovalDto>(NeedApprovalDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }
        }

        public async Task<APIResult> GetNoticeApprovalLog(int noticeReaderId)
        {
            string spName = @"up_RMMT_NOT_NoticeApprovalLog_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@NoticeReaderId", noticeReaderId, DbType.Int32);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<ApproalLogDto> NeedApprovalDtolist = await conn.QueryAsync<ApproalLogDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (NeedApprovalDtolist.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NeedApprovalDto>(NeedApprovalDtolist), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }
        }

        public async Task<APIResult> GetNoticeApprovalDetail(int noticeReaderId)
        {
            string spName = @"up_RMMT_NOT_NoticeApprovalDetail_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@NoticeReaderId", noticeReaderId, DbType.Int32);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    var list = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    var r1 = list.ReadFirstOrDefault<NoticeApprovalDetailDto>();
                    var r2 = list.ReadAsync().Result;

                    NoticeApprovalDetailDto dto = JsonConvert.DeserializeObject<NoticeApprovalDetailDto>(JsonConvert.SerializeObject(r1));
                    List<AttachDto> lst = JsonConvert.DeserializeObject<List<AttachDto>>(JsonConvert.SerializeObject(r2));

                    dto.AttachList = new List<AttachDto>();
                    dto.AttachList.AddRange(lst);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NoticeApprovalDetailDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }
        }

        public async Task<APIResult> NoticeApprovalS(NeedApproalParams param)
        {
            {
                string spName = @"up_RMMT_NOT_NoticeApproval_C";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@NoticeReaderId", param.NoticeReaderId, DbType.Int32);
                dp.Add("@PassYN", param.PassYN, DbType.Boolean);
                dp.Add("@ReplyContent", param.ReplyContent, DbType.String);
                dp.Add("@InUserId", param.UserId, DbType.Int32);

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

        public async Task<APIResult> GetNoticeDepartments()
        {
            string spName = @"up_RMMT_NOT_NoticeDepartments_R";


            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<MultiSelectDto> NeedApprovalDtolist = await conn.QueryAsync<MultiSelectDto>(spName, null, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<MultiSelectDto>(NeedApprovalDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }

        }

        public async Task<APIResult> GetDistributorListByUserId(int userId,string disCode, string disName)
        {
            string spName = @"up_RMMT_NOT_DistributorListByUserId_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserId", userId, DbType.Int32);
            dp.Add("@DisCode", disCode, DbType.String);
            dp.Add("@DisName", disName, DbType.String);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<MultiSelectDto> NeedApprovalDtolist = await conn.QueryAsync<MultiSelectDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<MultiSelectDto>(NeedApprovalDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }
        }

        public async Task<APIResult> GetApprovalStatus()
        {
            string spName = @"up_RMMT_NOT_ApprovalStatus_R";

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<ApprovalStatus> NeedApprovalDtolist = await conn.QueryAsync<ApprovalStatus>(spName, null, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ApprovalStatus>(NeedApprovalDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }
        }

        public async Task<APIResult> GetNeedApprovalDtoList2(int userId)
        {
            string spName = @"up_RMMT_NOT_NoticeApprovalList_R2";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserId", userId, DbType.Int16);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<NeedApprovalDto2> NeedApprovalDtolist = await conn.QueryAsync<NeedApprovalDto2>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NeedApprovalDto2>(NeedApprovalDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }
        }

        public async Task<APIResult> GetApprovalNoticeList(int UserId, string status, string sDate, string eDate, string noticeNo)
        {
            status = status == null ? "" : status;
            noticeNo = noticeNo == null ? "" : noticeNo;
            string spName = @"up_RMMT_NOT_NoticeApprovalNoticeList_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserId", UserId, DbType.Int16);
            dp.Add("@Status", status, DbType.String);
            dp.Add("@SDate", sDate, DbType.String);
            dp.Add("@EDate", eDate, DbType.String);
            dp.Add("@NoticeNo", noticeNo, DbType.String);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<NeedApprovalListDto> Dtolist = await conn.QueryAsync<NeedApprovalListDto>(spName, dp, null, null, CommandType.StoredProcedure);

                    string message = "";
                    if (Dtolist.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NeedApprovalListDto>(Dtolist), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }
        }

        public async Task<APIResult> GetApprovalReaderList(int noticeId, int userId)
        {
            string spName = @"up_RMMT_NOT_NoticeApprovalReaderList_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@NoticeId", noticeId, DbType.Int32);
            dp.Add("@UserId", userId, DbType.Int32);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<NoticeReaderDto> Dtolist = await conn.QueryAsync<NoticeReaderDto>(spName, dp, null, null, CommandType.StoredProcedure);

                    string message = "";
                    if (Dtolist.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NoticeReaderDto>(Dtolist), ResultCode = ResultType.Success, Msg = message };
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
