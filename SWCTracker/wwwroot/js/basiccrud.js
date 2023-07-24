var $btnmanage = $('.btn-manage'),
    $btnSave = $('#btn-save'),
    $btnback = $('.btn-back'),
    $divDetail = $('#div-detail');
$controlDetail = $('.control-detail');
$divList = $('.div-list');
$formMain = $('#form-main');
$entityID = $('#form-main #Id'),
    $listUrl = $('.list-url').val(),
    $getentitybyidUrl = $('.edit-url').data('url'),
    $deleteUrl = $('.delete-url').val(),
    $save = $('.save-url').val(),
    $gridListUrl = $('.save-list-url').val();

/*    $editUrl = $('.edit-url');*/
var $divSaveResults = $('#divSaveResults'),
    $spanGlyphicon = $('#spanGlyphicon'),
    $spanSaveResults = $('#spanSaveResults');
var saveFail = "there was an error while saving please try again";
var saveSuccess = "Success!! saving record";

$(document).ready(function () {

    $entityID = $('#form-main #Id'),
        $formMain = $('body #form-main');
    $btnmanage.click(function (e) {

        ElementFn.ToggleVisibility(false, $divList);
        ElementFn.ToggleVisibility(false, $divSaveResults);
        var parentId = 0;
        if ($(this).data('parentid')) {
            parentId = $(this).data('parentid');
        }
        PopulateDetailSection(0, parentId);

    });
    SaveEventInit();
});

function SaveEventInit() {
    $btnSave = $('#btn-save');
    $btnback = $('.btn-back'),
    $formMain = $('body #form-main');


    $btnSave.click(function (evt) {
        evt.preventDefault();
        var button = $(this);

        var validationResult = ValidationFn.CommonValidations($formMain);

        if (validationResult.IsSuccess) {
            AjaxActions.ManualValidateForm();

            if (ValidationFn.IsFormValid($formMain)) {

                var formData = $formMain.serializeArray();

                Grid_FormPostBackEvent.GeneratePostBackParams(formData);

        AjaxPostBack.Post($save,
            formData,
            AjaxActions.Before(true, button),
            AjaxActions.Success,
            AjaxActions.Error,
            AjaxActions.Complete(false, button));
            }
        }
        else {

            ElementFn.ToggleDisplaySaveResults(false,
                validationResult.Message,
                $divSaveResults,
                $spanSaveResults,
                $spanGlyphicon);
        }

    });

    $btnback.click(function (evt) {

        ElementFn.ToggleVisibility(false, $divDetail);
        ElementFn.ToggleVisibility(false, $controlDetail);
        ElementFn.ToggleVisibility(true, $divList);
        $divSaveResults.hide();
    });
}
function PopulateDetailSection(rowID, parentId) {


    AjaxPostBack.Get_ReturnHtml($getentitybyidUrl, {
        'ID': rowID,
        "parentId":parentId
    },
        ElementFn.PlaceHolder,
        PopulateEditForm,
        ElementFn.FnPlaceHolder,
        ElementFn.FnPlaceHolder);

}

function PopulateEditForm(
    response) {

    $divDetail.html(response);

    $.validator.unobtrusive.parse($('#form-main'));
    ElementFn.ToggleVisibility(true, $divDetail);
    ElementFn.ToggleVisibility(true, $controlDetail);


}


var AjaxActions = {
    Before: function (
        isLoading,
        postBackTrigger) {


        AjaxPostBack.ToggleButtonText(isLoading, postBackTrigger);
        ModalEvents.ShowBusyProcessing();

    },

    Success: function (
        response) {

        var saveMessage = saveSuccess;
        if (!response.isSuccess) {

            saveMessage = response.message;
            ElementFn.ToggleDisplaySaveResults(response.isSuccess,
                'Error ! ' + response.message,
                $divSaveResults,
                $spanSaveResults,
                $spanGlyphicon);

        } else {

            $mainBootGrid.bootgrid("reload");
            ElementFn.ToggleVisibility(true, $divList);
            ElementFn.ToggleVisibility(false, $divDetail);
            ElementFn.ToggleVisibility(false, $controlDetail);

            if (typeof RefreshAfterPostBack !== 'undefined' && $.isFunction(RefreshAfterPostBack)) {

                RefreshAfterPostBack();
            }
        }

        ModalEvents.HideBusyProcessing();
        $.toaster({ priority: response.isSuccess ? 'success' : 'danger', title: response.isSuccess ? 'Confirmation' : 'Error', message: saveMessage });

    },

    Error: function () {

        var isFalse = false;
        ElementFn.ToggleDisplaySaveResults(false,
            saveFail,
            $divSaveResults,
            $spanSaveResults,
            $spanGlyphicon);

        ModalEvents.HideBusyProcessing();

        $.toaster({ priority: isFalse ? 'success' : 'danger', title: isFalse ? 'Confirmation' : 'Error', message: saveFail });

    },

    Complete: function (
        isLoading,
        postBackTrigger) {
        AjaxPostBack.ToggleButtonText(isLoading, postBackTrigger);
    },

    ManualValidateForm: function () {

        var formBody = $('#form-main #Body');


        TinyMCEFn.TinyToText(formBody, 'Body');

    }
};

function EditBootGrid(event, rowID) {
    $divSaveResults.hide();
    ElementFn.ToggleVisibility(false, $divList);
    ElementFn.ToggleVisibility(false, $divSaveResults);
    $entityID.val(rowID);

    PopulateDetailSection(rowID,null);
}

var Grid_FormPostBackEvent =
{

    GeneratePostBackParams: function (formdata) {

        $.each($mainBootGrid, function (index, object) {

            var selectedIDs = $(object).bootgrid("getSelectedRows");

            if (selectedIDs.length > 0) {
                $.each(selectedIDs, function (index, value) {
                    formdata.push({
                        name: 'selectedIdList',
                        value: value
                    });
                });
            }
        });
   
    }
};


function OnBootgrid_RowSelected(e, rows) {

}

function OnBootgrid_RowDeselected(e, rows) {

}