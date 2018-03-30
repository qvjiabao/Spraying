<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CA0101New.aspx.cs"
    Inherits="Sinoo.Spraying.Page.CustomerManagement.CA0101New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add Customer</title>
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
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        <div id="content-header">
            <h1>Add Customer</h1>
        </div>
        <div id="breadcrumb">
            <a href="#" title="" class="tip-bottom" data-original-title="Customer Management"><i
                class="icon-home"></i>Customer Management</a><a href="#" title="" class="tip-bottom"
                    data-original-title="Customer Data">Customer Data</a> <a href="#" class="current">Add
                    Customer</a>
        </div>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="content-box">
                        <div class="content-h">
                            <div class="bh">
                                <i class="icon-plus-sign"></i><span>Add</span>
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
                                        <input type="hidden" name="Type" value="CA01New" />
                                        <asp:TextBox ID="txtCA01002" runat="server" data-rule="length[1~20, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Customer Name：<font style="color: Red">*</font></span>
                                        <asp:TextBox ID="txtCA01003" runat="server" data-rule="required;"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Customer Ename：</span>
                                        <asp:TextBox ID="txtCA01004" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Billing Add/Tel：</span>
                                        <asp:TextBox ID="txtCA01005" runat="server" data-rule="length[1~200, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Bank Name：</span>
                                        <asp:TextBox ID="txtCA01006" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Bank Account：</span>
                                        <asp:TextBox ID="txtCA01007" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Tax Registration No：</span>
                                        <asp:TextBox ID="txtCA01008" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Contact person：</span>
                                        <asp:TextBox ID="txtCA01009" runat="server" data-rule="length[1~50, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Tel：</span>
                                        <asp:TextBox ID="txtCA01010" runat="server" data-rule="mobile"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Fax：</span>
                                        <asp:TextBox ID="txtCA01011" runat="server" data-rule="mobile"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Post Code：</span>
                                        <asp:TextBox ID="txtCA01012" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Province：<font style="color: Red">*</font></span>
                                        <asp:DropDownList ID="ddlGA03Province" runat="server" data-rule="required">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>City：<font style="color: Red">*</font></span>
                                        <select id="ddlGA03City" runat="server" data-rule="required">
                                        </select>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Credit Days：</span>
                                        <asp:TextBox ID="txtCA01014" runat="server" data-rule="length[1~50, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Credit Amount：</span>
                                        <asp:TextBox ID="txtCA01015" runat="server" data-rule="length[1~50, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Customer Type：<font style="color: Red">*</font></span>
                                        <asp:DropDownList ID="ddlCA01016" runat="server" data-rule="required">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Grade：</span>
                                        <asp:TextBox ID="txtCA01017" runat="server" data-rule="length[1~10, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>CTC：<font style="color: Red">*</font></span>
                                        <asp:DropDownList ID="ddlCA01018" runat="server" data-rule="required">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>MDT：<font style="color: Red">*</font></span>
                                        <asp:DropDownList ID="ddlCA01019" runat="server" data-rule="required">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>SIC：<font style="color: Red">*</font></span>
                                        <select id="ddlCA01020" runat="server" data-rule="required">
                                        </select>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Devision Code：<font style="color: Red">*</font></span>
                                        <asp:TextBox ID="txtCA01021" runat="server" data-rule="required;length[1~10, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>240 Category：</span>
                                        <asp:DropDownList ID="ddlCA01022" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>220 Category：</span>
                                        <asp:DropDownList ID="ddlCA01023" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>FGD（Y/N）：</span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnCA01024" ID="rbtnCA01024Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnCA01024" ID="rbtnCA01024No" runat="server" />否
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Credit Customer（Y/N）：</span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnCA01025" ID="rbtnCA01025Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnCA01025" ID="rbtnCA01025No" runat="server" />否
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Key Customer：</span>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnCA01047" ID="rbtnCA01047Yes" runat="server" />是
                                        </label>
                                        <label class="radio inline">
                                            <asp:RadioButton GroupName="rbtnCA01047" ID="rbtnCA01047No" runat="server" />否
                                        </label>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>Comment：</span>
                                        <asp:TextBox ID="txtCA01026" runat="server" data-rule="length[1~500, true]" TextMode="MultiLine"></asp:TextBox>
                                    </label>
                                </div>
                            </div>
                            <div class="tab-pane" id="xiangxi">
                                <div class="control-group">
                                    <label>
                                        <span>注册地址：</span>
                                        <asp:TextBox ID="txtCA01027" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>实际办公地址：</span>
                                        <asp:TextBox ID="txtCA01028" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>网址：</span>
                                        <asp:TextBox ID="txtCA01029" runat="server" data-rule="length[1~100, true];url"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>电子信箱：</span>
                                        <asp:TextBox ID="txtCA01030" runat="server" data-rule="length[1~100, true];email"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>成立日期：</span>
                                        <asp:TextBox ID="txtCA01031" runat="server" onClick="WdatePicker({maxDate:'%y-%M-%d'})"
                                            CssClass="Wdate" data-rule="成立日期:length[1~100, true];date"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>法定代表人：</span>
                                        <asp:TextBox ID="txtCA01032" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>注册资本：</span>
                                        <asp:TextBox ID="txtCA01033" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>登记机关：</span>
                                        <asp:TextBox ID="txtCA01034" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>注册号：</span>
                                        <asp:TextBox ID="txtCA01035" runat="server" data-rule="length[1~100, true]"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>经营范围：</span>
                                        <asp:TextBox ID="txtCA01036" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>经营期限：</span>
                                        <asp:TextBox ID="txtCA01037" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>企业类型：</span>
                                        <asp:TextBox ID="txtCA01038" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>所属行业：</span>
                                        <asp:TextBox ID="txtCA01039" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>上级主管单位或母公司情况：</span>
                                        <asp:TextBox ID="txtCA01040" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>股东背景：</span>
                                        <asp:TextBox ID="txtCA01041" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>厂区情况：</span>
                                        <asp:TextBox ID="txtCA01042" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>仓库情况：</span>
                                        <asp:TextBox ID="txtCA01043" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>办公环境：</span>
                                        <asp:TextBox ID="txtCA01044" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>员工素质：</span>
                                        <asp:TextBox ID="txtCA01045" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>财务状况：</span>
                                        <asp:TextBox ID="txtCA01046" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>发货地址：</span>
                                        <asp:TextBox ID="txtCA01048" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>收货人：</span>
                                        <asp:TextBox ID="txtCA01049" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>发票邮寄地址：</span>
                                        <asp:TextBox ID="txtCA01050" runat="server"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="control-group">
                                    <label>
                                        <span>收发票人：</span>
                                        <asp:TextBox ID="txtCA01051" runat="server"></asp:TextBox>
                                    </label>
                                </div>
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
        <script src="/Scripts/base/base.getdata.js" type="text/javascript"></script>
        <script src="/Scripts/page/Page_CA0101New.js" type="text/javascript"></script>
        <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    </form>
</body>
</html>
