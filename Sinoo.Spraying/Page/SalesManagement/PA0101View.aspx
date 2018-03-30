<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PA0101View.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.PA0101View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Product Data</title>
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
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            View Product Data
        </h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Sales Management"><i
            class="icon-home"></i>Sales Management </a><a href="#" title="" class="tip-bottom"
                data-original-title="Product Data"><i class="icon-home"></i>Product Data
        </a><a href="#" class="current">View Product Data</a>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-search"></i><span>View</span></div>
                    </div>
                    <div class="xz">
                        <div class="control-group">
                            <label>
                                <span>Part No：</span>
                                <asp:Label ID="labPA01003" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Description：</span>
                                <asp:Label ID="labPA01005" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Guide Price One(1-35)：</span>
                                <asp:Label ID="labPriceOne" runat="server" Text="">
                                </asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Guide Price Two(36-99)：</span>
                                <asp:Label ID="labPriceTwo" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Guide Price Three(100-499)：</span>
                                <asp:Label ID="labPriceThree" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Guide Price Four(500+)：</span>
                                <asp:Label ID="labPriceFour" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Net Price：</span>
                                <asp:Label ID="labNetPrice" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="page-footer">
                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="编 辑" OnClick="btnUpdate_Click" />
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
    </form>
</body>
</html>
