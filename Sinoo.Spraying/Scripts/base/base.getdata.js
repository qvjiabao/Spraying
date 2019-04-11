getdata = {
    getControllerUrl: function () {
        return "/Handler/";
    },

    getCity: function (ProvinceID) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "LinkageHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { Fid: ProvinceID, Type: "GA03" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    },
    getCustomer: function (ProvinceID, CityID, CustomerName) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "GetDataHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { value: ProvinceID, value1: CityID, value2: CustomerName, Type: "CA01" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    },
    getOrder: function (Customer, Order) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "LinkageHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { CA01003: Customer, OA01002: Order, Type: "OA01" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    },
    getCB04: function (FatherId) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "LinkageHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { Fid: FatherId, Type: "CB04" },
            cache: false,
            success: function (result) {
                json = result;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.readyState);
                alert(textStatus);
            }
        });
        return json;
    },
    getUA01: function (FatherId) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "LinkageHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { Fid: FatherId, Type: "UA01" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    }
    ,
    getUA01SalesName: function (FatherId) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "LinkageHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { Fid: FatherId, Type: "UA01SalesName" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    }
    ,
    getUA01ByArea: function (FatherId) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "LinkageHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { Fid: FatherId, Type: "UA01ByArea" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    }
    ,
    getMenu: function () {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "BindMenuHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            //data: { Fid: ProvinceID, Type: "GA03" },
            cache: false,
            success: function (result) {

                json = result;
            }
        });
        return json;

        //        var json;
        //        $.ajax({
        //            url: getdata.getControllerUrl() + "BindMenuHandler.ashx?time=" + new Date().getTime(),
        //            async: false,
        //            dataType: 'json',
        //            type: 'POST',
        //            data: { Fid: ProvinceID, Type: "GA03" },
        //            cache: false,
        //            success: function (result) {
        //                alert(result);
        //                json = result;
        //            }
        //        });
        //        return json;
    },
    getCB03: function () {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "UserControlHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { Type: "CB03", OB04001: $("#UserControl_txtFMenu").val() },
            cache: false,
            success: function (result) {

                json = result;
            }
        });
        return json;
    },
    getOB02: function () {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "UserOB02lHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { Type: "OB02", OB02001: $("#UserControl_txtOB02001").val() },
            cache: false,
            success: function (result) {

                json = result;
            }
        });
        return json;
    },
    getPower: function () {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "BindPowerHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            //data: { Fid: ProvinceID, Type: "GA03" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    },
    getProduct: function (value) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "GetDataHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { value: value, Type: "OB01" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    }
    ,
    getProductTwo: function (value, id) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "GetDataHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            data: { value: value, id: id, Type: "OB01Tui" },
            cache: false,
            success: function (result) {
                json = result;
            }
        });
        return json;
    }
    ,
    GetExchangeRate: function () {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "CommonHandler.ashx?time=" + new Date().getTime(),
            async: false,
            dataType: 'json',
            type: 'POST',
            cache: false,
            success: function (result) {
                json = result;
                console.log(result);
            }
        });
        return json;
    }
    ,
    GetExchangeRateByOrderNo: function (orderNo) {
        var json;
        $.ajax({
            url: getdata.getControllerUrl() + "CommonHandler.ashx?time=" + new Date().getTime() + "&orderNo=" + orderNo,
            async: false,
            dataType: 'json',
            type: 'POST',
            cache: false,
            success: function (result) {
                json = result;
                console.log(result);
            }
        });
        return json;
    }
}