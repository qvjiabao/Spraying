<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Sinoo.Spraying.Index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Spraying Systems Co</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="css/jquery-ui.css" />
    <link rel="stylesheet" href="css/content-style.css" />
    <link rel="stylesheet" href="css/fullcalendar.css" />
    <link rel="stylesheet" href="css/unicorn.main.css" />
    <link rel="stylesheet" href="css/unicorn.grey.css" class="skin-color" />
    <link href="/Scripts/layer/theme/default/layer.css" rel="stylesheet" />
    <link href="css/jquery.mCustomScrollbar.css" rel="stylesheet" />
    <link href="css/unipop.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/validator/jquery.validator.css" rel="stylesheet" type="text/css" />
    <!-------弹出--------->
    <link rel="stylesheet" href="css/dialog_style.css" />
    <style type="text/css">
        .concat
        {
            display: block;
            float: left;
            position: relative;
            width: 220px;
            z-index: 16;
            height: 100%;
            overflow: auto;
        }
        
        .s .concat
        {
            width: 43px;
            -webkit-transition: all 1s ease;
            transition: all 0.7s ease;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div id="header">
        <h1>
            <a href="/Index.aspx">Unicorn Admin</a></h1>
    </div>
    <div id="user-nav" class="navbar navbar-inverse">
        <ul class="nav btn-group">
            <li class="btn btn-inverse"><a title="" href="Index.aspx" id="A1"><i class="icon icon-share-alt">
            </i><span class="text">首页</span></a></li>
            <li class="btn btn-inverse"><a title="" href="#"><i class="icon icon-user"></i>欢迎您：<span
                class="text"><%=(Session["USER_SESSION"] as Sinoo.Model.UserBase).UA01004 %></span></a></li>
            <li class="btn btn-inverse"><a href="#myModal" role="button" data-toggle="modal"
                onclick="Clear()"><i class="icon icon-share-alt"></i><span class="text">修改密码</span></a></li>
            <li class="btn btn-inverse"><a title="" href="#" id="runDemo1"><i class="icon icon-share-alt">
            </i><span class="text">退出</span></a></li>
        </ul>
    </div>
    <div class="concat">
        <div id="sidebar">
            <a href="#" class="visible-phone"><i class="icon icon-home"></i>Dashboard</a>
            <ul class="">
                <%=CreateMenu()%>
                <li class="submenu">
                    <div class="side_control">
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <!--------------------------换皮肤------------------------------------------------>
    <div id="style-switcher">
        <i class="icon-arrow-left icon-white"></i><span>Style:</span> <a href="#grey" style="background-color: #555555;
            border-color: #aaaaaa;"></a><a href="#blue" style="background-color: #2D2F57;">
        </a><a href="#red" style="background-color: #673232;"></a>
    </div>
    <!-------------------------------------------------------------------------->
    <div id="content">
        <iframe width="100%" id="iframe" allowtransparency="true" class="" name="iframe"
            height="100%" src="Welcome.htm" frameborder="0">需要浏览器iframe支持</iframe>
    </div>
    <%--新增弹出层--%>
    <div aria-hidden="false" aria-labelledby="myModalLabel" role="dialog" tabindex="-1"
        class="modal hide fade" id="myModal" style="height: auto; top: 100px; min-height: 250px">
        <div class="modal-header">
            <button aria-hidden="true" data-dismiss="modal" class="close" type="button">
                ×</button>
            <h3 id="myModalLabel">
                修改密码</h3>
        </div>
        <div class="modal-body" style="height: auto;">
            <table width="100%" border="1" frame="below" rules="rows" bordercolor="#e9e9e9">
                <tr>
                    <td width="20%">
                        <span>旧密码：</span>
                    </td>
                    <td width="80%">
                        <asp:TextBox ID="txtOldUA01003" runat="server" TextMode="Password" data-rule="required;length[1~20, true];remote[/Handler/UserChengPwdHandler.ashx, txtOldUA01003, Type ]"
                            placeholder="旧密码"></asp:TextBox>
                        <input type="hidden" name="Type" value="CheckOldPwd" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>新密码：</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewUA01003" runat="server" placeholder="新密码" data-rule="新密码:required;length[1~20, true]"
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>确认密码：</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTwoUA01003" runat="server" placeholder="确认密码" data-rule="确认密码:required;match(txtNewUA01003)"
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="imgbox_footer">
                <asp:Button class="btn btn-primary" Text="确 定" ID="btnSave" runat="server" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
    </form>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <!--<script src="Scripts/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>-->
    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="Scripts/script.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <!--<script src="js/fullcalendar.min.js"></script> 
        <script src="js/unicorn.dashboard.js"></script>-->
    <!--<script src="js/select2.min.js"></script> -->
    <!--<script src="js/jquery.dataTables.min.js"></script>-->
    <script src="Scripts/unicorn.js" type="text/javascript"></script>
    <!--<script src="js/unicorn.tables.js"></script>-->
    <!---------弹出层---------->
    <script type="text/javascript" src="Scripts/easy.js" data-main="global"></script>
    <script type="text/javascript" src="Scripts/shbrushjscript.js"></script>
    <script src="/Scripts/easydialog.js" type="text/javascript"></script>
    <script src="/Scripts/validator/jquery.validator.js" type="text/javascript"></script>
    <script src="/Scripts/validator/local/zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script type="text/javascript">
        function Clear() {
            $("input[type='password']").val("");
            $("#Form1").validator("hideMsg", "#txtOldUA01003");
            $("#Form1").validator("hideMsg", "#txtNewUA01003");
            $("#Form1").validator("hideMsg", "#txtTwoUA01003");
        }
        $(document).ready(function (e) {

            var h = $(window).height();

            $(".concat").height(h - 93);
            //$(".concat").mCustomScrollbar({
            //    scrollInertia: 150
            //});

            layer.open({
                title: 'Debts & Pending',
                type: 2,
                area: ['800px', '450px'],
                skin: 'layui-layer-rim', //加上边框
                content: ['TipList.aspx']
            });
        });
        function Save() {
            $.post("/Handler/UserChengPwdHandler.ashx", { newPwd: $("#txtNewUA01003").val(), Type: "UpdatePwd" }, function (result) {
                if (result == "true") {
                    alert('修改成功！');
                    location.href = 'Login.aspx';
                }
                else {
                    alert('修改失败！');
                }
            })
        }
    </script>
    <script type="text/javascript">
        E.config({
            baseUrl: 'http://easyjs.org/assets/components/dialog/js/'
        });

        E.use('dialog', function () {
            new E.ui.Dialog('#runDemo1', {
                title: 'Spraying Systems Co',
                content: '是否退出？',
                effects: 'fade',
                yesFn: function () {
                    window.parent.location.href = "/Login.aspx";
                    return false;
                },
                noFn: function () {

                }
            });

        });
    </script>
</body>
</html>
