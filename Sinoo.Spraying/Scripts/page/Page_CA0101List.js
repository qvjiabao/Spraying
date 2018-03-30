$(function () {
    var loadControl = function () {

        var ddlGA03Html = $('#ddlGA03Html').val();
        if (ddlGA03Html != "") {
            $('#ddlGA03City').html(ddlGA03Html).val($('#ddlGA03SelectValue').val());
        }

        $("#ddlGA03Province").unbind("change").change(function () {

            var List = getdata.getCity($("#ddlGA03Province").val());

            var strHtml = "<option value=\"\">请选择</option>";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<option value=\"" + List[i].GA03001 + "\">" + List[i].GA03002 + "</option>";
            }
            $("#ddlGA03City").html(strHtml);

        });
    };
    loadControl();
    if ($("#tb1 tr").length == 1) {   //导出提示
        $("#tb1 thead").append("<tr><td colspan=\"9\">系统暂无数据显示</td></tr>");
    }
});
function Import() {
    $("#myIframe").attr("src", "CA0101Import.aspx");
    $('#myModal').modal({ show: true });
}

function SaveCity() {
    $('#ddlGA03Html').val($('#ddlGA03City').html());
    $('#ddlGA03SelectValue').val($('#ddlGA03City').find('option:selected').val());
}

function DataClear() {
    $("input[type='text']").val("");
    $("select").val("");
    $("select[name='ddlGA03City']").html("<option value=\"\">请选择</option>");
    $("input[type='radio']").prop("checked", false);
}
function RemoveExportCss() {
    $("#btnExport").remove();
}
function RemoveImportCss() {
    $("#btnImport").remove();
}
function RemoveNewCss() {
    $("#btnNew").remove();
}
function RemoveEditCss() {
    $("a[name='Edit']").remove();
}
function RemoveViewCss() {
    $("a[name='View']").remove();
}
function RemoveDelCss() {
    $("a[name='Del']").remove();
}
function RemoveAddOrderCss() {
    $("a[name='AddOrder']").remove();
}