var $isPostBack = $('#IsPostBack');
$formMain = $('#form-main');
$getWageRateUrl = '/Calculator/GetWageRate';
$sectorId = $('#SectorId');
$occupationGroupId = $('#OccupationGroupId');
$occupationId = $('#OccupationId');
$workingHours = $('#WorkingHours');
$daysPerWeek = $('#DaysPerWeek');
$salary = $('#Salary');
$paramInput = $('input');
        $btnRefresh = $('#btn-refresh');
        $divDetail = $('#div-detail');
        $(document).ready(function () {

            if ($isPostBack.val() === 'True')
            {
                PageEvents.SelectChange();
            }
            $('select').change(function (evt) {
                PageEvents.SelectChange();
            })

            $paramInput.keyup(function (e) {

                PageEvents.SelectChange();
            })

        $btnRefresh.click(function (evt) {
            PageEvents.RefreshPage();
            })
        });

var PageEvents = {
    SelectChange: function () {
        if ($formMain.valid) {
            AjaxPostBack.Get($getWageRateUrl, {
                'sectorId': $sectorId.val(),
                'occupationId': $occupationId.val()
            },
                PageEvents.CalculateCost);

        }
    },
    RefreshPage:  function () {
            $occupationGroupId.val('0');
        $occupationId.val('0');
        $quantity.val('1');
        PageEvents.SelectChange();
            },
    CalculateCost: function (response) {

        $('.occupationGroup').html(DropDowns.SelectedText($occupationGroupId));
        $('.occupation').html(DropDowns.SelectedText($occupationId));

        $workingHours = $('#WorkingHours');
        $daysPerWeek = $('#DaysPerWeek');
        $salary = $('#Salary');

        var hourlySection = $('.hourly-rate');
        var dailySection = $('.daily-rate');
        var weeklySection = $('.weekly-rate');
        var monthlySection = $('.monthly-rate');

        var amount = 1.0;


        if (response.Response == null) {
            $('.rate-finYear').html('');
            $('.rate-grade').html('');
            $('.rate-amount').html('');
        } else {
             amount = response.Response.Amount;
            $('.rate-finYear').html(response.Response.FinYear);
            $('.rate-grade').html(response.Response.Grade);
            $('.rate-amount').html(ValueFormatter.ToRand(amount));
        }

        var isNegativeVariance = $salary.val() * 1 < amount;

        var btnMessage = isNegativeVariance ? 'You earn below agreed rate!' : 'You salary rate is compliant!';
        $('.btn-rate').html(btnMessage);

        PageEvents.SummarySection(isNegativeVariance, $salary.val(), amount);


        var weekHours = $daysPerWeek.val() * $workingHours.val() * 1;
        var monthHours = weekHours * 4;

        PageEvents.PopulateSection(hourlySection, $salary.val(), 1, 1, amount,'Hours');
        PageEvents.PopulateSection(dailySection, $salary.val(), $workingHours.val(), $workingHours.val(), amount,'Hours');
        PageEvents.PopulateSection(weeklySection, $salary.val(), weekHours, $daysPerWeek.val(), amount,'Days');
        PageEvents.PopulateSection(monthlySection, $salary.val(), monthHours, $daysPerWeek.val() * 4, amount, 'Days');




    },

    SummarySection(isNegativeVariance,salary,amount) {

        var wageRate = (salary * 1) / amount;
        var percent = wageRate * 100

        var percentDifference = percent > 100 ? percent -100: 100 - percent;

        var percentMessage = isNegativeVariance ? 'less' : 'more';
        var indicator = isNegativeVariance? '<i class="fe fe-arrow-down ms-1"></i> ': '<i class="fa fa-caret-up  me-1"></i> '

      
        $('.salary-rate').html(ValueFormatter.ToRand(salary));
        $('.percent-diference').html(indicator + percentDifference.toFixed(2) + '% ' + percentMessage + ' than current wage rate');

        if (isNegativeVariance) {
            $('.percent-diference').removeClass('text-success');
            $('.percent-diference').addClass('text-danger');
        } else {
            $('.percent-diference').removeClass('text-danger');
            $('.percent-diference').addClass('text-success');
        }

        var chart = '<div class="mx-auto chart-circle chart-circle-primary chart-circle-lg  mt-sm-0 mb-0 donutShadow" data-value="'
         + wageRate.toFixed(2) + '" data-thickness="15" data-color="#4454c3"><canvas width = "160" height = "160" ></canvas><div class="mx-auto chart-circle-value text-center mb-2"><h1 class="mb-0 mt-2 percent-rate">'
            + percent.toFixed(2) + '% ' + '</h1><small>Wage Rate</small></div></div >';

        $('.rate-chart').html(chart);
        PageEvents.LoadChart();
    },
    PopulateSection:function (
            sectionContainer, myRate,hours, totalUnits, expectedRate,unitType) {
            var applicableRate = myRate * hours;
            var applicableExpectedRate = expectedRate * hours;
            var variance = applicableRate - applicableExpectedRate;
            sectionContainer.find('.unit-type').html(unitType)
            sectionContainer.find('.my-rate').html(ValueFormatter.ToRand(applicableRate))
            sectionContainer.find('.totalunits').html(totalUnits)
            sectionContainer.find('.expected-rate').html(ValueFormatter.ToRand(applicableExpectedRate))
            sectionContainer.find('.variance-rate').html(ValueFormatter.ToRand(variance));
    },
    LoadChart: function () {
        if ($('body .chart-circle-primary').length) {
            $('body .chart-circle-primary').each(function () {
                let $this = $(this);

                $this.circleProgress({
                    fill: {
                        color: $this.attr('data-color')
                    },
                    size: $this.height(),
                    startAngle: -Math.PI / 4 * 2,
                    emptyFill: 'rgba(68, 84, 195, 0.4)',
                    lineCap: 'round'
                });
            });
        }
    }

    }
