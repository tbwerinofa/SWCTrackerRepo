var $processDescription = $('#ProcessDescription');
$divProcessCompliant = $('#div-process-compliant');
$isProcessCompliant = $('#form-main input[name="ProcessCompliant"]');
$inputProcessCompliant = $('#inputProcessCompliant');
var $complaintWizard = $('#complaintWizard');
$formMain = $('#form-main');
$(document).ready(function () {

    // Toolbar extra buttons
    var btnFinish = $('<button></button>').text('Finish')
        .addClass('btn btn-primary').on('click', function () {
            evt.preventDefault();
            $divSaveResults.hide();
            SaveRecord($(this));
        });


    var btnCancel = $('<button></button>').text('Cancel')
        .addClass('btn btn-secondary')
        .on('click', function () { $('#smartwizard-3').smartWizard("reset"); });
    // Dots Smart Wizard 1
    $complaintWizard.smartWizard({
        selected: 0,
        theme: 'dots',
        transitionEffect: 'fade',
        showStepURLhash: false,
        toolbarSettings: {
            toolbarExtraButtons: [btnFinish, btnCancel]
        }
    });

    SetDefaults();

    /*radio button event*/
    $isProcessCompliant.click(function () {

        $divProcessCompliant.find('input').val('');

        if ($(this).val() !== 'true') {

            $divProcessCompliant.fadeIn();
        }
        else {
            $divProcessCompliant.fadeOut();
        }
    })
});

function SaveRecord(button) {

    //var validationResult = ValidationFn.CommonValidations($formjoin);

    //if (validationResult.IsSuccess) {
    //    AjaxActions.ManualValidateForm();

    //    if (ValidationFn.IsFormValid($formMain)) {

    var formData = $formjoin.serializeArray();

    //        Grid_FormPostBackEvent.GeneratePostBackParams(formData);

    AjaxPostBack.Post($saveUrl,
        formData,
        AjaxActions.Before(true, button),
        PostBackActions.Success,
        AjaxActions.Error,
        AjaxActions.Complete(false, button));
    //    }
    //}
    //else {

    //    ElementFn.ToggleDisplaySaveResults(false,
    //        validationResult.Message,
    //        $divSaveResults,
    //        $spanSaveResults,
    //        $spanGlyphicon);
    //}

}

var PostBackActions = {


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

            ElementFn.ToggleVisibility(false, $complaintWizard);
            ElementFn.ToggleVisibility(true, $('#save-confirmation'));
        }

        ModalEvents.HideBusyProcessing();
        $.toaster({ priority: response.isSuccess ? 'success' : 'danger', title: response.isSuccess ? 'Confirmation' : 'Error', message: saveMessage });

    }
}

function SetDefaults() {


    if ($inputProcessCompliant.val() == 'Yes') {
        $('[name=ProcessCompliant][value=true]').prop("checked", true);
        $divProcessCompliant.fadeOut();
    }
    else {
        $('[name=ProcessCompliant][value=false]').prop("checked", true);
        $divProcessCompliant.fadeIn();
    }

}