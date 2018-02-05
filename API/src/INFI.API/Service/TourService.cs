using Dapper;
using Newtonsoft.Json;
using INFI.API.Common;
using INFI.API.Context;
using INFI.API.Models;
using INFI.API.Models.Tour;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace INFI.API.Service
{
    public interface ITourService
    {
        Task<APIResult> GetTourDistributorList(int userId, string disCode, string disName);
        Task<APIResult> GetTaskListByDisId(int disId, string startTime, string endTime, string status, string sourceType,int inUserId);
        Task<APIResult> GetItemListByTaskId(int taskId, string operation);
        Task<APIResult> GetItemInfoForScore(int tPId, int tIId, int seqNo);
        Task<APIResult> SaveCheckResult(ScoreCheckResultParam param);
        Task<APIResult> SaveItemApproalYN(ItemApproalParams param);
        Task<APIResult> GetCustomizedTaskByTaskId(int taskId, string operation);
        Task<APIResult> CustomizedTaskCheck(CustomizedTaskDto param);
        Task<APIResult> UpLoadAllScoreInfo(AllTaskInfoRegLstDto param);
        Task<APIResult> UploadLocalDB(LocalDBUploadParams param);
        Task<APIResult> GetTaskListByDisIdForExcel(string disCode, string startTime, string endTime, string status, string Pid);
        Task<APIResult> InsertCustomizedImpItem(CustomizedImpItemDto param);
        Task<APIResult> SearchAllPlansByDisId(string inUserId, string userType, string disId);
    }
    public class TourService : ITourService
    {
        public async Task<APIResult> GetTourDistributorList(int userId, string disCode, string disName)
        {
            try
            {
                string spName = @"up_RMMT_TOU_DistributorList_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@UserId", userId, DbType.Int32);
                dp.Add("@DisCode", disCode, DbType.String);
                dp.Add("@DisName", disName, DbType.String);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<TourDistributorDto> list = await conn.QueryAsync<TourDistributorDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<TourDistributorDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }
        public async Task<APIResult> GetTaskListByDisId(int disId, string startTime, string endTime, string status, string sourceType, int inUserId)
        {
            try
            {
                if (startTime == null)
                {
                    startTime = "";
                }
                if (endTime == null)
                {
                    endTime = "";
                }
                if (status == null)
                {
                    status = "";
                }
                if (sourceType == null)
                {
                    sourceType = "";
                }
                string spName = @"up_RMMT_TOU_TaskListByDisId_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@DisId", disId, DbType.Int32);
                dp.Add("@StartTime", startTime, DbType.String);
                dp.Add("@EndTime", endTime, DbType.String);
                dp.Add("@Status", status, DbType.String);
                dp.Add("@SourceType", sourceType, DbType.String);
                dp.Add("@InUserId", inUserId, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<TaskOfPlanDto> list = await conn.QueryAsync<TaskOfPlanDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<TaskOfPlanDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> GetItemListByTaskId(int taskId, string operation)
        {
            try
            {
                operation = operation == null ? "" : operation;
                string spName = @"up_RMMT_TOU_ItemListByTaskId_R"; 

                 DynamicParameters dp = new DynamicParameters();
                dp.Add("@TaskId", taskId, DbType.Int32);
                dp.Add("@Operation", operation, DbType.String);

                //using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                //{
                //    conn.Open();

                //    var list = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                //    var r1 = list.ReadAsync().Result;
                //    var r2 = list.ReadAsync().Result;
                //    var r3 = list.ReadAsync().Result;

                //    List<ItemOfTaskDto> lst = JsonConvert.DeserializeObject<List<ItemOfTaskDto>>(JsonConvert.SerializeObject(r1));
                //    List<CheckStandard> lst2 = JsonConvert.DeserializeObject<List<CheckStandard>>(JsonConvert.SerializeObject(r2));
                //    List<StandardPic> lst3 = JsonConvert.DeserializeObject<List<StandardPic>>(JsonConvert.SerializeObject(r3));
                //    foreach (var item in lst)
                //    {
                //        item.CSList = new List<CheckStandard>();
                //        item.CSList.AddRange(from l1 in lst2 where l1.TPId == item.TPId && l1.TIId == item.TIId && l1.SeqNo == item.SeqNo select l1);
                //        item.SPicList = new List<StandardPic>();
                //        item.SPicList.AddRange(from l1 in lst3 where l1.TPId == item.TPId && l1.TIId == item.TIId && l1.SeqNo == item.SeqNo select l1);
                //    }
                //    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ItemInfoForScore>(lst), ResultCode = ResultType.Success, Msg = "" };
                //    return result;
                //}

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    var list = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    var r1 = list.ReadAsync().Result;
                    var r2 = list.ReadAsync().Result;
                    var r3 = list.ReadAsync().Result;
                    var r4 = list.ReadAsync().Result;

                    ObservableCollection<ItemOfTaskDto> lst = JsonConvert.DeserializeObject<ObservableCollection<ItemOfTaskDto>>(JsonConvert.SerializeObject(r1));
                    ObservableCollection<CheckStandard> lst2 = JsonConvert.DeserializeObject<ObservableCollection<CheckStandard>>(JsonConvert.SerializeObject(r2));
                    ObservableCollection<StandardPic> lst3 = JsonConvert.DeserializeObject<ObservableCollection<StandardPic>>(JsonConvert.SerializeObject(r3));
                    ObservableCollection<PictureStandard> lst4 = JsonConvert.DeserializeObject<ObservableCollection<PictureStandard>>(JsonConvert.SerializeObject(r4));

                    foreach (var item in lst)
                    {
                        item.CSList = new ObservableCollection<CheckStandard>();
                        item.SPicList = new ObservableCollection<StandardPic>();
                        item.PStandardList = new ObservableCollection<PictureStandard>();
                        foreach (var i1 in lst2)
                        {
                            if (item.TPId == i1.TPId && item.TIId == i1.TIId && item.SeqNo == i1.SeqNo)
                            {
                                item.CSList.Add(i1);
                            }
                        }
                        foreach (var i3 in lst3)
                        {
                            if (item.TPId == i3.TPId && item.TIId == i3.TIId && item.SeqNo == i3.SeqNo)
                            {
                                item.SPicList.Add(i3);
                            }
                        }
                        foreach (var i4 in lst4)
                        {
                            if (item.TPId == i4.TPId && item.TIId == i4.TIId && item.SeqNo == i4.SeqNo)
                            {
                                item.PStandardList.Add(i4);
                            }
                        }

                    }
                    string message = "";
                    if (lst.Count == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ItemInfoForScore>(lst), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> GetItemInfoForScore(int tPId, int tIId, int seqNo)
        {
            try
            {
                string spName = @"up_RMMT_TOU_ItemInfoForScore_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@TPId", tPId, DbType.Int32);
                dp.Add("@TIId", tIId, DbType.Int32);
                dp.Add("@SeqNo", seqNo, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();


                    var list = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    var r1 = list.ReadAsync().Result;
                    var r2 = list.ReadAsync().Result;
                    var r3 = list.ReadAsync().Result;

                    List<ItemInfoForScore> lst = JsonConvert.DeserializeObject<List<ItemInfoForScore>>(JsonConvert.SerializeObject(r1));
                    List<CheckStandard> lst2 = JsonConvert.DeserializeObject<List<CheckStandard>>(JsonConvert.SerializeObject(r2));
                    List<StandardPic> lst3 = JsonConvert.DeserializeObject<List<StandardPic>>(JsonConvert.SerializeObject(r3));
                    foreach (var item in lst)
                    {
                        item.CSList = new List<CheckStandard>();
                        item.CSList.AddRange(lst2);
                        item.SPicList = new List<StandardPic>();
                        item.SPicList.AddRange(lst3);
                    }

                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ItemInfoForScore>(lst), ResultCode = ResultType.Success, Msg = "" };
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> SaveCheckResult(ScoreCheckResultParam param)
        {
            try
            {
                string XmlScore = CommonHelper.Serializer(typeof(List<ScoreRegDto>), param.ScoreLst);
                string XmlCheckResult = CommonHelper.Serializer(typeof(List<CheckResultRegDto>), param.CheckResultLst);
                string XmlStandardPic = CommonHelper.Serializer(typeof(List<StandardPicRegDto>), param.StandardPicLst);
                string XmlPictureStandard = CommonHelper.Serializer(typeof(List<PictureStandard>), param.PicStandLst);
                string spName = @"up_RMMT_TOU_ScoreReg_C";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@XmlScore", XmlScore, DbType.Xml);
                dp.Add("@XmlCheckResult", XmlCheckResult, DbType.Xml);
                dp.Add("@XmlStandardPic", XmlStandardPic, DbType.Xml);
                dp.Add("@XmlPictureStandard", XmlPictureStandard, DbType.Xml);
                dp.Add("@InUserId", param.UserId, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {

                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
                }

            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> SaveItemApproalYN(ItemApproalParams param)
        {
            try
            {
                string spName = @"up_RMMT_TOU_ItemApproalYN_C";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@TPId", param.TPId, DbType.Int32);
                dp.Add("@TIId", param.TIId, DbType.Int32);
                dp.Add("@Score", param.Score, DbType.Int32);
                dp.Add("@PlanApproalYN", param.PlanApproalYN, DbType.Boolean);
                dp.Add("@ResultApproalYN", param.ResultApproalYN, DbType.Boolean);
                dp.Add("@UserId", param.UserId, DbType.Int32);

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

        public async Task<APIResult> GetCustomizedTaskByTaskId(int taskId, string operation)
        {
            try
            {
                string spName = @"up_RMMT_TOU_CustomizedTaskByTaskId_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@TaskId", taskId, DbType.Int32);
                dp.Add("@Operation", operation, DbType.String);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<CustomizedTaskDto> list = await conn.QueryAsync<CustomizedTaskDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<CustomizedTaskDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> CustomizedTaskCheck(CustomizedTaskDto param)
        {
            try
            {
                string spName = @"up_RMMT_TOU_CustomizedTaskCheck_C";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@ScoreId", param.ScoreId, DbType.Int32);
                dp.Add("@TPId", param.TPId, DbType.Int32);
                dp.Add("@Remarks", param.Remarks, DbType.String);
                dp.Add("@UserId", param.UserId, DbType.Int32);

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

        public async Task<APIResult> UpLoadAllScoreInfo(AllTaskInfoRegLstDto param)
        {
            try
            {
                if (param != null && param.TaskInfoRegDtoLst != null)
                {
                    foreach (var item in param.TaskInfoRegDtoLst)
                    {
                        if (item.TPType == "C")//自定义任务
                        {
                            await CustomizedTaskCheck(item.CustomizedTask);
                        }
                        else
                        {
                            await SaveCheckResult(item.ScoreCheckResult);
                        }
                    }
                }
                return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }
        public async Task<APIResult> UploadLocalDB(LocalDBUploadParams param)
        {
            try
            {
                string XmlScore = CommonHelper.Serializer(typeof(List<ScoreLDB>), param.Score);
                string XmlCheckResult = CommonHelper.Serializer(typeof(List<CheckResultLDB>), param.CheckResult);
                string XmlStandardPic = CommonHelper.Serializer(typeof(List<StandardPicLDB>), param.StandardPic);
                string XmlTaskOfPlan = CommonHelper.Serializer(typeof(List<TaskOfPlanLDB>), param.TaskOfPlan);
                string XmlCustImproveItem = CommonHelper.Serializer(typeof(List<CustImproveItemDB>), param.CustImproveItem);
                string spName = @"up_RMMT_TOU_LocalDBUpload_C";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@XmlScore", XmlScore, DbType.Xml);
                dp.Add("@XmlCheckResult", XmlCheckResult, DbType.Xml);
                dp.Add("@XmlStandardPic", XmlStandardPic, DbType.Xml);
                dp.Add("@XmlTaskOfPlan", XmlTaskOfPlan, DbType.Xml);
                dp.Add("@XmlCustImproveItem", XmlCustImproveItem, DbType.Xml);
                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {

                        await conn.ExecuteAsync(spName, dp, tran, null, CommandType.StoredProcedure);
                        tran.Commit();
                    }
                    return new APIResult { Body = "", ResultCode = ResultType.Success, Msg = "" };
                }

            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> GetTaskListByDisIdForExcel(string disCode, string startTime, string endTime, string status, string Pid)
        {
            try
            {
                if (startTime == null)
                {
                    startTime = "";
                }
                if (endTime == null)
                {
                    endTime = "";
                }
                if (status == null)
                {
                    status = "";
                }
                string spName = @"up_RMMT_TOU_TaskListByDisIdForExcel_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@DisCode", disCode, DbType.Int32);
                dp.Add("@StartTime", startTime, DbType.String);
                dp.Add("@EndTime", endTime, DbType.String);
                dp.Add("@Status", status, DbType.String);
                dp.Add("@Pid", Pid, DbType.Int32);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();
                    var list = await conn.QueryMultipleAsync(spName, dp, null, null, CommandType.StoredProcedure);
                    var r1 = list.ReadAsync().Result;
                    var r2 = list.ReadAsync().Result;

                    List<ResultExcelDto> lst = JsonConvert.DeserializeObject<List<ResultExcelDto>>(JsonConvert.SerializeObject(r1));
                    List<LosePic> lst2 = JsonConvert.DeserializeObject<List<LosePic>>(JsonConvert.SerializeObject(r2));

                    ExcelResult er = new ExcelResult();
                    er.ResultList = new List<ResultExcelDto>();
                    er.LPicList = new List<LosePic>();
                    foreach (var item in lst)
                    {
                        er.ResultList.Add(item);
                    }
                    foreach (var item in lst2)
                    {
                        er.LPicList.Add(item);
                    }
                    string message = "";
                    if (lst.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<ExcelResult>(er), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

        public async Task<APIResult> InsertCustomizedImpItem(CustomizedImpItemDto param)
        {
            try
            {
                string spName = @"up_RMMT_TOU_AddCustomImpItem_C";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@PId", param.PId, DbType.Int32);
                dp.Add("@ImpTitle", param.ImpTitle, DbType.String);
                dp.Add("@ImpDesc", param.ImpDesc, DbType.String);
                dp.Add("@PlanApproalYN", param.PlanApproalYN, DbType.Boolean);
                dp.Add("@PlanFinishDate", param.PlanFinishDate, DbType.DateTime);
                dp.Add("@ResultApproalYN", param.ResultApproalYN, DbType.Boolean);
                dp.Add("@ResultFinishDate", param.ResultFinishDate, DbType.DateTime);
                dp.Add("@Remark", param.Remark, DbType.String);
                dp.Add("@InUserId", param.InUserId, DbType.Int32);
                dp.Add("@TCKind", param.TCKind, DbType.String);

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

        public async Task<APIResult> SearchAllPlansByDisId(string inUserId, string userType, string disId)
        {
            try
            {
                string spName = @"up_RMMT_TOU_GetPlansByInUserId_R";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@InUserId", inUserId, DbType.Int32);
                dp.Add("@UserType", userType, DbType.String);
                dp.Add("@DisId", disId, DbType.String);

                using (var conn = new SqlConnection(DapperContext.Current.SqlConnection))
                {
                    conn.Open();

                    IEnumerable<PlanDto> list = await conn.QueryAsync<PlanDto>(spName, dp, null, null, CommandType.StoredProcedure);
                    string message = "";
                    if (list.Count() == 0)
                    {
                        message = "没有数据";
                    }
                    APIResult result = new APIResult { Body = CommonHelper.EncodeDto<PlanDto>(list), ResultCode = ResultType.Success, Msg = message };
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new APIResult { Body = "", ResultCode = ResultType.Failure, Msg = ex.Message };
            }
        }

    }
}
