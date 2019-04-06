<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OA0102List.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.OA0102List" %>

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
    <link href="/Scripts/validator/jquery.validator.css" rel="stylesheet" type="text/css" />
    <link href="/css/PageControl.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/page/Page_PA0101List.js" type="text/javascript"></script>
    <title>Goods Return List</title>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            Goods Return List</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Sales Management"><i class="icon-home">
        </i>Sales Management </a><a href="#" class="current"> Goods Return List</a>
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
                                <span>Customer Code：</span>
                                <asp:TextBox ID="txtCustomerCode" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Customer Name：</span>
                                <asp:TextBox ID="txtCustomerName" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Return Order NO：</span>
                                <asp:TextBox ID="txtOrderNO" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Sales Name：</span>
                                <asp:DropDownList ID="ddltSalesName" runat="server">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Province：</span>
                                <asp:DropDownList ID="ddlProvince" runat="server">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Date：</span>
                                <asp:TextBox ID="txtBeginTime" Style="width: 91px;" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'txtEndTime\',{d:-1})}'})"
                                    CssClass="Wdate" runat="server"></asp:TextBox>&nbsp;-&nbsp;
                                <asp:TextBox ID="txtEndTime" Style="width: 91px;" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtBeginTime\',{d:1})}'})"
                                    CssClass="Wdate" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Part No：</span>
                                <asp:TextBox ID="txtPartNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="clear" style="clear: both">
                        </div>
                        <div class="control-group">
                            <asp:Button ID="btnSelect" CssClass="btn btn-primary" runat="server" Text="  查 询  " OnClick="btnSelect_Click" />
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
                            <asp:Button ID="btnNew" Text="  新 增  " OnClientClick="checked" runat="server" CssClass="btn btn-primary"
                                OnClick="btnNew_Click" />
                        </div>
                    </div>
                    <div class="content-bd">
                        <table width="100%" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                            <thead>
                                <tr>
                                    <th width="14%" scope="col">
                                        Customer Code
                                    </th>
                                    <th width="14%" scope="col">
                                        Customer Name
                                    </th>
                                    <th width="14%" scope="col">
                                        Order NO
                                    </th>
                                    <th width="14%" scope="col">
                                        Order Date
                                    </th>
                                    <th width="14%" scope="col">
                                        Sales Name
                                    </th>
                                    <th width="14%" scope="col">
                                        Province
                                    </th>
                                    <th width="16%" scope="col">
                                        Operation
                                    </th>
                                </tr>
                                <%=DataCount %>
                            </thead>
                            <asp:Repeater runat="server" ID="rpPA01">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("OA01036")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01003")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01002")%>
                                        </td>
                                        <td>
                                            <%# Convert.ToDateTime(Eval("OA01009")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA01005")%>
                                        </td>
                                        <td>
                                            <%# Eval("ProvinceName")%>
                                        </td>
                                        <td class="taskOptions">
                                            <a href="OA0102Edit.aspx?OA01001=<%# Eval("OA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>&type=list"
                                                class="tip-top" name="Edit" data-original-title="编辑"><i class="icon-pencil"></i></a>&nbsp;&nbsp;
                                            <a href="OA0102View.aspx?OA01001=<%# Eval("OA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="View" data-original-title="浏览"><i class="icon-search"></i></a>&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-bl ui-corner-br ui-helper-clearfix nomargin">
                            <div class="dataTables_paginate fg-buttonset ui-buttonset fg-buttonset-multi ui-buttonset-multi paging_full_numbers"
                                id="DataTables_Table_0_paginate">
                                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="paginator" CurrentPageButtonClass="cpb"
                                    Width="100%" runat="server" AlwaysShow="false" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" ShowInputBox="Never"
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
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/script.js" type="text/javascript"></script>
    <script src="/Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="/Scripts/unicorn.js" type="text/javascript"></script>
    <script src="/Scripts/validator/jquery.validator.js" type="text/javascript"></script>
    <script src="/Scripts/validator/local/zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_OA0102List.js" type="text/javascript"></script>
    </form>
</body>
</html>
