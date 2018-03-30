$(function () {
    var loadControl = function () {

        $("#ddlGA03Province").unbind("change").change(function () {

            var List = getdata.getCity($("#ddlGA03Province").val());

            var strHtml = "<option value=\"\">请选择</option>";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<option value=\"" + List[i].GA03001 + "\">" + List[i].GA03002 + "</option>";
            }
            $("#ddlGA03City").html(strHtml);

        });
        $("#ddlCA01019").unbind("change").change(function () {

            var List = getdata.getCB04($("#ddlCA01019").val());

            var strHtml = "<option value=\"\">请选择</option>";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<option value=\"" + List[i].CB04001 + "\">" + List[i].CB04002 + "</option>";
            }
            $("#ddlCA01020").html(strHtml);

        });
    };
    loadControl();
});

function LinkReturn() {
    window.location.href = "CA0101List.aspx";
}

function DataClear() {
    $("input[type='text']").val("");
    $("select").val("");
    $("input[type='radio']").attr("checked", false);
    $("textarea").val("");
}

function btnClientSave() {
    var isSave = true;
    $.ajax({
        type: "post",
        url: "/Handler/UniquenessHandler.ashx",
        data: { txtCA01002: $("#txtCA01002").val(), Type: "CA01New" },
        async: false,
        success: function (data) {
            var result = $.parseJSON(data)["error"];
            if (result != "" && typeof (result) != "undefined") {
                alert(result);
                $("#txtCA01002").focus();
                isSave = false;
            }
        }
    });
    return isSave;
}