        var $isPostBack = $('#IsPostBack');
        $formjoin = $('#form-main');
        $getHectareInputUrl = '@Url.Action("HectareInput")';
        $cropId = $('#OccupationGroupId');
        $waterSourceId = $('#WaterSourceId');
        $quantity = $('#Quantity');
        $btnRefresh = $('#btn-refresh');
        $divDetail = $('#div-detail');
        $(document).ready(function () {

            if ($isPostBack.val() === 'True')
            {
                PageEvents.SetControlData();
            }
            $('select').change(function (evt) {
                PageEvents.SelectChange();
            })

            $quantity.keyup(function (evt) {
            PageEvents.CalculateCost();
            })

        $btnRefresh.click(function (evt) {
            PageEvents.RefreshPage();
            })
        });

var PageEvents = {

    SetControlData: function () {
        $('.occupationGroup').html(DropDowns.SelectedText($occupationGroupId))
        $('.occupation').html(DropDowns.SelectedText($occupationId))
    },

            SelectChange: function () {


            AjaxPostBack.Get_ReturnHtml($getHectareInputUrl, {
                'cropId': $cropId.val(),
                'waterSourceId': $waterSourceId.val()
            },
                ElementFn.PlaceHolder,
                PageEvents.PopulateEditForm,
                ElementFn.FnPlaceHolder,
                ElementFn.FnPlaceHolder);

            },

        RefreshPage:  function () {
            $cropId.val('0');
        $waterSourceId.val('0');
        $quantity.val('1');
        PageEvents.SelectChange();
            },

        CalculateCost: function () {

                var quantity = $quantity.val() * 1;
        var tbody = $divDetail.find('tbody');
        var tFooterAmount = $divDetail.find('.total-amount');
        tbody.find('.td_hectare').html('<strong>' + quantity + '</strong>');
        var rows = tbody.find('tr');
        var totalAmount = 0;
        $(rows).each(function (index, value) {

                    var inputData = $(value).find('.input-quantity');

        var rowAmount = (inputData.data('quantity') / inputData.data('size')) * inputData.data('amount') * quantity;

        $(value).find('.td_cost').html('<strong>' + ValueFormatter.ToRand(rowAmount) + '</strong>');
        $(value).find('.input-cost').val(rowAmount);
        $(value).find('.input-hectare').val(quantity);
        totalAmount += rowAmount;
                });

        tFooterAmount.html('<strong><u>' + ValueFormatter.ToRand(totalAmount) + '</u></strong>');

            },

        PopulateEditForm:function (
        response) {
            $quantity.val('1');
        $divDetail.html(response);

        PageEvents.CalculateCost();

    }
        }
