using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INFI.Web.Common;
using INFI.Web.Common.Module;
using Npoi.Core.SS.UserModel;
using Npoi.Core.SS.Util;
using System.IO;
using Npoi.Core.HSSF.Util;
using Microsoft.AspNetCore.Hosting;
using Npoi.Core.XSSF.UserModel;
using Npoi.Core.HSSF.UserModel;
using System.Net.Http;
using System.Drawing;
using System.IO.Compression;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace INFI.Web.Controllers
{
    public class ReportController : Controller
    {

        private IHostingEnvironment _environment;
        public ReportController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        //[Authorize]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult REP010()
        {
            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.ToString("yyyy-MM-dd");
            ViewBag.FirstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd");
            return View();
        }
        public async Task<ActionResult> DownLoadForRename(string fileName = "", string sourcepath = "")
        {
            string contentType = "application/octet-stream";
            byte[] bytes = await CommonHelper.Current.HttpGetFileBytes(sourcepath);
            return this.File(bytes, contentType, fileName);
        }
        [HttpPost]
        public async Task<ActionResult> UploadScoreAjaxDown(string disCode, string startTime, string endTime, string status, string Pid, string disname, string name)
        {
            try
            {
                string result = await CommonHelper.GetHttpClient().GetStringAsync(CommonHelper.Current.GetAPIBaseUrl + "Tour/GetTaskListByDisIdForExcel/" + disCode + "/" + startTime + "/" + endTime + "/" + status + "/" + Pid);
                var apiResult = CommonHelper.DecodeString<APIResult>(result);
                if (apiResult.ResultCode == ResultType.Success)
                {
                    ExcelResult er = CommonHelper.DecodeString<ExcelResult>(apiResult.Body);
                    List<ResultExcelDto> list = er.ResultList;
                    string discode = list.FirstOrDefault<ResultExcelDto>().DisCode;
                    List<LosePic> lpList = er.LPicList;
                    if (lpList.Count % 2 == 1)
                    {
                        lpList.Add(new LosePic());
                    }
                    //未通过体系
                    List<ResultExcelDto> unlist2 = (from a in list where a.PassYN == "0" select a).ToList<ResultExcelDto>();

                    List<string> tiId = (from a in unlist2 select a.TITitle).Distinct().ToList<string>();

                    var uploads = Path.Combine(_environment.WebRootPath, "Template");
                    var newFile = Path.Combine(uploads, "ScoreViewList_" + discode + "_" + disname + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xlsx");
                    if (!System.IO.Directory.Exists(uploads))
                    {
                        System.IO.Directory.CreateDirectory(uploads);
                    }
                    using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                    {

                        IWorkbook workbook = new XSSFWorkbook();

                        ISheet sheet1 = workbook.CreateSheet("得分登记");

                        IFont font_b = workbook.CreateFont();
                        font_b.FontName = "Microsoft Yahei";
                        font_b.FontHeightInPoints = 10;

                        var style1 = (XSSFCellStyle)workbook.CreateCellStyle();
                        //style1.FillForegroundColor = HSSFColor.Grey25Percent.Index;

                        byte[] rgb = new byte[3] { 116, 163, 210 };
                        style1.SetFillForegroundColor(new XSSFColor(rgb));

                        style1.FillPattern = FillPattern.SolidForeground;
                        style1.Alignment = HorizontalAlignment.Center;
                        style1.BorderLeft = BorderStyle.Thin;
                        style1.BorderBottom = BorderStyle.Thin;
                        style1.BorderRight = BorderStyle.Thin;
                        style1.BorderTop = BorderStyle.Thin;
                        style1.SetFont(font_b);

                        var style_Stand = workbook.CreateCellStyle();
                        style_Stand.Alignment = HorizontalAlignment.Left;
                        style_Stand.WrapText = true;
                        style_Stand.SetFont(font_b);

                        var style_b2 = workbook.CreateCellStyle();
                        style_b2.WrapText = true;
                        style_b2.VerticalAlignment = VerticalAlignment.Center;
                        style_b2.SetFont(font_b);


                        var style2 = workbook.CreateCellStyle();
                        style2.Alignment = HorizontalAlignment.Left;
                        style2.SetFont(font_b);

                        var style3 = workbook.CreateCellStyle();
                        style3.Alignment = HorizontalAlignment.Right;
                        style3.SetFont(font_b);

                        var style4 = workbook.CreateCellStyle();
                        style4.Alignment = HorizontalAlignment.Center;
                        style4.VerticalAlignment = VerticalAlignment.Center;
                        style4.SetFont(font_b);

                        var style_t = (XSSFCellStyle)workbook.CreateCellStyle();
                        style_t.Alignment = HorizontalAlignment.Center;
                        style_t.SetFillForegroundColor(new XSSFColor(rgb));
                        //style_t.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                        style_t.FillPattern = FillPattern.SolidForeground;

                        IFont font_t = workbook.CreateFont();
                        font_t.FontHeightInPoints = 18;
                        font_t.FontName = "Microsoft Yahei";
                        style_t.SetFont(font_t);

                        //标题
                        IRow row_t = sheet1.CreateRow(0);
                        ICell cell_t = row_t.CreateCell(0);
                        cell_t.SetCellValue("巡检报告");
                        CellRangeAddress region_t = new CellRangeAddress(0, 0, 0, 6);
                        cell_t.CellStyle = style_t;
                        sheet1.AddMergedRegion(region_t);

                        IRow row = sheet1.CreateRow(1);
                        ICell cell = row.CreateCell(0);
                        cell.SetCellValue("经销商代码:" + discode + "                经销商名称：" + disname);
                        CellRangeAddress region = new CellRangeAddress(1, 1, 0, 6);
                        cell.CellStyle = style2;
                        sheet1.AddMergedRegion(region);



                        IRow row1 = sheet1.CreateRow(2);
                        ICell cel2 = row1.CreateCell(0);
                        //cel2.CellStyle = style;
                        cel2.SetCellValue("期间：" + startTime + "~" + endTime + "    操作者：" + name);
                        cel2.CellStyle = style2;
                        CellRangeAddress region2 = new CellRangeAddress(2, 2, 0, 6);
                        sheet1.AddMergedRegion(region2);

                        IRow row0 = sheet1.CreateRow(3);
                        ICell cel0 = row0.CreateCell(0);
                        //cel2.CellStyle = style;
                        cel0.SetCellValue("计划任务标题:" + (list.Count == 0 ? "" : list[0].PTitle));
                        cel0.CellStyle = style2;
                        CellRangeAddress region3 = new CellRangeAddress(3, 3, 0, 6);
                        sheet1.AddMergedRegion(region3);


                        var style_sub = (XSSFCellStyle)workbook.CreateCellStyle();
                        style_sub.Alignment = HorizontalAlignment.Center;
                        style_sub.SetFillForegroundColor(new XSSFColor(rgb));
                        //style_sub.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                        style_sub.FillPattern = FillPattern.SolidForeground;
                        style_sub.SetFont(font_b);

                        IRow row_sub = sheet1.CreateRow(5);
                        ICell cell_sub = row_sub.CreateCell(0);
                        cell_sub.SetCellValue("巡检结果汇总");
                        CellRangeAddress region_sub = new CellRangeAddress(5, 5, 0, 6);
                        cell_sub.CellStyle = style_sub;
                        sheet1.AddMergedRegion(region_sub);

                        IRow row_6 = sheet1.CreateRow(6);
                        ICell cell_6 = row_6.CreateCell(0);
                        cell_6.SetCellValue("未达标汇总:");
                        cell_6.CellStyle = style2;
                        CellRangeAddress region_6 = new CellRangeAddress(6, 6, 0, 0);
                        sheet1.AddMergedRegion(region_6);


                        if (tiId != null && tiId.Count > 0)
                        {
                            for (int i = 0; i < tiId.Count; i++)
                            {
                                IRow row_un = sheet1.CreateRow(7 + i);
                                ICell cell_un = row_un.CreateCell(0);
                                cell_un.SetCellValue((i + 1) + "、" + tiId[i]);
                                cell_un.CellStyle = style2;
                            }
                        }




                        int pw = 1;
                        int tcw = 1;
                        int tiw = 1;
                        int csw = 1;
                        int rw = 1;
                        if (list != null && list.Count > 0)
                        {
                            for (int k = 0; k < list.Count; k++)
                            {
                                if ((list[k].PTitle == null ? 0 : list[k].PTitle.Length) > pw)
                                {
                                    pw = list[k].PTitle.Length;
                                }
                                if ((list[k].TCTitle == null ? 0 : list[k].TCTitle.Length) > tcw)
                                {
                                    tcw = list[k].TCTitle.Length;
                                }
                                if ((list[k].TITitle == null ? 0 : list[k].TITitle.Length) > tiw)
                                {
                                    tiw = list[k].TITitle.Length;
                                }
                                if ((list[k].CSTitle == null ? 0 : list[k].CSTitle.Length) > csw)
                                {
                                    csw = list[k].CSTitle.Length;
                                }
                                if ((list[k].Remarks == null ? 0 : list[k].Remarks.Length) > rw)
                                {
                                    rw = list[k].Remarks.Length;
                                }
                            }
                        }
                        int dataCount = 10;
                        if (tiId != null && tiId.Count >= 0)
                        {
                            dataCount = tiId.Count + 10;
                        }
                        IRow row3 = sheet1.CreateRow(dataCount);

                        //任务卡名称
                        ICell cell6 = row3.CreateCell(0);
                        cell6.SetCellValue("任务卡名称");
                        cell6.CellStyle = style1;
                        CellRangeAddress region6 = new CellRangeAddress(dataCount, dataCount, 0, 0);
                        sheet1.AddMergedRegion(region6);
                        sheet1.SetColumnWidth(0, 7 * 600);
                        //体系名称
                        ICell cell7 = row3.CreateCell(1);
                        cell7.SetCellValue("体系名称");
                        cell7.CellStyle = style1;
                        CellRangeAddress region7 = new CellRangeAddress(dataCount, dataCount, 1, 1);
                        sheet1.AddMergedRegion(region7);
                        sheet1.SetColumnWidth(1, 7 * 600);
                        //检查标准
                        ICell cell8 = row3.CreateCell(2);
                        cell8.SetCellValue("检查标准");
                        cell8.CellStyle = style1;
                        ICell cell9 = row3.CreateCell(3);
                        cell9.SetCellValue("");
                        cell9.CellStyle = style1;
                        ICell cell_9 = row3.CreateCell(4);
                        cell_9.SetCellValue("");
                        cell_9.CellStyle = style1;
                        CellRangeAddress region8 = new CellRangeAddress(dataCount, dataCount, 2, 4);
                        sheet1.AddMergedRegion(region8);
                        sheet1.SetColumnWidth(2, (9 * 300));
                        sheet1.SetColumnWidth(3, (400));
                        sheet1.SetColumnWidth(4, (15 * 300));
                        //结果
                        //ICell cell9 = row3.CreateCell(3);
                        // cell9.SetCellValue("结果");
                        // cell9.CellStyle = style1;
                        //CellRangeAddress region9 = new CellRangeAddress(dataCount, dataCount, 4, 4);
                        //sheet1.AddMergedRegion(region9);


                        //得分
                        ICell cell10 = row3.CreateCell(5);
                        cell10.SetCellValue("得分");
                        cell10.CellStyle = style1;
                        CellRangeAddress region10 = new CellRangeAddress(dataCount, dataCount, 5, 5);
                        sheet1.AddMergedRegion(region10);
                        sheet1.SetColumnWidth(5, 1200);

                        //备注
                        ICell cell11 = row3.CreateCell(6);
                        cell11.SetCellValue("备注");
                        cell11.CellStyle = style1;
                        CellRangeAddress region11 = new CellRangeAddress(dataCount, dataCount, 6, 6);
                        sheet1.AddMergedRegion(region11);
                        sheet1.SetColumnWidth(6, 9 * 600);


                        int firstRow1 = dataCount + 1;
                        int lastRow1 = 0;
                        int sumCnt1 = 0;
                        int firstRow2 = dataCount + 1;
                        int lastRow2 = 0;
                        int sumCnt2 = 0;
                        int firstRow3 = dataCount + 1;
                        int lastRow3 = 0;
                        int sumCnt3 = 0;
                        //数据的动态加载
                        for (int i = 0; i < list.Count; i++)
                        {

                            object r = "r_" + i;
                            r = sheet1.CreateRow(dataCount + 1 + i);
                            int cnum = 0;
                            //计划任务名称

                            //任务卡名称
                            object c = "c_" + cnum;
                            c = (r as IRow).CreateCell(cnum);
                            (c as ICell).SetCellValue(list[i].TCTitle);
                            (c as ICell).CellStyle = style_b2;
                            if (i == sumCnt1)
                            {
                                int cnt = (from l1 in list where l1.TCId == list[i].TCId select l1).Count();
                                sumCnt1 += cnt;
                                lastRow1 = firstRow1 + cnt - 1;
                                if (list[i].CSId != null)
                                {
                                    sheet1.AddMergedRegion(new CellRangeAddress(firstRow1, lastRow1, 0, 0));
                                }
                                firstRow1 = lastRow1 + 1;
                            }
                            cnum++;


                            //体系名称
                            c = "c_" + cnum;
                            c = (r as IRow).CreateCell(cnum);
                            (c as ICell).SetCellValue(list[i].TITitle);
                            (c as ICell).CellStyle = style_b2;
                            //sheet1.AddMergedRegion(new CellRangeAddress(5 + i, 5 + i, 7 + cnum, 7 + cnum));
                            if (i == sumCnt2)
                            {
                                int cnt = (from l1 in list where l1.TCId == list[i].TCId && l1.TIId == list[i].TIId select l1).Count();
                                sumCnt2 += cnt;
                                lastRow2 = firstRow2 + cnt - 1;
                                if (list[i].CSId != null)
                                {
                                    sheet1.AddMergedRegion(new CellRangeAddress(firstRow2, lastRow2, 1, 1));
                                }
                                firstRow2 = lastRow2 + 1;
                            }
                            cnum++;


                            //检查标准名称
                            c = "c_" + cnum;
                            c = (r as IRow).CreateCell(cnum);
                            (c as ICell).SetCellValue(list[i].CSTitle);
                            (c as ICell).CellStyle = style_Stand;
                            //sheet1.AddMergedRegion(new CellRangeAddress(dataCount + i, dataCount + i, 7 + cnum, 7 + cnum));
                            //sheet1.AddMergedRegion(new CellRangeAddress(dataCount + i, dataCount + i, 2, 3));
                            cnum++;

                            //检查标准名称
                            c = "c_" + cnum;
                            c = (r as IRow).CreateCell(cnum);
                            (c as ICell).SetCellValue("");
                            (c as ICell).CellStyle = style_Stand;
                            cnum++;

                            //检查标准名称
                            c = "c_" + cnum;
                            c = (r as IRow).CreateCell(cnum);
                            (c as ICell).SetCellValue("");
                            (c as ICell).CellStyle = style_Stand;
                            sheet1.AddMergedRegion(new CellRangeAddress(dataCount + 1 + i, dataCount + 1 + i, 2, 4));
                            cnum++;

                            //结果
                            /*c = "c_" + cnum;
                            c = (r as IRow).CreateCell(cnum);
                            (c as ICell).SetCellValue(list[i].Result == "False" ? "否" : (list[i].Result == null ? "" : "是"));
                            (c as ICell).CellStyle = style4;
                            sheet1.AddMergedRegion(new CellRangeAddress(dataCount + i, dataCount + i, 7 + cnum, 7 + cnum));
                            cnum++;*/

                            //得分
                            c = "c_" + cnum;
                            c = (r as IRow).CreateCell(cnum);
                            (c as ICell).SetCellValue(list[i].Score);
                            (c as ICell).CellStyle = style4;
                            //sheet1.AddMergedRegion(new CellRangeAddress(dataCount + i, dataCount + i, 7 + cnum, 7 + cnum));
                            cnum++;

                            //备注
                            c = "c_" + cnum;
                            c = (r as IRow).CreateCell(cnum);
                            (c as ICell).SetCellValue(list[i].Remarks);
                            (c as ICell).CellStyle = style_b2;
                            if (i == sumCnt3)
                            {
                                int cnt = (from l1 in list where l1.TCId == list[i].TCId && l1.TIId == list[i].TIId select l1).Count();
                                sumCnt3 += cnt;
                                lastRow3 = firstRow3 + cnt - 1;
                                if (list[i].CSId != null)
                                {
                                    sheet1.AddMergedRegion(new CellRangeAddress(firstRow3, lastRow3, 6, 6));
                                }
                                firstRow3 = lastRow3 + 1;
                            }
                            cnum++;

                        }

                        IRow row_sub2 = sheet1.CreateRow(dataCount + 2 + list.Count);
                        ICell cell_sub2 = row_sub2.CreateCell(0);
                        cell_sub2.SetCellValue("巡检照片");
                        CellRangeAddress region_sub2 = new CellRangeAddress(dataCount + 2 + list.Count, dataCount + 2 + list.Count, 0, 6);
                        cell_sub2.CellStyle = style_sub;
                        sheet1.AddMergedRegion(region_sub2);

                        for (int i = 0; i < lpList.Count; i++)
                        {
                            XSSFClientAnchor anchor;
                            IRow row_pic = sheet1.CreateRow(dataCount + 2 + list.Count + i + 1);
                            if (i % 2 == 1)
                            {
                                row_pic.Height = 4000;
                                anchor = new XSSFClientAnchor(50, 50, 50, 50, 4, dataCount + 2 + list.Count + i + 1, 7, dataCount + 2 + list.Count + i + 2);
                            }
                            else
                            {
                                ICell cell_pic = row_pic.CreateCell(0);
                                cell_pic.CellStyle = style_b2;
                                ICell cell_pic2 = row_pic.CreateCell(4);
                                cell_pic2.CellStyle = style_b2;

                                CellRangeAddress region_tp_1 = new CellRangeAddress(dataCount + 2 + list.Count + i + 1, dataCount + 2 + list.Count + i + 1, 0, 2);
                                sheet1.AddMergedRegion(region_tp_1);
                                CellRangeAddress region_tp_2 = new CellRangeAddress(dataCount + 2 + list.Count + i + 1, dataCount + 2 + list.Count + i + 1, 4, 6);
                                sheet1.AddMergedRegion(region_tp_2);

                                if (lpList[i].PicName != null)
                                {
                                    cell_pic.SetCellValue((i + 1) + "、第" + (i + 1) + "个拍照点的名称" + lpList[i].PicName);
                                }
                                if (lpList[i + 1].PicName != null)
                                {
                                    cell_pic2.SetCellValue((i + 2) + "、第" + (i + 2) + "个拍照点的名称" + lpList[i + 1].PicName);
                                }
                                anchor = new XSSFClientAnchor(50, 50, 50, 50, 0, dataCount + 2 + list.Count + i + 2, 3, dataCount + 2 + list.Count + i + 3);
                            }
                            if (lpList[i].PicUrl != null)
                            {
                                string imagesPath = lpList[i].PicUrl;
                                HttpClient webClient = new HttpClient();
                                Stream stream = await webClient.GetStreamAsync(imagesPath);
                                Image img = Image.FromStream(stream).GetThumbnailImage(500, 500, null, IntPtr.Zero);
                                MemoryStream ms = new MemoryStream();
                                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                var patriarch = sheet1.CreateDrawingPatriarch();
                                anchor.AnchorType = AnchorType.MoveAndResize;
                                int index = workbook.AddPicture(ms.ToArray(), PictureType.PNG);
                                var signaturePicture = patriarch.CreatePicture(anchor, index);
                            }

                        }
                        workbook.Write(fs);
                        return Json(newFile);
                    }
                }
                else
                {
                    return Json("没有查询结果.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
