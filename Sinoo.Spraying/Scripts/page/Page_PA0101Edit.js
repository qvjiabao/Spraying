
$.ajaxSetup({
    async: false
});
function DataSave() {
    var bol;
    var txtPA01003 = $("#txtPA01003").val();
    var ID = $("#ID").val();
    var Type = "PA01Edit";
    $.post("/Handler/UniquenessHandler.ashx", { txtPA01003: txtPA01003, Type: Type, ID: ID }, function (result) {
        if ($.parseJSON(result)["ok"] == "") {
            bol = true;
        } else {
            bol = false;
            alert($.parseJSON(result)["error"]);
            $("#txtPA01003").focus();
        }
    })
    return bol;
}