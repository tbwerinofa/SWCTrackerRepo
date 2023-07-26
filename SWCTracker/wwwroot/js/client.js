$occupationGroupId = $('#OccupationGroupId');
$occupationId = $('#OccupationId');
$getOccupationByParentId = '/Calculator/GetOccupationSelectListItem_ByParentId';
$(document).ready(function () {


    $occupationGroupId.change(function (evt) {
        OccupationGroupChange();
    })

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
});