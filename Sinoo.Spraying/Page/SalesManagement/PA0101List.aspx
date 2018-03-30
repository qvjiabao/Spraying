<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PA0101List.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.PA0101List" %>

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
    <title>Product Data List</title>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            Product Data List</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Product Data"><i class="icon-home">
        </i>Product Data </a><a href="#" class="current">Product Data List</a>
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
                            Part No：
                            <asp:TextBox ID="txtPA01003" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="clear" style="clear: both">
                        </div>
                        <div class="control-group">
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
                <div id="myModal" class="modal hide fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title" id="myModalLabel">
                                    Import Product Data</h4>
                            </div>
                            <div class="modal-body">
                                <iframe id="myIframe" style="border: 0px; width: 99%; height: 100%;"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-file"></i><span>查询结果</span></div>
                        <div class="control-group control-anniu">
                            <asp:Button ID="btnNew" Text="  新 增  " OnClientClick="checked" runat="server" CssClass="btn btn-primary"
                                OnClick="btnNew_Click" />
                            &nbsp;
                            <input type="button" id="btnImport" class="btn btn-primary" value="  导 入  " onclick="Import()" />
                            &nbsp;
                            <asp:Button ID="btnExPort" Text="  导 出  " OnClientClick="javascript:return confirm('您确定导出数据吗？')"
                                runat="server" CssClass="btn btn-primary" OnClick="btnExPort_Click" />
                        </div>
                    </div>
                    <div class="content-bd">
                        <table width="100%" id="tb1" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                            <thead>
                                <tr>
                                    <th width="14%" scope="col">
                                        Part No
                                    </th>
                                    <th width="14%" scope="col">
                                        Description
                                    </th>
                                    <th width="14%" scope="col">
                                        Guide Price One(1-35)
                                    </th>
                                    <th width="14%" scope="col">
                                        Guide Price Two(36-99)
                                    </th>
                                    <th width="14%" scope="col">
                                        Guide Price Three(100-499)
                                    </th>
                                    <th width="14%" scope="col">
                                        Guide Price Four(500+)
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
                                            <%# Eval("PA01003")%>
                                        </td>
                                        <td>
                                            <%# Eval("PA01005")%>
                                        </td>
                                        <td>
                                            <%# Eval("One")%>
                                        </td>
                                        <td>
                                            <%# Eval("Two")%>
                                        </td>
                                        <td>
                                            <%# Eval("Three")%>
                                        </td>
                                        <td>
                                            <%# Eval("Four")%>
                                        </td>
                                        <td class="taskOptions">
                                            <a href="PA0101Edit.aspx?PA01001=<%# Eval("PA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>&type=list"
                                                class="tip-top" name="Edit" data-original-title="编辑"><i class="icon-pencil"></i>
                                            </a>&nbsp;&nbsp; <a href="PA0101View.aspx?PA01001=<%# Eval("PA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="View" data-original-title="浏览"><i class="icon-search"></i>
                                            </a>&nbsp;&nbsp;
                                            <asp:LinkButton class="tip-top" name="Del" data-original-title="删除" ID="lbtnRemoveData"
                                                CommandArgument='<%# Eval("PA01001") %>' OnClientClick="javascript:return confirm('是否删除数据？')"
                                                OnCommand="RemoveData" runat="server"><i class=" icon-remove"></i></asp:LinkButton>
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
    <script src="/Scripts/page/Page_PA0101List.js" type="text/javascript"></script>
    </form>
</body>
</html>
