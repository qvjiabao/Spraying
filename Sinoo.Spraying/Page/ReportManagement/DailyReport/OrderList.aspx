<%@ Page Language="C#" ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true"
    CodeBehind="OrderList.aspx.cs" Inherits="Sinoo.Spraying.Page.ReportManagement.DailyReport.OrderList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="/css/content-style.css" />
    <link rel="stylesheet" href="/css/fullcalendar.css" />
    <link rel="stylesheet" href="/css/unicorn.main.css" />
    <link rel="stylesheet" href="/css/unicorn.grey.css" class="skin-color" />
    <link rel="stylesheet" href="/css/select2.css" />
    <link rel="stylesheet" href="/css/jquery-ui.css" />
    <link href="/css/PageControl.css" rel="stylesheet" type="text/css" />
    <link href="/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <title>Order List</title>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            OrderList</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Report Management"><i
            class="icon-home"></i>Report Management </a><a href="#" class="current">Order List</a>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-search"></i><span>查询条件</span></div>
                    </div>
                    <div class="xz">
                        <div class="control-group">
                            <label onclick="return false">
                                <span>Order Date：</span>
                                <asp:TextBox ID="txtOC01011Start" runat="server" onClick="WdatePicker({maxDate:'#F{$dp.$D(\'txtOC01011End\')}'})"
                                    CssClass="Wdate" Style="width: 92px; background-position: 82px 7px;"></asp:TextBox>
                                -
                                <asp:TextBox ID="txtOC01011End" runat="server" onClick="WdatePicker({minDate:'#F{$dp.$D(\'txtOC01011Start\')}'})"
                                    CssClass="Wdate" Style="width: 92px; background-position: 82px 7px;"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label onclick="return false">
                                <span>Invoice Date：</span>
                                <asp:TextBox ID="txtOC01998Start" runat="server" onClick="WdatePicker({maxDate:'#F{$dp.$D(\'txtOC01998End\')}'})"
                                    CssClass="Wdate" Style="width: 92px; background-position: 82px 7px;"></asp:TextBox>
                                -
                                <asp:TextBox ID="txtOC01998End" runat="server" onClick="WdatePicker({minDate:'#F{$dp.$D(\'txtOC01998Start\')}'})"
                                    CssClass="Wdate" Style="width: 92px; background-position: 82px 7px;"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>New Cumstomer：</span>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnOA01044" ID="rbtnOA01044All" runat="server" Checked="true" />
                                    All
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnOA01044" ID="rbtnOA01044Yes" runat="server" />
                                    是
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnOA01044" ID="rbtnOA01044No" runat="server" />
                                    否
                                </label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Team：</span>
                                <asp:DropDownList ID="ddlUA01013" runat="server" data-rule="required" data-msg-required="请选择所属区域">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Sales Name：</span>
                                <input type="hidden" id="ddlUA01value" runat="server" />
                                <input type="hidden" id="ddlUA01html" runat="server" />
                                <asp:DropDownList ID="ddlUA01" runat="server" data-rule="required;">
                                    <asp:ListItem Value="">请选择</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Order Type：</span>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnOA01003" ID="rbtnOA010033" runat="server" />
                                    All
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnOA01003" ID="rbtnOA010031" runat="server" Checked="true" />
                                    Nozzle
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnOA01003" ID="rbtnOA010032" runat="server" />
                                    Autojet
                                </label>
                            </label>
                        </div>
                        <div class="clear" style="clear: both">
                        </div>
                        <div class="control-group">
                            <button class="btn btn-primary" type="button" id="demoBtn3">
                                高级查询</button>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnSelect" CssClass="btn btn-primary" runat="server" Text="  查  询  "
                                OnClientClick="SaveCustomerHtml()" OnClick="btnSelect_Click" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <input type="button" class="btn btn-primary" onclick="DataClear()" value="  重  置  " />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-file"></i><span>查询结果</span></div>
                        <div class="control-group  control-anniu">
                            <input type="button" value="发送邮件" class="btn btn-primary" id="btnEmail" onclick="mail()" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnExport" CssClass="btn btn-primary" runat="server" Text="  导   出  "
                                OnClientClick="javascript:return confirm('您确定导出数据吗？')" OnClick="btnExport_Click" />
                        </div>
                    </div>
                    <div class="content-bd" style="width: 100%; overflow-x: scroll;">
                        <table width="100%" id="tb1" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                            <thead>
                                <tr>
                                    <th width="5%" scope="col">
                                        Share Name1
                                    </th>
                                    <th width="5%" scope="col">
                                        Share%1
                                    </th>
                                    <th width="5%" scope="col">
                                        Share Name2
                                    </th>
                                    <th width="5%" scope="col">
                                        Share%2
                                    </th>
                                    <th width="5%" scope="col">
                                        Order Date
                                    </th>
                                    <th width="5%" scope="col">
                                        Order No
                                    </th>
                                    <th width="5%" scope="col">
                                        Customer Code
                                    </th>
                                    <th width="5%" scope="col">
                                        Customer Name
                                    </th>
                                    <th width="5%" scope="col">
                                        Contact Person
                                    </th>
                                    <th width="5%" scope="col">
                                        Sales Name
                                    </th>
                                    <th width="5%" scope="col">
                                        Part NO
                                    </th>
                                    <th width="5%" scope="col">
                                        Qty
                                    </th>
                                    <th width="5%" scope="col">
                                        Price
                                    </th>
                                    <th width="5%" scope="col">
                                        Amount(RMB)
                                    </th>
                                    <th width="5%" scope="col">
                                        Amount(US$)
                                    </th>
                                    <th width="5%" scope="col">
                                        Exchange Rate
                                    </th>
                                    <th width="5%" scope="col">
                                        SIC
                                    </th>
                                    <th width="5%" scope="col">
                                        CTC
                                    </th>
                                    <th width="5%" scope="col">
                                        AC
                                    </th>
                                    <th width="5%" scope="col">
                                        MDT
                                    </th>
                                    <th width="5%" scope="col">
                                        Province
                                    </th>
                                    <th width="5%" scope="col">
                                        Payment1
                                    </th>
                                    <th width="5%" scope="col">
                                        Date1
                                    </th>
                                    <th width="5%" scope="col">
                                        Payment2
                                    </th>
                                    <th width="5%" scope="col">
                                        Date2
                                    </th>
                                    <th width="5%" scope="col">
                                        Payment3
                                    </th>
                                    <th width="5%" scope="col">
                                        Date3
                                    </th>
                                    <th width="5%" scope="col">
                                        Payment4
                                    </th>
                                    <th width="5%" scope="col">
                                        Date4
                                    </th>
                                    <th width="5%" scope="col">
                                        Ouststanding
                                    </th>
                                    <th width="5%" scope="col">
                                        CustomerRequesDate
                                    </th>
                                    <th width="5%" scope="col">
                                        Arrival Date
                                    </th>
                                    <th width="5%" scope="col">
                                        Arrival No
                                    </th>
                                    <th width="5%" scope="col">
                                        Delivery Date
                                    </th>
                                    <th width="5%" scope="col">
                                        Invoice Date
                                    </th>
                                    <th width="5%" scope="col">
                                        New Customer
                                    </th>
                                    <th width="5%" scope="col">
                                        FGD
                                    </th>
                                    <th width="5%" scope="col">
                                        220 Category
                                    </th>
                                    <th width="5%" scope="col">
                                        240 Category
                                    </th>
                                    <th width="5%" scope="col">
                                        Customer Type
                                    </th>
                                    <th width="5%" scope="col">
                                        City
                                    </th>
                                </tr>
                                <%=DataCount %>
                            </thead>
                            <asp:Repeater runat="server" ID="repOrderList">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("OA01015")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01016")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01017")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01018")%>
                                        </td>
                                        <td>
                                            <%# Convert.ToDateTime(Eval("OA01009")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01003")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01009")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA01005") == DBNull.Value ? "" : Eval("UA01005").ToString()%>
                                        </td>
                                        <td>
                                            <%# Eval("OB01005")%>
                                        </td>
                                        <td>
                                            <%# Eval("OB01007")%>
                                        </td>
                                        <td>
                                            <%# Eval("OB01008")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01020") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OA01020")), 2).ToString()%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01022") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OA01022")), 2).ToString()%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01021")%>
                                        </td>
                                        <td>
                                            <%# Eval("CB04002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CB02002")%>
                                        </td>
                                        <td>
                                            <%# Eval("OB02002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CB03002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01013")%>
                                        </td>
                                        <td>
                                            <%# Eval("OP01007")%>
                                        </td>
                                        <td>
                                            <%#Eval("OP01008") %>
                                        </td>
                                        <td>
                                            <%# Eval("OP01009")%>
                                        </td>
                                        <td>
                                            <%#Eval("OP01010") %>
                                        </td>
                                        <td>
                                            <%# Eval("OP01011")%>
                                        </td>
                                        <td>
                                            <%#Eval("OP01012") %>
                                        </td>
                                        <td>
                                            <%# Eval("OP01013")%>
                                        </td>
                                        <td>
                                            <%#Eval("OP01014") %>
                                        </td>
                                        <td>
                                            <%# Eval("OP01015")%>
                                        </td>
                                        <td>
                                            <%#Eval("OA01010") is DBNull ? "" : Convert.ToDateTime(Eval("OA01010")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%#Eval("OC01009") is DBNull ? "" : Convert.ToDateTime(Eval("OC01009")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%# Eval("OC01008")%>
                                        </td>
                                        <td>
                                            <%#Eval("OC01011") is DBNull ? "" : Convert.ToDateTime(Eval("OC01011")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%#Eval("OC01015") is DBNull ? "" : Convert.ToDateTime(Eval("OC01015")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%#Eval("OA01044").ToString().Trim() == "0" ? "否" : "是"%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01024") == DBNull.Value? "":Eval("OA01044").ToString().Trim() == "0" ? "否" : "是" %>
                                        </td>
                                        <td>
                                            <%# Eval("OA01043")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01042")%>
                                        </td>
                                        <td>
                                            <%# Eval("CB01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("GA03002")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-bl ui-corner-br ui-helper-clearfix nomargin">
                            <div class="dataTables_paginate fg-buttonset ui-buttonset fg-buttonset-multi ui-buttonset-multi paging_full_numbers"
                                id="DataTables_Table_0_paginate">
                                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="paginator" CurrentPageButtonClass="cpb"
                                    Width="100%" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" showinputbox="Never"
                                    OnPageChanging="AspNetPager1_PageChanged" CustomInfoTextAlign="Left" LayoutType="Table"
                                    CustomInfoHTML="当前第 %CurrentPageIndex%页，共 %PageCount%页">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div aria-hidden="false" aria-labelledby="myModalLabel" role="dialog" tabindex="-1"
        class="modal hide fade" id="imgBox" style="height: auto; top: 10px; min-height: 350px;
        margin-left: -403px; min-width: 800px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">
                        ×</button>
                    <h4 class="modal-title" id="myModalLabel">
                        高级查询</h4>
                </div>
                <div class="modal-body">
                    <UC:UserControl ID="UserControl" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/script.js" type="text/javascript"></script>
    <script src="/Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery.ztree.core-3.5.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.ztree.excheck-3.5.js"></script>
    <script src="/Scripts/unicorn.js" type="text/javascript"></script>
    <script src="/Scripts/base/base.getdata.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_OrderList.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_UserControl.js" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    </form>
</body>
</html>
