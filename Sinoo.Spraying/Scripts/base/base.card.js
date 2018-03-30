card = {
    readbase: function (type) {
        return "0000000000000001";
    },
    write: function (str) {
        return true;
    },
    read: function () {
        return card.readbase(1);
    }
}