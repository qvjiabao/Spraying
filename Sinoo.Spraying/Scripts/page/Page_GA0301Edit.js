﻿function DataClear() {
    $("#txtGA03002").val("");
    $("#txtGA03004").val("");
}

$.ajaxSetup({
    async: false
});
function DataSave() {
    var bol;
    var txtGA03002 = $("#txtGA03002").val();
    var ID = $("#ID").val();
    var Type = "GA03EditProvince";
    $.post("/Handler/UniquenessHandler.ashx", { txtGA03002: txtGA03002, Type: Type, ID: ID }, function (result) {
        if ($.parseJSON(result)["ok"] == "") {
            bol = true;
        } else {
            bol = false;
            alert($.parseJSON(result)["error"]);
            $("#txtGA03002").focus();
        }
    })
    return bol;
}