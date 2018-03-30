
$(function () {
    $.post("/Handler/GetDataHandler.ashx?time=" + new Date().getTime()
            , { Type: "MessageArrived" }
            , function (data) {
                if ($.parseJSON(data).length > 0) {
                    var msghtml = "";
                    for (var i = 0; i < $.parseJSON(data).length; i++) {
                        msghtml += '<a href="/Page/SalesManagement/OA0101View.aspx?OA01001=' + $.parseJSON(data)[i]["OA01001"] + '" >订单号：【' + $.parseJSON(data)[i]["OA01002"] + '】</a></br>'
                    }
                    $.messager.show({
                        title: '到货提醒',
                        msg: msghtml,
                        timeout: 0,
                        showType: 'fade'
                    });
                }
            });

    $.post("/Handler/GetDataHandler.ashx?time=" + new Date().getTime()
            , { Type: "MessageDelivered" }
            , function (data) {
                if ($.parseJSON(data).length > 0) {
                    var msghtml = "";
                    for (var i = 0; i < $.parseJSON(data).length; i++) {
                        msghtml += '<a href="/Page/SalesManagement/OA0101View.aspx?OA01001=' + $.parseJSON(data)[i]["OA01001"] + '" >订单号：【' + $.parseJSON(data)[i]["OA01002"] + '】</a></br>'
                    }
                    $.messager.show({
                        title: '发货提醒',
                        msg: msghtml,
                        timeout: 0,
                        showType: 'fade'
                    });
                }
            });
    $.post("/Handler/GetDataHandler.ashx?time=" + new Date().getTime()
            , { Type: "NoPayment" }
            , function (data) {
                if ($.parseJSON(data).length > 0) {
                    var msghtml = "";
                    for (var i = 0; i < $.parseJSON(data).length; i++) {
                        msghtml += '<a href="/Page/SalesManagement/OA0101View.aspx?OA01001=' + $.parseJSON(data)[i]["OA01001"] + '" >订单号：【' + $.parseJSON(data)[i]["OA01002"] + '】</a></br>'
                    }
                    $.messager.show({
                        title: '未付款提醒',
                        msg: msghtml,
                        timeout: 0,
                        showType: 'fade'
                    });
                }
            });
})
