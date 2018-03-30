<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UA0101New.aspx.cs" Inherits="Sinoo.Spraying.Page.UserManagement.UA0101New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add User Data</title>
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
</head>
<body class="ys">
    <form id="form1" runat="server">
    <div id="content-header">
        <h1>
            Add User Data</h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="User Management"><i
            class="icon-home"></i>User Management </a><a href="#" title="" class="tip-bottom"
                data-original-title="User Data">User Data </a><a href="#" class="current">Add User Data</a>
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
                                <span>用户姓名：<font style="color: Red">*</font></span>
                                <asp:TextBox ID="txtUA01004" runat="server" data-rule="required;length[1~20, true]"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>用户英文名：</span>
                                <asp:TextBox ID="txtUA01005" runat="server" data-rule="length[1~100, true];letters"></asp:TextBox>
                            </label>
                        </div>
                        <div style="clear: both">
                        </div>
                        <div class="control-group">
                            <label>
                                <span>用户代码：</span>
                                <asp:TextBox ID="txtUA01006" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>身份证号：<font style="color: Red">*</font></span>
                                <input type="hidden" name="Type" value="UA01007New" />
                                <asp:TextBox ID="txtUA01007" runat="server" data-rule="required;ID_card"></asp:TextBox>
                            </label>
                        </div>
                        <div style="clear: both">
                        </div>
                        <div class="control-group">
                            <label>
                                <span>性别：<font style="color: Red">*</font></span>
                                <label class="radio inline">
                                    <input type="radio" name="rbtnUA01008" data-rule="checked" style="display: none;" />
                                    <asp:RadioButton GroupName="rbtnUA01008" ID="rbtnUA01008Male" runat="server" />男
                                </label>
                                <label class="radio inline">
                                    <asp:RadioButton GroupName="rbtnUA01008" ID="rbtnUA01008Famale" runat="server" />女
                                </label>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>职位：</span>
                                <asp:DropDownList ID="ddlUA01009" runat="server">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>手机号：</span>
                                <asp:TextBox ID="txtUA01010" runat="server" data-rule="mobile"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>联系电话：</span>
                                <asp:TextBox ID="txtUA01011" runat="server" data-rule="tel"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>邮箱：</span>
                                <asp:TextBox ID="txtUA01012" runat="server" data-rule="email"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>所属区域：<font style="color: Red">*</font></span>
                                <asp:DropDownList ID="ddlUA01013" runat="server" data-rule="required" data-msg-required="请选择所属区域">
                                    <asp:ListItem Value="">请选择</asp:ListItem>
                                    <asp:ListItem Value="全区域">AllAera</asp:ListItem>
                                    <asp:ListItem Value="北京">Beijing</asp:ListItem>
                                    <asp:ListItem Value="天津">TianJin</asp:ListItem>
                                    <asp:ListItem Value="西安">XiAn</asp:ListItem>
                                    <asp:ListItem Value="沈阳">ShenYang</asp:ListItem>
                                    <asp:ListItem Value="钢铁">Steelteam</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>入职时间：</span>
                                <asp:TextBox ID="txtUA01016" onClick="WdatePicker({minDate:'#F{$dp.$D(\'txtUA01017\',{d:1})}'})"
                                    CssClass="Wdate" runat="server" data-rule="date"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>工作地点：</span>
                                <asp:TextBox ID="txtUA01014" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>家庭住址：</span>
                                <asp:TextBox ID="txtUA01015" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>生日：</span>
                                <asp:TextBox ID="txtUA01017" Style="width: 93px;" onClick="WdatePicker({maxDate:'#F{$dp.$D(\'txtUA01016\',{d:-1})}'})"
                                    CssClass="Wdate" runat="server" data-rule="date"></asp:TextBox>&nbsp;-&nbsp;
                                <asp:DropDownList ID="ddlUA01025" runat="server" Style="width: 93px;">
                                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                                    <asp:ListItem Value="阳历" Text="阳历"></asp:ListItem>
                                    <asp:ListItem Value="阴历" Text="阴历"></asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>用户角色：<font style="color: Red">*</font></span>
                                <asp:DropDownList ID="ddlUA01024" runat="server" data-rule="checked;required" data-msg-required="请选择用户角色">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>配偶姓名：</span>
                                <asp:TextBox ID="txtUA01018" runat="server" data-rule="length[1~20, true]"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>配偶生日：</span>
                                <asp:TextBox ID="txtUA01019" onClick="WdatePicker({maxDate:'%y-%M-%d'})" CssClass="Wdate"
                                    runat="server" data-rule="date"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>结婚纪念日：</span>
                                <asp:TextBox ID="txtUA01020" onClick="WdatePicker({maxDate:'%y-%M-%d'})" CssClass="Wdate"
                                    runat="server" data-rule="date"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>子女姓名：</span>
                                <asp:TextBox ID="txtUA01021" runat="server" data-rule="length[1~20, true]"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>子女生日：</span>
                                <asp:TextBox ID="txtUA01022" onClick="WdatePicker({maxDate:'%y-%M-%d'})" CssClass="Wdate"
                                    runat="server" data-rule="date"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>系统帐户：<font style="color: Red">*</font></span>
                                <input type="hidden" name="Type1" value="UA01002New" />
                                <asp:TextBox ID="txtUA01002" runat="server" data-rule="required;length[1~50, true]"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>系统密码：<font style="color: Red">*</font></span>
                                <asp:TextBox ID="txtUA01003" runat="server" TextMode="Password" data-rule="系统密码:required;length[1~20, true]"></asp:TextBox>
                            </label>
                        </div>
                        <div class="control-group">
                            <label>
                                <span>确认系统密码：<font style="color: Red">*</font></span>
                                <asp:TextBox ID="txtUA01003Again" runat="server" TextMode="Password" data-rule="确认系统密码:match(txtUA01003);required"></asp:TextBox>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="page-footer">
                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="保 存" OnClientClick="return btnClientSave()"
                    OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnReturn" CssClass="btn btn-primary" runat="server" Text="返 回" novalidate
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
    <script src="/Scripts/page/Page_UA0101New.js" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    </form>
</body>
</html>
