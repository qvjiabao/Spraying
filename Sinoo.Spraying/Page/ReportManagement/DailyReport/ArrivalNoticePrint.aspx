<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArrivalNoticePrint.aspx.cs"
    Inherits="Sinoo.Spraying.Page.ReportManagement.DailyReport.ArrivalNoticePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
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
            $("#btnReturn").show();

        }
        function btnReturns() {
            location.href = "ArrivalNotice.aspx?PageIndex=<%=ViewState["PageIndex"] %>&type=print "
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="table_title">
        Arrival Notice</div>
    <table width="260" border="0" cellspacing="0" cellpadding="0" class="table_1">
        <tr>
            <td colspan="2">
                致：<asp:Label ID="labCA01003" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                自：斯普瑞喷雾系统（上海）有限公司
            </td>
        </tr>
        <tr>
            <td>
                收件人：<asp:Label ID="labCA01009" runat="server" Text=""></asp:Label>
            </td>
            <td>
                传真：<asp:Label ID="labCA01011" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                日期：<asp:Label ID="labDateTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <div class="table_r">
        <img src="/img/logoprint.png" width="470" height="89" /></div>
    <table width="100%" border="1" cellspacing="0" cellpadding="0" bordercolor="#666"
        class="table_2">
        <tr bgcolor="#d5d5d6">
            <td>
                Order Date
            </td>
            <td>
                Arrival Date
            </td>
            <td>
                Order No
            </td>
            <td>
                S/N
            </td>
            <td>
                Part No
            </td>
            <td>
                Order Qty
            </td>
            <td>
                Price
            </td>
            <td>
                Amount(RMB)
            </td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#  Eval("OA01009") is DBNull ? "" : Convert.ToDateTime(Eval("OA01009")).ToString("yyyy-MM-dd")%>
                    </td>
                    <td>
                        <%# Eval("OC01009") is DBNull ? "" : Convert.ToDateTime(Eval("OC01009")).ToString("yyyy-MM-dd")%>
                    </td>
                    <td>
                        <%# Eval("OA01002")%>
                    </td>
                    <td>
                        <%# Eval("OC01002")%>
                    </td>
                    <td>
                        <%# Eval("OB01005")%>
                    </td>
                    <td>
                        <%# Eval("OB01007")%>
                    </td>
                    <td>
                        <%# Eval("OB01008") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01008")), 2).ToString()%>
                    </td>
                    <td>
                        <%# Eval("OB01009") is DBNull ? "" : Math.Round(Convert.ToDecimal(Eval("OB01009")), 2).ToString()%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
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
                合计（RMB）
            </td>
            <td>
                <asp:Label ID="labOP01005" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <div class="clear">
    </div>
    <div class="table_bottom">
        <p>
            以上货物现已货到上海，根据合同要求，货到上海后付清全款发货。现传真通知贵公司，请将该汇款汇入以下地址：
            <br />
            <br />
            名称：斯普瑞喷雾系统（上海）有限公司<br />
            开户行：美国银行有限公司上海分行<br />
            账号：10987017<br />
            <br />
            如有任何疑问，请拨打：<asp:Label ID="labTel" runat="server" Text=""></asp:Label></p>
    </div>
    <input type="button" class="btn btn-primary" value="  打 印  " id="btnPrint" onclick="priview()" />
    &nbsp; &nbsp;
    <input class="btn btn-primary" id="btnReturn" onclick="btnReturns()" type="button"
        value="  返 回  " />
    </form>
</body>
</html>
