var FRUIT_URL = "http://epayqaapi.elandcloud.com/api/v1/epay/SearchPayResultSumByStoreDate";
var BASE_URL = "http://59.110.10.73:8005/infi/api/v1/";
var LOGIN_URL = BASE_URL + "Users";

//COMMON start
var SEARCHNOTIFIDIS = BASE_URL + "NotifiApproval/GetDistributorListByUserId";
var SEARCHNOTIFIDEP = BASE_URL + "NotifiApproval/GetNoticeDepartments";
var FILE_BASE_URL = "http://infidevapi.elandcloud.com/uploads/";//文件上传 下载路径
//COMMON end

//IMP start
var GETIMPPLANORRESULTDETAIL = BASE_URL + "ImprovementMng/GetImpPlanOrResultDetail";
var SAVEIMPROVEMENTRESULT = BASE_URL + "ImprovementMng/SaveImprovementResult";
var SAVEIMPROVEMENTITEM = BASE_URL + "ImprovementMng/SaveImprovementItem";
var INSERTALLOCATEIMPROVEMENTITEM = BASE_URL + "ImprovementMng/InsertAllocateImprovementItem";
var GEIMPROVEMENTITEMFROMSCORELIST = BASE_URL + "ImprovementMng/GeImprovementItemFromScoreList";
var GETALLTASKOFPLANFORIMP = BASE_URL + "ImprovementMng/GetAllTaskOfPlanForImp";
var GETIMPITEMFROMSCORE = BASE_URL + "ImprovementMng/GetImpItemFromScore";
//IMP end

//Tou start
var GETTOURDISTRIBUTORLIST = BASE_URL + "Tour/GetTourDistributorList";
var GETTASKLISTBYDISID = BASE_URL + "Tour/GetTaskListByDisId";
var GETITEMLISTBYTASKID = BASE_URL + "Tour/GetItemListByTaskId";
var SAVEITEMAPPROALYN = BASE_URL + "Tour/SaveItemApproalYN";
var SAVECHECKRESULT = BASE_URL + "Tour/SaveCheckResult";
var CUSTOMIZEDTASKCHECK = BASE_URL + "Tour/CustomizedTaskCheck";
var GETCUSTOMIZEDTASKBYTASKID = BASE_URL + "Tour/GetCustomizedTaskByTaskId";
var REGCUSTOMIZEDIMPITEM = BASE_URL + "Tour/RegCustomizedImpItem";
var GETALLPLANSBYDISID = BASE_URL + "Tour/GetAllPlansByDisId";
//Tou end

//Task start
var TASKGETSTATUS = BASE_URL + "PlanTask/GetStatus";
var TASKGETPLANSLIST = BASE_URL + "PlanTask/GetPlansList";
var TASKGETALLDISTRIBUTOR = BASE_URL + "PlanTask/GetAllDistributor";
var TASKGETMANDATORYPLANSDTLBYID = BASE_URL + "PlanTask/GetMandatoryPlansDtlById";
var TASKADDORUPDATEPLANS = BASE_URL + "PlanTask/AddorUpdatePlans";
var TASKGETALLTASKCARD = BASE_URL + "PlanTask/GetAllTaskCard";
var TASKGETPLANSDTLLIST = BASE_URL + "PlanTask/GetPlansDtlList";
var TASKDELETETASKOFPLANS = BASE_URL + "PlanTask/DeleteTaskOfPlans";
var TASKCLOSEPLANBYID = BASE_URL + "PlanTask/ClosePlanById";
var TASKGETREVIEWPLANSLIST = BASE_URL + "PlanTask/GetReviewPlansList";
var TASKGETREVIEWSTATUSBYGROUPCODE = BASE_URL + "PlanTask/GetReviewStatusByGroupCode";
var TASKREVIEWPLANS = BASE_URL + "PlanTask/ReviewPlans";
var TASKCREATETASKCARD = BASE_URL + "PlanTask/CreateTaskCard";
var TASKGETTASKCARDLIST = BASE_URL + "PlanTask/GetTaskCardList";
var GETDISTRIBUTORBYUSERID = BASE_URL + "PlanTask/GetDistributorByUserId";
var CREATEPLANS = BASE_URL + "PlanTask/CreatePlans";
var GETTASKCARDDTLLIST = BASE_URL + "PlanTask/GetTaskCardDtlList";
var UPDATETASKCARD = BASE_URL + "PlanTask/UpdateTaskCard";
var EXCELUPLOADPLANS = BASE_URL + "PlanTask/ExcelUploadPlans";
var PLANSPUSH = BASE_URL + "PlanTask/PlansPush";
var GETPUSHINFOBYPLANID = BASE_URL + "PlanTask/GetPushInfoForPlanId";
//Task end

//CAS start
var SEARCHCASESLIST = BASE_URL + "CasesInfo/GetCaseInfo";
var SAVECASESINFO = BASE_URL + "CasesInfo/SaveCaseInfo";
var UPDATECASESINFO = BASE_URL + "CasesInfo/UpdateCaseInfo";
//CAS end

//BAS start
var SAVEEMPLOYEEINFO = BASE_URL + "Users/SaveEmployeeInfo";
var GETEMPLOYEEINFO = BASE_URL + "Users/GetEmployeeInfo";
var GETDISTRIBUTORINFO = BASE_URL + "Users/GetDistributorInfo";
var SAVEDISTRIBUTORINFO = BASE_URL + "Users/SaveDistributorInfo";
var GETDISTRIBUTORINFO2 = BASE_URL + "Users/GetDistributorInfo2";
var SAVEDISTRIBUTORINFO2 = BASE_URL + "Users/SaveDistributorInfo2";
var GETORGINFO = BASE_URL + "Users/GetOrgInfo";
var GETGROUPLIST = BASE_URL + "Users/GetGroupList";
var GETTYPELIST = BASE_URL + "Users/GetTypeList";
var UPDATETYPE = BASE_URL + "Users/UpdateType";
var INSERTDEALERS = BASE_URL + "Users/InsertDealers";
//BAS end


//NoticeMade start
var SEARCHMADENOTICELIST = BASE_URL + "NotifiMng/SearchLength";
var SEARCHMADENOTICEDETAILINFO = BASE_URL + "NotifiMng/Search";
var SAVENOTICEMADE = BASE_URL + "NotifiMng/Save";
var DELETEMADENOTICELIST = BASE_URL + "NotifiMng/Delete";
var SEARCHNOTICEREADERS = BASE_URL + "NotifiMng/GetList";
var UPDATEREADERREADSTATUS = BASE_URL + "NotifiMng/Update";
var SEARCHDISREADER = BASE_URL + "NotifiMng/SearchDis";
//NoticeMade end


//NoticeFeedBack
var SEARCHNOTICEFEEDBACKLIST = BASE_URL + "NotifiFeedB/searchNoticeFeedbackList";
var SEARCHNOTICEFEEDBACKDTL = BASE_URL + "NotifiFeedB/SearchNoticeFeedBackDtl";
var SAVEFEEDBACKINFO = BASE_URL + "NotifiFeedB/SaveFeedBackNotice";
//END

//NoticeApproal
var GETAPPROVALNOTICELIST = BASE_URL + "NotifiApproval/GetApprovalNoticeList";
var GETAPPROVALREADERLIST = BASE_URL + "NotifiApproval/GetApprovalReaderList";
var GETNOTICEAPPROVALDETAIL = BASE_URL + "NotifiApproval/GetNoticeApprovalDetail";
var NOTICEAPPROVALS = BASE_URL + "NotifiApproval/NoticeApprovalS";
var GETNOTICEAPPROVALLOG = BASE_URL + "NotifiApproval/GetNoticeApprovalLog";
//END

//Calender
var SAVECALENDERITEM = BASE_URL + "CalenderMng/CreateCalenderPlans";
var SEACHCALENDERITEM = BASE_URL + "CalenderMng/GetCalenderListAllWeb";
var DELETECALENDERITEM = BASE_URL + "CalenderMng/DeleteCalenderPlans";
var GETCALENDERLISTALLWEBR02 = BASE_URL + "CalenderMng/GetCalenderListAllWebR02";
//END

//Home
var GETALLDOITEMLIST = BASE_URL + "HomeMng/AllItems";
//END

//报表查看
var SAVEREPORTATTACHMENT = BASE_URL + "Report/SaveReportAttachment";
var UPDATEATTACHMENTDOWNLOADCNT = BASE_URL + "Report/UpdateAttachmentDownloadCnt";
var GETATTACHMENTBYUSERID = BASE_URL + "Report/GetAttachmentByUserId";
var GETPLANSLISTFOREXCELDOWNLOAD = BASE_URL + "Report/GetPlansListForExcelDownload";


var REPORTGETREGION = BASE_URL + "Report/GetRegion";
var REPORTGETAREA = BASE_URL + "Report/GetArea";
//END

//OSS
accessid = '3JkljJxvXgjLz80X';
accesskey = 'L2ERHORPk3WkjqfGUb27RlxvT8x5f3';
host = 'http://vgic.oss-cn-beijing.aliyuncs.com/';

g_dirname = 'AreaTool/'
//END

//Statistic
var GETAREABYUSERID = BASE_URL + "Statistic/GetAreaByUserId";
var GETPATROLDATA = BASE_URL + "Statistic/GetPatrolData";
var GETZONEBYAREAID = BASE_URL + "Statistic/GetZoneByAreaId";
var GETIMPITEMSFORSTATISTIC = BASE_URL + "Statistic/GetImpItems";
var GETDISTRIBUTORBYAREAID = BASE_URL + "Statistic/GetDistributorByAreaId";
var GETINFOBYDISID = BASE_URL + "Statistic/GetInfoByDisId";
var INSERTAFTERSALESDATABYEXCEL = BASE_URL + "Statistic/InsertAfterSalesDataByExcel";
var GETAFTERSALESFIGURES = BASE_URL + "Statistic/GetAftersalesFigures";
var GETAFTERSALESFIGURESFORHIGHCHARTS = BASE_URL + "Statistic/GetAftersalesFiguresForHighCharts";
//END

//Record 
var GETRECORDINFOLIST = BASE_URL + "Record/GetRecordInfoList";
var RECORDINFOSEARCH = BASE_URL + "Record/RecordInfoSearch";
var RECORDINFOREG = BASE_URL + "Record/RecordInfoReg";
var UPDATERECORDINFO = BASE_URL + "Record/UpdateRecordInfo";
//End


//AppealMng
var SEARCHAPPLEALINFOLIST = BASE_URL + "AppealMng/SearchApplealInfoList";
var APPEALINFOREG = BASE_URL + "AppealMng/AppealInfoReg";
var APPEALINFOSEARCH = BASE_URL + "AppealMng/AppealInfoSearch";
var UPDATEAPPLEALINFO = BASE_URL + "AppealMng/UpdateApplealInfo";
//END