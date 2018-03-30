﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GA0701List.aspx.cs" Inherits="Sinoo.Spraying.Page.CustomerManagement.GA0701List" %>

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
    <link href="/css/unipop.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/validator/jquery.validator.css" rel="stylesheet" type="text/css" />
    <link href="/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <title>Customer File List</title>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            Customer File List</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Customer Management"><i
            class="icon-home"></i>Customer Management</a><a href="#" class="current">Customer File
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
                                <span>Customer Name：</span>
                                <asp:TextBox ID="txtCA01003" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>File Name：</span>
                                <asp:TextBox ID="txtGA07006" CssClass="form-control" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label onclick="return false">
                                <span>UpLoad Time：</span>
                                <asp:TextBox ID="txtGA07998Start" runat="server" Style="width: 92px; background-position: 82px 7px;"
                                    CssClass="Wdate" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'txtGA07998End\',{d:-1})}'})"></asp:TextBox>
                                -
                                <asp:TextBox ID="txtGA07998End" runat="server" Style="width: 92px; background-position: 82px 7px;"
                                    CssClass="Wdate" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtGA07998Start\',{d:1})}'})"></asp:TextBox>
                            </label>
                        </div>
                        <div class="clear" style="clear: both">
                        </div>
                        <div class="control-group">
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="  查 询  "
                                OnClick="btnSelect_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <button class="btn btn-primary" type="button" id="Button2" onclick="DataClear()">
                                &nbsp;&nbsp;重 置&nbsp;&nbsp;</button>
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
                            <button class="btn btn-primary" type="button" id="demoBtn3">
                                &nbsp;&nbsp;新 增 &nbsp;&nbsp;</button>
                        </div>
                    </div>
                    <div class="content-bd">
                        <table width="100%" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                            <thead>
                                <tr>
                                    <th width="25%" scope="col">
                                        File Name
                                    </th>
                                    <th width="25%" scope="col">
                                        Customer Name
                                    </th>
                                    <th width="25%" scope="col">
                                        Upload Time
                                    </th>
                                    <th width="25%" scope="col">
                                        Operate
                                    </th>
                                </tr>
                                <%=DataCount %>
                            </thead>
                            <asp:Repeater runat="server" ID="repGA07">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("GA07006")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01003")%>
                                        </td>
                                        <td>
                                            <%# Eval("GA07998")%>
                                        </td>
                                        <td class="taskOptions">
                                            <asp:LinkButton class="tip-top" name="DownLoad" data-original-title="下载" ID="lbtnDownLoad" CommandArgument='<%# Eval("GA07001") %>'
                                                runat="server" OnCommand="DownLoad"><i class="icon-download-alt"></i></asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton class="tip-top" name="Del" data-original-title="删除" ID="lbtnRemoveData" CommandArgument='<%# Eval("GA07001") %>'
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
        <input type="hidden" id="PageIndex" value="<%=GetPageIndex()%>" />
    </div>
    <%--新增弹出层--%>
    <div aria-hidden="false" aria-labelledby="myModalLabel" role="dialog" tabindex="-1"
        class="modal hide fade" id="imgBox" style="height: auto; top: 10px; min-height: 350px">
        <div class="modal-header">
            <button aria-hidden="true" data-dismiss="modal" class="close" type="button">
                ×</button>
            <h3 id="myModalLabel">
                选择客户</h3>
        </div>
        <div class="modal-body" style="height: auto;">
            <div>
                <p>
                    <span>Customer Area：</span><asp:DropDownList ID="ddlGA03ProvinceDialog" runat="server">
                    </asp:DropDownList>
                    <select id="ddlGA03CityDialog" runat="server">
                    </select>
                </p>
                <p>
                    <span>Customer Name：</span>
                    <asp:TextBox ID="CustomerName" runat="server" placeholder="模糊查询" Style="margin-top: 10px;"></asp:TextBox>
                    <input type="button" id="btnSelectCustomer" value="查 询" class="btn btn-primary" />
                </p>
                <table width="75%" class="table table-bordered table-striped table-hover with-check zhong nomargin"
                    style="border-bottom: 1px solid #ddd">
                    <thead>
                        <tr>
                            <th width="25%" scope="col">
                                Customer Code
                            </th>
                            <th width="25%" scope="col">
                                Customer Name
                            </th>
                            <th width="25%" scope="col">
                                Operate
                            </th>
                        </tr>
                    </thead>
                    <tbody id="TableBodyCustomer">
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Scripts/easydialog.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/script.js" type="text/javascript"></script>
    <script src="/Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="/Scripts/unicorn.js" type="text/javascript"></script>
    <script src="/Scripts/base/base.getdata.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_GA0701List.js" type="text/javascript"></script>
    <script type="text/javascript">
        $("#demoBtn3").click(function () {
            $('#imgBox').modal({ show: true });
            $("#ddlGA03ProvinceDialog").get(0).value = "";
            $("#ddlGA03CityDialog").get(0).value = "";
            $("#CustomerName").val("");
            $("#TableBodyCustomer").html("");
        });
    </script>
    </form>
</body>
</html>
