using Dapper;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.CasesInfo;
using INFI.API.Models.ImprovementDto;
using INFI.API.Models.NoticeApproal;
using INFI.API.Models.NotifiMngDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INFI.API.Service
{
    public interface INotifiMngService {
        Task<APIResult> SaveNoticeMade(NoticeInfoDto noticeInfoDto);
        Task<APIResult> SearchMadeNoticeDetailInfo(string noticeid);
        Task<APIResult> SearchMadeNoticeList(string fromDate, string toDate, string noticeReaders, string status, string needReply, string title, string noticeNo, string inUserId);
        Task<APIResult> UpdateMadeNoticeList(CasesDelParamDto paramDto);
        Task<APIResult> SearchNoticeReaders(string noticeId,string inUserId);
        Task<APIResult> UpdateReaderReadStatus(NoticeReadStatusDto paramDto);
        Task<APIResult> GetNoticeDisReader(string noticeId);
    }
    public class NotifiMngService : INotifiMngService
    {
        string connStr = DapperContext.Current.Configuration["Data:DefaultConnection:ConnectionString"];
        public async Task<APIResult> SaveNoticeMade(NoticeInfoDto noticeInfoDto)
        {

            string spName = @"up_RMMT_NOT_SaveMadeNotice_C";
            string xmlAttachList = CommonHelper.Serializer(typeof(List<AttachDto>), noticeInfoDto.AttachList);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@IdNotice", noticeInfoDto.NoticeId, DbType.Int32);
            dp.Add("@Title", noticeInfoDto.Title, DbType.String);
            dp.Add("@SDate", noticeInfoDto.SDate, DbType.String);
            dp.Add("@EDate", noticeInfoDto.EDate, DbType.String);
            dp.Add("@NeedReply", noticeInfoDto.NeedReply, DbType.Int32);
            dp.Add("@Content", noticeInfoDto.Content, DbType.String);
            dp.Add("@Status", noticeInfoDto.Status, DbType.String);
            dp.Add("@InUserId", noticeInfoDto.InUserId, DbType.Int32);
            dp.Add("@NoticeReaders", noticeInfoDto.NoticeReaders, DbType.String);
            dp.Add("@XmlData", xmlAttachList, DbType.Xml);
            dp.Add("@XmlRootName", "/ArrayOfAttachDto/AttachDto");

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                   {
                       IEnumerable<string> idResult = await conn.QueryAsync<string>(spName, dp,tran,null,CommandType.StoredProcedure);
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
                return new APIResult { Body ="", ResultCode = ResultType.Success, Msg = "" };
               
            }

        }

        public async Task<APIResult> SearchMadeNoticeDetailInfo(string noticeid)
        {
            string spName = @"up_RMMT_NOT_GetMadeNoticeDetailInfo_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@IdNotice",Convert.ToInt32( noticeid));

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    var noticeManys = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    NoticeDetailDto noticeDetailDto = noticeManys.ReadFirstOrDefault<NoticeDetailDto>();
                    if (noticeDetailDto == null) noticeDetailDto = new NoticeDetailDto();
                    var disList = noticeManys.Read<MultiSelectDto>();
                    var depList = noticeManys.Read<MultiSelectDto>();
                    var attachList = noticeManys.Read<AttachDto>();
                    noticeDetailDto.NoticeDisList.AddRange(disList);
                    noticeDetailDto.NoticeDepList.AddRange(depList);
                    noticeDetailDto.AttachList.AddRange(attachList);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NoticeDetailDto>(noticeDetailDto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }
       
        public async Task<APIResult> SearchMadeNoticeList(string fromDate, string toDate, string noticeReaders, string status, string needReply, string title, string noticeNo,string inUserId)
        {
            string spName = @"up_RMMT_NOT_GetMadeNoticeList_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@FromDate", fromDate);
            dp.Add("@ToDate", toDate);
            dp.Add("@NoticeReaders", noticeReaders);
            dp.Add("@Status", status);
            dp.Add("@NeedReply",needReply);
            dp.Add("@Title", title);
            dp.Add("@NoticeNo", noticeNo);
            dp.Add("@InUserId", inUserId);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<NoticeListInfoDto> noticeInfoDtoResult = await conn.QueryAsync<NoticeListInfoDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NoticeListInfoDto>(noticeInfoDtoResult), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message }; ;
                }
            }
        }

        public async Task<APIResult> UpdateMadeNoticeList(CasesDelParamDto paramDto)
        {
            string spName = @"up_RMMT_NOT_DELETEMadeNoticeList_D";
            string xmlIdList = CommonHelper.Serializer(typeof(List<IdParamDto>), paramDto.IdList);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@InUserId", paramDto.InUserId);
            dp.Add("@XmlData", xmlIdList, DbType.Xml);
            dp.Add("@XmlRootName", "/ArrayOfIdParamDto/IdParamDto");

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        await conn.QueryAsync<string>(spName, dp, tran, null, CommandType.StoredProcedure);
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

        public async Task<APIResult> SearchNoticeReaders(string noticeId,string inUserId)
        {
            string spName = @"up_RMMT_NOT_GetNoticeReaders_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@NoticeId", noticeId,DbType.Int32);
            dp.Add("@InUserId", inUserId, DbType.Int32);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<NotifiReadersDto> readerList = await conn.QueryAsync<NotifiReadersDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NotifiReadersDto>(readerList), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message }; ;
                }
            }
        }
        public async Task<APIResult> UpdateReaderReadStatus(NoticeReadStatusDto paramDto)
        {
            string spName = @"up_RMMT_NOT_UpdateReaderReadStatus_U";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@NoticeId", paramDto.NoticeId);
            dp.Add("@DisId", paramDto.DisId);
            dp.Add("@DepartId", paramDto.DepartId);
            dp.Add("@InUserId", paramDto.InUserId);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        await conn.QueryAsync<string>(spName, dp, tran, null, CommandType.StoredProcedure);
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

        public async Task<APIResult> GetNoticeDisReader(string noticeId)
        {
            string spName = @"up_RMMT_NOT_GetNoticeDisReader_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@NoticeId", noticeId, DbType.Int32);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<MultiSelectDto> readerList = await conn.QueryAsync<MultiSelectDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<MultiSelectDto>(readerList), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message }; ;
                }
            }
        }
    }
}
