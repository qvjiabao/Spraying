$(function () {
    var loadControl = function () {

        var ddlGA03Html = $('#ddlGA03html').val();
        if (ddlGA03Html != "") {
            $('#ddlGA03City').html(ddlGA03Html).val($('#ddlGA03Value').val());
        }

        //        var ddlUA01Html = $('#ddlUA01html').val();
        //        if (ddlUA01Html != "") {
        //            $('#ddlUA01').html(ddlUA01Html).val($('#ddlUA01Value').val());
        //        }

        $("#ddlGA03Province").unbind("change").change(function () {

            var List = getdata.getCity($("#ddlGA03Province").val());

            var strHtml = "<option value=\"\">请选择</option>";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<option value=\"" + List[i].GA03001 + "\">" + List[i].GA03002 + "</option>";
            }
            $("#ddlGA03City").html(strHtml);

        });
        //        $("#ddlGA03City").unbind("change").change(function () {

        //            var List = getdata.getUA01($("#ddlGA03City").val());
        //            var strHtml = "<option value=\"\">请选择</option>";

        //            for (var i = 0; i < $(List).length; i++) {
        //                strHtml += "<option value=\"" + List[i].UA01001 + "\">" + List[i].UA01004 + "</option>";
        //            }
        //            $("#ddlUA01").html(strHtml);

        //        });
        $("#ddlGA03ProvinceDialog").unbind("change").change(function () {

            var List = getdata.getCity($("#ddlGA03ProvinceDialog").val());

            var strHtml = "<option value=\"\">请选择</option>";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<option value=\"" + List[i].GA03001 + "\">" + List[i].GA03002 + "</option>";
            }
            $("#ddlGA03CityDialog").html(strHtml);

        });
        $("#btnNew").unbind("click").click(function () {
            $('#MyDilog').modal({ show: true });
        });

        $("#btnSelectCustomer").unbind("click").click(function () {

            var List = getdata.getCustomer($("#ddlGA03ProvinceDialog").val(), $("#ddlGA03CityDialog").val(), $("#CustomerName").val());

            var strHtml = "";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<tr><td>" + List[i].CA01002 + "</td><td>" + List[i].CA01003 + "</td><td><a href=\"OA0101New.aspx?Type=Order&PageIndex=" + $("#PageIndex").val() + "&CA01001=" + List[i].CA01001 + "\" >新增</a></td></tr>";
            }
            $("#TableBodyCustomer").html(strHtml);
        });
    };
    loadControl();
    if ($("#tb1 tr").length == 1) {   //导出提示
        $("#tb1 thead").append("<tr><td colspan=\"8\">系统暂无数据显示</td></tr>");
    }
});
//function Import() {
//    $("#myIframe").attr("src", "CA0101Import.aspx");
//    $('#myModal').modal({ show: true });
//}

function SaveCityCustomer() {
    $("#ddlGA03Value").val($("#ddlGA03City").find('option:selected').val());
    $("#ddlGA03html").val($('#ddlGA03City').html());

    //    $("#ddlUA01Value").val($("#ddlUA01").find('option:selected').val());
    //    $("#ddlUA01html").val($('#ddlUA01').html());
}

function DataClear() {
    $("input[type='text']").val("");
    $("select").val("");
    $("select[name='ddlGA03City']").html("<option value=\"\">请选择</option>");
    $("input[type='radio']").attr("checked", false);
}

function Import() {
    $("#Iframe1").attr("src", "OA0101Import.aspx");
    $('#Div1').modal({ show: true });
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
function RemovePrintCss() {
    $("a[name='print']").remove();
}

