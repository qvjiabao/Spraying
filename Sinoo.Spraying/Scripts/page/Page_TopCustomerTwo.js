$(function () {
    var loadControl = function () {
        $("#ddlUA01013").unbind("change").change(function () {

            var List = getdata.getUA01ByArea($("#ddlUA01013").val());
            var strHtml = "<option value=\"\">请选择</option>";
            if ($(List).length > 0) {
                for (var i = 0; i < $(List).length; i++) {
                    strHtml += "<option value=\"" + List[i].UA01001 + "\">" + List[i].UA01004 + "</option>";
                }
            }
            $("#ddlUA01004").html(strHtml);
        });
    };
    loadControl();
    if ($("#tb1 tr").length == 1) {   //导出提示
        $("#tb1 thead").append("<tr><td colspan=\"10\">系统暂无数据显示</td></tr>");
    }
});


function DataClear() {
    ClearTree();
    $("input[type='text']").val("");
    $("select").val("");
    $("input[type='radio'][name='OrderType']").remove("checked").eq(1).prop("checked", true);
    $("input[type='radio'][name='OA01044']").remove("checked").eq(0).prop("checked", true);
}


$("#demoBtn3").click(function () {
    $('#imgBox').modal({ show: true });
});