<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PA0101Import.aspx.cs" Inherits="Sinoo.Spraying.Page.SalesManagement.PA0101Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/bootstrap-responsive.min.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%%" id="t" class="table table-bordered table-striped table-hover with-check zhong nomargin">
            <tr>
                <td>
                    Excel文件：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button CssClass="btn btn-primary" ID="btnImport" runat="server" Text="导  入"
                        OnClick="btnImport_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label Style="font-size: 13px;" ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
