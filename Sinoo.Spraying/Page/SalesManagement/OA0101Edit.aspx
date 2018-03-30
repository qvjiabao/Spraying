<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OA0101Edit.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.OA0101Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Edit Orders Data</title>
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
            <h1>Edit Orders Data</h1>
        </div>
        <div id="breadcrumb">
            <a href="#" title="" class="tip-bottom" data-original-title="Sales Management"><i
                class="icon-home"></i>Sales Management</a><a href="#" title="" class="tip-bottom"
                    data-original-title="Orders Date">Orders Date</a> <a href="#" class="current">Edit Orders
                    Date</a>
        </div>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="content-box">
                        <div class="content-h">
                            <div class="bh">
                                <i class="icon-pencil"></i><span>Edit</span>
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
                                        <span>Customer Name：</span>
                                        <asp:Label ID="labCA01003" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                                <div class="clear" style="clear: both">
                                </div>
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
                                        <span>Order No：<font style="color: Red">*</font></span>
                                        <input type="hidden" name="Type" value="OA0101Edit" />
                                        <input type="hidden" name="ID" id="ID" runat="server" />
                                        <asp:TextBox ID="txtOA01002" runat="server" data-rule="required;length[1~20, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Invoice No：</span>
                                        <asp:TextBox ID="txtOA01008" runat="server" data-rule="length[1~20, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Agreement No：<font style="color: Red">*</font></span>
                                        <asp:TextBox ID="txtOA01053" runat="server" data-rule="required"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Order Date：<font style="color: Red">*</font></span>
                                        <asp:TextBox ID="txtOA01009" runat="server" onClick="WdatePicker({maxDate:'#F{$dp.$D(\'txtOA01010\',{d:-1})}'})"
                                            CssClass="Wdate" data-rule="required;date"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Customer Request Delivery Date：<font style="color: Red">*</font></span>
                                        <asp:TextBox ID="txtOA01010" runat="server" onClick="WdatePicker({minDate:'#F{$dp.$D(\'txtOA01009\',{d:1})}'})"
                                            CssClass="Wdate" data-rule="required;date"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Promised Delivery Days：<font style="color: Red">*</font></span>
                                        <asp:TextBox ID="txtOA01011" runat="server" onClick="WdatePicker({minDate:'#F{$dp.$D(\'txtOA01009\',{d:1})}'})"
                                            CssClass="Wdate" data-rule="required;date"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Promised Payment Days：</span>
                                        <asp:TextBox ID="txtOA01051" runat="server" onClick="WdatePicker()" CssClass="Wdate"
                                            data-rule="date"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Qutiation Ref No：</span>
                                        <asp:TextBox ID="txtOA01012" runat="server" data-rule="length[1~20, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Sales Name：<font style="color: Red">*</font></span>
                                        <asp:DropDownList ID="ddlUA01" runat="server" data-rule="required;">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Special Instruction：</span>
                                        <asp:TextBox ID="txtOA01014" runat="server" data-rule="length[1~500, true]" TextMode="MultiLine"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>AutoJet Order：<font style="color: Red">*</font></span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01005" ID="rbtnOA01005Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01005" ID="rbtnOA01005No" runat="server" />否
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label onclick="return false;">
                                        <span>Share Name1：</span>
                                        <asp:TextBox ID="txtOA01015" runat="server" Style="width: 94px;" ReadOnly="true"
                                            placeholder="点击选择"></asp:TextBox>
                                        &nbsp;
                                    <asp:TextBox ID="txtOA01049" runat="server" Style="width: 94px;" placeholder="其它人"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>share%1：</span>
                                        <asp:DropDownList ID="ddlOA01016" runat="server" disabled>
                                            <asp:ListItem Value="">请选择</asp:ListItem>
                                            <asp:ListItem Value="0.1">10%</asp:ListItem>
                                            <asp:ListItem Value="0.2">20%</asp:ListItem>
                                            <asp:ListItem Value="0.3">30%</asp:ListItem>
                                            <asp:ListItem Value="0.4">40%</asp:ListItem>
                                            <asp:ListItem Value="0.5">50%</asp:ListItem>
                                            <asp:ListItem Value="0.6">60%</asp:ListItem>
                                            <asp:ListItem Value="0.7">70%</asp:ListItem>
                                            <asp:ListItem Value="0.8">80%</asp:ListItem>
                                            <asp:ListItem Value="0.9">90%</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <label onclick="return false;">
                                            <span>Share Name2：</span>
                                            <asp:TextBox ID="txtOA01017" runat="server" Style="width: 94px;" ReadOnly="true"
                                                placeholder="点击选择"></asp:TextBox>
                                            &nbsp;
                                        <asp:TextBox ID="txtOA01050" runat="server" Style="width: 94px;" placeholder="其它人"></asp:TextBox>
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>share%2：</span>
                                        <asp:DropDownList ID="ddlOA01018" runat="server" disabled>
                                            <asp:ListItem Value="">请选择</asp:ListItem>
                                            <asp:ListItem Value="0.1">10%</asp:ListItem>
                                            <asp:ListItem Value="0.2">20%</asp:ListItem>
                                            <asp:ListItem Value="0.3">30%</asp:ListItem>
                                            <asp:ListItem Value="0.4">40%</asp:ListItem>
                                            <asp:ListItem Value="0.5">50%</asp:ListItem>
                                            <asp:ListItem Value="0.6">60%</asp:ListItem>
                                            <asp:ListItem Value="0.7">70%</asp:ListItem>
                                            <asp:ListItem Value="0.8">80%</asp:ListItem>
                                            <asp:ListItem Value="0.9">90%</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Cost(US$)：</span>
                                        <asp:TextBox ID="txtOA01019" runat="server" Text="" ReadOnly="true" placeholder="自动计算"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Amount(RMB)：</span>
                                        <asp:TextBox ID="txtOA01020" runat="server" Text="" ReadOnly="true" placeholder="自动计算"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Exchange Rate：<font style="color: Red">*</font></span>
                                        <asp:TextBox ID="txtOA01021" runat="server" data-rule="required;float4" onblur="USblur()"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Amount(US$)：</span>
                                        <asp:TextBox ID="txtOA01022" runat="server" Text="" ReadOnly="true" placeholder="自动计算"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>GP%：</span>
                                        <asp:TextBox ID="txtOA01023" runat="server" Text="" ReadOnly="true" placeholder="自动计算"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>New Application：</span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01024" ID="rbtnOA01024Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01024" ID="rbtnOA01024No" runat="server" onclick="$('#txtOA01024').val('')" />否
                                        </label>
                                        <label class="radio inline" style="margin-left: -15px;" onclick="return false;">
                                            <asp:TextBox ID="txtOA01024" MaxLength="100" runat="server" Style="width: 120px;"
                                                data-rule="New Application:length[1~100, true]" onblur="changeInputCheck(this,'rbtnOA01024Yes')"></asp:TextBox>
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Application Share：</span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01048" ID="rbtnOA01048Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01048" ID="rbtnOA01048No" runat="server" onclick="$('#txtOA01048').val('')" />否
                                        </label>
                                        <label class="radio inline" style="margin-left: -15px;" onclick="return false;">
                                            <asp:TextBox ID="txtOA01048" MaxLength="100" runat="server" Style="width: 120px;"
                                                data-rule="length[1~100, true]" onblur="changeInputCheck(this,'rbtnOA01048Yes')"></asp:TextBox>
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>AC：<font style="color: Red">*</font></span>
                                        <asp:DropDownList ID="ddlOA01025" runat="server" data-rule="required">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Payment Terms：</span>
                                        <asp:TextBox ID="txtOA01026" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>goods return：</span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01003" ID="rbtnOA01003Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01003" ID="rbtnOA01003No" runat="server" />否
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>New Cumstomer：<font style="color: Red">*</font></span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01044" ID="rbtnOA01044Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01044" ID="rbtnOA01044No" runat="server" />否
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Minifogger：</span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01045" ID="rbtnOA01045Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnOA01045" ID="rbtnOA01045No" runat="server" />否
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>End User：</span>
                                        <asp:TextBox ID="txtOA01046" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Application Description：</span>
                                        <asp:DropDownList ID="ddlOA01047" runat="server" data-rule="">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Remark：</span>
                                        <asp:TextBox ID="txtOA01027" TextMode="MultiLine" runat="server" data-rule="length[1~500, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Delivery Add(Goods)：</span>
                                        <asp:TextBox ID="txtOA01028" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Consignee(Goods)：</span>
                                        <asp:TextBox ID="txtOA01029" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Tel/MP(Goods)：</span>
                                        <asp:TextBox ID="txtOA01030" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Post code(Goods)：</span>
                                        <asp:TextBox ID="txtOA01031" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Delivery Add (lnvoice)：</span>
                                        <asp:TextBox ID="txtOA01032" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Consignee(lnvoice)：</span>
                                        <asp:TextBox ID="txtOA01033" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Tel/ MP(lnvoice)：</span>
                                        <asp:TextBox ID="txtOA01034" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Post code(lnvoice)：</span>
                                        <asp:TextBox ID="txtOA01035" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Dormant Customers：<font style="color: Red">*</font></span>
                                        <asp:DropDownList ID="ddlOA01054" runat="server" data-rule="Dormant Customers:required">
                                            <asp:ListItem Value="">请选择</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </div>
                            </div>
                            <div class="tab-pane" id="mingxi">
                                <input type="hidden" name="txtmingxi" id="txtmingxi" runat="server" />
                                <input type="button" class="btn btn-primary" onclick="AddProductInput()" value="新 增" />
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
                                            <th width="9%" scope="col">Operate
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
                                                    <td>
                                                        <a href="javascript:" name="btnSave" onclick="EditProduct(this)">修改</a> <a href="javascript:"
                                                            name="btnDelete" onclick="RemoveProduct(this)">删除</a>
                                                    </td>
                                                    <td style="display: none;">
                                                        <span name="PartID">
                                                            <%#Eval("OB01004")%></span>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <div class="tab-pane" id="fapiao">
                                <input type="hidden" id="txtfapiao" name="txtfapiao" runat="server" />
                                <input type="button" class="btn btn-primary" onclick="Repeat()" value="重复数据" />&nbsp;
                            <input type="button" class="btn btn-primary" onclick="RepeatTwo()" value="重复状态" />
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
                                        <th width="6.66%" scope="col">TrackingRemark
                                        </th>
                                        <th width="6.66%" scope="col">Invoiced
                                        </th>
                                        <th width="6.66%" scope="col">Invoice Date
                                        </th>
                                        <th width="6.66%" scope="col">ExpressCo invoice
                                        </th>
                                        <th width="6.66%" scope="col">Tracking Invoice
                                        </th>
                                        <th width="6.66%" scope="col">Tracking Invoice Remark
                                        </th>
                                        <th width="6.66%" scope="col">Operate
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
                                                            <%#Eval("OC01007").ToString().Trim() == "0" ? "N" : "Y"%></span></span>
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
                                                            <%#Eval("OC01011") is DBNull ? "":Convert.ToDateTime(Eval("OC01011")).ToString("yyyy-MM-dd")%></span>
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
                                                            <%#Eval("OC01015") is DBNull ? "":Convert.ToDateTime(Eval("OC01015")).ToString("yyyy-MM-dd")%></span>
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
                                                    <td>
                                                        <a href="javascript:" name="btnSave" onclick="EditInvoice(this)">修改</a>
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
                                                    <span id="labOP01002" runat="server"></span>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <span id="labOP01003" runat="server"></span>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <span id="labOP01004" runat="server"></span>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <span id="labOP01005" runat="server"></span>
                                                </center>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment1</center>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01007" runat="server" onblur="checkNum(this)"></asp:TextBox>
                                            </td>
                                            <td>Date1
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01008" runat="server" CssClass="Wdate" onclick="WdatePicker()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment2</center>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01009" runat="server" onblur="checkNum(this);"></asp:TextBox>
                                            </td>
                                            <td>Date2
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01010" runat="server" CssClass="Wdate" onclick="WdatePicker()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment3</center>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01011" runat="server" onblur="checkNum(this)"></asp:TextBox>
                                            </td>
                                            <td>Date3
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01012" runat="server" CssClass="Wdate" onclick="WdatePicker()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment4</center>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01013" runat="server" onblur="checkNum(this)"></asp:TextBox>
                                            </td>
                                            <td>Date4
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01014" runat="server" CssClass="Wdate" onclick="WdatePicker()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment5</center>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01017" runat="server" onblur="checkNum(this)"></asp:TextBox>
                                            </td>
                                            <td>Date5
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01018" runat="server" CssClass="Wdate" onclick="WdatePicker()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment6</center>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01019" runat="server" onblur="checkNum(this)"></asp:TextBox>
                                            </td>
                                            <td>Date6
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01020" runat="server" CssClass="Wdate" onclick="WdatePicker()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <center>
                                                    Payment7</center>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01021" runat="server" onblur="checkNum(this)"></asp:TextBox>
                                            </td>
                                            <td>Date7
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOP01022" runat="server" CssClass="Wdate" onclick="WdatePicker()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                        <td colspan="4" style="text-align: right;">
                                            goods return：
                                            <asp:DropDownList ID="ddlOP01006" runat="server">
                                                <asp:ListItem Value="1">是</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>--%>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">
                                                <input type="hidden" id="txtOP01015" runat="server" />
                                                Outsanding：<span id="labOP01015" runat="server">0</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">
                                                <input type="hidden" id="txtOP01016" runat="server" />
                                                Debts：<span id="labOP01016" runat="server">0</span>
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
                    <div>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="保 存" OnClientClick="return btnClientSave()"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnReturn" CssClass="btn btn-primary" runat="server" Text="返 回" novalidate
                            OnClick="btnReturn_Click" />
                    </div>
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
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="$('#'+$('#Cid',window.frames['myIframe'].document).val()).val($('#ddlUA01',window.frames['myIframe'].document).val()).focus().blur();">
                            确定</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            关闭</button>
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
        <script src="/Scripts/page/Page_OA0101Edit.js" type="text/javascript"></script>
        <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    </form>
</body>
</html>
