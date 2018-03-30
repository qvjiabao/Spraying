<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UA0101List.aspx.cs" Inherits="Sinoo.Spraying.Page.UserManagement.UA0101List" %>

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
    <title>User Data List</title>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            User Data List</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="User Management"><i class="icon-home">
        </i>User Management </a><a href="#" class="current">User Data List</a>
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
                                <span>用户名称：</span>
                                <asp:TextBox ID="txtUA01004" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>用户英文名称：</span>
                                <asp:TextBox ID="txtUA01005" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>用户代码：</span>
                                <asp:TextBox ID="txtUA01006" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>职位：</span>
                                <asp:DropDownList ID="ddlUA01009" runat="server">
                                    <asp:ListItem Value="">请选择</asp:ListItem>
                                    <asp:ListItem Value="1">销售</asp:ListItem>
                                    <asp:ListItem Value="2">经理</asp:ListItem>
                                    <asp:ListItem Value="3">主管</asp:ListItem>
                                    <asp:ListItem Value="4">销售协调</asp:ListItem>
                                    <asp:ListItem Value="5">系统调试</asp:ListItem>
                                    <asp:ListItem Value="6">其他</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="clear" style="clear: both">
                        </div>
                        <div class="control-group">
                            <asp:Button ID="btnSelect" CssClass="btn btn-primary" runat="server" Text="  查 询  " OnClick="btnSelect_Click" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <input type="button" class="btn btn-primary" onclick="DataClear()" value="  重 置  " />
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
                            &nbsp;<asp:Button ID="btnNew" Text="  新 增  " OnClientClick="checked" runat="server" CssClass="btn btn-primary"
                                OnClick="btnNew_Click" />
                        </div>
                    </div>
                    <div class="content-bd">
                        <table width="100%" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                            <thead>
                                <tr>
                                    <th width="11%" scope="col">
                                        用户名称
                                    </th>
                                    <th width="11%" scope="col">
                                        用户英文名
                                    </th>
                                    <th width="11%" scope="col">
                                        用户代码
                                    </th>
                                    <th width="11%" scope="col">
                                        性别
                                    </th>
                                    <th width="11%" scope="col">
                                        职位
                                    </th>
                                    <th width="11%" scope="col">
                                        手机号
                                    </th>
                                    <th width="11%" scope="col">
                                        联系电话
                                    </th>
                                    <th width="11%" scope="col">
                                        邮箱
                                    </th>
                                    <th width="11%" scope="col">
                                        操作
                                    </th>
                                </tr>
                                <%=DataCount %>
                            </thead>
                            <asp:Repeater runat="server" ID="repUA01">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("UA01004")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA01005")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA01006")%>
                                        </td>
                                        <td>
                                            <%# Convert.ToBoolean(Eval("UA01008"))==true?"男":"女"%>
                                        </td>
                                        <td>
                                            <%# Eval("UB01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA01010")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA01011")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA01012")%>
                                        </td>
                                        <td class="taskOptions">
                                            <a href="UA0101Edit.aspx?UA01001=<%# Eval("UA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="Edit" data-original-title="编辑"><i class="icon-pencil"></i></a>&nbsp;&nbsp;
                                            <a href="UA0101View.aspx?UA01001=<%# Eval("UA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="View" data-original-title="浏览"><i class="icon-search"></i></a>&nbsp;&nbsp;
                                            <asp:LinkButton class="tip-top" name="Del" data-original-title="删除" ID="lbtnRemoveData" CommandArgument='<%# Eval("UA01001") %>'
                                                OnClientClick="javascript:return confirm('是否删除数据？')" OnCommand="RemoveData" runat="server"><i class=" icon-remove"></i></asp:LinkButton>
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
    <script src="/Scripts/page/Page_UA0101List.js" type="text/javascript"></script>
    </form>
</body>
</html>
