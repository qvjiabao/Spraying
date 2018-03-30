<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeeklyPending.aspx.cs"
    Inherits="Sinoo.Spraying.Page.ReportManagement.WeeklyReport.WeeklyPending" %>

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
    <title>Weekly Pending List</title>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            Weekly Pending List</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Report Management"><i
            class="icon-home"></i>Report Management </a><a href="#" class="current">Weekly Pending
                List</a>
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
                            <label>
                                <span>Order Date：</span>
                                <asp:TextBox ID="txtBeginTime" Style="width: 91px;" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'txtEndTime\')}'})"
                                    CssClass="Wdate" runat="server"></asp:TextBox>&nbsp;-&nbsp;
                                <asp:TextBox ID="txtEndTime" Style="width: 91px;" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtBeginTime\')}'})"
                                    CssClass="Wdate" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Arrival Date：</span>
                                <asp:TextBox ID="txtBeginArrivalTime" Style="width: 91px;" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'txtEndArrivalTime\')}'})"
                                    CssClass="Wdate" runat="server"></asp:TextBox>&nbsp;-&nbsp;
                                <asp:TextBox ID="txtEndArrivalTime" Style="width: 91px;" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtBeginArrivalTime\')}'})"
                                    CssClass="Wdate" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Invoice Date：</span>
                                <asp:TextBox ID="txtBeginInvoiceTime" Style="width: 91px;" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'txtEndInvoiceTime\')}'})"
                                    CssClass="Wdate" runat="server"></asp:TextBox>&nbsp;-&nbsp;
                                <asp:TextBox ID="txtEndInvoiceTime" Style="width: 91px;" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtBeginInvoiceTime\')}'})"
                                    CssClass="Wdate" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Order No：</span>
                                <asp:TextBox ID="txtOrderNo" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>New Customer</span>
                                <label class="radio inline">
                                    <input id="Radio1" name="OA01044" runat="server" checked="true" value="all" type="radio" />All</label>
                                <label class="radio inline">
                                    <input name="OA01044" runat="server" value="Y" type="radio" />是</label>
                                <label class="radio inline">
                                    <input name="OA01044" runat="server" value="N" type="radio" />否</label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Team：</span>
                                <select id="ddlProvince" runat="server">
                                    <option value="">All</option>
                                    <option value="北京">BeiJing</option>
                                    <option value="天津">TianJin</option>
                                    <option value="沈阳">ShenYang</option>
                                    <option value="西安">XiAn</option>
                                    <option value="钢铁">Steelteam</option>
                                </select>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>SalesName：</span>
                                <select id="ddlUA01004" runat="server">
                                    <option value="">请选择</option>
                                </select>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Order type：</span>
                                <label class="radio inline">
                                    <input id="Radio2" name="OrderType" runat="server" value="all" type="radio" />
                                    All
                                </label>
                                <label class="radio inline">
                                    <input name="OrderType" runat="server" value="Nozzle" checked="true" type="radio" />
                                    Nozzle
                                </label>
                                <label class="radio inline">
                                    <input name="OrderType" runat="server" value="Autojet" type="radio" />
                                    Autojet
                                </label>
                            </label>
                        </div>
                        <div class="clear" style="clear: both">
                        </div>
                        <div class="control-group">
                            <button class="btn btn-primary" type="button" id="demoBtn3">
                                高级查询</button>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSelect" CssClass="btn btn-primary" runat="server" Text="  查 询  "
                                OnClick="btnSelect_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" id="btnReset" class="btn btn-primary" value="  重 置  " onclick="DataClear()" />
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
                        <div class="control-group control-anniu">
                            <input type="button" value="发送邮件" class="btn btn-primary" id="btnEmail" onclick="mail()" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnExPort" Text="  导 出  " OnClientClick="javascript:return confirm('您确定导出数据吗？')"
                                runat="server" CssClass="btn btn-primary" OnClick="btnExPort_Click" />
                        </div>
                    </div>
                    <div class="content-bd">
                        <table width="100%" id="tb1" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                            <thead>
                                <tr>
                                    <th width="8%" scope="col">
                                        Order Date
                                    </th>
                                    <th width="8%" scope="col">
                                        Order No
                                    </th>
                                    <th width="8%" scope="col">
                                        Agreement No
                                    </th>
                                    <th width="8%" scope="col">
                                        Customer Name
                                    </th>
                                    <th width="8%" scope="col">
                                        Sales Name
                                    </th>
                                    <th width="8%" scope="col">
                                        PartNo
                                    </th>
                                    <th width="8%" scope="col">
                                        Qty
                                    </th>
                                    <th width="8%" scope="col">
                                        Amount
                                    </th>
                                    <th width="8%" scope="col">
                                        Arrival Date
                                    </th>
                                    <th width="8%" scope="col">
                                        Invoice date
                                    </th>
                                    <th width="8%" scope="col">
                                        Customer Request Delivery Date
                                    </th>
                                    <th width="8%" scope="col">
                                        Pending Days
                                    </th>
                                </tr>
                                <%=DataCount%>
                            </thead>
                            <asp:Repeater runat="server" ID="rpGA03">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("OA01009") == DBNull.Value ? "" : Convert.ToDateTime(Eval("OA01009")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01053")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01003")%>
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
                                            <%# Eval("OB01009") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OB01009")), 2).ToString()%>
                                        </td>
                                        <td>
                                            <%# Eval("OC01009") == DBNull.Value ? "" : Convert.ToDateTime(Eval("OC01009")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%# Eval("OC01015") == DBNull.Value ? "" : Convert.ToDateTime(Eval("OC01015")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01010") == DBNull.Value ? "" : Convert.ToDateTime(Eval("OA01010")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%# Eval("Pendingdays") %>
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
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/base/base.getdata.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_UserControl.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_WeeklyPending.js" type="text/javascript"></script>
    </form>
</body>
</html>
