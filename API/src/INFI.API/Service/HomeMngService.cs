using Dapper;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.CalenderMngDto;
using INFI.API.Models.HomeDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Service
{
    public interface IHomeMngService
    {
        Task<APIResult> GetAllDoItemList(string userId, string searchdate);
        Task<APIResult> GetAllDoItemListForMobile(string userId);
    }
    public class HomeMngService : IHomeMngService
    {
        public async Task<APIResult> GetAllDoItemList(string userId, string searchdate)
        {
            //日历
            string spName1 = @"up_RMMT_CAL_GetCalenderPlansListAllWeb_R02";

            DynamicParameters dp1 = new DynamicParameters();
            dp1.Add("@Date", searchdate, DbType.String);
            dp1.Add("@UserId", userId, DbType.Int16);

            //待办事项
            string spName2 = @"up_RMMT_Home_GetAllDoItemList_R";

            DynamicParameters dp2 = new DynamicParameters();
            dp2.Add("@InUserId", userId, DbType.Int16);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    //日历
                    IEnumerable<EachDayTypeDto> calenderListAllDtolist = await conn.QueryAsync<EachDayTypeDto>(spName1, dp1, null, null, CommandType.StoredProcedure);
                    //待办事项
                    var itemManys = await conn.QueryMultipleAsync(spName2, dp2, null, null, CommandType.StoredProcedure);
                    ItemResultDto dto = itemManys.ReadFirstOrDefault<ItemResultDto>();
                    if (dto == null) dto = new ItemResultDto();
                    var firstList = itemManys.Read<DoItemDto>();
                    var secondList = itemManys.Read<DoItemDto>();
                    dto.FirstItemList.AddRange(calenderListAllDtolist);
                    dto.SecondItemList.AddRange(firstList);
                    dto.ThirdItemList.AddRange(secondList);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ItemResultDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
                }
            }
        }

        public async Task<APIResult> GetAllDoItemListForMobile(string userId)
        {
            //待办事项
            string spName = @"up_RMMT_Home_GetAllDoItemListForMobile_R";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@InUserId", userId, DbType.Int16);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    //待办事项
                    var itemManys = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    ItemResultDto dto = itemManys.ReadFirstOrDefault<ItemResultDto>();
                    if (dto == null) dto = new ItemResultDto();
                    var firstList = itemManys.Read<DoItemDto>();
                    var secondList = itemManys.Read<DoItemDto>();
                    dto.SecondItemList.AddRange(firstList);
                    dto.ThirdItemList.AddRange(secondList);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ItemResultDto>(dto), ResultCode = ResultType.Success, Msg = "" };
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
