
$(function () {
    var setting = {
        check: {
            enable: true
        },
        data: {
            simpleData: {
                enable: true
            }
        }

    }
    var List;
    $.ajax({
        url: getdata.getControllerUrl() + "BindMenuHandler.ashx?Type=View&GA02001=" + $("#txtGA02001").val() + "&time=" + new Date().getTime(),
        async: false,
        dataType: 'json',
        type: 'POST',
        cache: false,
        success: function (result) {
            List = result;
        }
    });

    $.fn.zTree.init($("#treeDemo"), setting, List);

    $.ajax({
        url: getdata.getControllerUrl() + "BindPowerHandler.ashx?Type=View&GA02001=" + $("#txtGA02001").val() + "&time=" + new Date().getTime(),
        async: false,
        dataType: 'json',
        type: 'POST',
        cache: false,
        success: function (result) {
            List = result;
        }
    });

    $.fn.zTree.init($("#treeDemo1"), setting, List);

});