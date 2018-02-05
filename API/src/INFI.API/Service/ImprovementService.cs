using Dapper;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.ImprovementDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INFI.API.Service
{
    public interface IImprovementService
    {
        Task<APIResult> SearchImprovementItemFromScoreList(string itemName, string sDate, string eDate, int inUserId, string statusType, string status, int disId, int depId, int planId, string sourceType);
        Task<APIResult> SearchImpItemFromScoreList(string sDate, string eDate, string tcKind, string sourceType, string itemName, string aStatus, string pStatus, string rStatus, string sDisId, string inUserId, string userType);
        Task<APIResult> SaveAllocateImprovementItem(ImprovementParamDto paramDto);
        Task<APIResult> SaveImprovementResult(ImprovementResultSaveParamDto paramDto);
        Task<APIResult> SearchImpPlanOrResultDetail(string improvementId, string searchType, string impResultId, string tPId, string itemId);

        Task<APIResult> SaveImprovementItem(ImpApprovalParamDto paramDto);
        Task<APIResult> SearchAllTaskOfPlanForImp(int inUserId);
    }
    public class ImprovementService : IImprovementService
    {
        string connStr = DapperContext.Current.Configuration["Data:DefaultConnection:ConnectionString"];
        public async Task<APIResult> SearchImprovementItemFromScoreList(string itemName, string sDate, string eDate, int inUserId, string statusType, string status, int disId, int depId, int planId,string sourceType)
        {
            string spName = @"up_RMMT_IMP_ImprovementItemFromScoreList_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@ItemName", itemName == null ? "" : itemName);
            dp.Add("@SDate", sDate);
            dp.Add("@EDate", eDate);
            dp.Add("@InUserId", inUserId);
            dp.Add("@StatusType", statusType);
            dp.Add("@Status", status == null ? "" : status);
            dp.Add("@DisId", disId);
            dp.Add("@DepId", depId);
            dp.Add("@PlanId", planId);
            dp.Add("@SourceType", sourceType);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<ImprovementMngDto> improvementMngResult = await conn.QueryAsync<ImprovementMngDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ImprovementMngDto>(improvementMngResult), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }



        public async Task<APIResult> SearchImpItemFromScoreList(string sDate, string eDate, string tcKind, string sourceType, string itemName, string aStatus, string pStatus, string rStatus, string sDisId, string inUserId, string userType)
        {
            string spName = @"up_RMMT_IMP_ImpItemFromScoreList_R2";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@SDate", sDate,DbType.String);
            dp.Add("@EDate", eDate, DbType.String);
            dp.Add("@TCKind", tcKind, DbType.String);
            dp.Add("@SourceType", sourceType, DbType.String);
            dp.Add("@ItemName", itemName, DbType.String);
            dp.Add("@AStatus", aStatus, DbType.String);
            dp.Add("@PStatus", pStatus, DbType.String);
            dp.Add("@RStatus", rStatus, DbType.String);
            dp.Add("@SDisId", sDisId, DbType.Int32);
            dp.Add("@InUserId", inUserId, DbType.Int32);
            dp.Add("@UserType", userType, DbType.String);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<ImpListDto> improvementMngResult = await conn.QueryAsync<ImpListDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ImpListDto>(improvementMngResult), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }
        public async Task<APIResult> SaveAllocateImprovementItem(ImprovementParamDto paramDto)
        {
            string spName = @"up_RMMT_IMP_AllocateImprovementItem_C";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@TPId", paramDto.tPId);
            dp.Add("@ItemId", paramDto.itemId);
            dp.Add("@DepartmentId", paramDto.departmentId);
            dp.Add("@AllocateYN", paramDto.allocateYN,DbType.Boolean);
            //dp.Add("@PlanApprovalId", paramDto.planApprovalId);
            //dp.Add("@ProcessId", paramDto.processId);
            dp.Add("@ImprovementCaption", paramDto.improvementCaption);
            dp.Add("@LostDescription", paramDto.lostDescription);
            dp.Add("@InUserId", paramDto.inUserId);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        IEnumerable<string> improvementMngResult = await conn.QueryAsync<string>(spName, dp, tran, null, CommandType.StoredProcedure);
                        APIResult result = new APIResult { Body = CommonHelper.EncodeDto<string>(improvementMngResult), ResultCode = ResultType.Success, Msg = "" };
                        tran.Commit();
                        return result;
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
            }
        }

        public async Task<APIResult> SaveImprovementResult(ImprovementResultSaveParamDto paramDto)
        {
            string spName = @"up_RMMT_IMP_SaveImprovementResult_C";
            string xmlAttachList = CommonHelper.Serializer(typeof(List<AttachDto>), paramDto.AttachList);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@ImprovementId", paramDto.ImprovementId);
            dp.Add("@ImpResultId", paramDto.ImpResultId);
            dp.Add("@ResultStatus", paramDto.ResultStatus);
            dp.Add("@ResultContent", paramDto.ResultContent);
            dp.Add("@InUserId", paramDto.InUserId);
            dp.Add("@XmlData", xmlAttachList, DbType.Xml);
            dp.Add("@XmlRootName", "/ArrayOfAttachDto/AttachDto");
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
        public async Task<APIResult> SaveImprovementItem(ImpApprovalParamDto paramDto)
        {
            string spName = @"up_RMMT_IMP_SaveImprovementItem_C";
            string xmlAttachList = CommonHelper.Serializer(typeof(List<AttachDto>), paramDto.AttachList);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@ImprovementContent", paramDto.ImprovementContent, DbType.String);
            dp.Add("@ExpectedTime", paramDto.ExpectedTime, DbType.String);
            dp.Add("@ImprovementId", paramDto.ImprovementId, DbType.Int32);
            dp.Add("@InUserId", paramDto.InUserId, DbType.Int32);
            dp.Add("@XmlData", xmlAttachList, DbType.Xml);
            dp.Add("@XmlRootName","/ArrayOfAttachDto/AttachDto", DbType.String);
            dp.Add("@SaveStatus", paramDto.SaveStatus, DbType.String);
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

        public async Task<APIResult> SearchImpPlanOrResultDetail(string improvementId, string searchType, string impResultId, string tPId, string itemId)
        {
            string spName = @"up_RMMT_IMP_ImpPlanOrResultDetail_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@ImprovementId", improvementId);
            dp.Add("@SearchType", searchType);
            dp.Add("@ImpResultId", impResultId);
            dp.Add("@TPId", tPId);
            dp.Add("@ItemId", itemId);

            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    var result = new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
                    if (searchType == "0")
                    {
                        var allocateManys = await con.QueryMultipleAsync(spName, dp, commandType: System.Data.CommandType.StoredProcedure);
                        //ImpPlanDetailDto impPlanDetailDto = planManys.ReadFirst<ImpPlanDetailDto>();
                        ImpAllocateDto allocateDto = allocateManys.ReadFirstOrDefault<ImpAllocateDto>();
                        if (allocateDto == null) allocateDto = new ImpAllocateDto();
                        var standardList = allocateManys.Read<StandardDto>();
                        var picList = allocateManys.Read<PicDto>();
                        var picdescList = allocateManys.Read<PicDescDto>();
                        allocateDto.StandardList.AddRange(standardList);
                        allocateDto.PicList.AddRange(picList);
                        allocateDto.PicDescList.AddRange(picdescList);
                        result = new APIResult { Body = CommonHelper.EncodeDto<ImpAllocateDto>(allocateDto), ResultCode = ResultType.Success, Msg = "" };
                    }
                    else if (searchType == "A")
                    {
                        var planManys = await con.QueryMultipleAsync(spName, dp, commandType: System.Data.CommandType.StoredProcedure);
                        //ImpPlanDetailDto impPlanDetailDto = planManys.ReadFirst<ImpPlanDetailDto>();
                        ImpPlanDetailDto impPlanDetailDto = planManys.ReadFirstOrDefault<ImpPlanDetailDto>();
                        if (impPlanDetailDto == null) impPlanDetailDto = new ImpPlanDetailDto();
                        var standardList = planManys.Read<StandardDto>();
                        var picList = planManys.Read<PicDto>();
                        var attachList = planManys.Read<AttachDto>();
                        var picdescList = planManys.Read<PicDescDto>();
                        impPlanDetailDto.StandardList.AddRange(standardList);
                        impPlanDetailDto.PicList.AddRange(picList);
                        impPlanDetailDto.AttachList.AddRange(attachList);
                        impPlanDetailDto.PicDescList.AddRange(picdescList);
                        result = new APIResult { Body = CommonHelper.EncodeDto<ImpPlanDetailDto>(impPlanDetailDto), ResultCode = ResultType.Success, Msg = "" };
                    }
                    else
                    {
                        var planManys = await con.QueryMultipleAsync(spName, dp, commandType: System.Data.CommandType.StoredProcedure);
                        //ImpResultDetailDto impResultDetailDto = planManys.ReadFirst<ImpResultDetailDto>();
                        ImpResultDetailDto impResultDetailDto = planManys.ReadFirstOrDefault<ImpResultDetailDto>();
                        if (impResultDetailDto == null) impResultDetailDto = new ImpResultDetailDto();
                         var attachList = planManys.Read<AttachDto>();
                        impResultDetailDto.AttachList.AddRange(attachList);
                        result = new APIResult { Body = CommonHelper.EncodeDto<ImpResultDetailDto>(impResultDetailDto), ResultCode = ResultType.Success, Msg = "" };
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }

        public async Task<APIResult> SearchAllTaskOfPlanForImp(int inUserId)
        {
            string spName = @"up_RMMT_IMP_GetAllTaskOfPlanForImp_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@InUserId", inUserId);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<NameValueDto> resultList = await conn.QueryAsync<NameValueDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<NameValueDto>(resultList), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }
    }

}
