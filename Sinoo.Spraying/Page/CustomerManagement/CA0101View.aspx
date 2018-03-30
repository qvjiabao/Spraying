<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CA0101View.aspx.cs" Inherits="Sinoo.Spraying.Page.CustomerManagement.CA0101View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Customer</title>
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
            <h1>View Customer</h1>
        </div>
        <div id="breadcrumb">
            <a href="#" title="" class="tip-bottom" data-original-title="Customer Management"><i
                class="icon-home"></i>Customer Management</a><a href="#" title="" class="tip-bottom"
                    data-original-title="Customer Data">Customer Data</a> <a href="#" class="current">View
                    Custome</a>
        </div>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="content-box">
                        <div class="content-h">
                            <div class="bh">
                                <i class="icon-search"></i><span>View</span>
                            </div>
                        </div>
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#jiben" data-toggle="tab">Essential Information</a></li>
                            <li><a href="#xiangxi" data-toggle="tab">Detailed Information</a></li>
                        </ul>
                        <div class="xz tab-content">
                            <div class="tab-pane active" id="jiben">
                                <div class="control-group">
                                    <label>
                                        <span>Customer Code：</span>
                                        <asp:Label ID="labCA01002" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Customer Name：</span>
                                        <asp:Label ID="labCA01003" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Customer Ename：</span>
                                        <asp:Label ID="labCA01004" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Billing Add/Tel：</span>
                                        <asp:Label ID="labCA01005" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Bank Name：</span>
                                        <asp:Label ID="labCA01006" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Bank Account：</span>
                                        <asp:Label ID="labCA01007" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Tax Registration No：</span>
                                        <asp:Label ID="labCA01008" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Contact person：</span>
                                        <asp:Label ID="labCA01009" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Tel：</span>
                                        <asp:Label ID="labCA01010" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Fax：</span>
                                        <asp:Label ID="labCA01011" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Post Code：</span>
                                        <asp:Label ID="labCA01012" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Province：</span>
                                        <asp:Label ID="labGA03Province" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>City：</span>
                                        <asp:Label ID="labGA03City" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Credit Days：</span>
                                        <asp:Label ID="labCA01014" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Credit Amount：</span>
                                        <asp:Label ID="labCA01015" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Customer Type：</span>
                                        <asp:Label ID="labCA01016" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Grade：</span>
                                        <asp:Label ID="labCA01017" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>CTC：</span>
                                        <asp:Label ID="labCA01018" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>MDT：</span>
                                        <asp:Label ID="labCA01019" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>SIC：</span>
                                        <asp:Label ID="labCA01020" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Devision Code：</span>
                                        <asp:Label ID="labCA01021" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>240 Category：</span>
                                        <asp:Label ID="labCA01022" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>220 Category：</span>
                                        <asp:Label ID="labCA01023" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>FGD（Y/N）：</span>
                                        <asp:Label ID="labCA01024" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Credit Customer（Y/N）：</span>
                                        <asp:Label ID="labCA01025" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Key Customer：</span>
                                        <asp:Label ID="labCA01047" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Comment：</span>
                                        <asp:Label ID="labCA01026" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                            </div>
                            <div class="tab-pane" id="xiangxi">
                                <div class="control-group">
                                    <label>
                                        <span>注册地址：</span>
                                        <asp:Label ID="labCA01027" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>实际办公地址：</span>
                                        <asp:Label ID="labCA01028" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>网址：</span>
                                        <asp:Label ID="labCA01029" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>电子信箱：</span>
                                        <asp:Label ID="labCA01030" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>成立日期：</span>
                                        <asp:Label ID="labCA01031" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>法定代表人：</span>
                                        <asp:Label ID="labCA01032" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>注册资本：</span>
                                        <asp:Label ID="labCA01033" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>登记机关：</span>
                                        <asp:Label ID="labCA01034" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>注册号：</span>
                                        <asp:Label ID="labCA01035" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>经营范围：</span>
                                        <asp:Label ID="labCA01036" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>经营期限：</span>
                                        <asp:Label ID="labCA01037" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>企业类型：</span>
                                        <asp:Label ID="labCA01038" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>所属行业：</span>
                                        <asp:Label ID="labCA01039" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>上级主管单位或母公司情况：</span>
                                        <asp:Label ID="labCA01040" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>股东背景：</span>
                                        <asp:Label ID="labCA01041" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>厂区情况：</span>
                                        <asp:Label ID="labCA01042" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>仓库情况：</span>
                                        <asp:Label ID="labCA01043" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>办公环境：</span>
                                        <asp:Label ID="labCA01044" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>员工素质：</span>
                                        <asp:Label ID="labCA01045" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>财务状况：</span>
                                        <asp:Label ID="labCA01046" runat="server" Text="Label"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>发货地址：</span>
                                        <asp:Label ID="labCA01048" runat="server"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>收货人：</span>
                                        <asp:Label ID="labCA01049" runat="server"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>发票邮寄地址：</span>
                                        <asp:Label ID="labCA01050" runat="server"></asp:Label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>收发票人：</span>
                                        <asp:Label ID="labCA01051" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="page-footer">
                    <asp:Button ID="btnEdit" CssClass="btn btn-primary" runat="server" Text="编 辑" OnClick="btnEdit_Click" />
                    &nbsp;
                <asp:Button ID="btnReturn" CssClass="btn btn-primary" runat="server" Text=" 返 回"
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
        <script src="/Scripts/page/Page_CA0101View.js" type="text/javascript"></script>
    </form>
</body>
</html>
