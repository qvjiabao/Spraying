$(function () {
    if ($("#tb1 tr").length == 1) {   //导出提示
        $("#tb1 thead").append("<tr><td colspan=\"7\">系统暂无数据显示</td></tr>");
    }
})

function Import() {
    $("#myIframe").attr("src", "PA0101Import.aspx");
    $('#myModal').modal({ show: true });
}
function DataClear() {
    $("input[type=text]").val("");
}

function RemoveExportCss() {
    $("#btnExPort").remove();
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
