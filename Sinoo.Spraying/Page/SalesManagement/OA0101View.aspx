<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OA0101View.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.OA0101View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Orders Data</title>
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
        tbody input[type='text'] {
            width: 100px;
        }

        tbody select {
            width: 58px;
        }

        tbody .rqxz_1 {
            background-position: 86px 7px;
        }
    </style>
</head>
<body class="ys">
    <form id="form1" runat="server">
        <div id="content-header">
            <h1>View Orders Data</h1>
        </div>
        <div id="breadcrumb">
            <a href="#" title="" class="tip-bottom" data-original-title="Sales Management"><i
                class="icon-home"></i>Sales Management</a><a href="#" title="" class="tip-bottom"
                    data-original-title="Orders Date">Orders Date</a><a href="#" class="current">View Orders
                    Date</a>
        </div>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="content-box">
                        <div class="content-h">
                            <div class="bh">
                                <i class="icon-search"></i><span>View</span>
                            </div>
                        </div>
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#dingdan" data-toggle="tab">Order Data</a></li>
                            <li><a href="#mingxi" data-toggle="tab">Order Detail</a></li>
                            <li><a href="#fapiao" data-toggle="tab">Invoice</a></li>
                            <li><a href="#fukuan" data-toggle="tab">Payment</a></li>
                        </ul>
                        <div class="xz tab-content">
                            <div class="tab-pane active" id="dingdan">
                                <div class="control-group">
                                    <label>
                                        <span>Autojet LastOrder No：</span>
                                        <asp:Label ID="labOA01006" runat="server" Text="无"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Nozzle LastOrder No：</span>
                                        <asp:Label ID="labOA01007" runat="server" Text="无"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Order No：</span>
                                        <asp:Label ID="labOA01002" runat="server" Text=""></asp:Label>
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
                                        <span>Agreement No：</span>
                                        <asp:Label ID="labOA01053" runat="server" Text=""></asp:Label>
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
                                        <span>Customer Request Delivery Date：</span>
                                        <asp:Label ID="labOA01010" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Promised Delivery Days：</span>
                                        <asp:Label ID="labOA01011" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Promised Payment Days：</span>
                                        <asp:Label ID="labOA01051" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Qutiation Ref No：</span>
                                        <asp:Label ID="labOA01012" runat="server" Text=""></asp:Label>
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
                                        <span>Special Instruction：</span>
                                        <asp:Label ID="labOA01014" runat="server" Text=""></asp:Label>
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
                                        <asp:Label ID="labOA01049" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Share%1：</span>
                                        <asp:Label ID="labOA01016" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>

                                <div class="control-group">
                                    <label>
                                        <span>Share Area1：</span>
                                        <asp:Label ID="labOA01055" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Share Name2：</span>
                                        <asp:Label ID="labOA01017" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="labOA01050" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Share%2：</span>
                                        <asp:Label ID="labOA01018" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>

                                <div class="control-group">
                                    <label>
                                        <span>Share Area2：</span>
                                        <asp:Label ID="labOA01056" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Cost(US$)：</span>
                                        <asp:Label ID="labOA01019" runat="server" Text=""></asp:Label>
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
                                        <span>GP%：</span>
                                        <asp:Label ID="labOA01023" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>New Application：</span>
                                        <asp:Label ID="labOA01024" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="labOA0102401" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Application Share：</span>
                                        <asp:Label ID="labOA01048" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="labOA0104801" runat="server" Text=""></asp:Label>
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
                                        <span>Payment Terms：</span>
                                        <asp:Label ID="labOA01026" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>goods return：</span>
                                        <asp:Label ID="labOA01003" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>New Cumstomer：</span>
                                        <asp:Label ID="labOA01044" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Minifogger：</span>
                                        <asp:Label ID="labOA01045" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>End User：</span>
                                        <asp:Label ID="labOA01046" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Application Description：</span>
                                        <asp:Label ID="labOA01047" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Comment：</span>
                                        <asp:Label ID="labOA01027" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Delivery Add(Goods)：</span>
                                        <asp:Label ID="labOA01028" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Consignee(Goods)：</span>
                                        <asp:Label ID="labOA01029" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Tel/MP(Goods)：</span>
                                        <asp:Label ID="labOA01030" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Post code(Goods)：</span>
                                        <asp:Label ID="labOA01031" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Delivery Add (lnvoice)：</span>
                                        <asp:Label ID="labOA01032" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Consignee(lnvoice)：</span>
                                        <asp:Label ID="labOA01033" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Tel/ MP(lnvoice)：</span>
                                        <asp:Label ID="labOA01034" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Post code(lnvoice)：</span>
                                        <asp:Label ID="labOA01035" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Dormant Customers：</span>
                                        <asp:Label ID="labOA01054" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                            </div>
                            <div class="tab-pane" id="mingxi">
                                <table width="100%%" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                                    <thead>
                                        <tr>
                                            <th width="9%" scope="col">Part NO
                                            </th>
                                            <th width="9%" scope="col">Description
                                            </th>
                                            <th width="9%" scope="col">Qty
                                            </th>
                                            <th width="9%" scope="col">Price
                                            </th>
                                            <th width="9%" scope="col">Amount
                                            </th>
                                            <th width="9%" scope="col">Net Price
                                            </th>
                                            <th width="9%" scope="col">Net Amount
                                            </th>
                                            <th width="9%" scope="col">TAX
                                            </th>
                                            <th width="9%" scope="col">Unit Cost
                                            </th>
                                            <th width="9%" scope="col">Total Cost
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
                                        <th width="3.66%" scope="col">S/N
                                        </th>
                                        <th width="9.66%" scope="col">Part NO
                                        </th>
                                        <th width="4.66%" scope="col">Qty
                                        </th>
                                        <th width="8.66%" scope="col">Arrived
                                        </th>
                                        <th width="6.66%" scope="col">Arrival No
                                        </th>
                                        <th width="6.66%" scope="col">Arrival Date
                                        </th>
                                        <th width="6.66%" scope="col">Delivered(Y/N)
                                        </th>
                                        <th width="6.66%" scope="col">Delivered Date
                                        </th>
                                        <th width="6.66%" scope="col">Express Company Goods
                                        </th>
                                        <th width="6.66%" scope="col">Tracking
                                        </th>
                                        <th width="6.66%" scope="col">Tracking Remark
                                        </th>
                                        <th width="6.66%" scope="col">Invoiced
                                        </th>
                                        <th width="6.66%" scope="col">Invoice Date
                                        </th>
                                        <th width="6.66%" scope="col">ExpressCo
                                        </th>
                                        <th width="6.66%" scope="col">Tracking Invoice
                                        </th>
                                        <th width="6.66%" scope="col">Tracking Invoice Remark
                                        </th>
                                    </thead>
                                    <tbody id="TableBodyfapiao">
                                        <asp:Repeater ID="rptfapiao" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <span name="SN">
                                                            <%#Eval("OC01002")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="PartNo">
                                                            <%#Eval("OC01005")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="Qty">
                                                            <%#Eval("OC01006")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="Arrived">
                                                            <%#Eval("OC01007").ToString().Trim() == "0" ? "N" : "Y"%></span>
                                                    </td>
                                                    <td>
                                                        <span name="ArrivalNo">
                                                            <%#Eval("OC01008")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="ArrivalDate">
                                                            <%#Eval("OC01009") is DBNull ? "" : Convert.ToDateTime(Eval("OC01009")).ToString("yyyy-MM-dd")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="Delivered">
                                                            <%#Eval("OC01010").ToString().Trim() == "0" ? "N" : "Y"%></span>
                                                    </td>
                                                    <td>
                                                        <span name="DeliveredDate">
                                                            <%#Eval("OC01011") is DBNull ? "" : Convert.ToDateTime(Eval("OC01011")).ToString("yyyy-MM-dd")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="ExpressCompanyGoods">
                                                            <%#Eval("OC01012")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="Tracking">
                                                            <%#Eval("OC01013")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="TrackingRemark">
                                                            <%#Eval("OC01019")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="Invoiced">
                                                            <%#Eval("OC01014").ToString().Trim() == "0" ? "N" : "Y"%></span>
                                                    </td>
                                                    <td>
                                                        <span name="InvoiceDate">
                                                            <%#Eval("OC01015") is DBNull ? "" : Convert.ToDateTime(Eval("OC01015")).ToString("yyyy-MM-dd")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="ExpressCo">
                                                            <%#Eval("OC01018")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="TrackingInvoice">
                                                            <%#Eval("OC01017")%></span>
                                                    </td>
                                                    <td>
                                                        <span name="TrackingInvoiceRemark">
                                                            <%#Eval("OC01020")%></span>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <div class="tab-pane" id="fukuan">
                                <table width="100%%" class="table table-bordered table-striped table-hover with-check zhong nomargin">
                                    <thead>
                                        <tr>
                                            <th width="35%" scope="col">Customer Name
                                            </th>
                                            <th width="25%" scope="col">Order No
                                            </th>
                                            <th width="15%" scope="col">Order Date
                                            </th>
                                            <th width="25%" scope="col">Order Amount
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="TableBodyfukuan">
                                        <tr>
                                            <td>
                                                <center>
                                                    <asp:Label ID="labOP01002" runat="server" Text=""></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="labOP01003" runat="server" Text=""></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="labOP01004" runat="server" Text=""></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="labOP01005" runat="server" Text=""></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment1</center>
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01007" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>Date1
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01008" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment2</center>
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01009" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>Date2
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01010" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment3</center>
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01011" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>Date3
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01012" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment4</center>
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01013" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>Date4
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01014" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment5</center>
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01017" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>Date5
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01018" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment6</center>
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01019" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>Date6
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01020" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment7</center>
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01021" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>Date7
                                            </td>
                                            <td>
                                                <asp:Label ID="labOP01022" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                        <td colspan="4" style="text-align: right;">
                                            goods return：
                                            <asp:Label ID="labOP01006" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>--%>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">Outsanding：<asp:Label ID="labOP01015" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">Debts：<asp:Label ID="labOP01016" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
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
                        <h4 class="modal-title" id="myModalLabel">Select Share Name</h4>
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
    </form>
</body>
</html>
