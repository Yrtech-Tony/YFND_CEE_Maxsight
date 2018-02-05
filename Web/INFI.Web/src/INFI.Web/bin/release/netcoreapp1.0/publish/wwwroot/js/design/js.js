// JavaScript Document
// 调用日历

$(document).ready(function(){
    (function(){
        //$('#toc').toc();
    })();
    //$("#calendar").asDatepicker({
    //        namespace: 'calendar',
    //        lang: 'zh',
    //        position: 'bottom'
    //    });
    
    $("#calendar2").asDatepicker({mode: 'range',namespace: 'calendar',
            lang: 'zh',
            position: 'bottom'});

    $("#calendar3").asDatepicker({
        mode: 'multiple', 
        calendars: 4 
    });

    $("#calendar4").asDatepicker();
    
    $('#calendar-mobile-single').asDatepicker({
        mobileMode: true
    });
    $('#calendar-mobile-range').asDatepicker({
        mode: 'range',
        mobileMode: true
    });
    $('#calendar-mobile-multiple').asDatepicker({
        mode: 'multiple',
        mobileMode: true
    });



    $('.asDatepicker').asDatepicker();
    $('.calendar-multiple').asDatepicker({mode: 'multiple', calendars: 3});

    $('#calendar-api-displayMode').asDatepicker({displayMode: 'inline'});

    $('#calendar-api-show-click').click(function(){
        $('#calendar-api-show').asDatepicker('show');
        return false;
    });
    $('#calendar-api-hide-click').click(function(){
        $('#calendar-api-hide').asDatepicker('hide');
        return false;
    });

    $('#api-multipleClear-click').click(function() {
        $("#calendar-api-multipleClear").asDatepicker('multipleClear');
        return false;
    });

    $('#api-getDate-click').click(function() {
        var html = '<div>' + $('#calendar-api-getDate').asDatepicker('getDate') + '</div>';
        $(html).prependTo($('#api-getDate-info'));
        return false;
    });

    $('#api-getDate-format-click').click(function() {
        var html = '<div>' + $('#calendar-api-getDate-format').asDatepicker('getDate', 'yyyy-mm-dd') + '</div>';
        $(html).prependTo($('#api-getDate-format-info'));
        return false;
    });

    $('#api-update-click').click(function() {
         $('#calendar-api-update').asDatepicker('update', {mode: 'range'});
        
        return false;
    });
});


$(function(){ 
    $('.input_test').bind({ 
        focus:function(){ 
        if (this.value == this.defaultValue){ 
            this.value=""; 
        } 
    }, 
        blur:function(){ 
            if (this.value == ""){ 
                this.value = this.defaultValue; 
            } 
        } 
    }); 
}) 
// 拟定通知部分
$('.jingxiao').click(function() {
  $('.meng').animate({opacity: 1}, 600);
  $('.meng').css({display: 'block'});
});
$('.yesbtn').click(function() {
  $('.meng').animate({opacity: 0}, 600);
  $('.meng').css({
    display: 'none'
  });
});
$('.juese').click(function() {
  $('.meng1').animate({opacity: 1}, 600);
  $('.meng1').css({
    display: 'block'
  });
});
$('.yesbtn').click(function() {
  $('.meng1').animate({opacity: 0}, 600);
  $('.meng1').css({
    display: 'none' 
  });
});
// 查看上传的文件
$('.changefile').toggle(function() {
  $('#upfiles').slideDown();
}, function() {
  $('#upfiles').slideUp();
});


// 文件上传
$(function () {  
      (function (i) {  
          $(".uploadImg").click(  
              function () {  
                  $(".uploadImg .file").first().change(function () {  
                      i = i + 1;  
                      var html = ' <input type="file" size="1" id="file' + i + '" name="file' + i + '" class="file" />';  
                      var oldelem = $("#file" + (i - 1));  
                      $(html).insertBefore(oldelem);  
                      $(".uploadImg").append(oldelem.hide());  

            var path = $(this).val();
              if (path.indexOf("\\") > -1) {
                path = path.substr(path.lastIndexOf("\\")+1)
              } 
                      var uphtml = $(' <li class="fix"><div>' +path+ '</div><div class="del" data=""' + $(this).attr("id") + '"></div></li>');  
                      $("#upfiles").append(uphtml);  
                  });  
              });  
      })(1);  

      //$(".del").live("click", function () {  
      //    var id = $(this).attr("data");  
      //    $("#" + id).remove();  
      //    $(this).parent("li").remove();  

      //});  

  });
// 删除按钮
$('#upfiles').hover(function(){
  $(this).find('.delimg').attr("src", "images/del_03.png"); 
},function(){
 $(this).find('.delimg').attr("src", "images/shanchu_03.png"); 
})

// 查询按钮
$('.wbjs').toggle(function() {
  $(this).next('.downcon').slideDown();
}, function() {
  $(this).next('.downcon').slideUp();
});
$('.downcon li').click(function(){
    $(this).parent(".downcon").siblings(".wbjs").text($(this).text()); 
     $(this).parent(".downcon").slideUp();
})
// 驳回意见
$('.bohui').toggle(function() {
  $('.bohui_con').animate({opacity:1}, 400);
  $('.bohui_con').css('display','block');
}, function() {
  $('.bohui_con').animate({opacity:0}, 400);
  $('.bohui_con').css('display','none');
});
// 审核部分删除附件
$('.fujian_con .del').click(function() {
   $(this).parent("li").remove();  
});
// 查询按钮
$('.chaxun').click(function(){
  $('.renwuka_con').css('display','block');
})
// 计划任务制作部分
// 添加行
$('.tianjia').click(function(){
  $('.jhrwzz li table').append('<tr><td><input type="text"></td><td><input type="text"></td><td><input type="text"style="width: 394px"></td><td><input type="text"></td><td class="jhrwzz_del"></td></tr>')
  $('.jhrwzz li table .jhrwzz_del').click(function(){
    $(this).parent("tr").remove();
  })
})
// 删除行
  $('.jhrwzz li table .jhrwzz_del').click(function(){
    $(this).parent("tr").remove();
  })
// 首页tab切换
$.fn.extend({
  Tap:function(){
    var _this=$(this);//指向调用tap函数的所有者
    $(_this).find('.index_tabbt li').click(function(){
      var i=$(_this).find('.index_tabbt li').index(this);
      $(_this).find('.index_tabbt li').eq(i).addClass('tabbtshow').siblings().removeClass('tabbtshow');
      $(_this).find('.index_tabcon span').eq(i).addClass('tabshow').siblings().removeClass('tabshow');
    })
  }
})
$('#box1').Tap();
$('#box2').Tap();