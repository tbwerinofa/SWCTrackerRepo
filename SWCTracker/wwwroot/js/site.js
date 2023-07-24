// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Global variable defined and start with underscore
var _active = "active",
    _defaultValue = '0',
    _defaultText = '-- choose --';
_reportStartingDialog = $('#ReportStartingDialog');
_reportFailedDialog = $('#ReportFailedDialog');
_documentPath = $('#DocumentPath').val();
_visibleGridArr = [];
var _activeBootGrid;

$(document).ready(function () {
    //LoadMenu();
    ModalEvents.RepositionBind();
});


var AjaxPostBack =
{

    Get_ReturnHtml: function (url, formData, beforeSendCallBack, successCallBack, failCallBack, doneCallBack) {

        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            data: formData,
            beforeSend: beforeSendCallBack,
            success: successCallBack,
            fail: failCallBack,
            always: doneCallBack
        });

    },
    Get: function (url, parameter, successCallBack, beforeSendCallBack, doneCallBack) {

        return $.ajax({
            url: url,
            type: 'get',
            cache: false,
            dataType: 'json',
            data: parameter,
            success: successCallBack,
            beforeSend: beforeSendCallBack,
            complete: doneCallBack,
            error: function (xhr, textStatus, errorThrown) {
            }
        });
    },

    Post: function (url, formData, beforeSendCallBack, successCallBack, failCallBack, doneCallBack) {

        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            traditional: true,
            data: formData,
            beforeSend: beforeSendCallBack,
            success: successCallBack,
            fail: failCallBack,
            //done: doneCallBack,
            always: doneCallBack
        });

    }

    , ToggleButtonText: function (isLoading, postBackTrigger) {

        if (isLoading) {
            postBackTrigger.button('loading');
        }
        else {
            postBackTrigger.button('reset');
        }

    }

    , FormAppend: function (form, dataArr) {

        $.each(dataArr, function (i, object) {
            form.append(
                $("<input>",
                    {
                        type: 'hidden',
                        id: object.id,
                        name: object.id,
                        value: object.value
                    }
                )
            );
        });
        return form;
    },
};

var ElementFn =
{
    ToggleVisibility: function (isVisible, element) {
        if (isVisible || isVisible === "true") {
            element.show();
        }
        else {
            element.hide();
        }
    },
    PlaceHolder: function FnPlaceHolder() {

    },
    ToggleDisplaySaveResults: function (isSuccess,
        message,
        displayDiv,
        messageSpan,
        iconSpan) {

        if (isSuccess) {
            displayDiv.addClass('alert-success').removeClass('alert-danger').fadeIn();
            messageSpan.html(message);
            iconSpan.addClass('glyphicon-ok').removeClass('glyphicon-warning-sign');
        }
        else {
            displayDiv.addClass('alert-danger').removeClass('alert-success').fadeIn();
            messageSpan.html(message);
            iconSpan.addClass('glyphicon-warning-sign').removeClass('glyphicon-ok');
        }

        setTimeout(function () {
            displayDiv.fadeOut();
        }, 50000);

        ElementFn.SetLocation(displayDiv);
    },
    SetLocation: function (displayDiv) {
        $('html,body').animate({
            scrollTop: displayDiv.offset().top
        });
    }
};

var DropDowns =
{
    LoadData: function (data, childElement, currentValue, excludeDefault) {

        childElement.empty();
        var count = 0;

        if (!data.isEmptyObject) {

            var auxArr = [];

            $.each(data, function (i, option) {
                count = count + 1;

                if (data.length === 1 && i === 0) {
                    currentValue = option.Value;
                }

                auxArr[i + 1] = DropDowns.GenerateOption(option.Value, option.Text, option.Value2, option.Value3, option.Selected);
            });

            childElement.append(auxArr.join(''));
        }



        //populate default value
        if (count !== 1 && !excludeDefault) {
            childElement.prepend(DropDowns.GenerateOption(_defaultValue, _defaultText, null, null, false));
        }


        //set selected value
        if (currentValue !== null) {
            childElement.val(currentValue);
        }

    },
    LoadDataAllowOne: function (data, childElement, currentValue, excludeDefault, allowOne) {

        childElement.empty();
        var count = 0;

        if (!data.isEmptyObject) {

            var auxArr = [];

            $.each(data, function (i, option) {
                count = count + 1;

                if (data.length === 1 && i === 0 && !allowOne) {
                    currentValue = option.Value;
                }

                auxArr[i + 1] = DropDowns.GenerateOption(option.Value, option.Text, option.Value2, option.Value3, option.Selected);
            });

            childElement.append(auxArr.join(''));
        }



        //populate default value
        if ((count !== 1 || allowOne) && !excludeDefault) {
            childElement.prepend(DropDowns.GenerateOption(_defaultValue, _defaultText, null, null, false));
        }


        //set selected value
        if (currentValue !== null) {
            childElement.val(currentValue);
        }

    },
    LoadDataMultiSelect: function (selectedArr, response, currentElement) {

        var IsSelected = false;

        var IsSingle = response !== null && response.length === 1 ? true : false;

        auxArr = [];
        if (response !== null) {
            $.each(response, function (i, option) {

                if (selectedArr !== null) {
                    IsSelected = $.inArray(option.Value, selectedArr) !== -1 ? true : false;
                }

                if (option.Selected) {
                    IsSelected = option.Selected;
                }

                if (IsSingle) {
                    IsSelected = true;
                }
                auxArr.push(DropDowns.GenerateOptionsArray(option, IsSelected));
            });
        }
        currentElement.multiselect('dataprovider', auxArr);

    },

    LoadDataMultiSelect_OptGroups: function (selectedArr, responseDataArray, currentElement) {
        var IsSelected = false;
        var IsSingle = responseDataArray !== null && responseDataArray.length === 1 ? true : false;

        optgroups = [];

        if (responseDataArray !== null) {

            var dataGroups = DropDowns.GenerateOptGroupsArray(responseDataArray);

            $.each(dataGroups, function (index, grpValue) {

                var options = DropDowns.GenerateOptGroupsChildrenArray(selectedArr, responseDataArray, grpValue);

                optgroups.push({
                    label: grpValue, children: options
                });

            });
        }
        currentElement.multiselect('dataprovider', optgroups);

    },

    InitMultiSelect: function (element, onDropDownHidden_Event, onDropDownChange_Event) {
        DropDowns.GenerateMultiSelect(element, onDropDownHidden_Event, onDropDownChange_Event);
        DropDowns.MultiSelectAutoHide();
    },

    InitSearchableSelect: function (element, onDropDownHidden_Event, onDropDownChange_Event) {
        DropDowns.GenerateMultiSelect(element, onDropDownHidden_Event, onDropDownChange_Event);
        DropDowns.MultiSelectAutoHide();
    },

    LoadDefault: function (data) {

        if (data.length !== 1) {

            isMultiple = true;

            data.unshift({
                Value: _defaultValue,
                Text: _defaultText
            });

        }
    },

    SelectedText: function (element) {
        return element.find(":selected").text();
    },
    SelectedDataAttr: function (element) {
        return element.find(":selected").data('id');
    },
    SelectedDataAttrNamed: function (element, name) {
        return element.find(":selected").data(name);
    },


    FilterData: function (dataArray, filterArray) {

        if (filterArray === null) {
            return dataArray;
        } else {
            var resultArray = $.grep(dataArray, function (e) {
                return $.inArray(e.Value3, filterArray) !== -1;
            });
            return resultArray;
        }
    },

    GetParentIDArray: function myfunction(dataArray, parentDropDwnControl) {

        var currentArr = DropDowns.FilterData(dataArray, parentDropDwnControl.val());
        var parentIDArr = [];

        $.each(currentArr, function (i, object) {
            var currentValue = object.Value.toString();
            parentIDArr.push(currentValue);
        });

        return parentIDArr;
    },

    RePopulateCascading: function (sourceArray, dropDownControl, parentDropDownControl) {

        var paramArray = parentDropDownControl.val();

        var filteredArray = DropDowns.FilterData(sourceArray, paramArray);

        dropDownControl.multiselect('dataprovider', []);
        DropDowns.LoadDataMultiSelect_OptGroups([], filteredArray, dropDownControl);
    },
    RePopulateCascading_Single: function (sourceArray, dropDownControl, parentDropDownControl, value) {

        dropDownControl.empty();
        var paramArray = parentDropDownControl.val();
        var array = [];
        array.push(paramArray);
        var filteredArray = DropDowns.FilterData(sourceArray, array);

        DropDowns.LoadData(filteredArray, dropDownControl, 0);

    },

    GenerateOption: function (value, text, value2, value3, iSelected) {
        var dataValue2 = value2 === null ? '' : 'data-value2="' + value2 + '"';
        var dataValue3 = value3 === null ? '' : 'data-value3="' + value3 + '"';
        var selected = iSelected ? 'selected ' : '';
        return "<option " + selected + dataValue2 + dataValue3 + " value='" + value + "'>" + text + "</option>";
    },
    GenerateMultiSelect: function (element, onDropDownHidden_Event, onDropDownChange_Event) {
        //element.multiselect('destroy');

        element.multiselect({
            includeSelectAllOption: true,
            numberDisplayed: 2,
            dropRight: true,
            enableFiltering: true,
            filterBehavior: 'text',
            enableCaseInsensitiveFiltering: true,
            nonSelectedText: _defaultText,
            buttonText: function (options, select) {
                if (options.length < 1) {
                    return _defaultText;
                }
                else if (options.length > 3) {
                    return options.length + ' items selected';
                }
                else {
                    var labels = [];
                    options.each(function () {
                        if ($(this).attr('label') !== undefined) {
                            labels.push($(this).attr('label'));
                        }
                        else {
                            labels.push($(this).html());
                        }
                    });
                    return labels.join(', ') + '';
                }
            },
            onChange: function (option, checked, select) {
                onDropDownChange_Event(option, checked, select);
            },
            onDropdownHidden: function (event) {
                onDropDownHidden_Event($(this)[0]);
            }
        });
    },
    MultiSelectAutoHide: function () {
        $('body ul.multiselect-container.dropdown-menu.pull-right').on('mouseleave', function () {
            var currentDropDown = $(this).closest('.btn-group').find('.dropdown-toggle');
            currentDropDown.dropdown('toggle');
            currentDropDown.blur();
        });
    },
    GenerateOptionsArray: function (option, isselected) {
        var optionData;
        if (option.DataLatitude !== null && option.DataLongitude !== null) {
            optionData = {
                label: option.Text,
                title: option.Text,
                value: option.Value,
                selected: isselected
            };

        } else {

            optionData = {
                label: option.Text,
                title: option.Text,
                value: option.Value,
                selected: isselected
            };

        }
        return optionData;
    },


    GenerateOptGroupsArray: function (data) {

        var optGroups = [];

        $.each(data, function (index, value) {

            if ($.inArray(value.Value2, optGroups) === -1) {

                optGroups.push(value.Value2);
            }

        });
        return optGroups;
    },

    GenerateOptGroupsChildrenArray: function (selectedArr, data, optGroup) {
        var auxArr = [];
        var isSingle = data !== null && data.length === 1 ? true : false;
        $.each(data, function (i, option) {

            var isSelected = false;

            if (selectedArr !== null) {
                isSelected = $.inArray(option.Value, selectedArr) !== -1 ? true : false;
            }

            if (isSingle) {
                isSelected = true;
            }
            if (optGroup === option.Value2) {

                auxArr.push({
                    label: option.Text,
                    title: option.Text,
                    value: option.Value,
                    selected: isSelected
                });
            }
        });

        return auxArr;
    }

};

var emptyObject = {};


var BootGridExtension = {

    Init: function (element, rowKey, rowKeyName, parentKey, hasDelete, hasSearch, hasCustomCommand, customFunction, customCommandArr, paramFunction) {
        var searchTemplate = hasSearch ? {} : { search: "" };
        element.bootgrid({
            ajax: true,
            selection: true,
            multiSelect: true,
            rowSelect: true,
            keepSelection: true,
            post: function () {
                return {
                    id: rowKey,
                    parentId: parentKey,
                    isValid: element.data('isvalid')
                };
            },
            searchSettings: {
                delay: 100,
                characters: 4
            },
            url: $listUrl,
            requestHandler: function (request) {
                //http://stackoverflow.com/questions/30834965/additional-parameters-for-grid
                return paramFunction(request);

            },
            formatters: {
                "commands": function (column, row) {
                    return BootGridExtension.Commands($editUrl, row, rowKey, rowKeyName, hasDelete, hasCustomCommand, customCommandArr);
                },
                "picture": function (column, row) {
                    if (typeof row.DocumentGuid === 'undefined' || row.DocumentGuid === null) {
                        return "<img class=\"img-thumbnail\" src=\"" + "../../assets/images/users/16.jpg" + "\" width=\"40\" />";
                    } else {
                        return "<img class=\"img-thumbnail\" src=\"" + _documentPath + row.Folder + "/" + row.DocumentGuid + "\" width=\"100\" />";
                    }
                },
                "picture-document": function (column, row) {

                    if (typeof row.IsImage !== 'undefined' && row.IsImage === false) {
                        return '<a href="' + _documentPath + row.Folder + "/" + row.DocumentGuid + '">' + row.AnchorTagName + '</a>';
                    } else {
                        return "<img class=\"img-thumbnail\" src=\"" + _documentPath + row.Folder + "/" + row.DocumentGuid + "\" width=\"100\" />";
                    }
                },
                "checkbox": function (column, row) {

                    var checked = (row[column.id]) ? "checked" : "";
                    var inpHidden = "<input type='hidden' class='primary-key' value='" + row[rowKey] + "' name='" + rowKey + "' />";
                    var checkBox = "<input class='checkbox' type='checkbox' value='" + row[column.id] + "' name='" + column.id + "' " + checked + " width='100' />";

                    return checkBox + inpHidden;
                },
                "primarykey": function (column, row) {

                    var checkBox = "<input type='hidden' value='" + row[column.id] + "' name='" + column.id + "'/>";

                    return checkBox;
                },

                "boolean-icon": function (column, row) {

                    if ((row[column.id])) {
                        return '<i class="text-success fas fa-thumbs-up  fa-bold" aria-hidden="true"></i>&nbsp;Yes';
                    }
                    else {
                        return '<i class="text-danger fas fa-thumbs-down  fa-bold" aria-hidden="true"></i>&nbsp;No';

                    }
                },
                "rand-value": function (column, row) {

                    if (row[column.id] !== null) {
                        return 'R ' + ValueFormatter.CommaSeparateNumber(row[column.id]);
                    } else {
                        return '';
                    }
                },
                "anchor-tag": function (column, row) {
                    //row.AnchorTagName must be define in data coming from server side
                    return '<a href="' + row.AnchorTagUrl + '">' + row.AnchorTagName + '</a>';
                },

                "dollar-value": function (column, row) {

                    if (row[column.id] !== null) {
                        return '$ ' + ValueFormatter.CommaSeparateNumber(row[column.id]);
                    } else {
                        return '';
                    }
                }
            }
        }).on("load.rs.jquery.bootgrid", function (e) {
            _visibleGridArr = [];
        })
            .on("loaded.rs.jquery.bootgrid", function () {
                /* Executes after data is loaded and rendered */
                $(this).find(".command-edit").on("click", function (e) {

                    //this function is implemented on calling page
                    var rowID = $(this).data("row-id");
                    EditBootGrid(e, rowID);
                })
                    .end()
                    .find(".command-delete").on("click", function (e) {
                        var rowID = $(this).data("row-id");

                        var message = "<div class='row'><div class='col-md-10'><div class='alert alert-danger'><i class='fa fa-warning'></i>&nbsp;&nbsp;You pressed delete on row:  <strong>" + $(this).data("row-name") + "</strong></div></div></div>";
                        bootbox.confirm(message, function (result) {
                            if (result) {
                                BootGridExtension.DeleteGridRow(rowID);
                            }

                        });
                    }).end()
                    .find(".command-custom").on("click", function (e) {
                        e.preventDefault();
                        var rowID = $(this).data("row-id");
                        var rowName = $(this).data("row-name");
                        if (customFunction !== null) {
                            customFunction(rowID, rowName);
                        }
                    });
            });

    },

    InitNoDefault: function (element, rowKey, rowKeyName, customCommandArr, hasSearch, paramFunction) {
        var searchTemplate = hasSearch ? {} : { search: "" };
        element
            .bootgrid({
                ajax: true,
                selection: true,
                multiSelect: true,
                rowSelect: true,
                keepSelection: true,
                //post: function () {
                //    return {
                //        id: "b0df282a-0d67-40e5-8558-c9e93b7befed"
                //    };
                //},
                url: $listUrl,
                ajaxSettings: {
                    method: "POST"
                },
                requestHandler: function (request) {
                    //http://stackoverflow.com/questions/30834965/additional-parameters-for-grid
                    return paramFunction(request);

                },


                formatters: {
                    "commands": function (column, row) {
                        return BootGridExtension.CommandsNoDefault(row, rowKey, rowKeyName, customCommandArr);
                    },
                    "picture": function (column, row) {
                        return "<img class=\"img-thumbnail\" src=\"" + _documentPath + row.Folder + "/" + row.DocumentGuid + "\" width=\"100\" />";
                    },
                    "checkbox": function (column, row) {

                        var checked = (row[column.id]) ? "checked" : "";
                        var inpHidden = "<input type='hidden' class='primary-key' value='" + row[rowKey] + "' name='" + rowKey + "' />";
                        var checkBox = "<input class='checkbox' type='checkbox' value='" + row[column.id] + "' name='" + column.id + "' " + checked + " width='100' />";

                        return checkBox + inpHidden;
                    },
                    "primarykey": function (column, row) {

                        var checkBox = "<input type='hidden' value='" + row[column.id] + "' name='" + column.id + "'/>";

                        return checkBox;
                    },

                    "boolean-icon": function (column, row) {

                        if ((row[column.id])) {
                            return '<i class="text-success fa fa-thumbs-o-up  fa-bold" aria-hidden="true"></i>&nbsp;Yes';
                        }
                        else {
                            return '<i class="text-danger fa fa-thumbs-o-down  fa-bold" aria-hidden="true"></i>&nbsp;No';

                        }
                    },
                    "rand-value": function (column, row) {

                        if (row[column.id] !== null) {
                            return 'R ' + ValueFormatter.CommaSeparateNumber(row[column.id]);
                        } else {
                            return '';
                        }
                    },
                    "anchor-tag": function (column, row) {
                        //row.AnchorTagName must be define in data coming from server side
                        return '<a href="' + row.AnchorTagUrl + '">' + row.AnchorTagName + '</a>';
                    },

                    "dollar-value": function (column, row) {

                        if (row[column.id] !== null) {
                            return '$ ' + ValueFormatter.CommaSeparateNumber(row[column.id]);
                        } else {
                            return '';
                        }
                    }




                },
                templates: searchTemplate

            })
            .on("load.rs.jquery.bootgrid", function (e) {
                _visibleGridArr = [];
            })
            .on("loaded.rs.jquery.bootgrid", function () {
                /* Executes after data is loaded and rendered */

                var bootgridRow = $(this);

                if (customCommandArr.length > 0) {
                    $.each(customCommandArr, function (i, Object) {

                        if (Object.Command !== undefined) {
                            var currentCommand = '.' + Object.Command;


                            bootgridRow.find(currentCommand).on("click", function (e) {

                                var rowID = $(this).data("row-id");
                                var rowName = $(this).data("row-name");


                                if (Object.CustomFunction !== null) {
                                    Object.CustomFunction(rowID, rowName, e);
                                }

                                e.preventDefault();
                            }).end();
                        }
                    });
                }
            })
            .on("selected.rs.jquery.bootgrid", function (e, rows) {
                OnBootgrid_RowSelected(e, rows);

            }).on("deselected.rs.jquery.bootgrid", function (e, rows) {
                OnBootgrid_RowDeselected(e, rows);
            });

    },

    Commands: function (editUrl, row, rowId, rowName, hasDelete, hasCustomCommand, customCommandArr) {

        var deleteButton = '';
        var customButton = '';
        var editButton = '';
        var hasEdit = editUrl === undefined ? false : true;

        if (hasEdit) { //edit exist at on page
            if ($('.edit-url').data('toggle-visibility')) {

                var editVisibilityColumn = $('.edit-url').data('toggle-visibility');
                if (!row["" + editVisibilityColumn + ""]) {
                    hasEdit = false;

                }
            }
            if (hasEdit) {

                var url = "#";
                if (editUrl !== "#") {
                    url = editUrl + "/" + row["" + rowId + ""];
                }
                editButton = BootGridExtension.GenerateAnchorTag(url,
                    row["" + rowId + ""],
                    '',
                    'command-edit',
                    'fas fa-edit',
                    'Edit');
            }
        }

        if (hasDelete) {

            if ($('.delete-url').data('toggle-visibility')) {
                var deleteVisibilityColumn = $('.delete-url').data('toggle-visibility');

                if (!row["" + deleteVisibilityColumn + ""]) {
                    hasDelete = false;

                }
            }
            if (hasDelete) {
                deleteButton = BootGridExtension.GenerateAnchorTag("#",
                    row["" + rowId + ""],
                    row["" + rowName + ""],
                    'command-delete',
                    'fa fa-trash',
                    'Delete Record');
            }
        }


        if (hasCustomCommand) {

            customButton = BootGridExtension.GenerateAnchorTag("#",
                row["" + rowId + ""],
                row["" + rowName + ""],
                'command-custom',
                'fa fa-th-list',
                "Reset Password");
        }

        if (customCommandArr.length > 0) {
            $.each(customCommandArr, function (i, Object) {

                var url = '';
                if (Object.Url === '') {
                    url = '#';
                } else {
                     
            if (Object.HasParam) {
                url =  Object.Url + "?id=" + row["" + rowId + ""];
            } else {
                url =  Object.Url + "/" + row["" + rowId + ""];
            }
                }
               
                if (typeof (Object.VisibilityColumn) === 'undefined' || Object.VisibilityColumn === '' || (Object.VisibilityColumn !== '' && row["" + Object.VisibilityColumn + ""])) {

                    customButton = customButton + '&nbsp;' + BootGridExtension.GenerateAnchorTag(url,
                        row["" + rowId + ""],
                        row["" + rowName + ""],
                        Object.Command,
                        Object.Icon,
                        Object.Title);
                }
            });
        }

        return editButton + deleteButton + customButton;
    },

    CommandsNoDefault: function (row, rowId, rowName, customCommandArr) {

        var customButton = '';
        if (customCommandArr.length > 0) {
            $.each(customCommandArr, function (i, Object) {

                var url = Object.Url === '' ? '#' : Object.Url + "/" + row["" + rowId + ""];

                customButton = customButton + '&nbsp;' + BootGridExtension.GenerateAnchorTag(url,
                    row["" + rowId + ""],
                    row["" + rowName + ""],
                    Object.Command,
                    Object.Icon,
                    Object.Title);

            });
        }

        return customButton;
    },

    GenerateAnchorTag: function (url, dataId, dataName, command, icon, title) {
        return "<a href=\""
            + url + "\" class=\"btn btn-xs btn-default "
            + command + "\" data-row-name=\""
            + dataName + "\" data-row-id=\""
            + dataId + "\" title=\"" + title + "\">"
            + BootGridExtension.GenerateSpanTag(icon)
            + "</a> ";
    },

    GenerateSpanTag: function (className) {
        return "<span class='" + className + "'></span>";
    },

    DeleteGridRow: function (id) {

        AjaxPostBack.Post($deleteUrl,
            { 'id': id },
            BootGridExtension.BeforeDelete,
            BootGridExtension.DeleteSuccess,
            BootGridExtension.DeleteError,
            BootGridExtension.DeleteComplete);

    },

    GenerateActionButton: function () {

        var dataArr = [];
        $.each($('.grid-button'), function (index, object) {
            var hasUrl = $(this).data('btn-url'),
                hasparam = $(this).data('btn-param'),
                hasmodal = $(this).data('modal');

            icon = $(this).data('btn-icon'),
                elementValue = hasUrl ? $(this).val() : '#';
            var title = '';
            var toolTip = '';
            var visibilityColumn = '';

            var modalContainerId = '';


            var customCommand = 'command-grid';
            if ($(this).data('command-click')) {
                customCommand = customCommand + ' command-click';

            }

            if ($(this).data('title')) {
                title = $(this).data('title');
            }

            if ($(this).data('tool-tip')) {
                toolTip = $(this).data('tool-tip');

            }
            if ($(this).data('toggle-visibility')) {
                visibilityColumn = $(this).data('toggle-visibility');
            }

            dataArr.push({ 'Url': elementValue, 'HasParam': hasparam, 'Command': customCommand, 'Icon': icon, 'Title': title, 'ToolTip': toolTip, 'Text': 'Edit', 'CustomFunction': ElementFn.PlaceHolder, 'HasModal': hasmodal, 'VisibilityColumn': visibilityColumn });
        });
        return dataArr;

    },

    BeforeDelete: function () {

    },

    DeleteSuccess: function (response) {

        if (response.isSuccess) {
            //$('#spanresults').notify("Success! Deleting Record.", { position: "right", className: "success" });
            $mainBootGrid.bootgrid("reload");
            $.toaster({
                priority: response.isSuccess ? 'success' : 'danger', title: response.isSuccess ? 'Confirmation' : 'Error', message: 'Success! Deleting Record.'
            });

            if (typeof RefreshAfterPostBack !== 'undefined' && $.isFunction(RefreshAfterPostBack)) {

                RefreshAfterPostBack();
            }
        }
        else {

            var message = response.message !== null ? response.message : "Error! Deleting Record.";
            $.toaster({ priority: response.isSuccess ? 'success' : 'danger', title: response.isSuccess ? 'Confirmation' : 'Error', message: message });
        }


    },

    DeleteError: function () {

    },

    DeleteComplete: function () {

    },

    Refresh: function (element) {

        element.bootgrid("reload");
    }


};

var DatePickerFn = {

    Init: function (inpDatePicker, startDate, endDate, changeDate, disableDates, highlightedDates) {

        inpDatePicker.datepicker('destroy');
        inpDatePicker.datepicker({
            format: 'dd MM yyyy',
            startDate: startDate,
            endDate: endDate,
            todayBtn: false,
            daysOfWeekDisabled: disableDates,
            daysOfWeekHighlighted: highlightedDates,
            calendarWeeks: true,
            forceParse: false,
            autoclose: true
        }).on('changeDate', function (ev) {
            changeDate(ev);
        });
    },
    Destroy: function (inpDatePicker) {
        inpDatePicker.datepicker('destroy');
    },
    GetMonthNameFromInt: function (intMonth) {
        var month = new Array();
        month[0] = "January";
        month[1] = "February";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "November";
        month[11] = "December";
        return month[intMonth];
    },

    LocalizedDateFormat: function (strDate) {

        var modifiedDateValue = strDate.split("-"); // Gives Output as YY,MM,DD 00:00:00

        var toSkipTimeZone = modifiedDateValue[2].split(".");

        var date = new Date(modifiedDateValue[0] + "/" + modifiedDateValue[1] + "/" + toSkipTimeZone[0]); // Here, passing the format as "yyyy/mm/dd" 

        return date;
    },

    LongDateFormat: function (date) {

        var monthName = DatePickerFn.GetMonthNameFromInt(date.getMonth());
        var fullDate = date.getDate() + ' ' + monthName + ' ' + date.getFullYear();
        return fullDate;
    },

    DateCompare: function (earlierDate, laterDate) {
        var isGreater = false;
        var startDate = new Date(earlierDate.val());
        var endDate = new Date(laterDate.val());
        if (startDate > endDate) {
            isGreater = true;
        }

        return isGreater;
    },
        HtmlActivate: function () {

        $(function () {
            if (!Modernizr.inputtypes.date) {
                $('input[type=date]').datepicker({
                    dateFormat: 'yy-mm-dd'
                }
                );
            }
        });

    }
};

var ValidationFn = {
    ToggleValidationError: function (element) {
        element.find('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        element.find('.input-validation-error').removeClass('input-validation-error');
    },
    CommonValidations: function (form) {
        var isValidResult = {
            IsSuccess: true,
            Message: null
        };


        var idNumber = form.find('.rsa-idnumber');
        if (idNumber.length > 0 && idNumber.val() !== '') {
            var isValid = ValidationFn.IsValidRSAID(idNumber.val());
            if (!isValid) {

                isValidResult = {
                    IsSuccess: false,
                    Message: "Please enter a valid South African ID"
                };

            }
        }
        return isValidResult;

    },
    IsValidRSAID: function (identityNumber) {
        var i, c,
            even = '',
            sum = 0,
            check = identityNumber.slice(-1);

        if (identityNumber.length !== 13 || identityNumber.match(/\D/)) {
            return false;
        }
        identityNumber = identityNumber.substr(0, identityNumber.length - 1);
        for (i = 0; c === identityNumber.charAt(i); i += 2) {
            sum += +c;
            even += identityNumber.charAt(i + 1);
        }
        even = '' + even * 2;
        for (i = 0; c === even.charAt(i); i++) {
            sum += +c;
        }
        sum = 10 - ('' + sum).charAt(1);
        return ('' + sum).slice(-1) === check;
    },

    IsTimeGreaterThan: function (startTime, endTime) {

        //interest in time of day so date is not important
        var dateBase = '20 November 2018 ';

        var concatStartTime = dateBase + startTime;
        var concatEndTime = dateBase + endTime;
        var stt = new Date(concatStartTime);
        stt = stt.getTime();

        var endt = new Date(concatEndTime);
        endt = endt.getTime();

        if (stt >= endt) {
            return false;
        }
        return true;
    },
    IsFormValid: function (form) {

        jQuery.validator.defaults.ignore = ":hidden";
        form.validate();
        $.validator.unobtrusive.parse(form);
        var isValid = form.valid();
        if (isValid) {
            ValidationFn.ResetFormValidation(form);
        }
        return isValid;
    },
    ResetFormValidation: function (form) {
        form.validate().resetForm();
        form.find(".error").removeClass("error");
    },
    NumberFormat: function () {
        $('.number-only').keypress(function (e) {
            if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });

        $('.decimal-number-only').keypress(function (e) {
            if (e.which !== 44 && e.which !== 46 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });

        $('.alphanumeric-only').keypress(function (e) {
            if (e.which < 48 || (e.which > 57 && e.which < 65) || (e.which > 90 && e.which < 97) || e.which > 122) {
                return false;
            }
        });
    }


};

var TinyMCEFn = {

    TinyToText: function (targetElement, tinyElement) {

        if (typeof tinyMCE !== 'undefined') {
            ed = tinyMCE.get(tinyElement);

            if (ed) {
                targetElement.val(ed.getContent());
            }
        }
    }
};


var ModalEvents = {

    ShowModal: function (modalContent, modalContainer) {

        modalContent.removeClass('hidden');
        modalContainer.modal({
            backdrop: 'static'
        });
        modalContainer.modal(show = true);
    },

    HideModal: function (modalContainer) {
        modalContainer.modal('hide');
    },
    ShowBusyProcessing: function () {
        $('.progress-modal-dialog').removeClass('hidden');
        $('#progressModal').modal({ backdrop: 'static' });
        $('#progressModal').modal(show = true);

    },
    HideBusyProcessing: function () {
        $('#progressModal').modal('hide');
    },

    RepositionBind: function myfunction() {
        // Reposition when a modal is shown

        //http://www.abeautifulsite.net/vertically-centering-bootstrap-modals/
        $('#progressModal').on('show.bs.modal', ModalEvents.Reposition);
        // Reposition when the window is resized

        $(window).on('resize', function () {
            $('#progressModal:visible').each(ModalEvents.Reposition);
        });


        $('.modal-centered').on('show.bs.modal', ModalEvents.Reposition);
        $('.bootbox').on('show.bs.modal', ModalEvents.Reposition);

        $('.modal-centered').on('show.bs.modal', ModalEvents.Reposition);
        // Reposition when the window is resized

        $(window).on('resize', function () {
            $('.modal-centered:visible').each(ModalEvents.Reposition);
        });
    },
    Reposition: function () {
        var modal = $(this),
            dialog = modal.find('.modal-dialog');
        modal.css('display', 'block');

        // Dividing by two centers the modal exactly, but dividing by three 
        // or four works better for larger screens.
        dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 3));
    }

}

var ValueFormatter = {
    ToRand: function (amount) {
        return "R " + ValueFormatter.CommaSeparateNumber((1 * amount).toFixed(2));
    },

    CommaSeparateNumber: function (val) {
        if (val !== null) {
            val = val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
        else {
            val = '';
        }
        return val;
    },

    RemoveCommaSeparateNumber: function (val) {
        if (val !== '') {
            val = parseFloat(val.replace(/,/g, ""));
        }
        else {
            val = '';
        }
        return val;
    },
    ChartNumberFormat: function (number, decimals, dec_point, thousands_sep) {
        // *     example: number_format(1234.56, 2, ',', ' ');
        // *     return: '1 234,56'
        number = (number + '').replace(',', '').replace(' ', '');
        var n = !isFinite(+number) ? 0 : +number,
            prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
            sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
            dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
            s = '',
            toFixedFix = function (n, prec) {
                var k = Math.pow(10, prec);
                return '' + Math.round(n * k) / k;
            };
        // Fix for IE parseFloat(0.55).toFixed(0) = 0;
        s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
        if (s[0].length > 3) {
            s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
        }
        if ((s[1] || '').length < prec) {
            s[1] = s[1] || '';
            s[1] += new Array(prec - s[1].length + 1).join('0');
        }
        return s.join(dec);
    }
}


var DomManipulate = {
    GenerateHiddenInput: function (name, value) {
        return '<input type="hidden" name="' + name + '" value="' + value + '" />';
    }
};

var CustomFn = {
    SetCookie:function (cname, cvalue, exdays) {
        const d = new Date();
d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
let expires = "expires=" + d.toUTCString();
document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
},

GetCookie:function (cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
},

CheckCookie:function (name) {
    let user = CustomFn.GetCookie(name);
    if (user != "") {
        return true;
    } else {
        return false;
    }
}
}