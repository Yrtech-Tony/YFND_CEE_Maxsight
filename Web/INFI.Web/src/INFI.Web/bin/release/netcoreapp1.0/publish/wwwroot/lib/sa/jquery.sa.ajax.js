// @reference ../jquery-1.9.1.js
// @reference ./popup/jquery.sa.popup.js
// @reference ../spin.js

// TODO : error customzing
// loading bar 여러번

$.sa = $.sa || {};

$.sa.loadingOpts = {
    type: "image",
    imagePath: "/images/ajax-loader.gif",
    lines: 13, // The number of lines to draw
    length: 7, // The length of each line
    width: 4, // The line thickness
    radius: 10, // The radius of the inner circle
    corners: 1, // Corner roundness (0..1)
    rotate: 0, // The rotation offset
    color: '#000', // #rgb or #rrggbb
    speed: 1, // Rounds per second
    trail: 60, // Afterglow percentage
    shadow: false, // Whether to render a shadow
    hwaccel: false, // Whether to use hardware acceleration
    className: 'spinner', // The CSS class to assign to the spinner
    zIndex: 2e9, // The z-index (defaults to 2000000000)
    top: 'auto', // Top position relative to parent in px
    left: 'auto' // Left position relative to parent in px
};

//application/json;charset=utf-8
$.sa.type = {
    dataType: "json",
    contentType: "json"
};

$.each(["get", "post","put"], function (i, method) {
    //$.sa[method] = function (url, data, callback, type, loading, loadingOptions) {
    //type: is image or spin
    $.sa[method] = function (url, data, callback, loading, loadingOptions, type, sync) {
        // shift arguments if data argument was omitted
        if (jQuery.isFunction(data) || data.success !== undefined) {
            sync = type;
            type = loadingOptions;
            loadingOptions = loading;
            loading = callback;
            callback = data;
            data = undefined;
        }
        var contentType = { json: "application/json;charset=utf-8", form: "application/x-www-form-urlencoded; charset=UTF-8" };
        var type = $.extend({}, $.sa.type, type);
        var loadingOpts = $.extend({}, $.sa.loadingOpts, loadingOptions);

        var efwCallback = {
            success: function (data, statusText, jqXHR) { },
            failure: function (data, statusText, jqXHR) {
                //Modify By JiangYafei 2015-1-19 09:04:37
                if (data.realStatus == 401) {
                    window.location.href = data.returnurl;
                } else {
                    $.sa.error(data.msg);
                }
                //End Modify
            },
            jsonError: function (data, statusText, jqXHR) {
                $.sa.error("Json Syntax Error");
            },
            serverError: function (xhr, statusText, thrownError) {
                //Modify By JiangYaFei 2015-1-19 09:08:21
                var detail = "";
                var data = $.parseJSON(xhr.responseText);
                //add by xiao.xinmiao 2015-12-30 01:04:09
                if (data.msg != undefined) {
                    $.sa.error(data.msg);
                }
                else {
                    for (var key in data) {
                        detail += "[" + key + "] " + data[key] + "<br />";
                        //End Modify
                    }
                    $.sa.error(detail, { 'title': xhr.status + " - " + thrownError });
                }
            }
        };
        if (jQuery.isFunction(callback)) {
            efwCallback.success = callback;
        } else {
            $.extend(efwCallback, callback);
        }

        var ajaxCallback = function (data, statusText, jqXHR) {
            if (data.resultCode == "0") {
                efwCallback.success(data, statusText, jqXHR);
            } else if (data.resultCode == "1") {
                efwCallback.failure(data, statusText, jqXHR);
            } else {
                efwCallback.jsonError(data, statusText, jqXHR);
            }
        };

        var loadingImage = null;
        var loadingBackground = null;
        var spinner = null;

        if (loading && loading.length > 0) {
            loadingBackground = $("<div style='position:absolute;'></div>");

            if (loadingOpts.type == "image") {
                loadingImage = $("<img src='" + loadingOpts.imagePath + "' style='position:absolute'>");
                loadingBackground.append(loadingImage.css({
                    "top": loading.outerHeight() / 2 - 50,
                    "left": loading.outerWidth() / 2 - 50
                })).css({
                    "backgroundColor": "rgba(0, 0, 0, 0.2)",
                    "z-index": "999"
                });
            } else {
                spinner = new Spinner(loadingOpts).spin(loading[0]);
            }

            $("body").append(loadingBackground.css({
                "top": loading.offset().top + "px",
                "left": loading.offset().left + "px",
                "width": loading.outerWidth() + "px",
                "height": $(document).height() + "px"
            }));
        }



        return jQuery.ajax({
            type: method,
            url: url,
            data: data,
            success: ajaxCallback,
            traditional: true,
            dataType: type.dataType,
            async: !(sync),
            contentType: contentType[type.contentType],
            headers: { 'Authorization': 'Bearer ' + fn_GetCookie("infi_ACCESS_TOKEN") }
        }).error(function (xhr, statusText, thrownError) {
            efwCallback.serverError(xhr, statusText, thrownError);
        }).always(function () {
            if (loadingOpts.type == "image") {
                if (loadingImage) {
                    loadingImage.remove();
                }
            } else {
                if (spinner) {
                    spinner.stop();
                }
            }
            if (loadingBackground) {
                loadingBackground.remove();
            }
        });
    };
});


