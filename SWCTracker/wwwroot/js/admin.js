var $editUrl = $('.edit-url').val(),
$deleteUrl = $('.delete-url').val(),
$listUrl = $('.list-url').val(),
$mainBootGrid = $("#grid-main"),
$saveFail = $('.save-fail');
//$spansaveResults = $('#spanresults');
$inputSaveResult = $('.save-result');


var $bootGridRowId = $mainBootGrid.data('rowid'),
    $bootGridRowName = $mainBootGrid.data('rowname');
$saveFail = $('.save-fail');
var isMultiple = false;

$(document).ready(function () {

    if ($mainBootGrid.length === 0) {
        //to be used where there are multiple grids
        $mainBootGrid = $(".grid-main");

    }
    var customCommandArr = BootGridExtension.GenerateActionButton();

    var hasDelete = $('.delete-url').length === 0 ? false : true;
    parentKey = null;
    hasSearch = false;
    isValid = false;

    if ($mainBootGrid.data('parentid')) {
        parentKey = $mainBootGrid.data('parentid');
    }

    if ($mainBootGrid.data('search')) {
        hasSearch = $mainBootGrid.data('search') === "true" ? true : false;
    }

    $.each($mainBootGrid, function (index, object) {
        var parentId = object.dataset.parentid;
        var bootGridRowId = $(object).data('rowid'),
            bootGridRowName = $(object).data('rowname');
        BootGridExtension.Init($(object), bootGridRowId, bootGridRowName, parentId, hasDelete, true, false, ElementFn.PlaceHolder, customCommandArr, ParamFn);
    });
  


    if ($inputSaveResult.val() === "true") {
        //DisplayOperationNotification($spansaveResults, true);
    }

});

function ParamFn(request) {


    if (typeof ParamFnEx !== 'undefined') {
        ParamFnEx(request);
    }

    return request;
}

function ActionButton() {

    var dataArr = [];
    $.each($('.grid-button'), function (index, object) {
        var hasUrl = $(this).data('btn-url'),
            hasparam = $(this).data('btn-param'),
            icon = $(this).data('btn-icon'),
            toolTip = $(this).data('tool-tip') ? $(this).data('tool-tip') : 'Edit',
            elementValue = $(this).val();


        dataArr.push({ 'Url': elementValue, 'HasParam': hasparam, 'Command': 'command-grid', 'Icon': icon, 'Title': toolTip, 'Text': toolTip, 'CustomFunction': ElementFn.PlaceHolder });
    });
 return dataArr;

}

function RefreshGrid() {
    $mainBootGrid.bootgrid('reload');

}

