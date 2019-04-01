<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="Sinoo.Spraying.Page.DataManagement.Setting" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑汇率</title>
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
    <script src="/Scripts/page/Page_GA0301Edit.js" type="text/javascript"></script>
</head>
<body class="ys">
    <form runat="server" id="form1">
        <div id="content-header">
            <h1>Exchange Rate
            </h1>
        </div>
        <div id="breadcrumb">
            <a href="#" title="" class="tip-bottom" data-original-title="Data Management"><i
                class="icon-home"></i>Data Management </a><a href="#" title="" class="tip-bottom"
                    data-original-title="Exchange Rate">Exchange Rate </a><a href="#" class="current">Edit Exchange Rate </a>
        </div>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="content-box">
                        <div class="content-h">
                            <div class="bh">
                                <i class="icon-plus-sign"></i><span>Edit</span>
                            </div>
                        </div>
                        <div class="xz">
                            <div class="control-group">
                                <label>
                                    <span>Exchange Rate：<font style="color: Red">*</font></span>
                                    <asp:TextBox ID="txtValue" data-rule="required;float4" runat="server"></asp:TextBox>
                                    <input type="hidden" name="SettingId" id="SettingId" runat="server" />
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="page-footer">
                    &nbsp;
                    <asp:Button ID="btnSave" CssClass="btn btn-primary"
                        runat="server" Text="保 存" OnClick="btnSave_Click" />
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
