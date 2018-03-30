$(function () {
    var loadControl = function () {

        $("#Find").click(function () {

            var List = getdata.getOrder($("#CA01003").val(), $("#OA01002").val());

            var strHtml = "";
            for (var i = 0; i < $(List).length; i++) {
                strHtml += "<tr><td>" + List[i].CA01003 + "</td><td>" + List[i].OA01002 + "</td><td><a href=\"OA0103New.aspx?OA01001="+List[i].OA01001 +"&PageIndex="+$('#PageIndex').val()+"\" >新增</a></td></tr>";
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
    $("#btnNew").remove();
}
function RemoveDownLoadCss() {
    $("a[name='DownLoad']").remove();
}
function RemoveDelCss() {
    $("a[name='Del']").remove();
}

