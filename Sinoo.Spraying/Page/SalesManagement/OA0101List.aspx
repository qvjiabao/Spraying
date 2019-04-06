<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" EnableEventValidation="False"
    CodeBehind="OA0101List.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.OA0101List" %>

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
    <link href="/Scripts/layer/theme/default/layer.css" rel="stylesheet" />
    <title>Orders Data List </title>

</head>
<body class="ys">
    <form id="form1" runat="server">
        <div id="content-header">
            <h1>Orders Data List</h1>
        </div>
        <div id="breadcrumb">
            <a href="#" title="" class="tip-bottom" data-original-title="Sales Management"><i
                class="icon-home"></i>Sales Management</a><a href="#" class="current">Orders Data List</a>
        </div>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div id="Div1" class="modal hide fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        &times;</button>
                                    <h4 class="modal-title" id="H1">Import Order Data</h4>
                                </div>
                                <div class="modal-body" style="height: 230px;">
                                    <iframe id="Iframe1" style="border: 0px; width: 99%; height: 100%;"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="content-box">
                        <div class="content-h">
                            <div class="bh">
                                <i class="icon-search"></i><span>查询条件</span>
                            </div>
                        </div>
                        <input type="hidden" value="<%=ViewState["PageIndex"] %>" id="PageIndex" />
                        <div class="xz">
                            <div class="control-group">
                                <label>
                                    <span>Customer Code：</span>
                                    <asp:TextBox ID="txtCA01002" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Customer Name：</span>
                                    <asp:TextBox ID="txtCA01003" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Order NO：</span>
                                    <asp:TextBox ID="txtOA01002" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Part NO：</span>
                                    <asp:TextBox ID="txtPA01003" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Province：</span>
                                    <asp:DropDownList ID="ddlGA03Province" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>City：</span>
                                    <input type="hidden" id="ddlGA03Value" runat="server" />
                                    <input type="hidden" id="ddlGA03html" runat="server" />
                                    <asp:DropDownList ID="ddlGA03City" runat="server">
                                        <asp:ListItem Value="">请选择</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Sales Name：</span>
                                    <input type="hidden" id="ddlUA01Value" runat="server" />
                                    <input type="hidden" id="ddlUA01html" runat="server" />
                                    <asp:DropDownList ID="ddlUA01" runat="server">
                                        <asp:ListItem Value="">请选择</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>AC：</span>
                                    <asp:DropDownList ID="ddlOB02" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </div>
                            <div class="control-group">
                                <label onclick="return false">
                                    <span>Date：</span>
                                    <asp:TextBox ID="txtOA01009Start" runat="server" onClick="WdatePicker({maxDate:'#F{$dp.$D(\'txtOA01009End\')}'})"
                                        CssClass="Wdate" Style="width: 92px; background-position: 82px 7px;"></asp:TextBox>
                                    -
                                <asp:TextBox ID="txtOA01009End" runat="server" onClick="WdatePicker({minDate:'#F{$dp.$D(\'txtOA01009Start\')}'})"
                                    CssClass="Wdate" Style="width: 92px; background-position: 82px 7px;"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label onclick="return false">
                                    <span>Invoice Date：</span>
                                    <asp:TextBox ID="txtOC01015Start" runat="server" onClick="WdatePicker({maxDate:'#F{$dp.$D(\'txtOC01015End\')}'})"
                                        CssClass="Wdate" Style="width: 92px; background-position: 82px 7px;"></asp:TextBox>
                                    -
                                <asp:TextBox ID="txtOC01015End" runat="server" onClick="WdatePicker({minDate:'#F{$dp.$D(\'txtOC01015Start\')}'})"
                                    CssClass="Wdate" Style="width: 92px; background-position: 82px 7px;"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Customer Request Delivery Date：</span>
                                    <asp:TextBox ID="txtOA01010" runat="server" onClick="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Minifogger：</span>
                                    <label class="radio inline">
                                        <asp:RadioButton GroupName="rbtnOA01046" ID="rbtnOA01046Yes" runat="server" />是
                                    </label>
                                    <label class="radio inline">
                                        <asp:RadioButton GroupName="rbtnOA01046" ID="rbtnOA01046No" runat="server" />否
                                    </label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>End User：</span>
                                    <asp:TextBox ID="txtOA01046" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Application Description：</span>
                                    <asp:TextBox ID="txtOA01047" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>New Application：</span>
                                    <asp:TextBox ID="txtOA01024" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Application Share：</span>
                                    <asp:TextBox ID="txtOA01048" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Agreement No：</span>
                                    <asp:TextBox ID="txtOA01053" CssClass="form-control" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="clear" style="clear: both">
                            </div>
                            <div class="control-group">
                                <asp:Button ID="btnSelect" CssClass="btn btn-primary" runat="server" Text="  查 询  "
                                    OnClientClick="SaveCityCustomer()" OnClick="btnSelect_Click" />
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
                                <i class="icon-file"></i><span>查询结果</span>
                            </div>
                            <div class="control-group control-anniu">
                                <input type="button" value="  新 增  " id="btnNew" class="btn btn-primary" onclick="return btnNew_onclick()" />
                                &nbsp; &nbsp;
                            <input type="button" id="btnImport" class="btn btn-primary" value="  导 入  " onclick="Import()" />
                                &nbsp; &nbsp;
                            <asp:Button ID="btnExport" Text="  导 出  " runat="server" OnClientClick="javascript:return confirm('您确定导出数据吗？')"
                                CssClass="btn btn-primary" OnClick="btnExport_Click" />
                            </div>
                        </div>
                        <div class="content-bd">
                            <table width="100%" id="tb1" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                                <thead>
                                    <tr>
                                        <th width="9%" scope="col">Customer Code
                                        </th>
                                        <th width="9%" scope="col">Customer Name
                                        </th>
                                        <th width="9%" scope="col">Invoice No
                                        </th>
                                        <th width="9%" scope="col">Order NO
                                        </th>
                                        <th width="9%" scope="col">Order Date
                                        </th>
                                        <th width="9%" scope="col">Amount(RMB)
                                        </th>
                                        <th width="9%" scope="col">Sales Name
                                        </th>
                                        <th width="9%" scope="col">Province
                                        </th>
                                        <th width="9%" scope="col">City
                                        </th>
                                        <th width="9%" scope="col">Outsanding
                                        </th>
                                        <th width="9%" scope="col">Operation
                                        </th>
                                    </tr>
                                    <%=DataCount %>
                                </thead>
                                <asp:Repeater runat="server" ID="repOA01">
                                    <ItemTemplate>
                                        <%# Eval("color") is DBNull ? "<tr>" : "<tr style='color:" + Eval("color").ToString() + "'>"%>
                                        <td>
                                            <%# Eval("CA01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01003")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01008")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01009") is DBNull? "": Convert.ToDateTime(Eval("OA01009")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td>
                                            <%# Eval("OA01020") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OA01020")), 2).ToString()%>
                                        </td>
                                        <td>
                                            <%# Eval("UA01005")%>
                                        </td>
                                        <td>
                                            <%# Eval("ProvinceName")%>
                                        </td>
                                        <td>
                                            <%# Eval("CityName")%>
                                        </td>
                                        <td>
                                            <%# Eval("OP01015") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OP01015")), 2).ToString()%>
                                        </td>
                                        <td class="taskOptions">
                                            <a href="OA0101Edit.aspx?OA01001=<%# Eval("OA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>&CA01001=<%# Eval("CA01001") %>"
                                                class="tip-top" name="Edit" data-original-title="编辑"><i class="icon-pencil"></i>
                                            </a>&nbsp;&nbsp; <a href="OA0101View.aspx?OA01001=<%# Eval("OA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="View" data-original-title="浏览"><i class="icon-search"></i>
                                            </a>&nbsp;&nbsp; <a href="OA0101CrystalReport.aspx?OA01001=<%# Eval("OA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="print" data-original-title="打印"><i class="icon-print"></i>
                                            </a>
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
        <%--新增弹出层--%>
        <div aria-hidden="false" aria-labelledby="myModalLabel" role="dialog" tabindex="-1"
            class="modal hide fade" id="MyDilog" style="height: auto; top: 10px; min-height: 350px">
            <div class="modal-header">
                <button aria-hidden="true" data-dismiss="modal" class="close" type="button">
                    ×</button>
                <h3 id="myModalLabel">Select Customer</h3>
            </div>
            <div class="modal-body" style="height: auto;">
                <div>
                    <span>Area：</span>&nbsp;<asp:DropDownList ID="ddlGA03ProvinceDialog" runat="server"
                        Style="width: 200px;">
                    </asp:DropDownList>
                    <select id="ddlGA03CityDialog" runat="server" style="width: 200px;">
                    </select>
                    <br />
                    <span>Customer：</span>
                    <input type="text" id="CustomerName" placeholder="模糊查询" style="margin-top: 10px; width: 189px;" />
                    &nbsp;&nbsp;<input type="button" class="btn btn-primary" value="查询" id="btnSelectCustomer" />
                    <table width="75%" class="table table-bordered table-striped table-hover with-check zhong nomargin"
                        style="border-bottom: 1px solid #ddd">
                        <thead>
                            <tr>
                                <th width="25%" scope="col">Customer Code
                                </th>
                                <th width="25%" scope="col">Customer Name
                                </th>
                                <th width="25%" scope="col">Operate
                                </th>
                            </tr>
                        </thead>
                        <tbody id="TableBodyCustomer">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div id="myModal" class="modal hide fade">
            <div class="modal-body">
                <iframe id="myIframe" width="100%"></iframe>
            </div>
        </div>
        <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
        <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
        <script src="/Scripts/script.js" type="text/javascript"></script>
        <script src="/Scripts/bootstrap.min.js" type="text/javascript"></script>
        <script src="/Scripts/unicorn.js" type="text/javascript"></script>
        <script src="/Scripts/base/base.getdata.js" type="text/javascript"></script>
        <script src="/Scripts/layer/layer.js" type="text/javascript"></script>
        <script src="/Scripts/page/Page_OA0101List.js?v=1.3" type="text/javascript"></script>
        <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    </form>
</body>
</html>
