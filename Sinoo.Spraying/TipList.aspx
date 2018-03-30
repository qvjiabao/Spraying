<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipList.aspx.cs" Inherits="Sinoo.Spraying.TipList" %>

<!DOCTYPE html>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Scripts/layer/com/bootstrap.css" rel="stylesheet" />
    <link href="Scripts/layer/com/bootstrap-table.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font-size: 12px;
        }
    </style>
</head>
<body style="padding-top: 10px">
    <!-- Nav tabs -->
    <ul id="myTab" class="nav nav-tabs" role="tablist" style="margin: 0px 20px 10px 20px;">
        <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">欠款订单</a></li>
        <li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">未发货订单</a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="home">
            <div class="container-fluid">
                <div class="table-responsive">
                    <table id="tabList" class="table-bordered"></table>
                </div>
            </div>
        </div>
        <div role="tabpanel" class="tab-pane" id="profile">
            <div class="container-fluid">
                <div class="table-responsive">
                    <table id="tabList2" class="table-bordered"></table>
                </div>
            </div>
        </div>
    </div>
    <script src="Scripts/layer/com/jquery.min.js"></script>
    <script src="Scripts/layer/com/bootstrap.js"></script>
    <script src="Scripts/layer/com/bootstrap-table.js"></script>
    <script src="Scripts/layer/com/bootstrap-table-zh-CN.js"></script>
    <script type="text/javascript">
        var TableInit = function () {
            var oTableInit = new Object();
            //初始化Table
            oTableInit.Init = function (url, columns, queryParams, tabId) {
                $("#" + tabId).bootstrapTable({
                    url: url,         //请求后台的URL（*）
                    method: 'post',                      //请求方式（*）
                    striped: true,                      //是否显示行间隔色
                    cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                    pagination: true,                   //是否显示分页（*）
                    queryParams: queryParams,           //传递参数（*）
                    sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
                    pageNumber: 1,                       //每页的记录行数（*）
                    pageSize: 5,                       //每页的记录行数（*）
                    pageList: [5, 10, 30],        //可供选择的每页的行数（*）
                    columns: columns,
                    contentType: "application/x-www-form-urlencoded"
                });
            };
            return oTableInit;
        };

        var oTable = new TableInit(); //初始化列表
        var tabQueryParams = function (params) {
            debugger;
            var temp = {
                limit: params.limit,   //每页多少条
                offset: params.offset  //第几页
            };
            return temp;
        };
        var tabColumns = [{ field: "OA01002", title: "OrderNo", width: "12%" }
            , { field: "CA01003", title: "CustomerName", width: "33%" }
            , { field: "OB01005", title: "PartNo", width: "15%" }
            , { field: "OB01007", title: "Qty", width: "15%" }
            , { field: "OB01009", title: "Amount", width: "15%" }
            , { field: "Pendingdays", title: "PendingDays", width: "15%" }];

        oTable.Init("/Handler/TipHandler.ashx?flag=pending", tabColumns, tabQueryParams, "tabList2");
    </script>
</body>
</html>
