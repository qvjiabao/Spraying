$(function () {

    var setting = {
        check: {
            enable: true
        },
        data: {
            simpleData: {
                enable: true
            }
        }

    }
    var List;
    $.ajax({
        url: getdata.getControllerUrl() + "BindMenuHandler.ashx?Type=Edit&GA02001=" + $("#ID").val() + "&time=" + new Date().getTime(),
        async: false,
        dataType: 'json',
        type: 'POST',
        cache: false,
        success: function (result) {
            List = result;
        }
    });

    $.fn.zTree.init($("#treeDemo"), setting, List);

    $.ajax({
        url: getdata.getControllerUrl() + "BindPowerHandler.ashx?Type=Edit&GA02001=" + $("#ID").val() + "&time=" + new Date().getTime(),
        async: false,
        dataType: 'json',
        type: 'POST',
        cache: false,
        success: function (result) {
            List = result;
        }
    });

    $.fn.zTree.init($("#treeDemo1"), setting, List);

});

function DataSave() {
    var txtGA02002 = $("#txtGA02002").val();
    var Type = "GA02Edit";
    var ID = $("#ID").val();
    var bol;
    $.ajax({
        type: "post",
        url: "/Handler/UniquenessHandler.ashx",
        data: { txtGA02002: txtGA02002, Type: Type,ID:ID },
        async: false,
        success: function (result) {
            if ($.parseJSON(result)["ok"] == "") {
                bol = true;
                var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = treeObj.getCheckedNodes(true);
                var v = "";
                for (var i = 0; i < nodes.length; i++) {
                    v += nodes[i].id + ","; //获取选中节点的值
                }
                $("#txtMenu").val(v);

                var treeObj1 = $.fn.zTree.getZTreeObj("treeDemo1");
                var nodes1 = treeObj1.getCheckedNodes(true);
                var v1 = "";
                for (var i = 0; i < nodes1.length; i++) {
                    v1 += nodes1[i].id + ","; //获取选中节点的值
                }
                $("#txtPower").val(v1);
            } else {
                bol = false;
                alert($.parseJSON(result)["error"]);
                $("#txtGA02002").focus();
            }
        }
    });

    return bol;
}

