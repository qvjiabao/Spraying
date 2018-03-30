$(function () {
    var loadControl = function () {
        $("#ddlGA03ProvinceDialog").unbind("change").change(function () {

            var List = getdata.getCity($("#ddlGA03ProvinceDialog").val());

            var strHtml = "<option value=\"\">请选择</option>";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<option value=\"" + List[i].GA03001 + "\">" + List[i].GA03002 + "</option>";
            }
            $("#ddlGA03CityDialog").html(strHtml);

        });

        $("#btnSelectCustomer").unbind("click").click(function () {

            var List = getdata.getCustomer($("#ddlGA03ProvinceDialog").val(), $("#ddlGA03CityDialog").val(), $("#CustomerName").val());

            var strHtml = "";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<tr><td>" + List[i].CA01002 + "</td><td>" + List[i].CA01003 + "</td><td><a href=\"GA0701New.aspx?CA01001=" + List[i].CA01001 + "&PageIndex="+$('#PageIndex').val()+"\" >新增</a></td></tr>";
            }
            $("#TableBodyCustomer").html(strHtml);
        });
    };
    loadControl();
});

function DataClear() {
    $("input[type='text']").val("");
}


function RemoveNewCss() {
    $("#demoBtn3").remove();
}
function RemoveDownLoadCss() {
    $("a[name='DownLoad']").remove();
}
function RemoveDelCss() {
    $("a[name='Del']").remove();
}

