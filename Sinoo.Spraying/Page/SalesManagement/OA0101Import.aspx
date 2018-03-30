<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OA0101Import.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.OA0101Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <table width="100%%" id="t" class="table table-bordered table-striped table-hover with-check zhong nomargin">
            <tr>
                <td>
                    订单信息：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
                <td>
                    <asp:Button CssClass="btn btn-primary" ID="btnImportOA01" runat="server" Text="导  入"
                        OnClick="btnImportOA01_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    订单明细：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload2" runat="server" />
                </td>
                <td>
                    <asp:Button CssClass="btn btn-primary" ID="btnImportOB01" runat="server" Text="导  入"
                     OnClick="btnImportOB01_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    发票发货信息：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload3" runat="server" />
                </td>
                <td>
                    <asp:Button CssClass="btn btn-primary" ID="btnImportOC01" runat="server"
                         Text="导  入" OnClick="btnImportOC01_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    付款信息：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload4" runat="server" />
                </td>
                <td>
                    <asp:Button CssClass="btn btn-primary" ID="btnImportOP01" runat="server" 
                        Text="导  入" OnClick="btnImportOP01_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label Style="font-size: 13px;" ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
