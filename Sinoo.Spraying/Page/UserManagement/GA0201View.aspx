<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GA0201View.aspx.cs" Inherits="Sinoo.Spraying.Page.UserManagement.GA0201View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Role Data</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="/css/content-style.css" />
    <link rel="stylesheet" href="/css/fullcalendar.css" />
    <link rel="stylesheet" href="/css/unicorn.main.css" />
    <link rel="stylesheet" href="/css/unicorn.grey.css" class="skin-color" />
    <link href="/Scripts/validator/jquery.validator.css" rel="stylesheet" type="text/css" />
    <link href="/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            View Role Data</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="User Management"><i
            class="icon-home"></i>User Management </a><a href="#" title="" class="tip-bottom"
                data-original-title="Role Data">Role Data </a><a href="#" class="current">View Role
                    Data</a>
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
                                <span>RoleName：</span>
                                <asp:Label ID="labGA02002" runat="server" Text=""></asp:Label></label>
                            <input type="hidden" runat="server" id="txtGA02001" name="txtGA02001" />
                        </div>
                        <div class="control-group">
                            <label>
                                <span>Remark： </span>
                                <asp:Label ID="labGA02003" runat="server" Text=""></asp:Label></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span6">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-th-list"></i><span>Menu</span></div>
                    </div>
                    <div>
                        <ul id="treeDemo" class="ztree">
                        </ul>
                    </div>
                </div>
            </div>
            <div class="span6">
                <div class="content-box">
                    <div class="content-h">
                        <div class="bh">
                            <i class="icon-th-large"></i><span>Power</span></div>
                    </div>
                    <div class="content-bd">
                        <ul id="treeDemo1" class="ztree">
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="page-footer">
                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="编 辑" OnClick="btnUpdate_Click" />
                &nbsp;
                <asp:Button ID="btnReturn" CssClass="btn btn-primary" runat="server" Text="返 回" OnClick="btnReturn_Click"
                    novalidate />
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
    <script src="/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/base/base.getdata.js" type="text/javascript"></script>
    <script src="/Scripts/page/Page_GA0201View.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery.ztree.core-3.5.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.ztree.excheck-3.5.js"></script>
    </form>
</body>
</html>
