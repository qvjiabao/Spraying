$(function () {
    var loadControl = function () {
        $("#ddlUA01013").unbind("change").change(function () {
            var List = getdata.getUA01SalesName($("#ddlUA01013").val());
            var strHtml = "<option value=\"\">请选择</option>";

            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<option value=\"" + List[i].UA01004 + "\">" + List[i].UA01004 + "</option>";
            }
            $("#ddlUA01").html(strHtml);

        });
    };
    loadControl();
});