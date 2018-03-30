<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OA0102View.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.OA0102View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Goods Return</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="/css/content-style.css" />
    <link rel="stylesheet" href="/css/fullcalendar.css" />
    <link rel="stylesheet" href="/css/unicorn.main.css" />
    <link rel="stylesheet" href="/css/unicorn.grey.css" class="skin-color" />
    <link rel="stylesheet" href="/css/select2.css" />
    <link rel="stylesheet" href="/css/jquery-ui.css" />
    <link href="/Scripts/validator/jquery.validator.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        tbody input[type='text']
        {
            width: 100px;
        }
        tbody select
        {
            width: 58px;
        }
        tbody .rqxz_1
        {
            background-position: 86px 7px;
        }
    </style>
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            View Goods Return</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Sales Management"><i
            class="icon-home"></i>Sales Management</a><a href="#" title="" class="tip-bottom"
                data-original-title="Goods Return">Goods Return</a><a href="#" class="current">View
                    Goods Return</a>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-plus-sign"></i><span>View</span></div>
                    </div>
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#dingdan" data-toggle="tab">Goods Return</a></li>
                        <li><a href="#mingxi" data-toggle="tab">Goods Return Detail</a></li>
                        <li><a href="#fapiao" data-toggle="tab">Invoice</a></li>
                    </ul>
                    <div class="xz tab-content">
                        <div class="tab-pane active" id="dingdan">
                            <div class="control-group">
                                <label>
                                    <span>Return Order No：</span>
                                    <asp:Label ID="labOA01002" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Order No：</span>
                                    <asp:Label ID="labOA01039" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Invoice No：</span>
                                    <asp:Label ID="labOA01008" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Order Date：</span>
                                    <asp:Label ID="labOA01009" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Sales Name：</span>
                                    <asp:Label ID="labOA01013" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>AutoJet Order：</span>
                                    <asp:Label ID="labOA01005" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label onclick="return false;">
                                    <span>Share Name1：</span>
                                    <asp:Label ID="labOA01015" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="labOA01049" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>share%1：</span>
                                    <asp:Label ID="labOA01016" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <label onclick="return false;">
                                        <span>Share Name2：</span>
                                        <asp:Label ID="labOA01017" runat="server" Text=""></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="labOA01050" runat="server" Text=""></asp:Label>
                                    </label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>share%2：</span>
                                    <asp:Label ID="labOA01018" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Amount(RMB)：</span>
                                    <asp:Label ID="labOA01020" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Exchange Rate：</span>
                                    <asp:Label ID="labOA01021" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Amount(US$)：</span>
                                    <asp:Label ID="labOA01022" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>AC：</span>
                                    <asp:Label ID="labOA01025" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Remark：</span>
                                    <asp:Label ID="labOA01027" runat="server" Text=""></asp:Label>
                                </label>
                            </div>
                        </div>
                        <div class="tab-pane" id="mingxi">
                            <table width="100%%" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                                <thead>
                                    <tr>
                                        <th width="9%" scope="col">
                                            Part NO
                                        </th>
                                        <th width="9%" scope="col">
                                            Description
                                        </th>
                                        <th width="9%" scope="col">
                                            Qty
                                        </th>
                                        <th width="9%" scope="col">
                                            Price
                                        </th>
                                        <th width="9%" scope="col">
                                            Amount
                                        </th>
                                        <th width="9%" scope="col">
                                            Net Price
                                        </th>
                                        <th width="9%" scope="col">
                                            Net Amount
                                        </th>
                                        <th width="9%" scope="col">
                                            TAX
                                        </th>
                                        <th width="9%" scope="col">
                                            Unit Cost
                                        </th>
                                        <th width="9%" scope="col">
                                            Total Cost
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="TableBodymingxi">
                                    <asp:Repeater ID="rptmingxi" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <span name="PartNO">
                                                        <%#Eval("OB01005")%></span>
                                                </td>
                                                <td>
                                                    <span name="Description">
                                                        <%#Eval("OB01006")%></span>
                                                </td>
                                                <td>
                                                    <span name="Qty">
                                                        <%#Eval("OB01007")%></span>
                                                </td>
                                                <td>
                                                    <span name="Price">
                                                        <%# Eval("OB01008") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OB01008")), 2).ToString()%>
                                                </td>
                                                <td>
                                                    <span name="Amount">
                                                        <%# Eval("OB01009") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OB01009")), 2).ToString()%>
                                                </td>
                                                <td>
                                                    <span name="NetPrice">
                                                        <%# Eval("OB01010") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OB01010")), 2).ToString()%>
                                                </td>
                                                <td>
                                                    <span name="NetAmount">
                                                        <%# Eval("OB01011") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OB01011")), 2).ToString()%>
                                                </td>
                                                <td>
                                                    <span name="TAX">
                                                        <%# Eval("OB01012") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OB01012")), 2).ToString()%>
                                                </td>
                                                <td>
                                                    <span name="UnitCost">
                                                        <%# Eval("OB01013") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OB01013")), 2).ToString()%>
                                                </td>
                                                <td>
                                                    <span name="TotalCost">
                                                        <%# Eval("OB01014") == DBNull.Value ? "" : Math.Round(Convert.ToDecimal(Eval("OB01014")), 2).ToString()%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="fapiao">
                            <table width="100%%" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                                <thead>
                                    <tr>
                                        <th width="12.5%" scope="col">
                                            S/N
                                        </th>
                                        <th width="12.5%" scope="col">
                                            Part NO
                                        </th>
                                        <th width="12.5%" scope="col">
                                            Qty
                                        </th>
                                        <th width="12.5%" scope="col">
                                            Arrived
                                        </th>
                                        <th width="12.5%" scope="col">
                                            Arrival Date
                                        </th>
                                        <th width="12.5%" scope="col">
                                            Delivered(Y/N)
                                        </th>
                                        <th width="12.5%" scope="col">
                                            Delivered Date
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="TableBodyfapiao">
                                    <asp:Repeater ID="rptfapiao" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <span name="SN">
                                                        <%# Eval("OC01002") %></span>
                                                </td>
                                                <td>
                                                    <span name="PartNO">
                                                        <%# Eval("OC01005") %></span>
                                                </td>
                                                <td>
                                                    <span name="Qty">
                                                        <%# Eval("OC01006") %></span>
                                                </td>
                                                <td>
                                                    <span name="Arrived">
                                                        <%# Eval("OC01007").ToString().Trim() == "0" ? "N" : "Y"%></span>
                                                </td>
                                                <td>
                                                    <span name="ArrivalDate">
                                                        <%# Eval("OC01009") is DBNull?"": Convert.ToDateTime(Eval("OC01009")).ToString("yyyy-MM-dd") %></span>
                                                </td>
                                                <td>
                                                    <span name="Delivered">
                                                        <%# Eval("OC01014").ToString().Trim() == "0" ? "N" : "Y" %></span></span>
                                                </td>
                                                <td>
                                                    <span name="DeliveredDate">
                                                        <%# Eval("OC01016") is DBNull ? "" : Convert.ToDateTime(Eval("OC01016")).ToString("yyyy-MM-dd")%></span></span>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="page-footer">
                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="编 辑" OnClick="btnUpdate_Click" />
                &nbsp;
                <asp:Button ID="btnReturn" CssClass="btn btn-primary" runat="server" Text="返 回" novalidate
                    OnClick="btnReturn_Click" />
            </div>
        </div>
    </div>
    <div id="myModal" class="modal hide fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">
                        Select Share Name</h4>
                </div>
                <div class="modal-body">
                    <iframe id="myIframe" name="myIframe" style="border: 0px; width: 99%; height: 100%;"
                        scrolling="no"></iframe>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        关闭</button>
                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="$('#'+$('#Cid',window.frames['myIframe'].document).val()).val($('#ddlUA01',window.frames['myIframe'].document).val());">
                        确定</button>
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
    <script src="/Scripts/base/base.getdata.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_OA0102New.js" type="text/javascript"></script>
    </form>
</body>
</html>
