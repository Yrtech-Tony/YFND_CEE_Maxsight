using Dapper;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.CalenderMngDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Service
{

    public interface ICalenderMngService
    {
        Task<APIResult> GetCalenderListAll(string userId,string Date);
        Task<APIResult> GetCalenderListAllWeb(string userId, string SDate, string EDate);
        Task<APIResult> GetCalenderListAllWebR02(string userId, string Date);
        Task<APIResult> CreateCalenderPlans(string id,string title,string Content,string Type,string SDate,string EDate,string UserID);
        Task<APIResult> DeleteCalenderPlans(string id);

    }
    public class CalenderMngService: ICalenderMngService
    {
        public async Task<APIResult> GetCalenderListAll(string userId, string Date)
        {        
            string spName = @"up_RMMT_CAL_GetCalenderPlansListAll_R";
            DynamicParameters dp = new DynamicParameters();     
            dp.Add("@Date", Date, DbType.String);       
            dp.Add("@UserId", userId, DbType.Int16);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<CalenderListAllDto> calenderListAllDtolist = await conn.QueryAsync<CalenderListAllDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<CalenderListAllDto>(calenderListAllDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }

            }

        }
        public async Task<APIResult> GetCalenderListAllWeb(string userId, string SDate, string EDate)
        {
            string spName = @"up_RMMT_CAL_GetCalenderPlansListAllWeb_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@SDate", SDate, DbType.String);
            dp.Add("@EDate", EDate, DbType.String);
            dp.Add("@UserId", userId, DbType.Int16);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<CalenderListAllWebDto> calenderListAllDtolist = await conn.QueryAsync<CalenderListAllWebDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<CalenderListAllWebDto>(calenderListAllDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }

            }

        }
        public async Task<APIResult> GetCalenderListAllWebR02(string userId, string Date)
        {
            string spName = @"up_RMMT_CAL_GetCalenderPlansListAllWeb_R02";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Date", Date, DbType.String);
            dp.Add("@UserId", userId, DbType.Int16);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<EachDayTypeDto> calenderListAllDtolist = await conn.QueryAsync<EachDayTypeDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<EachDayTypeDto>(calenderListAllDtolist), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }
        public async Task<APIResult> CreateCalenderPlans(string id, string title, string Content, string Type, string SDate, string EDate, string UserID)
        {            
            string spName = @"up_RMMT_CAL_CreateCalenderPlansList_C";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id, DbType.Int32);
            dp.Add("@Title", title, DbType.String);
            dp.Add("@Content", Content, DbType.String);
            dp.Add("@Type", Type, DbType.String);
            dp.Add("@SDate", SDate, DbType.String );
            dp.Add("@EDate", EDate, DbType.String);
            dp.Add("@UserID", UserID, DbType.Int32);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        IEnumerable<int>  result =await conn.QueryAsync<int>(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                        return new APIResult { Body = CommonHelper.EncodeDto<int>(result), ResultCode = ResultType.Success, Msg = "" };
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
        public async Task<APIResult> DeleteCalenderPlans(string id)
        {
            string spName = @"up_RMMT_CAL_DeleteCalenderPlansList_D";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id, DbType.Int32);     
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
