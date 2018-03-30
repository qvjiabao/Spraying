<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sinoo.Spraying.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unicorn Admin</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="/css/unicorn.login.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head> 
<body>
    <form runat="server" id="form1">
    <div class="login">
        <div id="logo">
            <img src="img/login.png" width="395" height="69" alt="" />
        </div>
        <div id="loginbox">
            <p>
                请输入您的用户名和密码</p>
            <div class="control-group">
                <div class="controls">
                    <div class="input-prepend">
                        <span class="add-on"><i class="icon-user"></i></span>
                        <asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <div class="input-prepend">
                        <span class="add-on"><i class="icon-lock"></i></span>
                        <asp:TextBox ID="txtPassWord" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-actions">
                <span class="pull-right">
                    <asp:Button ID="btnLogin" CssClass="btn btn-inverse" runat="server" Text="登录" OnClick="btnLogin_Click" />
                </span>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="/Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="/Scripts/unicorn.login.js"></script>
    <script type="text/javascript">
                if ("<%=Request.QueryString["NotLogin"] %>"!="") {
                    alert('用户未登录或登录状态已失效,请重新登录！');
                    window.parent.location.href = '/Login.aspx';
                }
    </script>
</body>
</html>
