using Dapper;
using Newtonsoft.Json;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.ImprovementDto;
using INFI.API.Models.NoticeApproal;
using INFI.API.Models.NoticeFeedBackDto;
using INFI.API.Models.NotifiMngDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace INFI.API.Service
{
    public interface INotifiFeedBService
    {
        Task<APIResult> sreachNotiFeedBMstList(string SDate, string EDate, int UserID, string ReplyYN, string ApprovalStatus, string NoticeNo, string Title);
        Task<APIResult> sreachNotiFeedBDtlList(int UserID, int NoticeReaderId);
        Task<APIResult> sreachNotiFeedBAttachMentList(int UserID, int NoticeReaderId);
        Task<APIResult> NotiFeedBackS(int NoticeId, int UserId, string ReplyContent, string Status, string XmlData);
        Task<APIResult> searchNotiFeedBMstListByUserId(string UserId);
        Task<APIResult> SaveFeedBackNotice(int NoticeId, int UserId, string ReplyContent, string Status, List<AttachDto> list);
        Task<APIResult> searchNoticeFeedBackDtl(string NoticeId, string UserId, string DisId, string DepartId);
        Task<APIResult> searchNoticeFeedbackList(string SDate, string EDate, string  UserID, string FeedBackYN, string ApprovalStatus, string NoticeNo, string Title);
    }
    public class NotifiFeedBService : INotifiFeedBService
    {
        public async Task<APIResult> sreachNotiFeedBMstList(string SDate, string EDate, int UserID, string ReplyYN, string ApprovalStatus, string NoticeNo, string Title)
        {
            string spName = @"up_RMMT_NOT_DistributorNoticeList_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@SDate", SDate, DbType.String);
            dp.Add("@EDate", EDate, DbType.String);
            dp.Add("@UserID", UserID, DbType.Int32);
            dp.Add("@ReplyYN", ReplyYN, DbType.String);
            dp.Add("@ApprovalStatus", ApprovalStatus, DbType.String);
            dp.Add("@NoticeNo", NoticeNo, DbType.String);
            dp.Add("@Title", Title, DbType.String);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<NotiFeedBMstDto> dto = await con.QueryAsync<NotiFeedBMstDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NotiFeedBMstDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }

        }
        public async Task<APIResult> sreachNotiFeedBDtlList(int UserID, int NoticeReaderId)
        {

            string spName = @"up_RMMT_NOT_DistributorNoticeDtlList_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserID", UserID, DbType.Int32);
            dp.Add("@NoticeReaderId", NoticeReaderId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<NotiFeedBDtlDto> dto = await con.QueryAsync<NotiFeedBDtlDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NotiFeedBDtlDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }
        }
        public async Task<APIResult> sreachNotiFeedBAttachMentList(int UserID, int NoticeReaderId)
        {
            string spName = @"up_RMMT_NOT_DistributorNoticeAttachmentList_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserID", UserID, DbType.Int32);
            dp.Add("@NoticeReaderId", NoticeReaderId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<NoticeFeedBackAttachMentDto> dto = await con.QueryAsync<NoticeFeedBackAttachMentDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NoticeFeedBackAttachMentDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }
        }
        public async Task<APIResult> NotiFeedBackS(int NoticeId, int UserId, string ReplyContent, string Status, string XmlData)
        {
            string spName = @"up_RMMT_NOT_NoticeFeedBackTorS_U";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@NoticeId", NoticeId, DbType.Int32);
            dp.Add("@ReplyContent", ReplyContent, DbType.String);
            dp.Add("@Status", Status, DbType.String);
            dp.Add("@UserId", UserId, DbType.Int32);
            dp.Add("@XmlData", XmlData, DbType.Xml);
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
        public async Task<APIResult> searchNotiFeedBMstListByUserId(string UserId)
        {
            string spName = @"up_RMMT_NOT_DistributorNoticeListByUserId_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserID", UserId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<FeedBackListDto> dto = await con.QueryAsync<FeedBackListDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<FeedBackListDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }

        }
        public async Task<APIResult> SaveFeedBackNotice(int NoticeId, int UserId, string ReplyContent, string Status, List<AttachDto> list)
        {
            string XmlData = CommonHelper.Serializer(typeof(List<AttachDto>), list);
            string spName = @"up_RMMT_NOT_NoticeFeedBackTorS_U";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@NoticeId", NoticeId, DbType.Int32);
            dp.Add("@ReplyContent", ReplyContent, DbType.String);
            dp.Add("@Status", Status, DbType.String);
            dp.Add("@UserId", UserId, DbType.Int32);
            dp.Add("@XmlData", XmlData, DbType.Xml);
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
        public async Task<APIResult> searchNoticeFeedBackDtl(string NoticeId, string UserId, string DisId, string DepartId)
        {
            try
            {
                string spName = @"up_RMMT_NOT_NoticeFeedBackDetail_R";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@UserId", UserId, DbType.Int32);
                dp.Add("@NoticeId", NoticeId, DbType.Int32);
                dp.Add("@DisId", DisId, DbType.Int32);
                dp.Add("@DepartId", DepartId, DbType.Int32);
                using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    con.Open();
                    var list = await con.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    var l1 = list.ReadAsync().Result;
                    var l2 = list.ReadAsync().Result;
                    var l3 = list.ReadAsync().Result;
                    List<NoticeFeedBackDtlDto> lst = JsonConvert.DeserializeObject<List<NoticeFeedBackDtlDto>>(JsonConvert.SerializeObject(l1));
                    List<AttachDto> lst1 = JsonConvert.DeserializeObject<List<AttachDto>>(JsonConvert.SerializeObject(l2));
                    List<AttachDto> lst2 = JsonConvert.DeserializeObject<List<AttachDto>>(JsonConvert.SerializeObject(l3));

                    foreach (var item in lst)
                    {
                        item.AttachList = new List<AttachDto>();
                        item.AttachList.AddRange(lst1);
                        item.NoticeAttachList = new List<AttachDto>();
                        item.NoticeAttachList.AddRange(lst2);
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NoticeFeedBackDtlDto>(lst), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
            }
            catch (Exception ex)
            {

                return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
            }
        }
        public async Task<APIResult> searchNoticeFeedbackList(string SDate, string EDate, string  UserID, string FeedBackYN, string ApprovalStatus, string NoticeNo, string Title)
        {
            string spName = @"up_RMMT_NOT_DistributorNoticeListForWeb_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@SDate", SDate, DbType.String);
            dp.Add("@EDate", EDate, DbType.String);
            dp.Add("@UserID", UserID, DbType.Int32);
            dp.Add("@FeedBackYN", FeedBackYN==null?"":FeedBackYN, DbType.String);
            dp.Add("@ApprovalStatus", ApprovalStatus, DbType.String);
            dp.Add("@NoticeNo", NoticeNo == null ? "" : NoticeNo, DbType.String);
            dp.Add("@Title", Title == null ? "" : Title, DbType.String);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<NoticeFeedBackListDto> dto = await con.QueryAsync<NoticeFeedBackListDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NoticeFeedBackListDto>(dto), ResultCode = ResultType.Success, Msg = "" };
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

