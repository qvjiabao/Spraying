<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UA0101View.aspx.cs" Inherits="Sinoo.Spraying.Page.UserManagement.UA0101View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View User Data</title>
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
        <h1>View User Data</h1>
    </div>
    <div id="breadcrumb">
    <a href="#" title="" class="tip-bottom" data-original-title="User Management"><i class="icon-home">
        </i>User Management </a><a href="#" title="" class="tip-bottom" data-original-title="User Data">User Data </a>
        <a href="#" class="current">View User Data</a>
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
                                <span>用户姓名：</span>
                                <asp:Label ID="labUA01004" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>用户英文名：</span>
                                <asp:Label ID="labUA01005" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>用户代码：</span>
                                <asp:Label ID="labUA01006" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>身份证号：</span>
                                <asp:Label ID="labUA01007" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>性别：</span>
                                <asp:Label ID="labUA01008" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>职位：</span>
                                <asp:Label ID="labUA01009" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>手机号：</span>
                                <asp:Label ID="labUA01010" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>联系电话：</span>
                                <asp:Label ID="labUA01011" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>邮箱：</span>
                                <asp:Label ID="labUA01012" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>所属区域：</span>
                                <asp:Label ID="labUA01013" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>入职时间：</span>
                                <asp:Label ID="labUA01016" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>工作地点：</span>
                                <asp:Label ID="labUA01014" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>家庭住址：</span>
                                <asp:Label ID="labUA01015" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>生日：</span>
                                <asp:Label ID="labUA01017" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>用户角色：</span>
                                <asp:Label ID="labUA01024" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>配偶姓名：</span>
                                <asp:Label ID="labUA01018" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>配偶生日：</span>
                                <asp:Label ID="labUA01019" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>结婚纪念日：</span>
                                <asp:Label ID="labUA01020" runat="server" Text="Label"></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>子女姓名：</span>
                                <asp:Label ID="labUA01021" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>子女生日：</span>
                                <asp:Label ID="labUA01022" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>系统帐户：</span>
                                <asp:Label ID="labUA01002" runat="server" Text=""></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="page-footer">
                <asp:Button ID="btnEdit" CssClass="btn btn-primary" runat="server" Text="编 辑" 
                    OnClick="btnEdit_Click" />
                &nbsp;
                <asp:Button ID="btnReturn" CssClass="btn btn-primary" runat="server" Text=" 返 回"
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
