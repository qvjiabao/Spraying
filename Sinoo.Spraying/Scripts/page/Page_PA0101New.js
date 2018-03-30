function DataClear() {
    $("input[type=text]").val("");
    $("textarea").val("");
}
$.ajaxSetup({
    async: false
});
function DataSave() {
    var bol;
    var txtPA01003 = $("#txtPA01003").val().trim();
    var Type = "PA01New";
    $.post("/Handler/UniquenessHandler.ashx", { txtPA01003: txtPA01003, Type: Type }, function (result) {
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