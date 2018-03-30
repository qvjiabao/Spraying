
$.ajaxSetup({
    async: false
});
function DataSave() {
    var bol;
    var txtCB04002 = $("#txtCB04002").val();
    var ID = $("#ID").val();
    var Type = "CB04Edit";
    $.post("/Handler/UniquenessHandler.ashx", { txtCB04002: txtCB04002, Type: Type, ID: ID }, function (result) {
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