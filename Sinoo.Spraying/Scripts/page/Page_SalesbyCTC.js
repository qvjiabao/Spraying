$(function () {
    var loadControl = function () {

        var ddlUA01html = $('#ddlUA01html').val();
        if (ddlUA01html != "") {
            $('#ddlUA01').html(ddlUA01html).val($('#ddlUA01value').val());
        }

        $("#ddlUA01013").unbind("change").change(function () {

            var List = getdata.getUA01ByArea($("#ddlUA01013").val());
            var strHtml = "<option value=\"\">请选择</option>";
            if ($(List).length > 0) {
                for (var i = 0; i < $(List).length; i++) {
                    strHtml += "<option value=\"" + List[i].UA01001 + "\">" + List[i].UA01004 + "</option>";
                }
            }
            $("#ddlUA01").html(strHtml);

        });
    };
    loadControl();
    if ($("#tb1 tr").length == 1) {   //导出提示
        $("#tb1 thead").append("<tr><td colspan=\"5\">系统暂无数据显示</td></tr>");
    }
});


function DataClear() {
    $("input[type='text']").val("");
    $("select").val("");
    $("select[name='ddlUA01']").html("<option value=\"\">请选择</option>");
    $("input[type='radio'][name='rbtnOA01044']").removeAttr("checked").eq(0).prop("checked", true);
    $("input[name='rbtnOA01003']").removeAttr("checked").eq(1).prop("checked", true);
    ClearTree();
}
function SaveCustomerHtml() {
    $("#ddlUA01value").val($("#ddlUA01").find('option:selected').val());
    $("#ddlUA01html").val($('#ddlUA01').html());
}
$("#demoBtn3").click(function () {
    $('#imgBox').modal({ show: true });
});