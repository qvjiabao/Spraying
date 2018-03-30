check = {
    //是否为空
    isNull: function (str) {
        if (null == str || "" == str.trim()) {
            return true;
        }
        else {
            return false;
        }
    },
    //校验是否全是数字
    isDigit: function (str) {
        var patrn = /^\d+$/;
        return patrn.test(str);
    },
    //校验是否是整数
    isInteger: function (str) {
        var patrn = /^([+-]?)(\d+)$/;
        return patrn.test(str);
    },
    //校验是否为正整数
    isPlusInteger: function (str) {
        var patrn = /^([+]?)(\d+)$/;
        return patrn.test(str);
    },
    //校验是否为负整数
    isMinusInteger: function (str) {
        var patrn = /^-(\d+)$/;
        return patrn.test(str);
    },
    //校验是否为浮点数
    isFloat: function (str) {
        var patrn = /^([+-]?)\d*\.\d+$/;
        return patrn.test(str);
    },
    //校验是否为正浮点数
    isPlusFloat: function (str) {
        var patrn = /^([+]?)\d*\.\d+$/;
        return patrn.test(str);
    },
    //校验是否为负浮点数
    isMinusFloat: function (str) {
        var patrn = /^-\d*\.\d+$/;
        return patrn.test(str);
    },
    //校验是否仅中文
    isChinese: function (str) {
        var patrn = /[\u4E00-\u9FA5\uF900-\uFA2D]+$/;
        return patrn.test(str);
    },
    //校验手机号码
    isMobile: function (str) {
        var patrn = /^0?1((3[0-9]{1})|(59)){1}[0-9]{8}$/;
        return patrn.test(str);
    },
    //校验电话号码
    isPhone: function (str) {
        var patrn = /^(0[\d]{2,3}-)?\d{6,8}(-\d{3,4})?$/;
        return patrn.test(str);
    },
    //校验URL地址
    isUrl: function (str) {
        var patrn = /^http[s]?:\/\/[\w-]+(\.[\w-]+)+([\w-\.\/?%&=]*)?$/;
        return patrn.test(str);
    },
    //校验电邮地址
    isEmail: function (str) {
        var patrn = /^[\w-]+@[\w-]+(\.[\w-]+)+$/;
        return patrn.test(str);
    },
    //校验邮编
    isZipCode: function (str) {
        var patrn = /^\d{6}$/;
        return patrn.test(str);
    },
    //校验合法时间
    isDate: function (str) {
        if (!/\d{4}(\.|\/|\-)\d{1,2}(\.|\/|\-)\d{1,2}/.test(str)) {
            return false;
        }
        var r = str.match(/\d{1,4}/g);
        if (r == null) { return false; };
        var d = new Date(r[0], r[1] - 1, r[2]);
        return (d.getFullYear() == r[0] && (d.getMonth() + 1) == r[1] && d.getDate() == r[2]);
    },
    //校验字符串：只能输入6-20个字母、数字、下划线(常用手校验用户名和密码)
    isString6_20: function (str) {
        var patrn = /^(\w){6,20}$/;
        return patrn.test(str);
    },
    //钱的正则。格式 200:55/500
    isMoney: function (str) {
        var parrn1 = /^([+]?)(\d+)$/;
        if (parrn1.test(str)) { 
            return true;
        }
        else {
            var patrn2 = /\d+\.\d{2}$/; 
            return patrn2.test(str);
        }
    }




}