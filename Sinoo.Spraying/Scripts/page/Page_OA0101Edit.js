$("#form1").validator({
    rules: {
        txtOA01024: function (elem) {
            return $("#rbtnOA01024Yes").is(":checked");
        },
        txtOA01048: function (elem) {
            return $("#rbtnOA01048Yes").is(":checked");
        }
    },
    fields: {
        txtOA01024: "required(txtOA01024)",
        txtOA01048: "required(txtOA01048)"
    },
    stopOnError: false,
    groups: [{
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

//新应用，应用分享
function changeInputCheck(obj, objName) {
    if (obj.value != "") {
        $('#' + objName).prop('checked', true)
    }
}

$("input[name='txtOA01002']").unbind("blur").blur(function () {
    $("#labOP01003").text($(this).val());
});
$("input[name='txtOA01009']").unbind("blur").blur(function () {
    $("#labOP01004").text($(this).val());
});

$("#txtOA01015").unbind("click").click(function () {
    $("#myIframe").attr("src", "OA0101Share.aspx?Cid=txtOA01015");
    $('#myModal').modal({ show: true });
});
$("#txtOA01017").unbind("click").click(function () {
    $("#myIframe").attr("src", "OA0101Share.aspx?Cid=txtOA01017");
    $('#myModal').modal({ show: true });
});

var tr;
var td;

var isAdd = true;
function AddProductInput() {
    if (!isAdd) {
        return false;
    }
    var html = "<tr>";
    html += "<td><input type=\"text\" name=\"PartNO\" onblur=\"GetProduct(this)\" /></td>";
    html += "<td><input type=\"text\" name=\"Description\"  maxlength=\"50\" /></td>";
    html += "<td><input type=\"text\" name=\"Qty\" onblur=\"checkNum(this);Sum(this)\"  maxlength=\"20\"/></td>";
    html += "<td><input type=\"text\" name=\"Price\" onblur=\"checkNum(this);Sum(this)\" maxlength=\"20\" /></td>";
    html += "<td><span name=\"Amount\"></span></td>";
    html += "<td><span name=\"NetPrice\"></span></td>";
    html += "<td><span name=\"NetAmount\"></span></td>";
    html += "<td><span name=\"TAX\"></span></td>";
    html += "<td><input type=\"text\" onblur=\"checkNum(this);Sum(this)\"  name=\"UnitCost\" maxlength=\"20\" /></td>";
    html += "<td><span name=\"TotalCost\"></span></td>";
    html += "<td><a href=\"javascript:\" name=\"btnSave\" onclick=\"AddProduct(this)\">保存</a>&nbsp;<a href=\"javascript:\" name=\"btnSave\" onclick=\"$(this).parents('tr').eq(0).remove();isAdd=true;\">取消</a></td>";
    html += "<td style=\"display:none;\" ><input type=\"text\" name=\"PartID\" /></td></tr>";

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


    //    //计算付款表单项的合计
    //    var Money = $("#labOP01005").text();
    //    $("#labOP01005").text(Number(Money) + Number(Amount));


    //计算总额
    var money = 0;
    $.each($("#TableBodymingxi tr"), function () {
        money += Number($(this).children("td").eq(4).children().text());
    });

    $("#labOP01005").text(money);

    $("#txtOA01020").val(money);

    if ($("#txtOA01021").val() != "") {    //订单总金额(US$)
        $("#txtOA01022").val(Number(Number(money / Number($("#txtOA01021").val()) / 1.16)).toFixed(2));  //订单总金额(US$)
    }

    //计算总成本
    money = 0;
    var boolchengben = false;
    $.each($("#TableBodymingxi tr"), function () {
        var chengben = $(this).children("td").eq(9).children().text()
        if (chengben == "" || chengben == "0" || chengben == "0.00") {
            boolchengben = true;
        }

        money += Number(chengben);
    });

    $("#txtOA01019").val(money);

    //订单明细有多项时，如果有任何一项没有成本，GP为空或者为0，
    if (boolchengben) {
        $("#txtOA01023").val("0%");  //计算利润率
    } else {

        if ($("#txtOA01021").val() != "" && $("#txtOA01021").val() != "0" && ($("#txtOA01020").val() != "0" || $("#txtOA01019").val() != "0")) {
            var num1 = (1 - (Number($("#txtOA01019").val()) * Number($("#txtOA01021").val()) * 1.16 * 1.15) / $("#txtOA01020").val());
            if (num1.toString().indexOf('.') > 0) {
                num1 = num1.toString().substring(0, num1.toString().indexOf('.') + 7);
            } else {
                num1 + '.00';
            }
            if (Number(num1) == 1) {
                $("#txtOA01023").val("0%");  //计算利润率
            } else {
                $("#txtOA01023").val(Number((Number(num1).toFixed(4) * 100)).toFixed(2) + "%");  //计算利润率
            }
        } else {
            $("#txtOA01023").val("0%");  //计算利润率
        }
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
        //AddInvoiceInput(PartNO, Qty);
    }

    //计算付款信息中的未付款金额
    GetOutsanding();

    //计算欠款金额
    CalculationDebts();

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

    var html = "<td><input type=\"text\" name=\"PartNO\" onblur=\"GetProduct(this)\" value='" + PartNO + "' ></td>";
    html += "   <td><input type=\"text\" name=\"Description\" maxlength=\"50\" value='" + Description + "' ></td>";
    html += "   <td><input type=\"text\" name=\"Qty\" onblur=\"checkNum(this);Sum(this)\"  maxlength=\"20\" value = '" + Qty + "' ></td>";
    html += "   <td><input type=\"text\" name=\"Price\" onblur=\"checkNum(this);Sum(this)\" maxlength=\"20\" value='" + Price + "' ></td>";
    html += "   <td><span name=\"Amount\">" + Amount + "</span></td>";
    html += "   <td><span name=\"NetPrice\">" + NetPrice + "</span></td>";
    html += "   <td><span name=\"NetAmount\">" + NetAmount + "</span></td>";
    html += "   <td><span name=\"TAX\">" + TAX + "</span></td>";
    html += "   <td><input type=\"text\" onblur=\"checkNum(this);Sum(this)\" name=\"UnitCost\" maxlength=\"20\" value=" + Unit + " ></td>";
    html += "   <td><span name=\"TotalCost\">" + TotalCost + "</span></td>";
    html += "   <td><a href=\"javascript:\" name=\"btnSave\" onclick=\"AddProduct(this,'Edit')\">保存</a></td>";
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

    //计算总额
    var money = 0;
    $.each($("#TableBodymingxi tr"), function () {
        money += Number($(this).children("td").eq(4).children().text());
    });

    $("#labOP01005").text(money);

    $("#txtOA01020").val(money);

    if ($("#txtOA01021").val() != "") {    //订单总金额(US$)
        $("#txtOA01022").val(Number(Number(money / Number($("#txtOA01021").val()) / 1.16)).toFixed(2));  //订单总金额(US$)
    }

    //计算总成本
    money = 0;
    var boolchengben = false;
    $.each($("#TableBodymingxi tr"), function () {
        var chengben = $(this).children("td").eq(9).children().text()
        if (chengben == "" || chengben == "0" || chengben == "0.00") {
            boolchengben = true;
        }

        money += Number(chengben);
    });

    $("#txtOA01019").val(money);
    //订单明细有多项时，如果有任何一项没有成本，GP为空或者为0，
    if (boolchengben) {
        $("#txtOA01023").val("0%");  //计算利润率
    } else {

        if ($("#txtOA01021").val() != "" && $("#txtOA01021").val() != "0" && ($("#txtOA01020").val() != "0" || $("#txtOA01019").val() != "0")) {
            var num1 = (1 - (Number($("#txtOA01019").val()) * Number($("#txtOA01021").val()) * 1.16 * 1.15) / $("#txtOA01020").val());
            if (num1.toString().indexOf('.') > 0) {
                num1 = num1.toString().substring(0, num1.toString().indexOf('.') + 7);
            } else {
                num1 + '.00';
            }
            if (Number(num1) == 1) {
                $("#txtOA01023").val("0%");  //计算利润率
            } else {
                $("#txtOA01023").val(Number((Number(num1).toFixed(4) * 100)).toFixed(2) + "%");  //计算利润率
            }
        } else {
            $("#txtOA01023").val("0%");  //计算利润率
        }
    }

    //计算付款信息中的未付款金额
    GetOutsanding();

    //计算欠款金额
    CalculationDebts();
}

function checkNum(obj) {
    if ((obj.name == "txtOP01007" || obj.name == "txtOP01009" || obj.name == "txtOP01011" || obj.name == "txtOP01013" || obj.name == "txtOP01017" || obj.name == "txtOP01019" || obj.name == "txtOP01021")
        && $.trim(obj.value) != "" && !/^([1-9]\d*|0)(?:\.\d{0,3}[1-9])?$/.test(obj.value)) {
        obj.value = "";
        alert("请输入数字!");
        window.setTimeout(function () { obj.focus(); }, 0);
        return false;
    } else if ((obj.name == "txtOP01007" || obj.name == "txtOP01009" || obj.name == "txtOP01011" || obj.name == "txtOP01013" || obj.name == "txtOP01017" || obj.name == "txtOP01019" || obj.name == "txtOP01021")) {
        GetOutsanding();
    }
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
    html += "   <td><span name=\"ArrivalNo\"></span></td>";
    html += "   <td><span name=\"ArrivalDate\"></span></td>";
    html += "   <td><span name=\"Delivered\">N</span></td>";
    html += "   <td><span name=\"DeliveredDate\"></span></td>";
    html += "   <td><span name=\"ExpressCompanyGoods\" ></span></td>";
    html += "   <td><span name=\"Tracking\"></span></td>";
    html += "   <td><span name=\"TrackingRemark\"></span></td>";
    html += "   <td><span name=\"Invoiced\">N</span></td>";
    html += "   <td><span name=\"InvoiceDate\"></span></td>";
    html += "   <td><span name=\"ExpressCo\"></span></td>";
    html += "   <td><span name=\"TrackingInvoice\"></span></td>";
    html += "   <td><span name=\"TrackingInvoiceRemark\"></span></td>";
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
    html += "<td><input type=\"text\" data-date-format:\"mm.dd.yy\" name=\"ArrivalNo\" maxlength=\"20\" /></td>";
    html += "<td><input type=\"text\" name=\"ArrivalDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" /></td>";
    html += "<td><select name=\"Delivered\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    html += "<td><input type=\"text\" name=\"DeliveredDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" /></td>";
    html += "<td><input type=\"text\" name=\"ExpressCompanyGoods\" maxlength=\"20\" /></td>";
    html += "<td><input type=\"text\" name=\"Tracking\" maxlength=\"20\" /></td>";
    html += "<td><input type=\"text\" name=\"TrackingRemark\" /></td>";
    html += "<td><select name=\"Invoiced\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    html += "<td><input type=\"text\" name=\"InvoiceDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" /></td>";
    html += "<td><input type=\"text\" name=\"ExpressCo\" maxlength=\"20\" /></td>";
    html += "<td><input type=\"text\" name=\"Tracking Invoice\" maxlength=\"20\" /></td>";
    html += "<td><input type=\"text\" name=\"TrackingInvoiceRemark\" /></td>";
    html += "<td><a href=\"javascript:\" name=\"btnSave\" onclick=\"AddInvoice(this)\">保存</a></td>";
    html += "</tr>";

    $("#TableBodyfapiao").append(html);
}

//商品修改导致的发票修改
function EditInvoiceInput(PartNO, Qty, index) {
    var td = $("#TableBodyfapiao tr").eq(index).children("td");
    td.eq(1).children().text(PartNO);
    td.eq(2).children().text(Qty);
}

//保存一行发票
function AddInvoice(obj) {

    tr = $(obj).parents("tr").eq(0);
    td = tr.children("td");


    var SN = $.trim(td.eq(0).children().text());
    var PartNo = $.trim(td.eq(1).children().text());
    var Qty = $.trim(td.eq(2).children().text());
    var Arrived = $.trim(td.eq(3).children().val());
    var ArrivalNo = $.trim(td.eq(4).children().val());
    var ArrivalDate = $.trim(td.eq(5).children().val());
    var Delivered = $.trim(td.eq(6).children().val());
    var DeliveredDate = $.trim(td.eq(7).children().val());
    var ExpressCompanyGoods = $.trim(td.eq(8).children().val());
    var Tracking = $.trim(td.eq(9).children().val());
    var TrackingRemark = $.trim(td.eq(10).children().val());
    var Invoiced = $.trim(td.eq(11).children().val());
    var InvoiceDate = $.trim(td.eq(12).children().val());
    var ExpressCo = $.trim(td.eq(13).children().val());
    var TrackingInvoice = $.trim(td.eq(14).children().val());
    var TrackingInvoiceRemark = $.trim(td.eq(15).children().val());

    var html = "<td><span name=\"SN\" >" + SN + "</span></td>";
    html += "   <td><span name=\"PartNo\" >" + PartNo + "</span></td>";
    html += "   <td><span name=\"Qty\" >" + Qty + "</span></td>";
    if (Arrived == "0") {
        html += "   <td><span name=\"Arrived\" >N</span></td>";
    } else {
        html += "   <td><span name=\"Arrived\" >Y</span></td>";
    }
    html += "   <td><span name=\"ArrivalNo\">" + ArrivalNo + "</span></td>";
    html += "   <td><span name=\"ArrivalDate\">" + ArrivalDate + "</span></td>";
    if (Delivered == "0") {
        html += "   <td><span name=\"Delivered\">N</span></td>";
    } else {
        html += "   <td><span name=\"Delivered\">Y</span></td>";
    }
    html += "   <td><span name=\"DeliveredDate\">" + DeliveredDate + "</span></td>";
    html += "   <td><span name=\"ExpressCompanyGoods\" >" + ExpressCompanyGoods + "</span></td>";
    html += "   <td><span name=\"Tracking\">" + Tracking + "</span></td>";
    html += "   <td><span name=\"TrackingRemark\">" + TrackingRemark + "</span></td>";
    if (Invoiced == "0") {
        html += "   <td><span name=\"Invoiced\">N</span></td>";
    } else {
        html += "   <td><span name=\"Invoiced\">Y</span></td>";
    }
    html += "   <td><span name=\"InvoiceDate\">" + InvoiceDate + "</span></td>";
    html += "   <td><span name=\"ExpressCo\">" + ExpressCo + "</span></td>";
    html += "   <td><span name=\"TrackingInvoice\">" + TrackingInvoice + "</span></td>";
    html += "   <td><span name=\"TrackingInvoiceRemark\">" + TrackingInvoiceRemark + "</span></td>";
    html += "   <td><a href=\"javascript:\" name=\"btnSave\" onclick=\"EditInvoice(this)\">修改</a>";
    html += "   </td>";

    tr.html(html);
    //计算欠款金额
    CalculationDebts();
}
//编辑发票信息
function EditInvoice(obj) {

    tr = $(obj).parents("tr").eq(0);
    td = tr.children("td");

    var SN = $.trim(td.eq(0).children().text());
    var PartNo = $.trim(td.eq(1).children().text());
    var Qty = $.trim(td.eq(2).children().text());
    var Arrived = $.trim(td.eq(3).children().text());
    var ArrivalNo = $.trim(td.eq(4).children().text());
    var ArrivalDate = $.trim(td.eq(5).children().text());
    var Delivered = $.trim(td.eq(6).children().text());
    var DeliveredDate = $.trim(td.eq(7).children().text());
    var ExpressCompanyGoods = $.trim(td.eq(8).children().text());
    var Tracking = $.trim(td.eq(9).children().text());
    var TrackingRemark = $.trim(td.eq(10).children().text());
    var Invoiced = $.trim(td.eq(11).children().text());
    var InvoiceDate = $.trim(td.eq(12).children().text());
    var ExpressCo = $.trim(td.eq(13).children().text());
    var TrackingInvoice = $.trim(td.eq(14).children().text());
    var TrackingInvoiceRemark = $.trim(td.eq(15).children().text());

    var html = "<td><span  name=\"SN\" >" + SN + "</span></td>";
    html += "<td><span  name=\"PartNO\" >" + PartNo + "</span></td>";
    html += "<td><span  name=\"Qty\" >" + Qty + "</span></td>";
    if (Arrived == "N") {
        html += "<td><select name=\"Arrived\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    } else {
        html += "<td><select name=\"Arrived\"><option value=\"1\" selected=\"selected\">Y</option><option value=\"0\" >N</option></select></td>";
    }
    html += "<td><input type=\"text\" name=\"ArrivalNo\" maxlength=\"20\" value=\"" + ArrivalNo + "\" ></td>";
    html += "<td><input type=\"text\" name=\"ArrivalDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" value=\"" + ArrivalDate + "\" ></td>";
    if (Delivered == "N") {
        html += "<td><select name=\"Delivered\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    } else {
        html += "<td><select name=\"Delivered\"><option value=\"1\" selected=\"selected\">Y</option><option value=\"0\" >N</option></select></td>";
    }
    html += "<td><input type=\"text\" name=\"DeliveredDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" value=\"" + DeliveredDate + "\" ></td>";
    html += "<td><input type=\"text\" name=\"ExpressCompanyGoods\" maxlength=\"20\"  value=\"" + ExpressCompanyGoods + "\" ></td>";
    html += "<td><input type=\"text\" name=\"Tracking\" maxlength=\"20\" value=\"" + Tracking + "\" ></td>";
    html += "<td><input type=\"text\" name=\"TrackingRemark\" value=\"" + TrackingRemark + "\" ></td>";
    if (Invoiced == "N") {
        html += "<td><select name=\"Invoiced\"><option value=\"1\">Y</option><option value=\"0\" selected=\"selected\">N</option></select></td>";
    } else {
        html += "<td><select name=\"Invoiced\"><option value=\"1\" selected=\"selected\">Y</option><option value=\"0\" >N</option></select></td>";
    }
    html += "<td><input type=\"text\" name=\"InvoiceDate\" onClick=\"WdatePicker()\" Class=\"Wdate\" value=\"" + InvoiceDate + "\"></td>";
    html += "<td><input type=\"text\" name=\"ExpressCo\" maxlength=\"20\"  value=\"" + ExpressCo + "\" /></td>";
    html += "<td><input type=\"text\" name=\"TrackingInvoice\" maxlength=\"20\"  value=\"" + TrackingInvoice + "\"></td>";
    html += "<td><input type=\"text\" name=\"TrackingInvoiceRemark\" value=\"" + TrackingInvoiceRemark + "\"></td>";
    html += "<td><a href=\"javascript:\" name=\"btnSave\" onclick=\"AddInvoice(this)\">保存</a></td>";

    tr.html(html);
}

function USblur() {
    if ($("#txtOA01020").val() != "")
        $("#txtOA01022").val(Number(Number($("#txtOA01020").val()) / Number($("#txtOA01021").val()) / 1.16).toFixed(2));  //订单总金额(US$)

    if ($("#txtOA01021").val() != "" && $("#txtOA01021").val() != "0" && $("#txtOA01019").val() != "" && ($("#txtOA01020").val() != "0" || $("#txtOA01019").val() != "0")) {    //计算利润率
        var num1 = (1 - (Number($("#txtOA01019").val()) * Number($("#txtOA01021").val()) * 1.16 * 1.15) / $("#txtOA01020").val());
        if (num1.toString().indexOf('.') > 0) {
            num1 = num1.toString().substring(0, num1.toString().indexOf('.') + 7);
        } else {
            num1 + '.00';
        }
        if (Number(num1) == 1) {
            $("#txtOA01023").val("0%");  //计算利润率
        } else {
            $("#txtOA01023").val(Number((Number(num1).toFixed(4) * 100)).toFixed(2) + "%");  //计算利润率
        }
    } else {
        $("#txtOA01023").val("0%");  //计算利润率
    }
}

//计算未付款金额和欠款金额
function GetOutsanding() {
    if ($.trim($("#labOP01005").text()) == "") {
        return;
    }
    var txtOP01007 = $("#txtOP01007").val();
    var txtOP01009 = $("#txtOP01009").val();
    var txtOP01011 = $("#txtOP01011").val();
    var txtOP01013 = $("#txtOP01013").val();
    var txtOP01017 = $("#txtOP01017").val();
    var txtOP01019 = $("#txtOP01019").val();
    var txtOP01021 = $("#txtOP01021").val();
    var money = Number($.trim($("#labOP01005").text())).toFixed(2) - Number(txtOP01007).toFixed(2) - Number(txtOP01009).toFixed(2)
        - Number(txtOP01011).toFixed(2) - Number(txtOP01013).toFixed(2) - Number(txtOP01017).toFixed(2) - Number(txtOP01019).toFixed(2) - Number(txtOP01021).toFixed(2);
    //    if (money < 0) {
    //        money = 0;
    //    }
    $("#labOP01015").text(Number(money).toFixed(2));
    $("#txtOP01015").val($("#labOP01015").text());
    //计算欠款金额
    CalculationDebts();
}

function GetProduct(obj) {
    if (obj.value != "") {
        var List = getdata.getProduct($(obj).val().trim());
        if ($(List).length < 1) {
            //$(obj).parents("tr").eq(0).children("td").children().eq(12).val(0);

            obj.value = "";
            $(obj).parents("td").eq(0).prev("td").children("input").val("");
            alert("没有该商品信息!");
            window.setTimeout(function () { obj.focus(); }, 0);
            return false;
        }
        $(obj).parents("tr").eq(0).children("td").children().eq(12).val(List[0].PA01001);
        $(obj).parents("td").eq(0).next().children("input").val(List[0].PA01005);
        $(obj).parents("tr").eq(0).children("td").children().eq(8).val(List[0].Netprice);
    }
}

//保存时拼接字符串
function btnClientSave() {

    var isSave = true;
    $.ajax({
        type: "post",
        url: "/Handler/UniquenessHandler.ashx",
        data: { txtOA01002: $("#txtOA01002").val(), Type: "OA0101Edit", ID: $("#ID").val() },
        async: false,
        success: function (data) {
            var result = $.parseJSON(data)["error"];
            if (result != "" && typeof (result) != "undefined") {
                alert(result);
                $("#txtOA01002").focus();
                isSave = false;
            }
        }
    });

    var strmingxi = " ";

    $("#TableBodymingxi tr").each(function () {
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

    var strfapiao = " ";
    $("#TableBodyfapiao tr").each(function () {
        if ($(this).children("td").eq(16).children("a").eq(0).text() == "保存") {
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
        strfapiao += $(this).children("td").eq(6).children().text() + "^";
        strfapiao += $(this).children("td").eq(7).children().text() + "^";
        strfapiao += $(this).children("td").eq(8).children().text() + "^";
        strfapiao += $(this).children("td").eq(9).children().text() + "^";
        strfapiao += $(this).children("td").eq(10).children().text() + "^";
        strfapiao += $(this).children("td").eq(11).children().text() + "^";
        strfapiao += $(this).children("td").eq(12).children().text() + "^";
        strfapiao += $(this).children("td").eq(13).children().text() + "^";
        strfapiao += $(this).children("td").eq(14).children().text() + "^";
        strfapiao += $(this).children("td").eq(15).children().text() + "&";

    });
    strfapiao = strfapiao.substring(0, strfapiao.length - 1);
    $("#txtfapiao").val(strfapiao);  //明细所有数据

    return isSave
}

//计算欠款
function CalculationDebts() {
    var index = 0;
    var Debts = 0;
    $("#TableBodyfapiao tr").each(function () {
        //        if ($(this).children("td").eq(3).children("span").text() == "Y" && $(this).children("td").eq(10).children("span").text() == "Y") {
        //        if ($.trim($(this).children("td").eq(6).children("span").text()) == "Y" || $.trim($(this).children("td").eq(10).children("span").text()) == "Y") {
        if ($.trim($(this).children("td").eq(6).children("span").text()) == "Y"
        || $.trim($(this).children("td").eq(10).children("span").text()) == "Y") {

            Debts += Number($("#TableBodymingxi tr").eq(index).children("td").eq(4).children("span").text());
        }
        index++;
    });
    var money = Number(Debts).toFixed(2) - Number($("#txtOP01007").val()).toFixed(2)
        - Number($("#txtOP01009").val()).toFixed(2) - Number($("#txtOP01011").val()).toFixed(2)
        - Number($("#txtOP01013").val()).toFixed(2) - Number($("#txtOP01017").val()).toFixed(2) - Number($("#txtOP01019").val()).toFixed(2)
        - Number($("#txtOP01021").val()).toFixed(2);
    if (money < 0) {
        money = 0;
    }
    $("#labOP01016").text(Number(money).toFixed(2));
    $("#txtOP01016").val(Number($("#labOP01016").text()).toFixed(2));
    //    var index = 0;
    //    var Debts = 0;
    //    $("#TableBodyfapiao tr").each(function () {

    //        if ($.trim($(this).children("td").eq(3).children("span").text()) == "Y" && $.trim($(this).children("td").eq(10).children("span").text()) == "Y") {
    //            //if ($.trim($(this).children("td").eq(6).children("span").text()) == "Y" || $.trim($(this).children("td").eq(10).children("span").text()) == "Y") {
    //            Debts += Number($("#TableBodymingxi tr").eq(index).children("td").eq(4).children("span").text());
    //        }
    //        index++;
    //    });
    //    $("#labOP01016").text(Number(Debts) - Number($("#txtOP01007").val()) - Number($("#txtOP01009").val()) - Number($("#txtOP01011").val()) - Number($("#txtOP01013").val()));
    //    $("#txtOP01016").val($("#labOP01016").text());
}

$("#rbtnOA01003Yes").click(function () {
    $("#TableBodyfukuan input[type='text']").val("").attr("disabled", "disabled");
    $("#ddlOP01006").val("0").attr("disabled", "disabled");
});
$("#rbtnOA01003No").click(function () {
    $("#TableBodyfukuan input[type='text']").val("").removeAttr("disabled");
    $("#ddlOP01006").val("0").removeAttr("disabled");
});

$(function () {

    if ($("#rbtnOA01003Yes").is(":checked")) {
        $("#TableBodyfukuan input[type='text']").val("").attr("disabled", "disabled");
        $("#ddlOP01006").val("0").attr("disabled", "disabled");
    }
    if ($("#txtOA01015").val() != "" || $("#txtOA01049").val() != "") {
        $("#ddlOA01016").removeAttr("disabled").attr("data-rule", "required").trigger("validate");
    }

    if ($("#txtOA01017").val() != "" || $("#txtOA01050").val() != "") {
        $("#ddlOA01018").removeAttr("disabled").attr("data-rule", "required").trigger("validate");
    }
});

window.onload = function () {

    $('#form1').validator('hideMsg', '#ddlOA01016');

    $('#form1').validator('hideMsg', '#ddlOA01018');
}


//重复数据
function Repeat() {
    var bl = confirm('数据是否和第一行相同？')
    if (bl) {
        if ($("#TableBodyfapiao tr").length > 1) {
            var fapiaotd3 = $("#TableBodyfapiao tr").eq(0).children("td").eq(3).children().text();
            var fapiaotd4 = $("#TableBodyfapiao tr").eq(0).children("td").eq(4).children().text();
            var fapiaotd5 = $("#TableBodyfapiao tr").eq(0).children("td").eq(5).children().text();
            var fapiaotd6 = $("#TableBodyfapiao tr").eq(0).children("td").eq(6).children().text();
            var fapiaotd7 = $("#TableBodyfapiao tr").eq(0).children("td").eq(7).children().text();
            var fapiaotd8 = $("#TableBodyfapiao tr").eq(0).children("td").eq(8).children().text();
            var fapiaotd9 = $("#TableBodyfapiao tr").eq(0).children("td").eq(9).children().text();
            var fapiaotd10 = $("#TableBodyfapiao tr").eq(0).children("td").eq(10).children().text();
            var fapiaotd11 = $("#TableBodyfapiao tr").eq(0).children("td").eq(11).children().text();
            var fapiaotd12 = $("#TableBodyfapiao tr").eq(0).children("td").eq(12).children().text();
            var fapiaotd13 = $("#TableBodyfapiao tr").eq(0).children("td").eq(13).children().text();
            var fapiaotd14 = $("#TableBodyfapiao tr").eq(0).children("td").eq(14).children().text();
            var fapiaotd15 = $("#TableBodyfapiao tr").eq(0).children("td").eq(15).children().text();
            $("#TableBodyfapiao tr").each(function () {
                if ($(this).children("td").eq(16).children("a").eq(0).text() == "保存") {
                    alert('请保存发票信息！');
                    return false;
                }
                if ($(this).children("td").eq(4).children().text().trim() == ""
                       && $(this).children("td").eq(5).children().text().trim() == ""
                       && $(this).children("td").eq(7).children().text().trim() == ""
                       && $(this).children("td").eq(8).children().text().trim() == ""
                       && $(this).children("td").eq(9).children().text().trim() == ""
                       && $(this).children("td").eq(11).children().text().trim() == ""
                       && $(this).children("td").eq(12).children().text().trim() == ""
                       && $(this).children("td").eq(13).children().text().trim() == "") {
                    $(this).children("td").eq(3).children().text(fapiaotd3);
                    $(this).children("td").eq(6).children().text(fapiaotd6);
                    $(this).children("td").eq(10).children().text(fapiaotd10);
                }
                if ($(this).children("td").eq(4).children().text().trim() == "")
                    $(this).children("td").eq(4).children().text(fapiaotd4);
                if ($(this).children("td").eq(5).children().text().trim() == "")
                    $(this).children("td").eq(5).children().text(fapiaotd5);
                if ($(this).children("td").eq(7).children().text().trim() == "")
                    $(this).children("td").eq(7).children().text(fapiaotd7);
                if ($(this).children("td").eq(8).children().text().trim() == "")
                    $(this).children("td").eq(8).children().text(fapiaotd8);
                if ($(this).children("td").eq(9).children().text().trim() == "")
                    $(this).children("td").eq(9).children().text(fapiaotd9);
                if ($(this).children("td").eq(11).children().text().trim() == "")
                    $(this).children("td").eq(11).children().text(fapiaotd11);
                if ($(this).children("td").eq(12).children().text().trim() == "")
                    $(this).children("td").eq(12).children().text(fapiaotd12);
                if ($(this).children("td").eq(13).children().text().trim() == "")
                    $(this).children("td").eq(13).children().text(fapiaotd13);
                if ($(this).children("td").eq(14).children().text().trim() == "")
                    $(this).children("td").eq(14).children().text(fapiaotd14);
                if ($(this).children("td").eq(15).children().text().trim() == "")
                    $(this).children("td").eq(15).children().text(fapiaotd15);


                //计算付款信息中的未付款金额
                GetOutsanding();

                //计算欠款金额
                CalculationDebts();
            });
        } else {
            alert('发票信息不能少于1条');
        }

    }
}



//重复数据
function RepeatTwo() {
    var bl = confirm('数据状态是否和第一行相同？')
    if (bl) {
        if ($("#TableBodyfapiao tr").length > 1) {
            var fapiaotd3 = $("#TableBodyfapiao tr").eq(0).children("td").eq(3).children().text();
            var fapiaotd6 = $("#TableBodyfapiao tr").eq(0).children("td").eq(6).children().text();
            var fapiaotd10 = $("#TableBodyfapiao tr").eq(0).children("td").eq(11).children().text();
            $("#TableBodyfapiao tr").each(function () {
                if ($(this).children("td").eq(16).children("a").eq(0).text() == "保存") {
                    alert('请保存发票信息！');
                    return false;
                }
                $(this).children("td").eq(3).children().text(fapiaotd3);
                $(this).children("td").eq(6).children().text(fapiaotd6);
                $(this).children("td").eq(11).children().text(fapiaotd10);

                //计算付款信息中的未付款金额
                GetOutsanding();

                //计算欠款金额
                CalculationDebts();
            });
        } else {
            alert('发票信息不能少于1条')
        }

    }
}
