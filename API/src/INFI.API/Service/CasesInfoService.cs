using Dapper;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.CasesInfo;
using INFI.API.Models.ImprovementDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Service
{
    public interface ICasesInfoService
    {
        Task<APIResult> InsertOrUpdateCasesInfo(CasesParamDto casesParamDto);
        Task<APIResult> SearchCasesDetail(string caseId);
        Task<APIResult> SearchCasesList(string sDate, string eDate, string caseType, string content, string inUserId);
       Task<APIResult> DeleteCasesInfo(CasesDelParamDto paramDto);
    }
    public class CasesInfoService : ICasesInfoService
    {
        string connStr = DapperContext.Current.Configuration["Data:DefaultConnection:ConnectionString"];
        public async Task<APIResult> InsertOrUpdateCasesInfo(CasesParamDto casesParamDto)
        {
            string spName = @"up_RMMT_CAS_SaveCasesInfoItem_C";
            string xmlAttachList = CommonHelper.Serializer(typeof(List<AttachDto>), casesParamDto.AttachList);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@CaseId", casesParamDto.Id, DbType.Int32);
            dp.Add("@CaseType", casesParamDto.CaseType);
            dp.Add("@CasePoint", casesParamDto.CasePoint);
            dp.Add("@LossRemark", casesParamDto.LossRemark);
            dp.Add("@ImproveRemark", casesParamDto.ImproveRemark); 
            dp.Add("@CaseTitle", casesParamDto.CaseTitle);
            dp.Add("@InUserId", casesParamDto.InUserId, DbType.Int32);
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

        public async Task<APIResult> SearchCasesDetail(string caseId)
        {
            try
            {
                string spName = @"up_RMMT_CAS_SearchCasesDetail_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@CaseId", caseId, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    var caseManys = await conn.QueryMultipleAsync(spName, dp, commandType: System.Data.CommandType.StoredProcedure);
                    CasesInfoDto caseDetailDto = caseManys.ReadFirstOrDefault<CasesInfoDto>();
                    if (caseDetailDto == null) caseDetailDto = new CasesInfoDto();
                    var attachList = caseManys.Read<AttachDto>();
                    caseDetailDto.AttachList.AddRange(attachList);
                    var result = new APIResult { Body = CommonHelper.EncodeDto<CasesInfoDto>(caseDetailDto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> SearchCasesList(string sDate, string eDate, string caseType, string content, string inUserId)
        {
            try
            {
                string spName = @"up_RMMT_CAS_SearchCasesList_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@SDate", sDate, DbType.String);
                dp.Add("@EDate", eDate, DbType.String);
                dp.Add("@CaseType", caseType, DbType.String);
                dp.Add("@Content", content, DbType.String);
                dp.Add("@InUserId", inUserId, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<CasesListDto> casesList = await conn.QueryAsync<CasesListDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (casesList.Count() == 0)
                        message = "没有数据";
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<CasesListDto>(casesList), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> DeleteCasesInfo(CasesDelParamDto paramDto)
        {
            string spName = @"up_RMMT_CAS_DeleteCasesInfoItem_D";
            string xmlIdList = CommonHelper.Serializer(typeof(List<IdParamDto>), paramDto.IdList);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@InUserId", paramDto.InUserId, DbType.Int32);
            dp.Add("@XmlData", xmlIdList, DbType.Xml);
            dp.Add("@XmlRootName", "/ArrayOfIdParamDto/IdParamDto");

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
    }
 }
