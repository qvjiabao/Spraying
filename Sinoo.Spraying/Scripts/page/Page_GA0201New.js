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
    };

    var List = getdata.getMenu();

    $.fn.zTree.init($("#treeDemo"), setting, List);
    var setting1 = {
        check: {
            enable: true
        },
        data: {
            simpleData: {
                enable: true
            }
        }
    };

    var List1 = getdata.getPower();
    $.fn.zTree.init($("#treeDemo1"), setting1, List1);
});
function DataClear() {
    $("#txtGA02002").val("");
    $("#txtGA02003").val("");

    var setting = {
        check: {
            enable: true
        },
        data: {
            simpleData: {
                enable: true
            }
        }
    };
    var List = getdata.getMenu();

    $.fn.zTree.init($("#treeDemo"), setting, List);
    var setting1 = {
        check: {
            enable: true
        },
        data: {
            simpleData: {
                enable: true
            }
        }
    };

    var List1 = getdata.getPower();
    $.fn.zTree.init($("#treeDemo1"), setting1, List1);
};

function DataSave() {
     var txtGA02002 = $("#txtGA02002").val();
     var Type = "GA02New";
     var bol;
    $.ajax({
        type: "post",
        url: "/Handler/UniquenessHandler.ashx",
        data: { txtGA02002:txtGA02002 ,Type:Type},
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



