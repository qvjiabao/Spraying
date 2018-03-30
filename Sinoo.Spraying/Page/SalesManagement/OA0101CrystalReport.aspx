<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="OA0101CrystalReport.aspx.cs"
    Inherits="Sinoo.Spraying.Page.SalesManagement.OA0101CrystalReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/jscript">
        function priview() {
            $("#btnPrint").hide();
            $("#btnReturn").hide();
            var bdhtml = window.document.body.innerHTML;
            window.document.body.innerHTML = bdhtml;
            window.print();
            $("#btnPrint").show();
            $("#btnReturn").hide();
        }
                function btnReturns() {
            location.href = "OA0101List.aspx?PageIndex=<%=ViewState["PageIndex"] %> "
        }

        $(function(){
            var sumOB01008=0;
            var sumOB01009=0;
            var sumOB01010=0;
            var sumOB01011=0;
            var sumOB01012=0;
            var sumOB01013=0;
            var sumOB01014=0;
            $.each($(".table_3 tr").slice($("#trproduct").index()+1,$("#trsum").index()),function(){
                sumOB01008+=Number($(this).children().eq(5).text());
                sumOB01009+=Number($(this).children().eq(6).text());
                sumOB01010+=Number($(this).children().eq(7).text());
                sumOB01011+=Number($(this).children().eq(8).text());
                sumOB01012+=Number($(this).children().eq(9).text());
                sumOB01013+=Number($(this).children().eq(10).text());
                sumOB01014+=Number($(this).children().eq(11).text());
            })

            $("#trsum").children().eq(5).text(Number(sumOB01008).toFixed(2));
            $("#trsum").children().eq(6).text(Number(sumOB01009).toFixed(2));
            $("#trsum").children().eq(7).text(Number(sumOB01010).toFixed(2));
            $("#trsum").children().eq(8).text(Number(sumOB01011).toFixed(2));
            $("#trsum").children().eq(9).text(Number(sumOB01012).toFixed(2));
            $("#trsum").children().eq(10).text(Number(sumOB01013).toFixed(2));
            $("#trsum").children().eq(11).text(Number(sumOB01014).toFixed(2));
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="table_top">
        <div style="float: left; width: 22.3%;">
            <table width="300px" border="1" cellspacing="0" cellpadding="0" bordercolor="#666">
                <tr>
                    <td class="table_width">
                        Customer Code
                    </td>
                    <td class="table_width">
                        <asp:Label ID="labCA01002" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_width">
                        Invoice No
                    </td>
                    <td class="table_width">
                        <asp:Label ID="labOA01008" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_width">
                        Status
                    </td>
                    <td class="table_width">
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 56%; float: left;">
            <span>Spraying System Co---Distribution(Shanghai)<br />
                斯普瑞喷雾系统(上海)有限公司</span>
            <h2>
                Sales Order Draft 销售订单拟稿</h2>
        </div>
        <div class="num">
            <ul>
                <li class="li_01">Draft No.编号</li>
                <li class="li_02">
                    <asp:Label ID="labOA01002" runat="server" Text=""></asp:Label></li>
            </ul>
        </div>
    </div>
    <table width="100%" border="1" cellspacing="0" cellpadding="0" bordercolor="#666"
        class="table_3">
        <tr>
            <td colspan="4">
                Customer Name 客户名称/中文：<asp:Label ID="labCA01003" runat="server" Text=""></asp:Label>
            </td>
            <td colspan="7">
                Goods Delivery Add 送货地址：<asp:Label ID="labOA01028" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="11">
                Customer Name 客户名称/英文：<asp:Label ID="labCA01004" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="11">
                Billing Address/TEL 开票地址/电话：<asp:Label ID="labCA01005" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Bank Name 开户银行：<asp:Label ID="labCA01006" runat="server" Text=""></asp:Label>
            </td>
            <td colspan="7">
                Contact Person/Tel 联系人及电话：<asp:Label ID="labOA01029ANDOA01030" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Bank account NO 银行账号：<asp:Label ID="labCA01007" runat="server" Text=""></asp:Label>
            </td>
            <td colspan="7">
                PC 邮编：<asp:Label ID="labOA01031" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Tax registration NO 税务登记号：<asp:Label ID="labCA01008" runat="server" Text=""></asp:Label>
            </td>
            <td colspan="7">
                Invoice Delivery Add 寄发票地址：<asp:Label ID="labOA01032" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Contact person/DEPT 联系人及部门：<asp:Label ID="labCA01009" runat="server"></asp:Label>
            </td>
            <td colspan="7">
                Contact Person/Tel 联系人及电话：<asp:Label ID="labOA01033ANDOA01034" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                TEL/FAX 电话及传真：<asp:Label ID="labCA01010ANDCA01011" runat="server"></asp:Label>
            </td>
            <td colspan="7">
                PC 邮编：<asp:Label ID="labOA01035" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" rowspan="2">
                Special Instruction 特殊说明：<asp:Label ID="labOA01014" runat="server" Text=""></asp:Label>
            </td>
            <td class="table_center">
                客户要求发货日期
            </td>
            <td class="table_center">
                承诺日期
            </td>
            <td class="table_center">
                实际到货日期
            </td>
            <td class="table_center">
                预付款
            </td>
            <td class="table_center">
                信用期
            </td>
            <td colspan="2" class="table_center">
                信用额
            </td>
        </tr>
        <tr>
            <td class="table_center">
                <asp:Label ID="labOA01010" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="labOA01011" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="Label18" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="Label21" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="labCA01014" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center" colspan="2">
                <asp:Label ID="labCA01015" runat="server" Text=""></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" class="table_center">
                Purchase Order No 客户订单号
            </td>
            <td class="table_center">
                Quotation Ref.No 报价参号
            </td>
            <td class="table_center">
                Salesman Name &amp;Code 销售员
            </td>
            <td class="table_center">
                Specialist Name &amp;Code 顾问
            </td>
            <td class="table_center">
                Share with /% 分享比例%
            </td>
            <td class="table_center">
                SIC
            </td>
            <td class="table_center">
                CTC
            </td>
            <td>
                Devision Code
            </td>
            <td colspan="2" class="table_center">
                Application Code 使用编号
            </td>
        </tr>
        <tr>
            <td class="table_center" colspan="2">
            </td>
            <td class="table_center">
                <asp:Label ID="labOA01012" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="labUA01004" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="Label25" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="labShare" runat="server"></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="labCB04002" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="labCB02002" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center">
                <asp:Label ID="labCA01021" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td class="table_center" colspan="2">
                <asp:Label ID="labOB02002" runat="server" Text=""></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="6" class="table_center">
                ACCESS
            </td>
            <td colspan="2" class="table_center">
                AMAPS
            </td>
            <td class="table_center">
                VATSYSTEM
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr id="trproduct">
            <td class="table_center">
                S/N 编号
            </td>
            <td class="table_center">
                Part No 部件编号
            </td>
            <td class="table_center">
                Description 规格
            </td>
            <td class="table_center">
                Ordered Quantity 订货数量
            </td>
            <td class="table_center">
                Price含税单价
            </td>
            <td class="table_center">
                AMT 含税金额
            </td>
            <td class="table_center">
                NetPrice 不含税单价
            </td>
            <td class="table_center">
                NET AMT 不含税金额
            </td>
            <td class="table_center">
                TAX 税额
            </td>
            <td class="table_center">
                Unit Cost单
            </td>
            <td class="table_center">
                Total Cost合计
            </td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="table_center">
                        <%# Eval("OC01002")%>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01005") %>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01006") %>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01007") %>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01008") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01008")), 2).ToString()%>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01009") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01009")), 2).ToString()%>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01010") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01010")), 2).ToString()%>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01011") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01011")), 2).ToString()%>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01012") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01012")), 2).ToString()%>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01013") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01013")), 2).ToString()%>
                    </td>
                    <td class="table_center">
                        <%# Eval("OB01014") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01014")), 2).ToString()%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr id="trsum">
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td class="table_center">
                Total合计
            </td>
            <td class="table_center">
                &nbsp;
            </td>
            <td class="table_center">
                &nbsp;
            </td>
            <td class="table_center">
                &nbsp;
            </td>
            <td class="table_center">
                &nbsp;
            </td>
            <td class="table_center">
                &nbsp;
            </td>
            <td class="table_center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td class="table_center">
                VAT SYSTEM
            </td>
            <td class="table_center">
                价税合计
            </td>
            <td class="table_center">
                <asp:Label ID="labOA01020" runat="server"></asp:Label>
            </td>
            <td colspan="2" class="table_center">
                GP%
            </td>
            <td colspan="3" class="table_center">
                <asp:Label ID="labOA01023" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="table_center">
                Raised By 填制人
            </td>
            <td class="table_center">
                Apprived By 销售经理
            </td>
            <td colspan="2" class="table_center">
                Delivered By 运输人
            </td>
            <td colspan="3" class="table_center">
                Exceed 超出
            </td>
            <td colspan="3" class="table_center">
                Approved By G/M 总经理
            </td>
        </tr>
        <tr>
            <td colspan="2" class="table_center">
                    <asp:Label ID="labSessionUser" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" class="table_center">
                Date 日期
            </td>
            <td class="table_center">
                Date 日期
            </td>
            <td colspan="2" class="table_center">
                Date 日期
            </td>
            <td colspan="3" class="table_center">
                Date 日期
            </td>
            <td colspan="3" class="table_center">
                Date 日期
            </td>
        </tr>
        <tr>
            <td colspan="2" class="table_center">
                &nbsp;
                    <asp:Label ID="labOA01002Two" runat="server"></asp:Label>
            </td>
            <td class="table_center">
                &nbsp;
            </td>
            <td colspan="2" class="table_center">
                &nbsp;
            </td>
            <td colspan="3" class="table_center">
                &nbsp;
            </td>
            <td colspan="3" class="table_center">
                &nbsp;
            </td>
        </tr>
    </table>
    <input type="button" class="btn btn-primary" value="  打 印  " id="btnPrint" onclick="priview()" />
    &nbsp; &nbsp;
    <input class="btn btn-primary" id="btnReturn" onclick="btnReturns()" type="button"
        value="  返 回  " />
    </form>
</body>
</html>
