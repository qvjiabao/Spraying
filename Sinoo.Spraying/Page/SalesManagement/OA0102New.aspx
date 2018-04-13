<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OA0102New.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.OA0102New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add Goods Return</title>
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
            Add Goods Return</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Sales Management"><i
            class="icon-home"></i>Sales Management</a><a href="#" title="" class="tip-bottom"
                data-original-title="Goods Return">Goods Return</a><a href="#" class="current">Add Goods
                    Return</a>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-plus-sign"></i><span>Add</span></div>
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
                                    <span>Return Order No：<font style="color: Red">*</font></span>
                                    <input type="hidden" name="Type" value="OA0102New01" />
                                    <asp:TextBox ID="txtOA01002" data-rule="required;length[1~20, true];" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Order No：<font style="color: Red">*</font></span>
                                    <input type="hidden" name="Type1" value="OA0102New02" />
                                    <asp:TextBox ID="txtOA01039" data-rule="required;length[1~20, true];" onblur="ShowSalesName()"
                                        runat="server"></asp:TextBox>
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
                                    <span>Order Date：<font style="color: Red">*</font></span>
                                    <asp:TextBox ID="txtOA01009" runat="server" onClick="WdatePicker()" CssClass="Wdate"
                                        data-rule="required;date"></asp:TextBox>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Sales Name：</span>
                                    <asp:Label ID="labOA01013" runat="server" Text=""></asp:Label>
                                    <input type="hidden" name="txtOA01013" runat="server" id="txtOA01013" />
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>AutoJet Order：</span>
                                    <label class="radio inline">
                                        <asp:RadioButton GroupName="OA01005" ID="rbtnOA01005Yew" runat="server" />是
                                    </label>
                                    <label class="radio inline">
                                        <asp:RadioButton GroupName="OA01005" ID="rbtnOA01005No" runat="server" />否
                                    </label>
                                </label>
                            </div>
                            <div class="control-group">
                                <label onclick="return false;">
                                    <span>Share Name1：</span>
                                    <asp:TextBox ID="txtOA01015" placeholder="点击选择" ReadOnly="true" runat="server" Style="width: 94px;"></asp:TextBox>
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
                                        <asp:TextBox ID="txtOA01017" placeholder="点击选择" ReadOnly="true" runat="server" Style="width: 94px;"></asp:TextBox>
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
                                    <span>Amount(RMB)：</span>
                                    <input id="txtOA01020" placeholder="自动计算" readonly="readonly" name="txtOA01020" runat="server"
                                        type="text" />
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Exchange Rate：</span>
                                    <input type="text" id="txtOA01021" runat="server" name="txtOA01021" data-rule="float4"
                                        onblur="USblur()" />
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Amount(US$)：</span>
                                    <input id="txtOA01022" placeholder="自动计算" readonly="readonly" name="txtOA01022" runat="server"
                                        type="text" />
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>AC：<font style="color: Red">*</font></span>
                                    <asp:DropDownList ID="ddlOA01025" runat="server" data-rule="required;">
                                    </asp:DropDownList>
                                </label>
                            </div>
                            <div class="control-group">
                                <label>
                                    <span>Remark：</span>
                                    <asp:TextBox ID="txtOA01027" data-rule="length[1~500, true]" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </label>
                            </div>
                        </div>
                        <div class="tab-pane" id="mingxi">
                            <input type="button" class="btn btn-primary" onclick="AddProductInput()" value="新 增" />
                            <input type="hidden" name="txtmingxi" id="txtmingxi" runat="server" />
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
                                        <th width="9%" scope="col">
                                            Operate
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="TableBodymingxi">
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="fapiao">
                            <input type="hidden" id="txtfapiao" name="txtfapiao" runat="server" />
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
                                            Invoice(Y/N)
                                        </th>
                                        <th width="12.5%" scope="col">
                                            Invoice Date
                                        </th>
                                        <th width="12.5%" scope="col">
                                            Operate
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="TableBodyfapiao">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="page-footer">
                <asp:Button ID="btnSave" OnClientClick="return btnClientSave()" CssClass="btn btn-primary"
                    runat="server" Text="保 存" OnClick="btnSave_Click" />
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
                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="$('#'+$('#Cid',window.frames['myIframe'].document).val()).val($('#ddlUA01',window.frames['myIframe'].document).val()).focus().blur()">
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
    <script src="/Scripts/page/Page_OA0102New.js?v=1.0" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    </form>
</body>
</html>
