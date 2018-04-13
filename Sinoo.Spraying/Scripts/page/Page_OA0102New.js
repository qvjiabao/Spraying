$("#form1").validator({
    stopOnError: false,
    groups: [
    {
        fields: "txtOA01015 txtOA01049",
        callback: function ($elements) {
            var me = this, count = 0;
            $elements.each(function () {
                if (me.test(this, 'required')) count += 1;
            });
            if (count == 0) {
                $("#ddlOA01016").val("");
                $("#ddlOA01016").attr("disabled", "disabled");
                $('#form1').validator('hideMsg', '#ddlOA01016');
            } if (count == 1) {
                $("#ddlOA01016").removeAttr("disabled").attr("data-rule", "required").trigger("validate");
            }
            return count == 1 || count == 0 || '请录入一项';
        },
        target: "#txtOA01049"

    }, {
        fields: "txtOA01017 txtOA01050",
        callback: function ($elements) {
            var me = this, count = 0;
            $elements.each(function () {
                if (me.test(this, 'required')) count += 1;
            });
            if (count == 0) {
                $("#ddlOA01018").val("");
                $("#ddlOA01018").attr("disabled", "disabled");
                $('#form1').validator('hideMsg', '#ddlOA01018');
            } if (count == 1) {
                $("#ddlOA01018").removeAttr("disabled").attr("data-rule", "required").trigger("validate");
            }
            return count == 1 || count == 0 || '请录入一项';
        },
        target: "#txtOA01050"
    }]
});


$("#txtOA01015").unbind("click").click(function () {
    $("#myIframe").attr("src", "OA0101Share.aspx?Cid=txtOA01015");
    $('#myModal').modal({ show: true });
});
$("#txtOA01017").unbind("click").click(function () {
    $("#myIframe").attr("src", "OA0101Share.aspx?Cid=txtOA01017");
    $('#myModal').modal({ show: true });
});

function DataClear() {
    $("input[type=text]").val("");
    $("select").val("");
}


var tr;
var td;

var isAdd = true;
function AddProductInput() {
    if (!isAdd) {
        return false;
    }
    var html = "<tr>";
    html += "<td><input type=\"text\" name=\"PartNO\" onblur=\"GetProduct(this,$('#txtOA01039').val())\" /></td>";
    html += "<td><input type=\"text\" name=\"Description\"  maxlength=\"50\" /></td>";
    html += "<td><input type=\"text\" name=\"Qty\" onblur=\"checkNum(this);Sum(this)\"  maxlength=\"20\"/></td>";
    html += "<td><input type=\"text\" name=\"Price\"  maxlength=\"20\" onblur=\"checkNum(this);Sum(this)\"  /></td>";
    html += "<td><span name=\"Amount\"></span></td>";
    html += "<td><span name=\"NetPrice\"></span></td>";
    html += "<td><span name=\"NetAmount\"></span></td>";
    html += "<td><span name=\"TAX\"></span></td>";
    html += "<td><input type=\"text\" onblur=\"checkNum(this);Sum(this)\"  maxlength=\"20\"  name=\"UnitCost\" /></td>";
    html += "<td><span name=\"TotalCost\"></span></td>";
    html += "<td><a href=\"javascript:\" name=\"btnSave\" onclick=\"AddProduct(this)\">保存</a>&nbsp;<a href=\"javascript:\" name=\"btnSave\" onclick=\"$(this).parents('tr').eq(0).remove();isAdd=true;\">取消</a></td>";
    html += "<td style=\"display:none;\" ><input type=\"text\" name=\"PartID\" /></td>";
    html += "</tr>";

    $("#TableBodymingxi").append(html);

    isAdd = false;


}


function AddProduct(obj, type) {

    tr = $(obj).parents("tr").eq(0);

    td = tr.children("td");

    var PartNO = $.trim(td.eq(0).children().val())
    var Description = $.trim(td.eq(1).children().val())
    var Qty = $.trim(td.eq(2).children().val())
    var Price = $.trim(td.eq(3).children().val())
    var Amount = $.trim(td.eq(4).children().text())
    var NetPrice = $.trim(td.eq(5).children().text())
    var NetAmount = $.trim(td.eq(6).children().text())
    var TAX = $.trim(td.eq(7).children().text())
    var Unit = $.trim(td.eq(8).children().val())
    var TotalCost = $.trim(td.eq(9).children().text())
    var PartNOId = $.trim(td.eq(11).children().val())

    if (PartNO == "") {
        alert("请填写型号！");
        return false;
    }

    var money = 0;
    $("#mingxi tr").each(function () {
        money += Number($(this).children("td").eq(4).children().text());
    });


    $("#txtOA01020").val(money);

    if ($("#txtOA01021").val() != "") {    //订单总金额(US$)
        $("#txtOA01022").val(Number(Number(money) / Number($("#txtOA01021").val()) / 1.16).toFixed(2));  //订单总金额(US$)
    }


    var html = "<td><span name=\"PartNO\" >" + PartNO + "</span></td>";
    html += "   <td><span name=\"Description\" >" + Description + "</span></td>";
    html += "   <td><span name=\"Qty\" >" + Qty + "</span></td>";
    html += "   <td><span name=\"Price\" >" + Price + "</span></td>";
    html += "   <td><span name=\"Amount\">" + Amount + "</span></td>";
    html += "   <td><span name=\"NetPrice\">" + NetPrice + "</span></td>";
    html += "   <td><span name=\"NetAmount\">" + NetAmount + "</span></td>";
    html += "   <td><span name=\"TAX\">" + TAX + "</span></td>";
    html += "   <td><span name=\"UnitCost\" >" + Unit + "</span></td>";
    html += "   <td><span name=\"TotalCost\">" + TotalCost + "</span></td>";
    html += "   <td><a href=\"javascript:\" name=\"btnSave\" onclick=\"EditProduct(this)\">修改</a>";
    html += "       <a href=\"javascript:\" name=\"btnDelete\" onclick=\"RemoveProduct(this)\">删除</a></td>";
    html += "<td style=\"display:none;\" ><span name=\"PartID\" >" + PartNOId + "</span></td>";


    tr.html(html);

    var index = tr.index();

    if (type == "Edit") {
        EditInvoiceInput(PartNO, Qty, index);
    } else {
        //同时添加发票信息(型号/数量)
        AddInvoiceInputByMX(PartNO, Qty);
    }

    isAdd = true;
}

function EditProduct(obj) {

    tr = $(obj).parents("tr").eq(0);
    td = tr.children("td");

    var PartNO = $.trim(td.eq(0).children().text())
    var Description = $.trim(td.eq(1).children().text())
    var Qty = $.trim(td.eq(2).children().text())
    var Price = $.trim(td.eq(3).children().text())
    var Amount = $.trim(td.eq(4).children().text())
    var NetPrice = $.trim(td.eq(5).children().text())
    var NetAmount = $.trim(td.eq(6).children().text())
    var TAX = $.trim(td.eq(7).children().text())
    var Unit = $.trim(td.eq(8).children().text())
    var TotalCost = $.trim(td.eq(9).children().text())
    var PartNOId = $.trim(td.eq(11).children().text())


    var html = "<td><input type=\"text\" name=\"PartNO\" onblur=\"GetProduct(this,$('#txtOA01039').val())\" value=" + PartNO + " ></td>";
    html += "   <td><input type=\"text\" name=\"Description\" value=" + Description + " ></td>";
    html += "   <td><input type=\"text\" name=\"Qty\" onkeyup=\"checkNum(this)\" onblur=\"Sum(this)\"  maxlength=\"20\" value = " + Qty + " ></td>";
    html += "   <td><input type=\"text\" name=\"Price\" onkeyup=\"checkNum(this)\" onblur=\"Sum(this)\" maxlength=\"20\" value=" + Price + " ></td>";
    html += "   <td><span name=\"Amount\">" + Amount + "</span></td>";
    html += "   <td><span name=\"NetPrice\">" + NetPrice + "</span></td>";
    html += "   <td><span name=\"NetAmount\">" + NetAmount + "</span></td>";
    html += "   <td><span name=\"TAX\">" + TAX + "</span></td>";
    html += "   <td><input type=\"text\" onkeyup=\"checkNum(this)\" name=\"UnitCost\"  maxlength=\"20\" onblur=\"Sum(this)\" value=" + Unit + " ></td>";
    html += "   <td><span name=\"TotalCost\">" + TotalCost + "</span></td>";
    html += "   <td><a href=\"javascript:\" name=\"btnSave\" onclick=\"AddProduct(this,'Edit')\">保存</a>";
    html += "   </td>";
    html += "<td  style=\"display:none;\" ><input type=\"text\" name=\"PartID\" value=" + PartNOId + " ></td>";

    tr.html(html);
}

function RemoveProduct(obj) {
    if (!confirm('确认删除吗？')) {
        return false;
    }
    tr = $(obj).parents("tr").eq(0);
    var index = tr.index();
    tr.remove();


    //删除发票
    $("#TableBodyfapiao tr").eq(index).remove();

    for (var i = 0; i < $("span[name='SN']").length; i++) {
        $("span[name='SN']").eq(i).text(Number(i) + Number(1));
    }


    var money = 0;
    $("#mingxi tr").each(function () {
        money += Number($(this).children("td").eq(4).children().text());
    });


    $("#txtOA01020").val(money);

    if ($("#txtOA01021").val() != "") {    //订单总金额(US$)
        $("#txtOA01022").val(Number(Number(money) / Number($("#txtOA01021").val()) / 1.16).toFixed(2));  //订单总金额(US$)
    }

}

function checkNum(obj) {

    if (obj.value == "") {
        return;
    }

    if (obj.name == "Qty" && $.trim(obj.value) != "" && !/^\+?[1-9][0-9]*$/.test($.trim(obj.value))) {
        obj.value = "";
        alert("请输入整数!");
        window.setTimeout(function () { obj.focus(); }, 0);
        return false;
    }

    if (obj.name == "Price" && $.trim(obj.value) != "" && !/^([1-9]\d*|0)(?:\.\d{0,3}[1-9])?$/.test(obj.value)) {
        obj.value = "";
        alert("请输入数字!");
        window.setTimeout(function () { obj.focus(); }, 0);
        return false;
    }

    if (obj.name == "UnitCost" && $.trim(obj.value) != "" && !/^([1-9]\d*|0)(?:\.\d{0,3}[1-9])?$/.test(obj.value)) {
        obj.value = "";
        alert("请输入数字!");
        window.setTimeout(function () { obj.focus(); }, 0);
        return false;
    }
}

function Sum(obj) {
    var td1 = $(obj).parents("tr").eq(0).children("td");



    if ($.trim(td1.eq(2).children().val()) != "" && $.trim(td1.eq(3).children().val()) != "") {
        td1.eq(4).children().text(Number(Number($.trim(td1.eq(2).children().val())) * Number($.trim(td1.eq(3).children().val()))).toFixed(2)); //含税合计
        td1.eq(6).children().text(Number((Number($.trim(td1.eq(3).children().val())) / 1.16) * Number($.trim(td1.eq(2).children().val()))).toFixed(2)); //不含税合计
        td1.eq(7).children().text(Number((Number($.trim(td1.eq(6).children().text())) * 0.17)).toFixed(2)); //税额
    }
    if ($.trim(td1.eq(3).children().val()) != "") {
        td1.eq(5).children().text(Number(Number($.trim(td1.eq(3).children().val())) / 1.16).toFixed(2));
    }
    if ($.trim(td1.eq(8).children().val()) != "" && $.trim(td1.eq(3).children().val()) != "") {
        td1.eq(9).children().text(Number(Number($.trim(td1.eq(8).children().val())) * Number($.trim(td1.eq(2).children().val()))).toFixed(2));
    }

}
//订单明细新增时的发票新增
function AddInvoiceInputByMX(PartNO, Qty) {

    var SN = Number($("#TableBodyfapiao tr").length) + Number(1);

    var html = "<tr><td><span name=\"SN\" >" + SN + "</span></td>";
    html += "   <td><span name=\"PartNo\" >" + PartNO + "</span></td>";
    html += "   <td><span name=\"Qty\" >" + Qty + "</span></td>";
    html += "   <td><span name=\"Arrived\" >N</span></td>";
    html += "   <td><span name=\"ArrivalDate\"></span></td>";
    html += "   <td><span name=\"Delivered\">N</span></td>";
    html += "   <td><span name=\"DeliveredDate\"></span></td>";
    html += "   <td><a href=\"javascript:\" name=\"btnSave\" onclick=\"EditInvoice(this)\">修改</a>";
    html += "   </td></tr>";

    $("#TableBodyfapiao").append(html);
}

//发票新增
function AddInvoiceInput(PartNO, Qty) {

    var SN = Number($("#TableBodyfapiao tr").length) + Number(1);

    var html = "<tr>";
    html += "<td><span  name=\"SN\" >" + SN + "</span></td>";
    html += "<td><span  name=\"PartNO\" >" + PartNO + "</span></td>";
    html += "<td><span  name=\"Qty\" >" + Qty + "</span></td>";
    html += "<td><select name=\"Arrived\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    html += "<td><input type=\"text\" name=\"ArrivalDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" /></td>";
    html += "<td><select name=\"Delivered\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    html += "<td><input type=\"text\" name=\"DeliveredDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" /></td>";
    html += "<td><a href=\"javascript:\" name=\"btnSave\" onclick=\"AddInvoice(this)\">保存</a></td>";
    html += "</tr>";


    $("#TableBodyfapiao").append(html);

}

//保存一行发票
function AddInvoice(obj) {

    tr = $(obj).parents("tr").eq(0);
    td = tr.children("td");


    var SN = $.trim(td.eq(0).children().text())
    var PartNo = $.trim(td.eq(1).children().text())
    var Qty = $.trim(td.eq(2).children().text())
    var Arrived = $.trim(td.eq(3).children().val())
    var ArrivalDate = $.trim(td.eq(4).children().val())
    var Delivered = $.trim(td.eq(5).children().val())
    var DeliveredDate = $.trim(td.eq(6).children().val())

    var html = "<td><span name=\"SN\" >" + SN + "</span></td>";
    html += "   <td><span name=\"PartNo\" >" + PartNo + "</span></td>";
    html += "   <td><span name=\"Qty\" >" + Qty + "</span></td>";
    if (Arrived == "0") {
        html += "   <td><span name=\"Arrived\" >N</span></td>";
    } else {
        html += "   <td><span name=\"Arrived\" >Y</span></td>";
    }
    html += "   <td><span name=\"ArrivalDate\">" + ArrivalDate + "</span></td>";
    if (Delivered == "0") {
        html += "   <td><span name=\"Delivered\">N</span></td>";
    } else {
        html += "   <td><span name=\"Delivered\">Y</span></td>";
    }
    html += "   <td><span name=\"DeliveredDate\">" + DeliveredDate + "</span></td>";
    html += "   <td><a href=\"javascript:\" name=\"btnSave\" onclick=\"EditInvoice(this)\">修改</a>";
    html += "   </td>";

    tr.html(html);
}
//编辑发票信息
function EditInvoice(obj) {

    tr = $(obj).parents("tr").eq(0);
    td = tr.children("td");

    var SN = $.trim(td.eq(0).children().text())
    var PartNo = $.trim(td.eq(1).children().text())
    var Qty = $.trim(td.eq(2).children().text())
    var Arrived = $.trim(td.eq(3).children().text())
    var ArrivalDate = $.trim(td.eq(4).children().text())
    var Delivered = $.trim(td.eq(5).children().text())
    var DeliveredDate = $.trim(td.eq(6).children().text())

    var html = "<td><span  name=\"SN\" >" + SN + "</span></td>";
    html += "<td><span  name=\"PartNO\" >" + PartNo + "</span></td>";
    html += "<td><span  name=\"Qty\" >" + Qty + "</span></td>";
    if (Arrived == "N") {
        html += "<td><select name=\"Arrived\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    } else {
        html += "<td><select name=\"Arrived\"><option value=\"1\" selected=\"selected\">Y</option><option value=\"0\" >N</option></select></td>";
    }
    html += "<td><input type=\"text\" name=\"ArrivalDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" value=\"" + ArrivalDate + "\" ></td>";
    if (Delivered == "N") {
        html += "<td><select name=\"Delivered\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    } else {
        html += "<td><select name=\"Delivered\"><option value=\"1\" selected=\"selected\">Y</option><option value=\"0\" >N</option></select></td>";
    }
    html += "<td><input type=\"text\" name=\"DeliveredDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" value=\"" + DeliveredDate + "\" ></td>";
    html += "<td><a href=\"javascript:\" name=\"btnSave\" onclick=\"AddInvoice(this)\">保存</a></td>";

    tr.html(html);
}


function USblur() {
    if ($("#txtOA01020").val() != "")
        $("#txtOA01022").val(Number(Number($("#txtOA01020").val()) / Number($("#txtOA01021").val()) / 1.16).toFixed(2));  //订单总金额(US$)
}

function btnClientSave() {
    var isSave = true;
    var txtOA01002 = $("#txtOA01002").val();
    var txtOA01039 = $("#txtOA01039").val();
    var Type = "OA0102New01";
    var Type1 = "OA0102New02";
    $.ajax({
        type: "post",
        url: "/Handler/UniquenessHandler.ashx",
        data: { txtOA01002: txtOA01002, Type: Type },
        async: false,
        success: function (result) {
            if ($.parseJSON(result)["ok"] == "") {

            } else {
                isSave = false;
                alert($.parseJSON(result)["error"]);
                $("#txtOA01002").focus();
            }
        }
    });
    if (txtOA01039 != "") {
        if (isSave) {
            $.ajax({
                type: "post",
                url: "/Handler/UniquenessHandler.ashx",
                data: { txtOA01039: txtOA01039, Type: Type1 },
                async: false,
                success: function (result) {
                    if ($.parseJSON(result)["ok"] == "") {

                    } else {
                        isSave = false;
                        alert($.parseJSON(result)["error"]);
                        $("#txtOA01039").focus();
                    }
                }
            });
        }
    }

    var strmingxi = "";
    var i = 0;



    $("#mingxi tr").each(function () {
        if (i == 0) {
            i++;
            return true;
        }

        if ($(this).children("td").eq(10).children("a").eq(0).text() == "保存") {
            alert('请保存订单明细！');
            isSave = false;
            return false;
        }
        strmingxi += $(this).children("td").eq(0).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(1).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(2).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(3).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(4).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(5).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(6).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(7).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(8).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(9).children().text() + "_^_";
        strmingxi += $(this).children("td").eq(11).children().text() + "_&_";
    });
    //strmingxi = strmingxi.substring(0, strmingxi.length - 1);
    $("#txtmingxi").val(strmingxi);  //明细所有数据
    var strfapiao = "";
    var j = 0;
    $("#fapiao tr").each(function () {
        if (j == 0) {
            j++;
            return true;
        }

        if ($(this).children("td").eq(7).children("a").eq(0).text() == "保存") {
            alert('请保存发票信息！');
            isSave = false;
            return false;
        }

        strfapiao += $(this).children("td").eq(0).children().text() + "^";
        strfapiao += $(this).children("td").eq(1).children().text() + "^";
        strfapiao += $(this).children("td").eq(2).children().text() + "^";
        strfapiao += $(this).children("td").eq(3).children().text() + "^";
        strfapiao += $(this).children("td").eq(4).children().text() + "^";
        strfapiao += $(this).children("td").eq(5).children().text() + "^";
        strfapiao += $(this).children("td").eq(6).children().text() + "&";

    });
    strfapiao = strfapiao.substring(0, strfapiao.length - 1);
    $("#txtfapiao").val(strfapiao);  //明细所有数据

    return isSave
}

//商品修改导致的发票修改
function EditInvoiceInput(PartNO, Qty, index) {
    var td = $("#TableBodyfapiao tr").eq(index).children("td");
    td.eq(1).children().text(PartNO);
    td.eq(2).children().text(Qty);
}


function GetProduct(obj, id) {
    if (obj.value != "") {
        var List = getdata.getProductTwo(obj.value, id);
        if ($(List).length < 1) {
            //$(obj).parents("tr").eq(0).children("td").children().eq(12).val(0);
            obj.value = "";
            $(obj).parents("td").eq(0).prev("td").children("input").val("");
            alert("关联订单不存在该商品信息!");
            window.setTimeout(function () { obj.focus(); }, 0);
            return false;
        }
        $(obj).parents("tr").eq(0).children("td").children().eq(12).val(List[0].PA01001);
        $(obj).parents("td").eq(0).next().children("input").val(List[0].PA01005);
        $(obj).parents("tr").eq(0).children("td").children().eq(8).val(List[0].Netprice);
    }
}


function ShowSalesName() {
    $.post("/Handler/GetDataHandler.ashx", { value: $("#txtOA01039").val(), Type: "OA01SalesName" }, function (data) {
        if (data == "]") {
            $("#labOA01013").html("");
            $("#txtOA01013").val("");
        }
        else {
            $("#labOA01013").html($.parseJSON(data)[0]["UA01004"]);
            $("#txtOA01013").val($.parseJSON(data)[0]["OA01013"]);
        }

    });
}
