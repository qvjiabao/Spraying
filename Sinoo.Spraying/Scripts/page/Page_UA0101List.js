function DataClear() {
    $("input[type='text']").val("");
    $("select").val("");
}

function RemoveNewCss() {
    $("#btnNew").remove();
}
function RemoveEditCss() {
    $("a[name='Edit']").remove();
}
function RemoveViewCss() {
    $("a[name='View']").remove();
}
function RemoveDelCss() {
    $("a[name='Del']").remove();
}
