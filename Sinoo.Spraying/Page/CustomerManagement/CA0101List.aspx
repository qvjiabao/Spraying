<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="CA0101List.aspx.cs"
    Inherits="Sinoo.Spraying.Page.CustomerManagement.CA0101List" %>

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
    <title>Customer List</title>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            Customer List</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Customer Management"><i
            class="icon-home"></i>Customer Management</a><a href="#" class="current">Customer List</a>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-search"></i><span>查询条件</span>
                        </div>
                    </div>
                    <div class="xz">
                        <div class="control-group">
                            <label>
                                <span>MDT：</span>
                                <asp:DropDownList ID="ddlCA01019" runat="server">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>CTC：</span>
                                <asp:DropDownList ID="ddlCA01018" runat="server">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Customer Type：</span>
                                <asp:DropDownList ID="ddlCA01016" runat="server">
                                </asp:DropDownList>
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
                                <input type="hidden" id="ddlGA03SelectValue" runat="server" />
                                <input type="hidden" id="ddlGA03Html" runat="server" />
                                <select id="ddlGA03City" runat="server">
                                    <option value="">请选择</option>
                                </select>
                            </label>
                        </div>
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
                                <span>SIC：</span>
                                <asp:DropDownList ID="ddlCA01020" runat="server">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Credit Customer(Y/N)：</span>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnCA01025" ID="rbtnCA01025Yes" runat="server" />是
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnCA01025" ID="rbtnCA01025No" runat="server" />否
                                </label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>FGD(Y/N)：</span>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnCA01024" ID="rbtnCA01024Yes" runat="server" />是
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnCA01024" ID="rbtnCA01024No" runat="server" />否
                                </label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Key Customer：</span>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnCA01047" ID="rbtnCA01047Yes" runat="server" />是
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnCA01047" ID="rbtnCA01047No" runat="server" />否
                                </label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Black List：</span>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnCA01052" ID="rbtnCA01052Yes" runat="server" />是
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnCA01052" ID="rbtnCA01052No" runat="server" />否
                                </label>
                            </label>
                        </div>
                        <div class="clear" style="clear: both">
                        </div>
                        <div class="control-group">
                            <asp:Button ID="btnSelect" CssClass="btn btn-primary" runat="server" Text="  查 询  "
                                OnClientClick="SaveCity()" OnClick="btnSelect_Click" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <input type="button" class="btn btn-primary" onclick="DataClear()" value="  重 置  " />
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
                                    Import CustomerData</h4>
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
                            <i class="icon-file"></i><span>查询结果</span>
                        </div>
                        <div class="control-group control-anniu">
                            <asp:Button ID="btnNew" Text="  新 增  " OnClientClick="checked" runat="server" CssClass="btn btn-primary"
                                OnClick="btnNew_Click" />
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
                                    <th width="11%" scope="col">
                                        Customer Code
                                    </th>
                                    <th width="11%" scope="col">
                                        Customer Name
                                    </th>
                                    <th width="11%" scope="col">
                                        Province
                                    </th>
                                    <th width="11%" scope="col">
                                        City
                                    </th>
                                    <th width="11%" scope="col">
                                        Customer Type
                                    </th>
                                    <th width="11%" scope="col">
                                        MDT
                                    </th>
                                    <th width="11%" scope="col">
                                        SIC
                                    </th>
                                    <th width="11%" scope="col">
                                        CTC
                                    </th>
                                    <th width="11%" scope="col">
                                        Operate
                                    </th>
                                </tr>
                                <%=DataCount %>
                            </thead>
                            <asp:Repeater runat="server" ID="repUA01">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("CA01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CA01003")%>
                                        </td>
                                        <td>
                                            <%# Eval("ProvinceName")%>
                                        </td>
                                        <td>
                                            <%# Eval("CityName")%>
                                        </td>
                                        <td>
                                            <%# Eval("CB01002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CB03002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CB04002")%>
                                        </td>
                                        <td>
                                            <%# Eval("CB02002")%>
                                        </td>
                                        <td class="taskOptions">
                                            <a href="CA0101Edit.aspx?CA01001=<%# Eval("CA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="Edit" data-original-title="编辑"><i class="icon-pencil"></i>
                                            </a><a href="CA0101View.aspx?CA01001=<%# Eval("CA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="View" data-original-title="浏览"><i class="icon-search"></i>
                                            </a>
                                            <asp:LinkButton class="tip-top" name="Del" data-original-title="删除" ID="lbtnRemoveData"
                                                CommandArgument='<%# Eval("CA01001") %>' OnClientClick="javascript:return confirm('是否删除数据？')"
                                                OnCommand="RemoveData" runat="server"><i class=" icon-remove"></i></asp:LinkButton>
                                            <a href="../SalesManagement/OA0101New.aspx?Type=Customer&CA01001=<%# Eval("CA01001") %>&PageIndex=<%=ViewState["PageIndex"] %>"
                                                class="tip-top" name="AddOrder" data-original-title="添加订单"><i class="icon-plus-sign">
                                                </i></a>
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
    <script src="/Scripts/base/base.getdata.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_CA0101List.js" type="text/javascript"></script>
    <script type="text/javascript">
        
    </script>
    </form>
</body>
</html>
