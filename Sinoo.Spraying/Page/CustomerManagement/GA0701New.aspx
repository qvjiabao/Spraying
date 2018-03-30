<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GA0701New.aspx.cs" Inherits="Sinoo.Spraying.Page.CustomerManagement.GA0701New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add Customer File</title>
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
    <script type="text/javascript">
        function addFile() {
            var div = document.createElement("div");
            div.setAttribute("class", "control-group");
            var f = document.createElement("input");
            f.setAttribute("type", "file");
            f.setAttribute("name", "File");
            f.setAttribute("size", "50");
            div.appendChild(f);
            var d = document.createElement("input");
            d.setAttribute("type", "button");
            d.setAttribute("onclick", "deteFile(this)");
            d.setAttribute("value", "移除");
            d.setAttribute("class", "btn btn-primary");
            div.appendChild(d);
            document.getElementById("jiben").appendChild(div);
        }
        function deteFile(o) {
            while (o.tagName != "DIV") o = o.parentNode;
            o.parentNode.removeChild(o);
        }   
    </script>
</head>
<body class="ys">
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
    <div id="content-header">
        <h1>
            Add Customer File
        </h1>
    </div>
    <div id="breadcrumb">
        <a href="#" title="" class="tip-bottom" data-original-title="Customer Management"><i
            class="icon-home"></i>Customer Management</a><a href="#" class="tip-bottom" data-original-title="Customer File"
                class="current">Customer File</a><a href="#" class="current">Add Customer File</a>
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
                        <li class="active"><a href="#jiben" data-toggle="tab">Customer File</a></li>
                    </ul>
                    <div class="xz tab-content">
                        <div class="tab-pane active" id="jiben">
                            <div class="control-group" id="div2">
                                <label>
                                    <span>Customer Code：</span>
                                    <asp:Label ID="txtCA01002" runat="server"></asp:Label>
                                </label>
                            </div>
                            <div class="control-group" id="div3">
                                <label>
                                    <span>Customer Name：</span>
                                    <asp:Label ID="txtCA01003" runat="server"></asp:Label>
                                </label>
                            </div>
                            <div class="control-group" id="div1">
                                <label>
                                    <span>Customer File：</span>
                                    <input type="file" size="50" name="File" /><br />
                                </label>
                            </div>
                            <div>
                                <input type="button" value="添加文件" class="btn btn-primary" onclick="addFile()" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="page-footer">
                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="保 存" OnClick="btnSave_Click" />
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
    </form>
</body>
</html>
