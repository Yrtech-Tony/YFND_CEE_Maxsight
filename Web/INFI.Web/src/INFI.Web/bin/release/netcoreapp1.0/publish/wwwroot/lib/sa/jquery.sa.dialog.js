-/// <reference path="jquery.sa.ajax.js" />
(function ($)
{
    var modaldialog = {};

    // Creates and shows the modal dialog
    function showDialog(content, options)
    {
        options['title'] = "提示信息";
        // Merge default title (per type), default settings, and user defined settings
        //settings中包含：title，timeout，showClose，width（options也是可能包含前面那四个）
        if (options.type == "success")
        {
            var settings = $.extend({ title: modaldialog.DialogTitles[options.type] }, modaldialog.defaultsForAlert, options);
        } else
        {
            var settings = $.extend({ title: modaldialog.DialogTitles[options.type] }, modaldialog.defaults, options);
        }

        //create dialog
        var $dialog = $('<div></div>');
        var dialoghtml = "<div class='modal-header' style='margin:1px'>" +
                "<button type='button' class='close'><span aria-hidden='true'>&times;</span><span class='sr-only'>" + "关闭" + "</span></button>" +
                "<h4 class='modal-title'></h4>" +
			"</div>" +
			"<div class='modal-body' style='max-height:500px; overflow:auto'></div>" +
            "<div class='modal-footer'>";

        dialoghtml += "<input type='button' value='" + "确定" + "' class='btn btn-default btn-dialog-ok'>";

        dialoghtml += "<input type='button' value='" + "关闭" + "' class='btn btn-default btn-dialog-close'>";

        dialoghtml += "<input type='button' value='" + "取消" + "' class='btn btn-default btn-dialog-cancel'>";

        dialoghtml += "</div>";
        $dialog.html(dialoghtml);

        var $dialogmask = $('<div></div>');

        $dialogmask.hide();
        $dialog.hide();

        $dialogmask.addClass("dialog-mask");
        $dialog.addClass("dialog");
        if (settings.popIndex != undefined)
        {
            $dialog.addClass(settings.popIndex);
        }

        //为该对象添加close方法，开发者可以自己控制dialog的关闭
        $dialog.close = function ()
        {
            modaldialog.close(this, $(".dialog-mask"));
        }

        //document.body.appendChild(dialogmask);
        //document.body.appendChild(dialog);
        $('body').append($dialogmask);
        $('body').append($dialog);

        //获取dialog元素，方便使用
        var $dlh = $dialog.find(".modal-header");
        var $dlt = $dialog.find(".modal-title");
        var $dlx = $dialog.find(".close");
        var $dlc = $dialog.find(".modal-body");
        var $dlf = $dialog.find(".modal-footer");
        var $dlbclose = $dialog.find(".btn-dialog-close");
        var $dlbok = $dialog.find(".btn-dialog-ok");
        var $dlbcancel = $dialog.find(".btn-dialog-cancel");

        // Set the click event for the "Ok" "Cancel" "x" and "Close" buttons	
        $dlx.on("click", function () { modaldialog.close($dialog, $dialogmask); if (typeof (settings.fnClose) != "undefined") { settings.fnClose(); }; });
        $dlbclose.on("click", function () { modaldialog.close($dialog, $dialogmask); if (typeof (settings.fnClose) != "undefined") { settings.fnClose(); }; });
        //dlbok.on("click", function () { modaldialog.close(dl, $(dialogmask)); if (typeof (settings.fnOk) != "undefined") { settings.fnOk(); }; });
        $dlbok.on("click", function () { if (typeof (settings.fnOk) != "undefined") { settings.fnOk(); }; });
        $dlbcancel.on("click", function () { modaldialog.close($dialog, $dialogmask); if (typeof (settings.fnCancel) != "undefined") { settings.fnCancel(); }; });

        $dlt.append(settings.title);
        $dlc.append(content);

        // set css
        if (settings.maxWidth && $(window).width() > parseInt(settings.maxWidth))
        {
            $dialog.css('width', settings.maxWidth);
        } else
        {
            if (settings.minWidth && $(window).width() < parseInt(settings.minWidth))
            {
                $dialog.css('width', settings.minWidth);
            } else
            {
                if (settings.minWidth)
                {
                    $dialog.css('width', settings.minWidth);
                } else
                {
                    $dialog.css('width', settings.width);
                }
            }
        }

        if (settings.position == modaldialog.Positions[4])
        {
            var dialogTop = Math.abs($(window).height() - $dialog.height()) / 2;
            $dialog.css('left', ($(window).width() - $dialog.width()) / 2);
            $dialog.css('top', (dialogTop >= 25) ? dialogTop : 25);
        } else
        {
            $dialog.addClass(settings.position);
        }

        switch (settings.type)
        {
            case ("error"): $dlh.addClass("alert-danger"); break;
            case ("warning"): $dlh.addClass("alert-warning"); break;
            case ("success"): $dlh.addClass("alert-success"); break;
            case ("confirm"): $dlh.addClass("alert-info"); break;
        }


        if (settings.type == 'confirm')
        {
            $dlbclose.hide();
            $dlbok.show();
            $dlbcancel.show();
        } else
        {
            $dlbclose.show();
            $dlbok.hide();
            $dlbcancel.hide();
        }
        //根据页面定义的显示状态进行显示或者隐藏
        if (settings.showClose)
            $dlx.show();
        if (settings.showCloseBtn)
            $dlbclose.show();
        if (settings.showOKBtn)
            $dlbok.show();
        if (settings.showCancelBtn)
            $dlbcancel.show();

        //move
        if (settings.type != "success")
        {
            $dialog.dialogMove();
            $('body').css("overflow", "hidden");
        }

        //show
        $dialog.fadeIn("slow");
        if (settings.isModal)
        {
            $dialogmask.fadeIn("normal");
        }

        //是否是自动关闭
        if (settings.timeout)
        {
            //            window.setTimeout("dl.fadeOut('slow', 0); $(dialogmask).fadeOut('normal', 0);", (settings.timeout * 1000));
            //window.setTimeout("closeDialog($(dialog),$(dialogmask))", (settings.timeout * 1000));
            //            var autoDl = $(dialog);
            //            var autoDm = $(dialogmask);
            $dialog.fadeOut((settings.timeout * 1000), function () { removeDialog($dialog, $dialogmask); });
            $dialogmask.fadeOut((settings.timeout * 1000));
        }

        //返回该dialog对象，用户可自由控制
        return $dialog;
    };

    //end showDialog

    //-------About Pop--------------------------------------------------------------------------------------

    function showPop(url, options)
    {

        var settings = $.extend({ title: modaldialog.DialogTitles[options.type] }, options);

        //create dialog
        var $dialog = $('<div tabindex="0"><a style="display:none;">hide</a></div>');
        $dialog.html(
			"<div class='modal-header'>" +
                "<button type='button' class='close'><span aria-hidden='true'>&times;</span><span class='sr-only'>" + "Close" + "</span></button>" +
                "<h4 class='modal-title'></h4>" +
			"</div>" +
			"<div class='modal-body'>" +
        //            "<iframe frameborder=0 height=100% width=100% scrolling='auto' onload='$(this).height(0);var height = $(this).contents().height() + 10;$(this).height(height>500?500:height);'>这是个iframe</iframe>" +
        "<iframe frameborder=0 height=100% width=100% scrolling='auto'>这是个iframe</iframe>" +
            "</div>"
			);

        var $dialogmask = $('<div tabindex="0"><a style="display:none;">hide</a></div>');

        //获取dialog元素，方便使用
        var $dl = $dialog;
        var $dlh = $dl.find(".modal-header");
        var $dlt = $dl.find(".modal-title");
        var $dlx = $dl.find(".close");
        var $dlc = $dl.find(".modal-body");
        var $dliframe = $dl.find("iframe");

        $dialogmask.hide();
        $dialog.hide();

        //点击ESC键，关闭Pop add by xu.hailong 2015-1-16
        $("body").bind("keydown", function ()
        {
            if (event.keyCode == 27)
            {
                $dialog.close();
            }
        })

        //添加标题
        $dlt.append(settings.title);
        if (settings.IsHtml)
        {
            $dliframe.remove();
            var $ct = $('<div style="height:100%;overflow:auto"></div>');
            $ct.html(url);
            $dlc.html($ct);
        }
        else
        {
            //构造参数 添加src
            //Modify by jiangyafei 2015-4-3 14:49:59
            var urlParas = "";
            if (options.params != undefined)
            {
                $.each(options.params, function (i)
                {
                    urlParas += (i + "=" + encodeURI(options.params[i]) + "&");
                })
                urlParas = urlParas.substr(0, urlParas.length - 1);
            }
            $dliframe.attr("src", url + "?" + urlParas);
            //End
            //-----------------------load取代上面的iframe方式------
            //dlc.load(url + "?" + urlParas);
        }
        $dialogmask.addClass("dialog-mask");
        $dialog.addClass("dialog");
        if (settings.popIndex != undefined)
        {
            $dialog.addClass(settings.popIndex);
        }

        //为该对象添加close方法，开发者可以自己控制dialog的关闭
        $dialog.close = function ()
        {
            $("body").unbind("keydown");
            modaldialog.close(this, $(".dialog-mask"));
        }

        //pop页面回凋函数 
        window.popcallback = function (data)
        {
            options.popcallback(data);
            modaldialog.close($dialog, $(".dialog-mask"));
        }


        $('body').append($dialogmask);
        $('body').append($dialog);

        // Set the click event for the "Ok" "Cancel" "x" and "Close" buttons	
        $dlx.on("click", function () { modaldialog.close($dl, $dialogmask); });

        // set css
        if (typeof (settings.height) != "undefined")
        {
            $dlc.css('height', settings.height);
        } else
        {
            if (settings.maxHeight && $(window).height() > parseInt(settings.maxHeight))
            {
                $dlc.css('height', settings.maxHeight);
            } else
            {
                if (settings.minHeight && $(window).height() < parseInt(settings.minHeight))
                {
                    $dlc.css('height', settings.minHeight);
                } else
                {
                    if ($(window).height() < 500)
                    {
                        $dlc.css('height', $(window).height() * 0.8 - Math.abs($dlh.height()));
                    }
                    else
                    {
                        $dlc.css('height', 500);
                    }

                }
            }
        }

        if (typeof (settings.width) != "undefined")
        {
            $dl.css('width', settings.width);
        } else
        {
            if ($(window).width() > 600)
            {
                if (settings.maxWidth && $(window).width() > parseInt(settings.maxWidth))
                {
                    $dl.css('width', settings.maxWidth);
                } else
                {
                    if (settings.minWidth && $(window).width() < parseInt(settings.minWidth))
                    {
                        $dl.css('width', settings.minWidth);
                    } else
                    {
                        $dl.css('width', $(window).width() - 200);
                    }
                }
            } else
            {
                if (settings.maxWidth && $(window).width() > parseInt(settings.maxWidth))
                {
                    $dl.css('width', settings.maxWidth);
                } else
                {
                    if (settings.minWidth && $(window).width() < parseInt(settings.minWidth))
                    {
                        $dl.css('width', settings.minWidth);
                    } else
                    {
                        $dl.css('width', $(window).width() - 20);
                    }
                }
            }
        }

        //set position
        var dialogTop = Math.abs($(window).height() - $dl.height()) / 2;
        $dl.css('left', ($(window).width() - $dl.width()) / 2);
        $dl.css('top', (dialogTop >= 25) ? dialogTop : 25);

        //move
        $dl.dialogMove();
        //$('body').css("overflow", "hidden"); //禁用body滚动条

        //show
        $dl.fadeIn("slow");
        $dialogmask.fadeIn("normal");

        //返回该dialog对象，用户可自由控制
        return $dialog;

    };

    //---------end Pop

    modaldialog.error = function $$modaldialog$error(content, options)
    {
        if (typeof (options) == "undefined")
        {
            options = {};
        }
        options['type'] = "error";
        options['title'] = "DialogTitle";
        return (showDialog(content, options));
    }
    modaldialog.warning = function $$modaldialog$error(content, options)
    {
        if (typeof (options) == "undefined")
        {
            options = { title:"DialogTitle" };
        }
        options['type'] = "warning";
        options['title'] = "DialogTitle";
        return (showDialog(content, options));
    }
    modaldialog.success = function $$modaldialog$error(content, options)
    {
        if (typeof (options) == "undefined")
        {
            options = { title: "DialogTitle" };
        }
        options['type'] = "success";
        options['title'] = "DialogTitle";
        return (showDialog(content, options));
    }
    modaldialog.confirm = function $$modaldialog$error(content, options)
    {
        if (typeof (options) == "undefined")
        {
            options = { title: "DialogTitle" };
        }
        options['type'] = "confirm";
        options['title'] = "DialogTitle";
        return (showDialog(content, options));
    }

    modaldialog.close = function closeDialog(dialog, dialogmask)
    {
        dialog.stop();
        dialogmask.stop();

        dialog.fadeOut("slow", function () { removeDialog(dialog, dialogmask); });
        dialogmask.fadeOut("normal");
    };
    function removeDialog(dl, dm)
    {
        //---------Add By JiangYaFei 2015-3-31 10:56:25
        var $dliframe = dl.find("iframe");
        if ($dliframe != "undefined")
        {
            $dliframe.attr("src", "");
            $dliframe.remove();
        }
        //---------End
        document.body.removeChild(dl[0]);
        document.body.removeChild(dm[0]);
        //关闭模态框，显示滚动条
        $('body').css("overflow", "visible");
    }

    modaldialog.pop = function $$modaldialog$pop(url, options)
    {
        if (typeof (options) == "undefined")
        {
            options = {};
        }
        options['type'] = "pop";
        return (showPop(url, options));
    }

    modaldialog.DialogTypes = new Array("error", "warning", "success", "confirm");
    modaldialog.DialogTitles = {
        "error": "!! Error !!"
		, "warning": "Warning!"
		, "success": "Success"
		, "confirm": "Please Choose"
        , "pop": "PopView"
    };

    modaldialog.Positions = new Array("ltop", "rtop", "lbottom", "rbottom", "center");

    modaldialog.defaults = {
        timeout: 0
		, showClose: false
        , showOKBtn: false
        , showCancelBtn: false
        , showCloseBtn: false
		, width: 525
		, isModal: true
        , position: "center"
    };

    modaldialog.defaultsForAlert = {
        timeout: 3
		, showClose: true
		, width: 525
		, isModal: false
        , position: "rbottom"
    };

    $.extend({ modaldialog: modaldialog });
})(jQuery);

(function ($) {
    $.fn.dialogMove = function () {
        var moveObj = $(this);
        var old_position = {};
        var new_position = {};
        var offset = {};
        var isDown = false;
        var topOffSet = 0; //fixed和absolute时top偏差

        var dlh = moveObj.find(".modal-header");
        dlh.css("cursor", "move");

        dlh.mousedown(function (e) {
            old_position = { X: e.clientX, Y: e.clientY };
            offset = moveObj.offset();
            topOffSet = offset.top - parseInt(moveObj.css("top"));
            isDown = true;
            moveObj.css({
                position: "absolute",
                top: offset.top
            });
        });

        dlh.mouseup(function (e) {
            isDown = false;
            moveObj.css({
                position: "fixed",
                top: parseInt(moveObj.css("top")) - topOffSet
            });
        });

        $('body').mousemove(function (e) {
            if (isDown) {
                new_position = { X: e.clientX, Y: e.clientY };
                moveObj.css({
                    left: offset.left + new_position.X - old_position.X,
                    top: offset.top + new_position.Y - old_position.Y
                });
            }
        });
    }
})(jQuery);


//----------------------

(function ($) {

    $.sa.error = function $$sa$error(content, options) {
        return $.modaldialog.error(content, options);
    }
    $.sa.warning = function $$sa$error(content, options) {
        return $.modaldialog.warning(content, options);
    }
    $.sa.alert = function $$sa$error(content, options) {
        return $.modaldialog.success(content, options);
    }
    $.sa.confirm = function $$sa$error(content, options) {
        return $.modaldialog.confirm(content, options);
    }

    $.sa.pop = function $$sa$error(url, options) {
        return $.modaldialog.pop(url, options);
    }

    $.sa.upload = function $$sa$error(options) {
        var options = $.extend({ title: "FileUpload", maxWidth: 800, minWidth: 350 }, options);
        return $.sa.pop('/File/WebUpload', options);
    }
    $.sa.dataupload = function $$sa$error(options) {
        var options = $.extend({ title: "DataUpload", maxWidth: 800, minWidth: 350 }, options);
        return $.sa.pop('/DataUpload/DataUpload', options);
    }

})(jQuery);
