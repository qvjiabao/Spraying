function DataClear() {
    $("#txtCB04002").val("");
    $("#txtCB04004").val("");

    $("#txtCB04005").val("");
    $("select[name=drpCB04003]").val("");
}

$.ajaxSetup({
    async: false
});
function DataSave() {
    var bol;
    var txtCB04002 = $("#txtCB04002").val();
    var Type = "CB04New";
    $.post("/Handler/UniquenessHandler.ashx", { txtCB04002: txtCB04002, Type: Type }, function (result) {
        if ($.parseJSON(result)["ok"] == "") {
            bol = true;
        } else {
            bol = false;
            alert($.parseJSON(result)["error"]);
            $("#txtCB04002").focus();
        }
    })
    return bol;
}