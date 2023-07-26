var $processDescription = $('#ProcessDescription');
$divProcessCompliant = $('#div-process-compliant');
$isProcessCompliant = $('#form-main input[name="ComplaintDetail_ProcessCompliant"]');
$inputProcessCompliant = $('#inputProcessCompliant');
var $complaintWizard = $('#complaintWizard');
$formMain = $('#form-main');
$(document).ready(function () {
   
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

    $complaintWizard.on("leaveStep", function (e, anchorObject, currentStepIndex, nextStepIndex, stepDirection) {
        return confirm("Do you want to leave the step " + currentStepIndex + "?");
    });

    $complaintWizard.steps({
        headerTag: 'h3',
        bodyTag: 'section',
        autoFocus: true,
        titleTemplate: '<span class="number">#index#<\/span> <span class="title">#title#<\/span>',
        onStepChanging: function (event, currentIndex, newIndex) {
            if (currentIndex < newIndex) {
                // Step 1 form validation
                if (currentIndex === 0) {
                    var fname = $('#firstname').parsley();
                    var lname = $('#lastname').parsley();
                    if (fname.isValid() && lname.isValid()) {
                        return true;
                    } else {
                        fname.validate();
                        lname.validate();
                    }
                }
                // Step 2 form validation
                if (currentIndex === 1) {
                    var email = $('#email').parsley();
                    if (email.isValid()) {
                        return true;
                    } else {
                        email.validate();
                    }
                }
                // Always allow step back to the previous step even if the current step is not valid.
            } else {
                return true;
            }
        }
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
        $('[name=ComplaintDetail_ProcessCompliant][value=true]').prop("checked", true);
        $divProcessCompliant.fadeOut();
    }
    else {
        $('[name=ComplaintDetail_ProcessCompliant][value=false]').prop("checked", true);
        $divProcessCompliant.fadeIn();
    }

}