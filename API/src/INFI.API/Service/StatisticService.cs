using Dapper;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.NotifiMngDto;
using INFI.API.Models.PlanTaskMngDto;
using INFI.API.Models.StatisticDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Service
{

    public interface IStatisticService
    {
        Task<APIResult> GetAreaByUserId(string UserId);
        Task<APIResult> GetImpItemProcessList(string sDate,
                                                string eDate,
                                                int areaId,
                                                string disId,
                                                string itemName,
                                                string allocate,
                                                string statusType,
                                                string status,
                                                int inUserId);
        Task<APIResult> GetZoneByAreaId(string AreaId);
        Task<APIResult> GetPatrolData(string SDate, string EDate, string Area, string Zone,string UserId);
        Task<APIResult> GetDistributorByAreaId(string AreaId,string UserId, string DisName);
        Task<APIResult> GetDisListByUserId(string UserId);
        Task<APIResult> GetInfoByDisId(string DisId);
        Task<APIResult> GetAftersalesFigures(string disId,string yearMonthCSS,string yearMonthCCM,string yearMonthB);
        Task<APIResult> GetAftersalesFiguresForHighCharts(string disId,string year);
        Task<APIResult> InsertAfterSalesDataByExcel(string InUserId, List<ExcelAfterSalesData> AfterSalesData);

    }
    public class StatisticService : IStatisticService
    {
        public async Task<APIResult> GetAreaByUserId(string UserId)
        {
            string spName = @"up_RMMT_STA_GetAreaByUserId_R";
            DynamicParameters dp = new DynamicParameters();            
            dp.Add("@UserId", UserId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<StatusDto> dto = await con.QueryAsync<StatusDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<StatusDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }

        }
        public async Task<APIResult> GetImpItemProcessList(string sDate,
                                                string eDate,
                                                int areaId,
                                                string disId,
                                                string itemName,
                                                string allocate,
                                                string statusType,
                                                string status,
                                                int inUserId)
        {
            string spName = @"up_RMMT_STA_ImpItemProcessList_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@SDate", sDate, DbType.String);
            dp.Add("@EDate", eDate, DbType.String);
            dp.Add("@AreaId", areaId, DbType.Int32);
            dp.Add("@DisId", disId, DbType.String);
            dp.Add("@ItemName", itemName, DbType.String);
            dp.Add("@Allocate", allocate, DbType.String);
            dp.Add("@StatusType", statusType, DbType.String);
            dp.Add("@Status", status, DbType.String);
            dp.Add("@InUserId", inUserId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<ImpItemDto> dto = await con.QueryAsync<ImpItemDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ImpItemDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }

        public async Task<APIResult> GetZoneByAreaId(string AreaId)
        {
            string spName = @"up_RMMT_STA_GetZoneByAreaId_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@AreaId", AreaId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<StatusDto> dto = await con.QueryAsync<StatusDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<StatusDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }
        }
        public async Task<APIResult> GetPatrolData(string SDate, string EDate, string Area, string Zone,string UserId)
        {
            string spName = @"up_RMMT_STA_GetAreaPatrolData_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@SDate", SDate, DbType.String);
            dp.Add("@EDate", EDate, DbType.String);
            dp.Add("@Area", Area, DbType.Int32);
            dp.Add("@Zone", Zone, DbType.Int32);
            dp.Add("@UserId", UserId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<PatrolListDto> dto = await con.QueryAsync<PatrolListDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<PatrolListDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }

        }
        public async Task<APIResult> GetDistributorByAreaId(string AreaId,string UserId, string DisName)
        {
            string spName = @"up_RMMT_STA_GetDistributorByAreaId_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@AreaId", AreaId, DbType.Int32);
            dp.Add("@UserId", UserId, DbType.Int32);
            dp.Add("@DisName", DisName, DbType.String);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<DisOfAreaDto> NeedApprovalDtolist = await conn.QueryAsync<DisOfAreaDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<DisOfAreaDto>(NeedApprovalDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }

            }
        }
        public async Task<APIResult> GetDisListByUserId(string UserId)
        {
            string spName = @"up_RMMT_STA_GetDisListByUserId_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserId", UserId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<StatusDto> dto = await con.QueryAsync<StatusDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<StatusDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }

        }
        public async Task<APIResult> GetInfoByDisId(string DisId)
        {
            string spName = @"up_RMMT_STA_GetInfoByDisId_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@DisId", DisId, DbType.Int32);
            using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                con.Open();
                try
                {
                    IEnumerable<InfoByDisIdDto> dto = await con.QueryAsync<InfoByDisIdDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<InfoByDisIdDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {

                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }
        }
        public async Task<APIResult> GetAftersalesFigures(string disId, string yearMonthCSS, string yearMonthCCM, string yearMonthB)
        {
            string spName = @"up_RMMT_STA_GetAftersalesFigures_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@DisId", Convert.ToInt32(disId));
            dp.Add("@YearMonthCSS", yearMonthCSS);
            dp.Add("@YearMonthCCM", yearMonthCCM);
            dp.Add("@YearMonthB", yearMonthB);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    var afManys = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    AftersalesFiguresDto afDto = new AftersalesFiguresDto();
                    List<BusinessDto> Blist2 = new List<BusinessDto>();
                    var BList = afManys.Read<BusinessDto>();
                    foreach (var item in BList)
                    {
                        Blist2.Add(new BusinessDto
                        {
                            LTypeName = item.LTypeName                                                                            
                            ,Actual = CommonHelper.FormatterString(item.Actual, 0,false)                                                                            
                            ,Target = CommonHelper.FormatterString(item.Target, 0, false)                                                                            
                            ,Rate = CommonHelper.FormatterString(item.Rate, 2,true)
                        });
                        Blist2.Add(new BusinessDto
                        {
                            LTypeName = "YTM"                                                                           
                            ,Actual = CommonHelper.FormatterString(item.ActualYTM, 0,false)                                                                           
                            ,Target = CommonHelper.FormatterString(item.TargetYTM, 0, false)                                                                           
                            ,Rate = CommonHelper.FormatterString(item.RateYTM, 2, true)
                        });
                    }
                    var QList = afManys.Read<QualityDto>();
                    List<QualityDto> QList2 = new List<QualityDto>();
                    foreach (var item in QList)
                    {
                        QList2.Add(new QualityDto() { LTypeName = item.LTypeName
                                                                        , ScoreAndRank = CommonHelper.FormatterString(item.Score,2,false) + "/" + CommonHelper.FormatterString( item.Rank,0,false)
                                                                        , AverageAndTotal = CommonHelper.FormatterString(item.Average,2,false) + "/" + CommonHelper.FormatterString(item.Total,0,false) });
                    }

                    var PList = afManys.Read<PartsStockDto>();

                    foreach (var item in PList)
                    {
                        item.Target = CommonHelper.FormatterString(item.Target,2,false);
                        item.Actual = CommonHelper.FormatterString(item.Actual, 2, false);
                    }
                    var CSSYearTargetlist = afManys.Read<CSSYearTargetDto>();
                    foreach (var item in CSSYearTargetlist)
                    {
                        item.CSSYearTarget = CommonHelper.FormatterString(item.CSSYearTarget, 2, false);
                    }
                    afDto.BusinessList.AddRange(Blist2);
                    afDto.QualityList.AddRange(QList2);
                    afDto.PartsStockList.AddRange(PList);
                    afDto.CSSYearTargetList.AddRange(CSSYearTargetlist);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<AftersalesFiguresDto>(afDto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }
        public async Task<APIResult> GetAftersalesFiguresForHighCharts(string disId, string year)
        {
            string spName = @"up_RMMT_STA_GetAftersalesFiguresForHighCharts_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@DisId", Convert.ToInt32(disId));
            dp.Add("@Year", year);          
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    var afManys = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    // AftersalesFiguresDto afDto = afManys.ReadFirstOrDefault<AftersalesFiguresDto>();
                    //if (afDto == null) afDto = new AftersalesFiguresDto();
                    AfterFiguresHighChartsDto afDto = new AfterFiguresHighChartsDto();
                    var TList = afManys.Read<HighChartsDto>();
                    var PList = afManys.Read<HighChartsDto>();
                    var AList = afManys.Read<HighChartsDto>();
                    var PSList = afManys.Read<HighChartsDto>();
                    var CList = afManys.Read<HighChartsDto>();
                    var FList = afManys.Read<HighChartsDto>();
                    var IList = afManys.Read<HighChartsDto>();

                    afDto.Throughputs.AddRange(TList);
                    afDto.Parts.AddRange(PList);
                    afDto.AfterSales.AddRange(AList);
                    afDto.PartsStock.AddRange(PSList);
                    afDto.CSS.AddRange(CList);
                    afDto.FFV.AddRange(FList);
                    afDto.IKB.AddRange(IList);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<AfterFiguresHighChartsDto>(afDto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }

        }
        public async Task<APIResult> InsertAfterSalesDataByExcel(string InUserId, List<ExcelAfterSalesData> AfterSalesData)
        {
            string AfterSalesDataXML = CommonHelper.Serializer(typeof(List<ExcelAfterSalesData>), AfterSalesData);       
            string spName = @"up_RMMT_STA_InsertAfterSalesDataByExcel_C";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@InUserId", InUserId, DbType.Int32);      
            dp.Add("@AfterSalesDataXML", AfterSalesDataXML, DbType.Xml);
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
