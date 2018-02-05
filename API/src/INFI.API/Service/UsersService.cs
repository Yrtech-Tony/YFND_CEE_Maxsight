using Dapper;
using Newtonsoft.Json;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.PlanTaskMngDto;
using INFI.API.Models.UsersDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Service
{
    public interface IUsersService
    {
        Task<APIResult> Login(string UserName, string Password);
        Task<APIResult> LoginForBs(string UserName, string Password);
        Task<APIResult> SaveEmployeeInfo(ParamEmpInfoDto empInfo);
        Task<APIResult> GetEmployeeInfo(string DisId, string DepartId, string UserType, string UserName, string UseYN);
        Task<APIResult> UpdatePsw(ParamEmpPswDto user);
        Task<APIResult> GetDistributorInfo(string DisId, string UseYN);
        Task<APIResult> SaveDistributorInfo(ParamDisInfoDto disInfoDto);
        Task<APIResult> GetOrgInfo(string UserId);
        Task<APIResult> GetPushInfo();
        Task<APIResult> GetGroupList();
        Task<APIResult> GetTypeList(string GroupCode, string Name, string UseYN);
        Task<APIResult> UpdateType(string UserId, List<CodeHiddenDto> list);
        Task<APIResult> GetAllDataForLocalDB(string UserId);
        Task<APIResult> SaveDealerList(ParamDealerDto paramDto);
    }
    public class UsersService : IUsersService
    {
        public async Task<APIResult> Login(string UserName, string Password)
        {
            string spName = @"up_sant_Login_Login_R_01";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", UserName);
            dp.Add("@Password", Password);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    var userManys = await conn.QueryMultipleAsync(spName, param: dp, commandType: System.Data.CommandType.StoredProcedure);
                    UserDto userDto = userManys.ReadFirst<UserDto>();
                    var zionList = userManys.Read<ZionDto>();
                    var areaList = userManys.Read<AreaDto>();
                    var serverList = userManys.Read<ServerDto>();
                    var departmentList = userManys.Read<DepartmentDto>();
                    //var impPlanStatusList = userManys.Read<ImpPlanStatusDto>();
                    //var impResultStatusList = userManys.Read<ImpResultStatusDto>();
                    var impStatusList = userManys.Read<ImpStatusDto>();
                    foreach (var item in areaList)
                    {
                        item.ServerList.AddRange(serverList.Where(s => s.AId == item.AId));
                    }
                    foreach (var item in zionList)
                    {
                        item.AreaList.AddRange(areaList.Where(a => a.QId == item.QId));
                    }
                    userDto.ZionList.AddRange(zionList);
                    userDto.DepartmentList.AddRange(departmentList);
                    //userDto.ImpPlanStatusList.AddRange(impPlanStatusList);
                    //userDto.ImpResultStatusList.AddRange(impResultStatusList);
                    userDto.ImpStatusList.AddRange(impStatusList);

                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<UserDto>(userDto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
                }
            }
        }

        public async Task<APIResult> LoginForBs(string UserName, string Password)
        {
            string spName = @"up_sant_Login_LoginForBs_R_01";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", UserName);
            dp.Add("@Password", Password);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    var userManys = await conn.QueryMultipleAsync(spName, param: dp, commandType: System.Data.CommandType.StoredProcedure);
                    UserRoleDto userRoleDto = userManys.ReadFirst<UserRoleDto>();
                    var roleDto = userManys.Read<RoleDto>();
                    var zionList = userManys.Read<ZionDto>();
                    var areaList = userManys.Read<AreaDto>();
                    var serverList = userManys.Read<ServerDto>();
                    var departmentList = userManys.Read<DepartmentDto>();
                    foreach (var item in areaList)
                    {
                        item.ServerList.AddRange(serverList.Where(s => s.AId == item.AId));
                    }
                    foreach (var item in zionList)
                    {
                        item.AreaList.AddRange(areaList.Where(a => a.QId == item.QId));
                    }
                    userRoleDto.RoleList.AddRange(roleDto);
                    userRoleDto.ZionList.AddRange(zionList);
                    userRoleDto.DepartmentList.AddRange(departmentList);
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<UserRoleDto>(userRoleDto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message }; ;
                }
            }
        }

        public async Task<APIResult> SaveEmployeeInfo(ParamEmpInfoDto empInfo)
        {
            try
            {
                string spName = @"up_RMMT_BAS_InsertEmployeeInfo_S";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@EmpId", empInfo.EmpId);
                dp.Add("@DisId", empInfo.DisId);
                dp.Add("@DepartId", empInfo.DepartId);
                dp.Add("@UserType", empInfo.UserType, DbType.String);
                dp.Add("@UserName", empInfo.UserName, DbType.String);
                dp.Add("@LoginId", empInfo.LoginId, DbType.String);
                dp.Add("@Password", empInfo.Password, DbType.String);
                dp.Add("@CellNo", empInfo.CellNo, DbType.String);
                dp.Add("@PhoneNo", empInfo.PhoneNo, DbType.String);
                dp.Add("@Email", empInfo.Email, DbType.String);
                dp.Add("@UseYN", empInfo.UseYN, DbType.Boolean);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {

                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                }
                return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> GetEmployeeInfo(string DisId, string DepartId, string UserType, string UserName, string UseYN)
        {
            string spName = @"up_RMMT_BAS_GetEmployeeInfo_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@DisId", DisId == null ? "" : DisId);
            dp.Add("@DepartId", DepartId == null ? "" : DepartId);
            dp.Add("@UserType", UserType == null ? "" : UserType);
            dp.Add("@UserName", UserName == null ? "" : UserName);
            dp.Add("@UseYN", UseYN == null ? "" : UseYN);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<EmpInfoDto> list = await conn.QueryAsync<EmpInfoDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<EmpInfoDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message }; ;
                }
            }
        }

        public async Task<APIResult> UpdatePsw(ParamEmpPswDto user)
        {
            try
            {
                string spName = @"up_RMMT_BAS_UpdatePsw_S";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@UserId", user.UserId);
                dp.Add("@NewPassword", user.NewPassword);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {

                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                }
                return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> GetDistributorInfo(string DisId, string UseYN)
        {
            string spName = @"up_RMMT_BAS_GetDistributorInfo_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@DisId", DisId == null ? "" : DisId);
            dp.Add("@UseYN", UseYN == null ? "" : UseYN);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<DisInfoDto> list = await conn.QueryAsync<DisInfoDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<DisInfoDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message }; ;
                }
            }
        }

        public async Task<APIResult> SaveDistributorInfo(ParamDisInfoDto disInfoDto)
        {
            try
            {
                string spName = @"up_RMMT_BAS_InsertDistributorInfo_S";

                string XmlData = CommonHelper.Serializer(typeof(List<DisInfoDto>), disInfoDto.XmlData);
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@XmlData", XmlData);
                dp.Add("@UserId", disInfoDto.UserId);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {

                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                }
                return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> GetOrgInfo(string UserId)
        {
            string spName = @"up_RMMT_BAS_GetOrgInfo_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserId", UserId);

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    var userManys = await conn.QueryMultipleAsync(spName, param: dp, commandType: System.Data.CommandType.StoredProcedure);
                    var zionList = userManys.Read<ZionDto>();
                    var areaList = userManys.Read<AreaDto>();
                    var serverList = userManys.Read<ServerDto>();
                    foreach (var item in areaList)
                    {
                        item.ServerList.AddRange(serverList.Where(s => s.AId == item.AId));
                    }
                    foreach (var item in zionList)
                    {
                        item.AreaList.AddRange(areaList.Where(a => a.QId == item.QId));
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ZionDto>(zionList), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message }; ;
                }
            }
        }

        public async Task<APIResult> GetPushInfo()
        {
            string spName = @"up_RMMT_BAS_GetPushInfo_R";

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<PushInfo> list = await conn.QueryAsync<PushInfo>(spName, null, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<PushInfo>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message }; ;
                }
            }
        }

        public async Task<APIResult> GetGroupList()
        {
            string spName = @"up_RMMT_BAS_GetAllGroupList_R";

            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<StatusDto> list = await conn.QueryAsync<StatusDto>(spName, null, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<StatusDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message }; ;
                }
            }

        }
        public async Task<APIResult> GetTypeList(string GroupCode, string Name, string UseYN)
        {
            string spName = @"up_RMMT_BAS_GetTypeList_R";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@GroupCode", GroupCode);
            dp.Add("@Name", Name == null ? "" : Name);
            dp.Add("@UseYN", UseYN);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                try
                {
                    IEnumerable<CodeHiddenDto> list = await conn.QueryAsync<CodeHiddenDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<CodeHiddenDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
                catch (Exception ex)
                {
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message }; ;
                }
            }
        }
        public async Task<APIResult> UpdateType(string UserId, List<CodeHiddenDto> list)
        {
            string XmlData = CommonHelper.Serializer(typeof(List<CodeHiddenDto>), list);
            string spName = @"up_RMMT_BAS_SaveType_U";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@xmlData", XmlData, DbType.Xml);
            dp.Add("@InUserId", UserId, DbType.Int32);

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
        public async Task<APIResult> GetAllDataForLocalDB(string UserId)
        {
            try
            {
                string spName = @"up_RMMT_BAS_GetAllTableForLocalDB_R";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@UserId", UserId, DbType.Int32);
                using (var con = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    con.Open();
                    var list = await con.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    var l1 = list.ReadAsync().Result;
                    var l2 = list.ReadAsync().Result;
                    var l3 = list.ReadAsync().Result;
                    var l4 = list.ReadAsync().Result;
                    var l5 = list.ReadAsync().Result;
                    var l6 = list.ReadAsync().Result;
                    var l7 = list.ReadAsync().Result;
                    var l8 = list.ReadAsync().Result;
                    var l9 = list.ReadAsync().Result;
                    var l10 = list.ReadAsync().Result;
                    var l11 = list.ReadAsync().Result;
                    var l12 = list.ReadAsync().Result;
                    List<DistributorDto> lst1 = JsonConvert.DeserializeObject<List<DistributorDto>>(JsonConvert.SerializeObject(l1));
                    List<PlansDto> lst2 = JsonConvert.DeserializeObject<List<PlansDto>>(JsonConvert.SerializeObject(l2));
                    List<EmployeeDto> lst3 = JsonConvert.DeserializeObject<List<EmployeeDto>>(JsonConvert.SerializeObject(l3));
                    List<TaskOfPlanDto> lst4 = JsonConvert.DeserializeObject<List<TaskOfPlanDto>>(JsonConvert.SerializeObject(l4));
                    List<INFI.API.Models.UsersDto.TaskCardDto> lst5 = JsonConvert.DeserializeObject<List<INFI.API.Models.UsersDto.TaskCardDto>>(JsonConvert.SerializeObject(l5));
                    List<INFI.API.Models.UsersDto.TaskItemDto> lst6 = JsonConvert.DeserializeObject<List<INFI.API.Models.UsersDto.TaskItemDto>>(JsonConvert.SerializeObject(l6));
                    List<INFI.API.Models.UsersDto.CheckStandardDto> lst7 = JsonConvert.DeserializeObject<List<INFI.API.Models.UsersDto.CheckStandardDto>>(JsonConvert.SerializeObject(l7));
                    List<INFI.API.Models.UsersDto.PictureStandardDto> lst8 = JsonConvert.DeserializeObject<List<INFI.API.Models.UsersDto.PictureStandardDto>>(JsonConvert.SerializeObject(l8));
                    List<ScoreDto> lst9 = JsonConvert.DeserializeObject<List<ScoreDto>>(JsonConvert.SerializeObject(l9));
                    List<CheckResultDto> lst10 = JsonConvert.DeserializeObject<List<CheckResultDto>>(JsonConvert.SerializeObject(l10));
                    List<StandardPicDto> lst11 = JsonConvert.DeserializeObject<List<StandardPicDto>>(JsonConvert.SerializeObject(l11));
                    List<HiddenCodeDto> lst12 = JsonConvert.DeserializeObject<List<HiddenCodeDto>>(JsonConvert.SerializeObject(l12));
                    LocalDBListDto dto = new LocalDBListDto()
                    {
                        DistributorLst = lst1,
                        PlansLst = lst2,
                        EmployeeLst = lst3,
                        TaskOfPlanLst = lst4,
                        TaskCardLst = lst5,
                        TaskItemLst = lst6,
                        CheckStandardLst = lst7,
                        PictureStandardLst = lst8,
                        ScoreLst = lst9,
                        CheckResultLst = lst10,
                        StandardPicLst = lst11,
                        HiddenCodeLst = lst12
                    };
                    //List<LocalDBListDto> ldblist = new List<LocalDBListDto>();
                    //ldblist.Add(dto);                   
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<LocalDBListDto>(dto), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }
            }
            catch (Exception ex)
            {

                return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = ex.Message };
            }
        }

        public async Task<APIResult> SaveDealerList(ParamDealerDto paramDto)
        {
            if (paramDto.DisList != null)
            {
                paramDto.DisList = paramDto.DisList.Where(a => a.DisCode != null).ToList<DealerDto>();
            }
            string spName = @"up_RMMT_BAS_SaveDealers_C";
            string xmlDisList = CommonHelper.Serializer(typeof(List<DealerDto>), paramDto.DisList);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@InUserId", paramDto.InUserId, DbType.Int32);
            dp.Add("@XmlData", xmlDisList, DbType.Xml);
            dp.Add("@XmlRootName", "/ArrayOfDealerDto/DealerDto", DbType.String);
            using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
            {
                conn.Open();
                IEnumerable<DisInfoDto> list;
                using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        list = await conn.QueryAsync<DisInfoDto>(spName, dp, tran, null, CommandType.StoredProcedure);
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
                return new APIResult { Body = CommonHelper.EncodeDto<DisInfoDto>(list), ResultCode = ResultType.Success, Msg = "" };
            }
        }
    }
}
