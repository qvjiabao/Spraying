$(function () {
    var setting = {
        
        check: {
            enable: true,
            chkboxType : { "Y" : "ps", "N" : "ps" }
        },
        data: {
            simpleData: {
                enable: true
            }
        }
    };

    var List = getdata.getCB03();

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

    var List1 = getdata.getOB02();
    $.fn.zTree.init($("#treeDemo1"), setting1, List1);
});
//function DataClear() {
//    $("#txtGA02002").val("");
//    $("#txtGA02003").val("");

//    var setting = {
//        check: {
//            enable: true
//        },
//        data: {
//            simpleData: {
//                enable: true
//            }
//        }
//    };
//    var List = getdata.getCB03();

//    $.fn.zTree.init($("#treeDemo"), setting, List);
//    var setting1 = {
//        check: {
//            enable: true
//        },
//        data: {
//            simpleData: {
//                enable: true
//            }
//        }
//    };

//    var List1 = getdata.getOB02();
//    $.fn.zTree.init($("#treeDemo1"), setting1, List1);
//};

function DataSave() {

    var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
    var nodes = treeObj.getCheckedNodes(true);
    var v = "";
    for (var i = 0; i < nodes.length; i++) {
        v += nodes[i].id + ","; //获取选中节点的值
    }
    $("#UserControl_txtFMenu").val(v);

    var treeObj1 = $.fn.zTree.getZTreeObj("treeDemo1");
    var nodes1 = treeObj1.getCheckedNodes(true);
    var v1 = "";
    for (var i = 0; i < nodes1.length; i++) {
        v1 += nodes1[i].id + ","; //获取选中节点的值
    }
    $("#UserControl_txtOB02001").val(v1);
    $('#imgBox').modal({ show: false });
}


//清空树
function ClearTree() {
    var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
    treeObj.checkAllNodes(false);
    treeObj = $.fn.zTree.getZTreeObj("treeDemo1");
    treeObj.checkAllNodes(false);
    $(".hiddenInputControl").val("");
}