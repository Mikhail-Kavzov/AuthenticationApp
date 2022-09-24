function SelectedCheckbox() {
    var selectedItems = new Array();
    $(".selected").each(function (index) { selectedItems.push(this.id); });
    return (selectedItems);
}
$('#BlockBtn').click(() => queryPutToolbar('Blocked', '/Home/BlockUsers/'));
$('#UnblockBtn').click(() => queryPutToolbar('Active', '/Home/UnBlockUsers/'));
$('#DeleteBtn').click(() => queryDeleteToolbar('/Home/DeleteUsers/'));

function queryPutToolbar(status, Url) {
    let idCheckBoxes = SelectedCheckbox();
    $.ajax({
        url: Url,
        method: 'put',
        dataType: 'html',
        data: { id: idCheckBoxes },
        success: function (data) {
            $('.selected').each(function (index) { $(this).children('td').last().text(status) });
        }
    });
}

function queryDeleteToolbar(Url) {
    let idCheckBoxes = SelectedCheckbox();
    $.ajax({
        url: Url,
        method: 'delete',
        dataType: 'html',
        data: { id: idCheckBoxes },
        success: function (data) {
            $(".selected").remove();
        }
    });
}