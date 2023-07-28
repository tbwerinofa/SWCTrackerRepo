var $processDescription = $('#ProcessDescription');
$divProcessCompliant = $('#div-process-compliant');
$isProcessCompliant = $('#form-main input[name="ComplaintDetail_ProcessCompliant"]');
$inputProcessCompliant = $('#inputProcessCompliant');
var $complaintWizard = $('#complaintWizard');
$formMain = $('#form-main');
$saveUrl = '/Complaint/SaveModel';
$(document).ready(function () {
    var formValidator = ($formMain).validate();
    // Input Masks
    $('#dateMask').mask('99/99/9999');
    $('#phoneMask').mask('(999) 999-9999');
    $('#ssnMask').mask('999-99-9999');
    $('.fc-datepicker').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true
    });

    // Toolbar extra buttons
    var btnFinish = $('<button></button>').text('Finish')
        .addClass('btn btn-primary').on('click', function (evt) {

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

    $complaintWizard.on("leaveStep", function (e, anchorObject, currentStepIndex, nextStepIndex, stepDirection) {

      
        if (nextStepIndex === 'forward') {
            if ($formMain.valid()) {
                $('.validation-summary-errors').hide()
                formValidator.resetForm();
                return true
            } else {
                $('.validation-summary-errors').show()
                return false;
            }
        }
        return true;

    });

    SetDefaults();

    /*radio button event*/
    $isProcessCompliant.click(function () {

       // $divProcessCompliant.find('input').val('');

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

    var formData = $formMain.serializeArray();

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

            ElementFn.ToggleVisibility(false, $('.complaint-container'));
            ElementFn.ToggleVisibility(true, $('#save-confirmation'));
        }

        ModalEvents.HideBusyProcessing();
        $.toaster({ priority: response.isSuccess ? 'success' : 'danger', title: response.isSuccess ? 'Confirmation' : 'Error', message: saveMessage });

    }
}

function SetDefaults() {


    if ($inputProcessCompliant.val() == 'Yes') {
        $('[name=ComplaintDetail_ProcessCompliant][value=true]').prop("checked", true);
        $divProcessCompliant.fadeOut();
    }
    else {
        $('[name=ComplaintDetail_ProcessCompliant][value=false]').prop("checked", true);
        $divProcessCompliant.fadeIn();
    }

}