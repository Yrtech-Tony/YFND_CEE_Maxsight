// Write your Javascript code.
//创建select option
//Create By zhang.jie 2016-09
function JsonToCboList(nameKey, valueKey, dataJson, options) {
    var cboList = "";
    if (options && options.SelectType) {
        if (options.SelectType.toUpperCase() == "SELECT") {
            cboList += "<option Value='select'>- 请选择 -</option>";
        }
        else if (options.SelectType.toUpperCase() == "ALL") {
            cboList += "<option Value='0'>- 全部 -</option>";
        } else if (options.SelectType.toUpperCase() == "BLANK") {
            cboList += "<option Value=''></option>";
        }
    }

    if (dataJson && dataJson.length != 0) {
        for (var i = 0; i < dataJson.length; i++) {
            if (dataJson[i][nameKey] && dataJson[i][valueKey]) {
                cboList += "<option  Value=" + dataJson[i][valueKey] + ">" + dataJson[i][nameKey] + "</option>";
            }
        }
    }

    return cboList;
}

//给日期控件注册格式验证事件，验证不通过，则恢复原值
//Create By zhang.jie 2016-09-22
function DateFormatCheckEvent(elementID, options) {
    var _oldValueDate;
    $("#" + elementID).prop('maxLength', '10');

    if (options && options.CheckType.toUpperCase() == "BLANK") {

        $("#" + elementID)
             .focus(function () {
                 _oldValueDate = $(this).val(); //获取焦点时，取得日期作为原值
             })
             .blur(function () {
                 if ($.trim($(this).val()) == '') {
                     $(this).val('');
                 }
                 else if (!FormatDate(elementID)) {
                     $(this).val(_oldValueDate); //失去焦点时，验证日期格式，验证不通过则恢复原值
                 }
             });
    } else {
        $("#" + elementID)
        .focus(function () {
            _oldValueDate = $(this).val(); //获取焦点时，取得日期作为原值
        })
        .blur(function () {
            if (!FormatDate(elementID)) {
                $(this).val(_oldValueDate); //失去焦点时，验证日期格式，验证不通过则恢复原值
                $(this).datetimepicker('update'); //Add by zhang.lulu 2015年12月16日08:27:47
            }
        });
    }
}

//Format Date yyyy-mm-dd 
//Create By zhang.jie 2016-09-22
function FormatDate(dateID) {
    var date_after_input = $("#" + dateID).val();
    var check = true;

    if (date_after_input.length != 10) {
        check = false;
    } else {
        var arr = date_after_input.split('-');
        if (arr.length != 3) {
            check = false;
        } else {
            if (arr[0].length != 4) {
                check = false;
            }
            if (arr[1].length != 2) {
                check = false;
            }
            if (arr[2].length != 2) {
                check = false;
            }
            //check num
            if (!(/^\d+$/.test(arr[0]) && /^\d+$/.test(arr[1]) && /^\d+$/.test(arr[2]))) {
                check = false;
            }

            if ((arr[0] % 4 == 0 && arr[0] % 100 != 0) || (arr[0] % 100 == 0 && arr[0] % 400 == 0)) {
                if (arr[1] == "02") {
                    if (parseInt(arr[2], 10) > 29) {
                        check = false;
                    }
                }
            } else {
                if (arr[1] == "02") {
                    if (parseInt(arr[2], 10) > 28) {
                        check = false;
                    }
                }
            }

            if (parseInt(arr[1], 10) > 12 || parseInt(arr[1], 10) < 0) {
                check = false;
            }

            if (arr[1] == "01" || arr[1] == "03" || arr[1] == "05" || arr[1] == "07" || arr[1] == "08" || arr[1] == "10" || arr[1] == "12") {
                if (parseInt(arr[2], 10) > 31 || parseInt(arr[2], 10) < 0) {
                    check = false;
                }
            }

            if (arr[1] == "04" || arr[1] == "06" || arr[1] == "09" || arr[1] == "11") {
                if (parseInt(arr[2], 10) > 30 || parseInt(arr[2], 10) < 0) {
                    check = false;
                }
            }

            if (parseInt(arr[0], 10) < 1900) {
                check = false;
            }
        }
    }
    return check;
}

function fn_GetBrowerClient8Width() {
    var height = window.document.body.clientHeight * 0.8;
    return height + "px";
}
function fn_GetBrowerClient6Width() {
    var height = window.document.body.clientHeight * 0.6;
    return height + "px";
}
function NameValueListToJson(nameKey, valueKey, dataJson, options) {
    var json = {};
    if (options && options.SelectType) {
        if (options.SelectType.toUpperCase() == "SELECT") {
            json['select'] = "- " + $.getResource("Select") + " -";
        }
        else if (options.SelectType.toUpperCase() == "ALL") {
            json['0'] = "- " + $.getResource("All") + " -";
        } else if (options.SelectType.toUpperCase() == "BLANK") {
            json[''] = "";
        }
    }
    if (dataJson && dataJson.length != 0) {
        for (var i = 0; i < dataJson.length; i++) {
            if (dataJson[i][nameKey] && dataJson[i][valueKey]) {
                json[dataJson[i][valueKey]] = dataJson[i][nameKey];
            }
        }
    }
    return json;
}
//获取传递参数值
function fn_GetRequest() {
    var url = decodeURI(location.search); //获取url中"?"符后的字串    

    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        var strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
}

Request = fn_GetRequest();

//获取指定名称的cookie的值
function fn_GetCookie(objname) {
    var arrstr = document.cookie.split("; ");
    for (var i = 0; i < arrstr.length; i++) {
        var temp = arrstr[i].split("=");
        if (temp[0] == objname) return unescape(temp[1]);
    }
}

//Unicode转中文
function Reconvert(str) {
    str = str.replace(/(\\u)(\w{1,4})/gi, function ($0) {
        return (String.fromCharCode(parseInt((escape($0).replace(/(%5Cu)(\w{1,4})/g, "$2")), 16)));
    });
    str = str.replace(/(&#x)(\w{1,4});/gi, function ($0) {
        return String.fromCharCode(parseInt(escape($0).replace(/(%26%23x)(\w{1,4})(%3B)/g, "$2"), 16));
    });
    str = str.replace(/(&#)(\d{1,6});/gi, function ($0) {
        return String.fromCharCode(parseInt(escape($0).replace(/(%26%23)(\d{1,6})(%3B)/g, "$2")));
    });

    return str;
}

/***
options:
tipTitle:tip框的标题
tipType:弹出的tip是否可编辑，默认不可编辑,可编辑时值为“E”
tipEditYNCol:标示可否编辑的列名，值为E时可以编辑，其余为不可编辑
tipWidth:tip框的宽度
tipHeight:tip框的高度
***/
//Create By cheng.cuicui 2016-04-28
function fn_TipOk(grdID, trID, colName) {
    var tipElement = "td[aria-describedby='" + grdID + "_" + colName + "']";
    $("table[id='" + grdID + "'] tr[id='" + trID + "'] " + tipElement).text($("#txtContent").val());
    $("table[id='" + grdID + "'] tr[id='" + trID + "']").addClass("edited");
}

function fn_tipped(grdID, colName, options) {
    var title = ""; //标题
    var content = ''; //内容
    var type = ''; //可编辑状态
    var width = '';
    var height = '';
    var editYNCol = '';

    if (options) {
        title = options.tipTitle;
        type = options.tipType;
        width = options.tipWidth;
        height = options.tipHeight;
        editYNCol = options.tipEditYNCol;
    }

    if (editYNCol != undefined && editYNCol != "") {
        Tipped.create("table[id='" + grdID + "'] td[aria-describedby='" + grdID + "_" + colName + "']", //要弹hover的标签，不同的Grid不一样
                    function (element) {
                        if ($(element).parent().find("td[aria-describedby='" + grdID + "_" + editYNCol + "']").text() == "E") {
                            var txtOk = "<button class='btn btn-primary' id='btnOk' onclick=\"fn_TipOk('" + grdID + "','" + $(element).parent().attr('id') + "','" + colName + "')\"><i class='glyphicon glyphicon-ok'></i></button></center>";
                            content = "<center><textarea id='txtContent' style='padding:2px;resize:none;min-height:70px;border:dashed 1px;width:" + width + ";height:" + height + "'>" + element.innerHTML.replace("&nbsp;","").replace(/\r/ig, "").replace(/\n/ig, "<br/>")+ "</textarea><br/>" + txtOk;
                        } else {
                            content = "<div style='width:" + width + ";height:" + height + "'>" + element.innerHTML.replace(/\r/ig, "").replace(/\n/ig, "<br/>"); +"</div>";
                        }
                        return {
                            title: title,
                            content: content
                        };
                    },
                    {
                        skin: 'light',
                        size: 'x-small',
                        radius: true,
                        position: 'bottomleft',
                        onShow: function (element) {
                            $(element).addClass('highlight');
                        }
                    });
    } else {
        Tipped.create("table[id='" + grdID + "'] td[aria-describedby='" + grdID + "_" + colName + "']", //要弹hover的标签，不同的Grid不一样
                    function (element) {
                        if (type == "E") {
                            var txtOk = "<button class='btn btn-primary' id='btnOk' onclick=\"fn_TipOk('" + grdID + "','" + $(element).parent().attr('id') + "','" + colName + "')\"><i class='glyphicon glyphicon-ok'></i></button></center>";
                            content = "<center><textarea id='txtContent' style='resize:none;min-height:70px;border:dashed 1px;width:" + width + ";height:" + height + "'>" + element.innerHTML.replace("&nbsp;", "").replace(/\r/ig, "").replace(/\n/ig, "<br/>") + "</textarea><br/>" + txtOk;
                        } else {
                            content = "<div style='width:" + width + ";height:" + height + "'>" + element.innerHTML.replace(/\r/ig, "").replace(/\n/ig, "<br/>"); +"</div>";
                        }
                        return {
                            title: title,
                            content: content
                        };
                    },
                    {
                        skin: 'light',
                        size: 'x-small',
                        radius: true,
                        position: 'bottomleft',
                        onShow: function (element) {
                            $(element).addClass('highlight');
                        }
                    });
    }
}

//设置Cookie
function Setcookie(name, value) {

    //设置名称为name,值为value的Cookie
    var expdate = new Date();   //初始化时间
    expdate.setHours(expdate.getHours() + 12 * 24 * 30);   //时间
    document.cookie = name + "=" + value + ";expires=" + expdate.toGMTString() + ";path=/";
}

//获取Cookie
function GetCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}

//检查Cookie是否存在
function CheckCookie(name) {
    cookieValue = GetCookie(name)
    if (cookieValue != null && cookieValue != "")
        return true;
    else
        return false;
}
/// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
        };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
    if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
    }
//调用： 

//var time1 = new Date().Format("yyyy-MM-dd");
//var time2 = new Date().Format("yyyy-MM-dd HH:mm:ss");  