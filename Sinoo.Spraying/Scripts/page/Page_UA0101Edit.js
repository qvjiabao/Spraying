$(function () {
    var loadControl = function () {

    };
    loadControl();
});
function LinkReturn() {
    window.location.href = "<%=GetReturnUrl()%>";
}
function btnClientSave() {
    var isSave = true;
    $.ajax({
        type: "post",
        url: "/Handler/UniquenessHandler.ashx",
        data: { txtUA01007: $("#txtUA01007").val(), Type: "UA01007Edit", ID: $("#UA01001").val() },
        async: false,
        success: function (data) {
            var result = $.parseJSON(data)["error"];
            if (result != "" && typeof (result) != "undefined") {
                alert(result);
                $("#txtUA01007").focus();
                isSave = false;
            }
        }
    });

    if (!isSave) {
        return isSave;
    }
    $.ajax({
        type: "post",
        url: "/Handler/UniquenessHandler.ashx",
        data: { txtUA01002: $("#txtUA01002").val(), Type: "UA01002Edit", ID: $("#UA01001").val() },
        async: false,
        success: function (data) {
            var result = $.parseJSON(data)["error"];
            if (result != "" && typeof (result) != "undefined") {
                alert(result);
                $("#txtUA01002").focus();
                isSave = false;
            }
        }
    });
    return isSave;
}