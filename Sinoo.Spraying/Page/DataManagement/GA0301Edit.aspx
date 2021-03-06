﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GA0301Edit.aspx.cs" Inherits="Sinoo.Spraying.Page.DataManagement.GA0301Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑省份资料</title>
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
        <h1>
            Edit Province Date
        </h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Data Management"><i
            class="icon-home"></i>Data Management </a><a href="#" title="" class="tip-bottom"
                data-original-title="Province Date">Province Date </a><a href="#" class="current">Edit
                    Province Date </a>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-plus-sign"></i><span>Edit</span></div>
                    </div>
                    <div class="xz">
                        <div class="control-group">
                            <label>
                                <span>Province Name：<font style="color: Red">*</font></span>
                                <asp:TextBox ID="txtGA03002" data-rule="required;length[1~20, true];" runat="server"></asp:TextBox>
                                <input type="hidden" name="Type" value="GA03EditProvince" />
                                <input type="hidden" name="ID" id="ID" runat="server" />
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Remark：</span>
                                <asp:TextBox ID="txtGA03004" data-rule="length[1~500, true]" runat="server" TextMode="MultiLine"></asp:TextBox>
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
                <asp:Button ID="btnReturn" CssClass="btn btn-primary" novalidate runat="server" Text="返 回"
                    OnClick="btnReturn_Click" />
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
    <script src="/Scripts/page/Page_GA0301Edit.js" type="text/javascript"></script>
    </form>
</body>
</html>
