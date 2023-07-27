$occupationGroupId = $('#OccupationGroupId');
$occupationId = $('#OccupationId');
$getOccupationByParentId = '/Calculator/GetOccupationSelectListItem_ByParentId';


$(document).ready(function () {

    $('.input-rate').val();

    if ($('#form-home').length > 0) {
        $occupationGroupId = $('#OccupationGroupId');
        $occupationId = $('#OccupationId');
            OccupationGroupChange();
        }
   

    $occupationGroupId.change(function (evt) {
        OccupationGroupChange();
    })


});


function OccupationGroupChange() {

    DropDowns.LoadData(emptyObject, $occupationId, 0);

    if ($occupationGroupId.val() !== "0") {

        AjaxPostBack.Get($getOccupationByParentId, {
            'parentId': $occupationGroupId.val()
        }, PopulateChild);
    }
}

function PopulateChild(response) {
    DropDowns.LoadData(response.Response, $occupationId, 0, true)
}