<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PA0101New.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.PA0101New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add Product Data</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="/css/content-style.css" />
    <link rel="stylesheet" href="/css/fullcalendar.css" />
    <link rel="stylesheet" href="/css/unicorn.main.css" />
    <link rel="stylesheet" href="/css/unicorn.grey.css" class="skin-color" />
    <link href="/Scripts/validator/jquery.validator.css" rel="stylesheet" type="text/css" />
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            Add Product Data</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Sales Management"><i
            class="icon-home"></i>Sales Management </a><a href="#" title="" class="tip-bottom"
                data-original-title="Product Data"><i class="icon-home"></i>Product Data
        </a><a href="#" class="current">Add Product Data</a>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-plus-sign"></i><span>Add</span></div>
                    </div>
                    <div class="xz">
                        <div class="control-group">
                            <label>
                                <span>Part No：<font style="color: Red">*</font></span>
                                <asp:TextBox ID="txtPA01003" data-rule="required;length[1~200, true];" runat="server"></asp:TextBox>
                                <input type="hidden" name="Type" value="PA01New" />
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Guide Price One(1-35)：</span>
                                <asp:TextBox ID="txtPriceOne" data-rule="moneys" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Guide Price Two(36-99)：</span>
                                <asp:TextBox ID="txtPriceTwo" data-rule="moneys" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Guide Price Three(100-499)：</span>
                                <asp:TextBox ID="txtPriceThree" data-rule="moneys" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Guide Price Four(500+)：</span>
                                <asp:TextBox ID="txtPriceFour" data-rule="moneys" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Net Price：</span>
                                <asp:TextBox ID="txtNetPrice" data-rule="moneys" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Description：</span>
                                <asp:TextBox ID="txtPA01005" data-rule="length[1~500, true]" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="page-footer">
                <asp:Button ID="btnSave" CssClass="btn btn-primary" OnClientClick="return DataSave()"
                    runat="server" Text="保 存" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnReturn" novalidate CssClass="btn btn-primary" runat="server" Text="返 回"
                    OnClick="btnReturn_Click" />
            </div>
        </div>
        <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
        <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
        <script src="/Scripts/script.js" type="text/javascript"></script>
        <script src="/Scripts/bootstrap.min.js" type="text/javascript"></script>
        <script src="/Scripts/unicorn.js" type="text/javascript"></script>
        <script src="/Scripts/validator/jquery.validator.js" type="text/javascript"></script>
        <script src="/Scripts/validator/local/zh_CN.js" type="text/javascript"></script>
        <script src="/Scripts/page/Page_PA0101New.js" type="text/javascript"></script>
    </form>
</body>
</html>
