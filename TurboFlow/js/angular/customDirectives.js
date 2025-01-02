
(function () {
    var app = angular.module("customDirectives", ['ivh.treeview', 'ui.mask', 'ngMask', 'ui.utils.masks']);
    app.directive('appFilter', function () {
        var directive = {};
        directive.restrict = "E";
        directive.template = "<div ng-transclude ></div>";
        directive.transclude = true;
        directive.scope = {
            model: '='
        };
        directive.link = function (scope, elem, attrs) {

            if (scope.model == null) {
                scope.model = {};
            }

            scope.model.getFilters = function () {
                var childs = elem.find("[ng-model]");
                var arrFilter = [];
                for (var i = 0; i < childs.length; i++) {
                    var child = $(childs[i]);

                    var modelParts = child.attr("ng-model").split(".");
                    var model = modelParts[modelParts.length - 1];

                    var fieldName = child.attr("fieldname");
                    if (fieldName == null) {
                        fieldName = model;
                    }

                    var filterItem = {};

                    filterItem.Value = scope.model[model];
                    filterItem.FieldName = fieldName;
                    filterItem.Comparison = child.attr("comparison");
                    if (filterItem.Comparison == null) {
                        filterItem.Comparison = "Equals";
                    }
                    arrFilter.push(filterItem);
                }

                console.log(arrFilter);

                return arrFilter;

            }

        };

        return directive;
    });

    app.directive('tooltip', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                $(element)
                    .attr('title', scope.$eval(attrs.tooltip))
                    .tooltip({ placement: "right" });

            }
        }
    })
     

    app.directive('uiModal', function () {
        var directive = {};

        directive.restrict = "E";
        directive.template = '<div ng-transclude class="modal fade" role="dialog" style="display:none; height: auto; max-height: 565px;  >';
        directive.transclude = true;
        directive.scope = {
            modal: '=',
            size: '@',
            classpara: '@'
        };

        directive.link = function (scope, element, attrs) {

            var div = $("[role=dialog]:first", element);

            for (var attr in attrs.$attr) {
                if (attrs.hasOwnProperty(attr)) {
                    div.attr(attrs.$attr[attr], attrs[attr]);
                }
            }



            div.addClass(scope.size);
            div.addClass(scope.classpara);

            if (scope.modal == null) {
                scope.modal = {};
            }

            scope.modal.open = function (tabNumber, tabActiveID) {
                if (hasTabs) {

                    if (tabNumber == 'undefined') {
                        tabNumber = '0';
                    }

                    if (tabActiveID == 'undefined' || tabActiveID == '') {
                        tabActiveID = null;
                    }

                    $(".tab-pane").removeClass("active in");

                    var id = "#" + div.find(".nav-tabs").attr("id");
                    var tabContentId = "#" + div.find(".tab-content").attr("id");

                    $(id + " li:eq(" + tabNumber + ") a").tab('show');

                    if (tabActiveID != null) {
                        tabContentId = "#" + tabActiveID;
                        $(tabContentId).addClass("fade active in");
                    }
                    else {
                        $(tabContentId + " > div:first-child").addClass("fade active in");
                    }
                }
                div.modal('show');
            };

            scope.modal.close = function () {
                div.modal('hide');
            };

            var hasTabs = div.find(".modal-body")[0].hasAttribute("has-tabs");

        };
        return directive;
    });

    app.directive('datetimepicker', function () {
        var directive = {};

        directive.restrict = "E";
        directive.require = 'ngModel';
        directive.template = '<input type="text" class="form-control" ng-model="selectedDate" ng-change="dateChanged" ng-required="isRequired" ng-disabled="isDisabled"/>';
        directive.scope = {
            datetimepickerOptions: '=',
            isRequired: '@',
            modelValue: '=ngModel',
            isDisabled: '@'
        };

        directive.link = function (scope, element, attr, modelController) {
            var $input = $(element).find("input");
            scope.datetimepickerOptions = scope.datetimepickerOptions != undefined ? scope.datetimepickerOptions : {};

            $input.datetimepicker(scope.datetimepickerOptions);

            $input.on('dp.change', function (event) {
                $input.data("DateTimePicker").date(this.value);
                $input.data("DateTimePicker").viewDate(this.value);
                scope.selectedDate = this.value;
                modelController.$setViewValue(this.value);
            });

            scope.isRequired = attr.hasOwnProperty("isRequired");

            scope.$watch('isRequired', function (newVal) {
                if (typeof newVal === 'string') {
                    scope.isRequired = newVal === "true";
                }
            });

            scope.isDisabled = attr.$attr.hasOwnProperty("isDisabled");

            scope.$watch('isDisabled', function (newVal) {
                if (typeof newVal === 'string') {
                    scope.isDisabled = newVal === "true";
                }
            });

            modelController.$formatters.push(function (modelValue) {
                scope.selectedDate = modelValue;
            });
        };
        return directive;
    });

    app.directive('datepicker', function () {
        var directive = {};

        directive.restrict = "E";
        directive.require = 'ngModel';
        directive.template = '<div>' +
            '<input type="text" class="form-control" ng-model="selectedDate" ng-change="dateChanged" ng-required="isRequired" ng-hide="isHide"  ng-disabled="isDisabled"/></div>';
        directive.scope = {
            datepickerOptions: '=',
            isRequired: '@',
            modelValue: '=ngModel',
            minDate: '=',
            isDisabled: '@',
            placeholder: '@'
        };

        directive.link = function (scope, element, attr, modelController) {
            var $input = $(element).find("input");
            scope.datepickerOptions = scope.datepickerOptions != undefined ? scope.datepickerOptions : {};

            var defaults = {
                orientation: "bottom auto",
                format: "mm/dd/yyyy"
            };

            if (scope.minDate != null) {
                defaults.startDate = scope.minDate
            }

            scope.datepickerOptions = _.defaults(scope.datepickerOptions, defaults);

            $(element).datepicker(scope.datepickerOptions);

            scope.isRequired = attr.hasOwnProperty("isRequired");

            scope.$watch('isRequired', function (newVal) {
                if (typeof newVal === 'string') {
                    scope.isRequired = newVal === "true";
                }
            });

            scope.isDisabled = attr.$attr.hasOwnProperty("isDisabled");

            scope.$watch('isDisabled', function (newVal) {
                if (typeof newVal === 'string') {
                    scope.isDisabled = newVal === "true";
                }
            });

            //Verificamos si no ha cambiado nuestra fecha minima...
            scope.$watch('minDate', function (newVal) {
                if (typeof newVal === 'string') {
                    var selectedDate = $(element).datepicker("getDate");
                    var strDate = moment(newVal, "MM/DD/YYYY");


                    if (selectedDate != null) {
                        if (strDate._d > selectedDate) {
                            strDate = moment(strDate).format("MM/DD/YYYY");
                            scope.selectedDate = strDate;
                            modelController.$setViewValue(strDate);
                        }
                    }

                    $(element).data('datepicker').setStartDate(newVal);

                }
            });

            scope.isHide = attr.$attr.hasOwnProperty("isHide");

            $input.on("change", function () {
                var selectedDate = $(element).datepicker("getDate");
                var strDate = moment(this.value, "MM/DD/YYYY");

                if (strDate != null) {
                    strDate = moment(strDate).format("MM/DD/YYYY");
                }
                else {
                    $(element).datepicker("setDate", '');
                    scope.selectedDate = "";
                    modelController.$setViewValue("");
                    return
                }

                if (strDate.indexOf("/") != -1) {
                    scope.selectedDate = strDate;
                    modelController.$setViewValue(strDate);
                } else {
                    $(element).datepicker("setDate", '');
                    scope.selectedDate = "";
                    modelController.$setViewValue("");
                }
            });

            $(element).datepicker().on("changeDate", function (e) {

            });

            modelController.$formatters.push(function (modelValue) {
                var value = modelValue === undefined ? "" : modelValue;
                $(element).datepicker("setDate", value != "" ? value : new Date().toLocaleDateString());
                scope.selectedDate = value;
            });
        };
        return directive;
    });

    app.directive('datepickerRange', function () {
        var directive = {};

        directive.restrict = "E";
        directive.require = 'ngModel';
        directive.template = '<div ng-model="dates" class="input-daterange input-group" id="datepicker"> ' +
                                '<input ng-model="StartDate" type="text" class="input-sm form-control" name="start" ng-required="isRequired" ng-disabled="isDisabled"/>' +
                                '<span class="input-group-addon">{{labelSeparator}}</span>' +
                                '<input ng-model="EndDate" type="text" class="input-sm form-control" name="end" ng-required="isRequired" ng-disabled="isDisabled"/>' +
                            '</div>';
        directive.scope = {
            datepickerOptions: '=',
            labelSeparator: '@',
            modelValue: '=ngModel',
            isDisabled: '@',
        };

        directive.link = function (scope, element, attr, modelController) {
            var $inputStart = $(element).find("[name='start']");
            var $inputEnd = $(element).find("[name='end']");

            scope.dates = {};
            scope.isRequired = attr.$attr.hasOwnProperty("isRequired");
            scope.isDisabled = attr.$attr.hasOwnProperty("isDisabled");;
            scope.labelSeparator = scope.labelSeparator != undefined ? scope.labelSeparator : "A";
            scope.datepickerOptions = scope.datepickerOptions != undefined ? scope.datepickerOptions : {};

            var defaults = {
                orientation: "bottom auto",
                format: "mm/dd/yyyy"
            };

            scope.datepickerOptions = _.defaults(scope.datepickerOptions, defaults);

            $(".input-daterange").datepicker(scope.datepickerOptions);

            // Jvillarreal se cambia changeDate por change (Ya que no entraba cuando era empty)
            $inputStart.datepicker().on("change", function (e) {
                scope.dates.StartDate = this.value;
                modelController.$setViewValue(scope.dates);
                //Jvillarreal, si no tiene valores reinicializa range picker
                if ($inputStart.val() == "" && $inputEnd.val() == "") {
                    $(".input-daterange").datepicker("destroy");
                    $(".input-daterange").datepicker(scope.datepickerOptions);
                    $inputStart.val('');
                    $inputStart.focus();
                }
            });

            // Jvillarreal se cambia changeDate por change (Ya que no entraba cuando era empty)
            $inputEnd.datepicker().on("change", function (e) {
                scope.dates.EndDate = this.value;
                modelController.$setViewValue(scope.dates);
                //Jvillarreal, si no tiene valores reinicializa range picker
                if ($inputStart.val() == "" && $inputEnd.val() == "") {
                    $(".input-daterange").datepicker("destroy");
                    $(".input-daterange").datepicker(scope.datepickerOptions);
                    $inputEnd.val('');
                    $inputEnd.focus();
                }
            });

            $inputStart.datepicker().on("changeDate", function (e) {
                scope.dates.StartDate = this.value;
                modelController.$setViewValue(scope.dates);
            });

            // Jvillarreal se cambia changeDate por change (Ya que no entraba cuando era empty)
            $inputEnd.datepicker().on("changeDate", function (e) {
                scope.dates.EndDate = this.value;
                modelController.$setViewValue(scope.dates);
            });

            scope.$watch('isDisabled', function (newVal) {
                if (typeof newVal === 'string') {
                    scope.isDisabled = newVal === "true";
                }
            });

            modelController.$formatters.push(function (modelValue) {
                scope.dates.StartDate = modelValue.StartDate;
                scope.dates.EndDate = modelValue.EndDate;

                $inputStart.datepicker("setDate", modelValue.StartDate);
                $inputEnd.datepicker("setDate", modelValue.EndDate);

                $inputStart.val(modelValue.StartDate);
                $inputEnd.val(modelValue.EndDate);



                modelController.$setViewValue(scope.dates);
            });
        };
        return directive;
    });

    app.directive('pageSelect', function () {
        return {
            restrict: 'E',
            template: '<select type="text" class="select-page" ng-model="inputPage" ng-change="selectPage(inputPage)"' +
                'style="padding:0; line-height: normal; height:15px !important; min-height:15px !important; width:40px; min-width:40px; font-size:13px; margin-bottom:0px !important;"  ng-options="item.value as item.text for item in itemsPager" ></select>',
            link: function (scope, element, attrs) {



                scope.$watch('currentPage', function (page) {
                    scope.inputPage = page;
                });

                scope.$watch('numPages', function (page) {
                    scope.itemsPager = [];

                    for (var i = 0; i < scope.numPages; i++) {
                        var item = { value: (i + 1), text: (i + 1).toString() };
                        scope.itemsPager.push(item);
                    }
                });
            }
        };
    });

    app.directive('stPaginationScroll', ['$timeout', function (timeout) {
        return {
            require: 'stTable',
            link: function (scope, element, attr, ctrl) {
                var itemByPage = 20;
                var pagination = ctrl.tableState().pagination;
                var lengthThreshold = 50;
                var timeThreshold = 400;
                var handler = function () {
                    //call next page
                    ctrl.slice(pagination.start + itemByPage, itemByPage);
                };
                var promise = null;
                var lastRemaining = 9999;
                var container = angular.element(element.parent());

                container.bind('scroll', function () {
                    var remaining = container[0].scrollHeight - (container[0].clientHeight + container[0].scrollTop);

                    //if we have reached the threshold and we scroll down
                    if (remaining < lengthThreshold && (remaining - lastRemaining) < 0) {

                        //if there is already a timer running which has no expired yet we have to cancel it and restart the timer
                        if (promise !== null) {
                            timeout.cancel(promise);
                        }
                        promise = timeout(function () {
                            handler();

                            //scroll a bit up
                            container[0].scrollTop -= 500;

                            promise = null;
                        }, timeThreshold);
                    }
                    lastRemaining = remaining;
                });
            }

        }
    }]);

    app.directive('keyEnter', function () {
        return function (scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.keyEnter);
                    });

                    event.preventDefault();
                }
            });
        };
    });

    app.directive('jsGrid', function () {
        var directive = {};

        directive.restrict = "E";
        directive.template = '<div id="{{gridId}}"></div>';
        directive.scope = {
            rows: '=',
            gridId: '@'
        };
        directive.link = function (scope, element, attr, modelController) {

            var gridID = scope.gridId;

            $scope.$watch('rows', function (newValue) {
                var settings = Ex.GetDefaultSettingsJsGrid();

                settings.id = 'table_' + gridID,
                settings.controller = {
                    loadData: function () {
                        var d = $.Deferred();
                        var datos = []
                        datos.push({});
                        datos.push({});
                        d.resolve(datos);
                        return d.promise();
                    }
                };
                settings.height = "auto",
                settings.fields = [
                    { name: "TipoTelefono", type: "text", width: 200, title: 'A' },
                    { name: "CodigoPais", type: "number", width: 200, title: 'b' },
                    { name: "Telefono", type: "text", width: 200, title: 'C' }
                ],
                $("#" + gridID + "").jsGrid("destroy");
                $("#" + gridID + "").jsGrid(settings);
            });


        };
        return directive;
    });

    app.directive('restrictInput', [function () {

        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var ele = element[0];
                var regex = RegExp(attrs.restrictInput);
                var value = ele.value;

                ele.addEventListener('keyup', function (e) {
                    if (regex.test(ele.value)) {
                        value = ele.value;
                    } else {
                        ele.value = value;
                    }
                });
            }
        };
    }]);

    app.directive('upload', ['$interval', 'dateFilter', function ($interval, dateFilter) {
        return {
            scope: {

                // funcion al terminar de subir el archivo
                done: '&',
                // nombre de la variable de sesion
                sesionName: '@',
                //nombre de la url del handler para subir la pagina
                url: '@',
                // objeto para guardar informacion del archivo del cliente 
                file: '=ngModel',
                //validar extension
                acceptFileTypes: '@'

            },
            require: 'ngModel',
            controller: function () {
                var timeoutId;
                var $me = this;
                $me.name = '';
                $me.updateTime = function () {
                    $me.name = $me.name.length > 2 ? '' : $me.name + '.';
                };
                $me.endTime = function () {
                    $interval.cancel(timeoutId);
                };
                $me.starTime = function () {
                    $me.name = '';
                    timeoutId = $interval(function () {
                        $me.updateTime();
                    }, 500);
                };
            },
            controllerAs: 'ctrl',
            template:
              '<div class="control-group">' +
                    '<div class="controls">' +
                        '<span class="btn btn-white fileinput-button" title="ADJUNTAR">' +
                            '<i class="fa fa-paperclip"></i>' +
                            '<input type="file" name="file" ng-model="file">' +
                            '<span>&nbsp;{{ !file.isBusy ? "" :  "Cargando" + ctrl.name }}&nbsp;</span>' +
                        '</span>' +
                    '</div>' +
                '</div>',
            replace: true,
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

                scope.file = scope.file || {};

                var inputEL = $(element).find('input[type=file]')

                $(inputEL).on("change", function (event) {
                    var $path = $(this).val();
                    scope.$apply(function () {

                        if (scope.file == null)
                        { scope.file = scope.file || {}; }

                        scope.file.path = $path;
                        //scope.path = event.target.files[0].name;
                        //scope.fileread = event.target.files[0];
                        // or all selected files:
                        // scope.fileread = event.target.files;
                    });
                });


                //http://stackoverflow.com/questions/15549094/jquery-file-upload-plugin-how-to-validate-files-on-add
                inputEL.fileupload({
                    runSubmitOnChange: false,
                    dataType: 'json',
                    url: scope.url,
                    //acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,                    
                    formData: { "SesionName": scope.sesionName },
                    start: function (e) {
                        var $me = this;
                        scope.$apply(function () {
                            scope.file.isBusy = true;
                            scope.ctrl.starTime();
                            $($me).attr('disabled', 'disabled');
                            $($me).parent('span').addClass('disabled');
                        });
                    },
                    add: function (e, data) {
                        var regex = RegExp(scope.acceptFileTypes || '.');
                        if (regex.test(data.files[0].name)) {
                            scope.file.inValid = false;
                            scope.$apply();
                            data.submit();
                        }
                        else {
                            //alert('Extension no valida');
                            scope.file.inValid = true;
                            item.
                                        scope.file.path = "";
                            scope.file.isBusy = false;
                            scope.$apply();
                            return false;
                        }
                    },
                    fail: function (e, data) {
                        var $me = this;
                        scope.$apply(function () {
                            scope.file.isBusy = false;
                            scope.ctrl.endTime();
                            $($me).removeAttr('disabled', 'disabled');
                            $($me).parent('span').removeClass('disabled');
                        });
                        alert("Error: " + data.errorThrown + " Text-Status: " + data.textStatus);
                    },
                    done: function (e, data) {
                        var $me = this;
                        var $params = {};
                        $params.e = e;
                        $params.data = data;
                        scope.$apply(function () {
                            scope.file.isBusy = false;
                            scope.ctrl.endTime();
                            $($me).removeAttr('disabled', 'disabled');
                            $($me).parent('span').removeClass('disabled');
                            scope.done({ "e": $params.e, "data": $params.data });
                        });
                    }
                });
            }
        };
    }]);

    app.directive('decimal', function () {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/[^0-9.]/g, '');

                        if (digits.split('.').length > 2) {
                            digits = digits.substring(0, digits.length - 1);
                        }

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }
                        return parseFloat(digits);
                    }
                    return "";
                }
                ctrl.$parsers.push(inputValue);
            }
        };
    });

    app.directive('exSearch', ["_", "config", "server", function (_, config, server) {
        var directive = {};
        directive.template = function (element, attr) {

            var minlenght = attr.minimumInputLength == null ? "3" : attr.minimumInputLength;
            var template = '<div class="ui fluid category search" >' +
                                '<div class="ui icon input fluid">' +
                                    '<input class="prompt fluid" type="text" placeholder="{{placeholder}}" ng-disabled="disabled">' +
                                    '<i class="search icon"></i> ' +
                                '</div>' +
                                '<div class="results"></div>' +
                           '</div>';
            return template;
        }

        directive.restrict = "E";

        directive.scope = {
            control: '=?',
            source: '@',
            filter: '=?',
            default: '@',
            constructor: '@',
            disabled: '=ngDisabled',
            labelName: '@',
            onSelected: '&',
            minCharacters: '@',
            onChange: '&',
            selectedItem: '=?'
        };

        directive.require = ['ngModel', 'exSearch'];

        directive.link = function (scope, element, attr, controllers) {

            var modelController = controllers[0];
            var ctrl = controllers[1];
            if (scope.labelName == null) {
                scope.labelName = "Name";
            }

            scope.freeText = attr.freeText;
            if (scope.control == null) {
                scope.control = {};
            }


            scope.config = {};

            if (scope.minCharacters == null) {
                scope.minCharacters = 3;
            }

            var $search = $(".ui.search", element);

            var path = config.apiUrl + "Source/Search" + scope.source + "/{query}";

            var apiSettings = {
                url: path,
                method: 'post',
                beforeSend: function (settings) {
                    settings.urlData.query = settings.urlData.query.replaceAll('/', '').replaceAll('&', '').trim();
                    settings.data = scope.filter;
                    return true;
                }
            }

            var userSelected = false;

            $search
                .search({
                    apiSettings: apiSettings,
                    maxResults: 20,
                    cache: false,
                    showNoResults: true,
                    fields: {
                        results: 'items',
                        title: 'Name',
                        description: 'Description'
                    },
                    onResults: function (response) {
                        if (scope.constructor != null) {
                            server.createAll(scope.constructor, response.items);
                        }
                    },
                    onSelect: function (result, response) {
                        scope.selectedItem = result;
                        scope.control.selectedItem = result;
                        userSelected = true;
                        modelController.$setViewValue(result.Id);
                        if (scope.onSelected != null) {
                            scope.onSelected({ item: result });
                        }
                        scope.$apply();
                    },
                    minCharacters: scope.minCharacters
                });

            var $input = $(".prompt", $search);

            $input.change(function () {
                if (scope.freeText != null) {
                    //scope.onSelected({ item: { Id: 0, Name: $input.val() } });
                    scope.onChange({ item: { Id: 0, Name: $input.val() } });
                } else {
                    if ($input.val() === "") {
                        scope.selectedItem = null;
                        scope.control.selectedItem = null;
                        modelController.$setViewValue(null);
                    }
                }

            });


            scope.clearSelection = function () {
                $input.val("");
            };

            modelController.$formatters.push(function (modelValue) {
                if (modelValue == null) {
                    scope.selectedItem = null;
                    scope.control.selectedItem = null;
                    $input.val("");
                } else {
                    if (!userSelected) {
                        if (isNaN(modelValue)) {
                            $input.val(modelValue);
                        } else {
                            $("div.search", element).addClass("loading");
                            ctrl.getItem("", modelValue).
                                then(function (item) {
                                    $("div.search", element).removeClass("loading");
                                    if (item != null) {
                                        item.selectedById = true;
                                        $input.val(item.Name);
                                        if (scope.onSelected != null) {
                                            scope.onSelected({ item: item });
                                        }
                                    }
                                });
                        }

                    }
                }
                userSelected = false;
                return modelValue;
            });

        };

        directive.controller = dropDownController;
        directive.controllerAs = "ctrl";

        return directive;

    }]);

    app.directive('allowPattern', [allowPatternDirective]);

    function allowPatternDirective() {
        return {
            restrict: "A",
            compile: function (tElement, tAttrs) {
                return function (scope, element, attrs) {
                    // I handle key events
                    element.bind("keypress", function (event) {
                        var keyCode = event.which || event.keyCode; // I safely get the keyCode pressed from the event.
                        var keyCodeChar = String.fromCharCode(keyCode); // I determine the char from the keyCode.

                        // If the keyCode char does not match the allowed Regex Pattern, then don't allow the input into the field.
                        if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                            event.preventDefault();
                            return false;
                        }

                    });
                };
            }
        };
    }


})();

