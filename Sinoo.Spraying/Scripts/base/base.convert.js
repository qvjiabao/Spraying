Convert = { 
    ConvertDate: function (value) {

        if (value != null) {
            var reg = new RegExp('/', 'g');
            var d = eval('new ' + value.replace(reg, ''));
            return new Date(d).format('yyyy-MM-dd')
        }
        else {
            return "";
        }
    }

};