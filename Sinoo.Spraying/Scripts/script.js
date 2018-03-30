$(document).ready(function(e) {
    /*全选*/
    $("#checkall").click(function() {
        if (this.checked) {
            $("input[name='checkname']").each(function() {
                this.checked = true;
            });
        } else {
            $("input[name='checkname']").each(function() {
                this.checked = false;
            });
        }
    });

    /*日历*/
    $(".rqxz,.rqxz_1").datepicker({
        showButtonPanel: true
    });

    /*
 	侧边栏
	*/
    $(".side_control").click(function() {
        if ($("body").hasClass("s")) {
            $("body").removeClass("s");
            $(this).removeClass("on");
        } else {
            $("body").addClass("s");
            $(this).addClass("on");
        }
    });
/*邮件满意度回访记录*/
    $(".widget-title").click(function() {

        var that = $(this);
        if (that.find("i").hasClass("icon-minus-sign")) {
            that.find("i").removeClass("icon-minus-sign").addClass("icon-plus-sign");
        } else {
            $("i.icon-minus-sign").removeClass("icon-minus-sign").addClass("icon-plus-sign");
            that.find("i.icon-plus-sign").removeClass("icon-plus-sign").addClass("icon-minus-sign");
        }

    });
	
	/*
 	控制首页content高度
	*/
	(function(){
		
		var wHeight = $(window).height();
		
		var headerH = $("#header").outerHeight(true);
		
		$("#content").height(wHeight - headerH + 10);
			
	})();
	
	
	
	$("#sidebar > ul ul li").click(function(){
		$("#sidebar > ul ul li.active").removeClass("active");	
		
	    $(this).addClass("active");
	});
	
	

});

$((function($) {
    $.datepicker.regional['zh-CN'] = {
        clearText: '清除',
        clearStatus: '清除已选日期',
        closeText: '关闭',
        closeStatus: '不改变当前选择',
        prevText: '<上月',
        prevStatus: '显示上月',
        prevBigText: '<<',
        prevBigStatus: '显示上一年',
        nextText: '下月>',
        nextStatus: '显示下月',
        nextBigText: '>>',
        nextBigStatus: '显示下一年',
        currentText: '今天',
        currentStatus: '显示本月',
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        monthNamesShort: ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'],
        monthStatus: '选择月份',
        yearStatus: '选择年份',
        weekHeader: '周',
        weekStatus: '年内周次',
        dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
        dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
        dayStatus: '设置 DD 为一周起始',
        dateStatus: '选择 m月 d日, DD',
        dateFormat: 'yy-mm-dd',
        firstDay: 1,
        initStatus: '请选择日期',
        isRTL: false
    };
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
})(jQuery));