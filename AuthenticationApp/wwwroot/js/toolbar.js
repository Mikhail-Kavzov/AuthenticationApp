function SelectedCheckbox() {
    var selectedItems = new Array();
    $(".selected").each(function (index) { selectedItems.push(this.id); });
    return (selectedItems);
}
$('#BlockBtn').click(() => queryPutToolbar('Blocked', '/Home/BlockUsers/'));
$('#UnblockBtn').click(() => queryPutToolbar('Active', '/Home/UnBlockUsers/'));
$('#DeleteBtn').click(() => queryDeleteToolbar('/Home/DeleteUsers/'));
$('#QuitBtn').click(() => window.location.href = '/Account/Logout');

function queryPutToolbar(status, Url) {
    let idCheckBoxes = SelectedCheckbox();
    $.ajax({
        url: Url,
        method: 'put',
        dataType: 'json',
        data: { id: idCheckBoxes },
        success: function (data) {
            if (data === '')
                $('.selected').each(function (index) { $(this).children('td').last().text(status) });
            else
                window.location.href = data.redirectToUrl;
        }
    });
}

function queryDeleteToolbar(Url) {
    let idCheckBoxes = SelectedCheckbox();
    $.ajax({
        url: Url,
        method: 'delete',
        dataType: 'json',
        data: { id: idCheckBoxes },
        success: function (data) {
            if (data === '')
                $(".selected").remove();
            else
                window.location.href = data.redirectToUrl;
        }
    });
}