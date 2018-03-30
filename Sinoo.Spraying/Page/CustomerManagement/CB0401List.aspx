<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CB0401List.aspx.cs" Inherits="Sinoo.Spraying.Page.CustomerManagement.CB0401List" %>

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
    <title>客户行业代码列表</title>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            Customer SIC List</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Customer Management"><i
            class="icon-home"></i>Customer Management</a><a href="#" class="current">Customer SIC
                List</a>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="control-group control-anniu">
                            <asp:Button ID="btnNew" Text="  新 增  " runat="server" CssClass="btn btn-primary"
                                OnClick="btnNew_Click" />
                        </div>
                    </div>
                    <div class="content-bd">
                        <table width="100%%" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                            <thead>
                                <tr>
                                    <th width="20%" scope="col">
                                        SIC
                                    </th>
                                    <th width="20%" scope="col">
                                        MDT
                                    </th>
                                    <th width="20%" scope="col">
                                        Remark
                                    </th>
                                    <th width="20%" scope="col">
                                        Operation
                                    </th>
                                </tr>
                                <%=DataCount %>
                            </thead>
                            <asp:Repeater runat="server" ID="rpCB04">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("CB04002") %>
                                        </td>
                                        <td>
                                            <%# Eval("CB03002") %>
                                        </td>
                                        <td>
                                            <%# Eval("CB04005").ToString().Length > 20 ? Eval("CB04005").ToString().Substring(0, 20) + "..." : Eval("CB04005").ToString()%>
                                        </td>
                                        <td class="taskOptions">
                                            <a href="CB0401Edit.aspx?CB04001=<%# Eval("CB04001") %>&PageIndex=<%=ViewState["PageIndex"] %>&type=list"
                                                class="tip-top" name="Edit" data-original-title="编辑"><i class="icon-pencil"></i>
                                            </a>&nbsp;&nbsp;
                                            <asp:LinkButton class="tip-top" name="Del" data-original-title="删除" ID="lbtnRemoveData"
                                                CommandArgument='<%# Eval("CB04001") %>' OnClientClick="javascript:return confirm('是否删除数据？')"
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
    <script src="/Scripts/page/Page_CB0401List.js" type="text/javascript"></script>
    <script src="/Scripts/validator/local/zh_CN.js" type="text/javascript"></script>
    </form>
</body>
</html>
