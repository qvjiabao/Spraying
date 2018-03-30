<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl.ascx.cs"
    Inherits="Sinoo.Spraying.UserControl.UserControl" %>
<div class="container-fluid">
    <div class="row-fluid">
        <div class="span6">
            <div class="content-box">
                <div class="content-h">
                    <div class="bh">
                        <i class="icon-th-list"></i><span>行业分类</span></div>
                </div>
                <div>
                    <ul id="treeDemo" class="ztree">
                    </ul>
                    <input type="hidden" id="txtFMenu" class="hiddenInputControl" runat="server" name="txtFMenu" />
                </div>
            </div>
        </div>
        <div class="span6">
            <div class="content-box">
                <div class="content-h">
                    <div class="bh">
                        <i class="icon-th-large"></i><span>应用代码</span></div>
                </div>
                <div class="content-bd">
                    <ul id="treeDemo1" class="ztree">
                    </ul>
                    <input type="hidden" id="txtOB02001" class="hiddenInputControl" runat="server" name="txtPower" />
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row-fluid">
    <div class="page-footer">
        <input id="btnSave" aria-hidden="true" data-dismiss="modal" type="button" class="btn btn-primary" style="margin-top:27px;"
            value="确 定" onclick="return DataSave()" />
    </div>
</div>
