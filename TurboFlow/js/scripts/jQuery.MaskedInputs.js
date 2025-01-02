var NumberDecimalSeparator = ".";
var NumberGroupSeparator = ",";
var CurrencySymbol = "$";

(function ($) {
    var methods = {
        init: function (options) {
        },
        destroy: function () {
            return this.each(function () {
                $(window).unbind('.maskOnLeave');
            })
        },
        nothing: function () {

        },
        percent: function (elements, decimals) {
            // elements can be multiple selector
            var expression = new RegExp("^(\\d)+(\\" + NumberGroupSeparator + "\\d*)*(?:\\" + NumberDecimalSeparator + "\\d{0," + decimals + "})?%?$");
            registerKeyPressValidation(elements, expression);
            elements.attr("data-IsNumeric", "true");
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();
                if ($.trim(value) != "") {
                    value = value.replace("%", "");
                    value = addGroupSeparators(value, decimals);
                    element.val(value + "%");
                }
            });
        },
        currency: function (elements, decimals) {
            var expression = new RegExp("^(\\" + CurrencySymbol + "\\s*)?((\\d)+(\\" + NumberGroupSeparator + "\\d*)*(?:\\" + NumberDecimalSeparator + "\\d{0," + decimals + "})?)?$");
            registerKeyPressValidation(elements, expression);
            elements.attr("data-IsNumeric", "true");
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();
                if ($.trim(value) != "") {
                    value = value.replace(CurrencySymbol, "");
                    value = addGroupSeparators(value, decimals);
                    value = value.replace(/\s/g, "");
                    if (value.length > 0)
                        element.val(CurrencySymbol + " " + value);
                    else
                        element.val("");
                }
            });
        },
        numeric: function (elements, decimals, soloNumeros) {
            var expression = decimals === 0
                ? new RegExp("^[0-9]+$")
                : new RegExp("^(\\d)+(\\" + NumberGroupSeparator + "\\d*)*(?:\\" + NumberDecimalSeparator + "\\d{0," + decimals + "})?$");
            registerKeyPressValidation(elements, expression);
            elements.attr("data-IsNumeric", "true");
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var bolAceptaCero = false;
                var aceptaCeros = element[0].getAttribute("aceptaCero");
                if(aceptaCeros!=null){
                    bolAceptaCero = true;
                }
                var value = element.val();
                if ($.trim(value) != "") {                    
                    value = addGroupSeparators(value, decimals, soloNumeros,bolAceptaCero);
                    element.val(value);
                
                }

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);
                
                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
                var isFormatCurrency = element.getAttribute("IsFormatCurrency");
                if (isFormatCurrency != null) {
                    if (isFormatCurrency) {
                        $('#' + element.id).formatCurrency();
                    }
                }
            });

        },
        alphabetic: function (elements) {
            var expression = new RegExp("^[a-zA-Z \s]*$");
            registerKeyPressValidation(elements, expression);
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });

        },
        nothing: function (elements) {
            elements.on("blur", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    Exertus.ValidateConstraint(element);
                }
            });

        },
        // Solo permite capturar letras y los caracteres especiales (.,-)
        especial: function (elements) {
            var expression = new RegExp("^[a-zA-Z-/.,áéíóúÁÉÍÓÚ \s]*$");
            registerKeyPressValidation(elements, expression);
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });

        },
        // Solo permite capturar letras,numeros y los caracteres -/
        referencia: function (elements) {
            var expression = new RegExp("^[a-zA-Z-/1234567890 \s]*$");
            registerKeyPressValidation(elements, expression);
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });

        },
        especialDescripcion: function (elements) {
            var expression = new RegExp("^[a-zA-Z#-_*()¡!¿?:1234567890 \s]*$");
            registerKeyPressValidation(elements, expression);
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });

        },
        sticker: function (elements) {
            var expression = new RegExp("^[1234567890\s]*$");
            registerKeyPressValidation(elements, expression);
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });

        },
        alphaNumeric: function (elements) {
            var expression = new RegExp("^[a-zA-Z_ 1234567890\s]*$");
            registerKeyPressValidation(elements, expression);
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });
        },
        correo: function (elements) {
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });
        },
        rfc: function (elements) {
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });
        },
        curp: function (elements) {
            elements.off(".maskOnLeave");
            elements.on("blur.maskOnLeave", function () {
                //element is the control wich trigger blur event
                var element = $(this);
                var value = element.val();

                if (element.context.OnValueChanged != null)
                    element.context.OnValueChanged(value);

                element = element[0];
                if (element.DataConstraint != null) {
                    var valid = Exertus.ValidateConstraint(element);
                    element.Valid = valid;
                    if (element.OnValidated != null)
                        element.OnValidated();
                }
            });
        }

       

    };

    var registerKeyPressValidation = function (element, regex) {
        element.keypress(function (e) {
            var key;
            key = e.which;
            var selectedText = GetSelectedText();
            var val = "";

            if (selectedText == "") {
                var caretPosition = GetCaret(this);
                //get the key character code and appends to the current value
                val = [this.value.slice(0, caretPosition), String.fromCharCode(key), this.value.slice(caretPosition)].join('');
            }
            else {
                val = this.value.replace(selectedText, String.fromCharCode(key));
            }

            //if the result string is correct then exit of the validation
            if (val.match(regex)) {
                return;
            }
            else {
                //Else cancel the key press event
                e.preventDefault();
            }
        });
    };

    $.fn.maskOnLeave = function (options) {
        var settings = $.extend({
            'format': null,
            'decimals': null,
            'SoloNumeros': null
        }, options);
        var elements = this;
        switch (settings.format) {
            case "percent":
                methods.percent(elements, settings.decimals);
                break;
            case "currency":
                methods.currency(elements, settings.decimals);
                break;
            case "numeric":
                methods.numeric(elements, settings.decimals, settings.SoloNumeros);
                break;
            case "alphabetic":
                methods.alphabetic(elements);
                break;
            case "nothing":
                methods.nothing(elements);
                break;   
            case "especial":
                methods.especial(elements);
                break;  
             case "especialDescripcion":
                methods.especialDescripcion(elements);
                break;  
            case "alphaNumeric":
                methods.alphaNumeric(elements);
                break;   
             case "correo":
                methods.correo(elements);
                break; 
              case "rfc":
                methods.rfc(elements);
                break;
            case "curp":
                methods.curp(elements);
                break;
            case "sticker":
                methods.sticker(elements);
                break;
            case "referencia":
                methods.referencia(elements);
                break;
            default:
                $.error('option format: ' + settings.format + ' does not exist on jQuery.maskOnLeave');
                break;
        }
        elements.each(function () {
            var control = $(this);
            control.attr("maskOnLeave", settings.format);
            var value = GetControlValue(control);
            control.data("decimals", settings.decimals);
            SetFormatedValue(control, settings.format, value, settings.decimals, settings.SoloNumeros);

            
        });
    };
})(jQuery);

function SetAllFormatMaskOnLeave() {
    $("[maskOnLeave]").each(function () {
        var control = $(this);
        var value = GetControlValue(control);
        var format = control.attr("maskOnLeave");
        var decimals = control.data("decimals");
        SetFormatedValue(control, format, value, decimals);
    });
}

(function ($) {
    $.widget("ui.EvaluateMath", {
        options: {
            'formula': '',
            'controls': [],
            'format': null,
            'decimals': 2,
            'evaluated': null
        },

        //Initializes the widget
        _create: function () {
            var self = this;
            var element = $(self.element);
            var options = self.options;
            element.attr("data-IsNumeric", "true");
            element.addClass("ui-EvaluateMath");
            var value = $.trim(GetControlValue(element));
            SetFormatedValue(element, options.format, value, options.decimals);

            for (i = 0; i < options.controls.length; i++) {
                // control wich depends the formula
                var evaluateMathEvents = ".EvaluateMath" + element.attr("id");

                options.controls[i].off(evaluateMathEvents);
                options.controls[i].on("change." + evaluateMathEvents, function () { self.calculate(); });
                options.controls[i].on("calculated." + evaluateMathEvents, function () { self.calculate(); });
            }
        },

        destroy: function () {
            this.removeClass("ui-EvaluateMath");
            this.unbind("." + this.widgetName)
    			.removeData(this.widgetName);
            this.widget().unbind("." + this.widgetName);
        },

        calculate: function () {
            var element = $(this.element);
            var options = this.options;

            var result;
            var numericResult;
            try {
                result = this.evaluate();
                numericResult = result;
                SetFormatedValue(element, options.format, result, options.decimals);
            }
            catch (ex) {
                result = "";
                element.val("");
            }
            if (options.evaluated != null) {
                options.evaluated(numericResult);
            }
            element.trigger("calculated");
        },

        evaluate: function () {
            var element = this.element;
            var options = this.options;

            var pattern = /\{\d+\}/g;
            var invalidCharacterPattern = new RegExp("[^\\d\\" + NumberDecimalSeparator + ")]", "g");
            //var invalidCharacterPattern = /[^\d\.)]/g;
            var args = new Array();
            for (i = 0; i < options.controls.length; i++) {
                var value;
                var currentControl = options.controls[i];
                if (currentControl.is("input")) {
                    value = currentControl.val();
                }
                else {
                    value = currentControl.html();
                }
                if ($.trim(value) == "") {
                    args[i] = 'NaN';
                }
                else {
                    args[i] = value.replace(invalidCharacterPattern, "").replace(NumberDecimalSeparator, ".");
                }

            }
            var result = options.formula.replace(pattern, function (capture) { return args[capture.match(/\d+/)]; });
            try {
                result = eval(result);
                if (isNaN(result)) {
                    result = "";
                }
            }
            catch (ex) {
                //alert("error on formula: " + options.formula);
                result = "";
            }
            return result;
        }
    });
})(jQuery);

function evaluateMathAll() {
    $(".ui-EvaluateMath").EvaluateMath("calculate");
}

function GetControlValue(control) {
    var result;
    if (control.is("input")) {
        result = control.val();
    }
    else {
        result = control.html();
    }
    return result;
}

function SetFormatedValue(control, format, value, decimals, soloNumeros) {
    value = value === "" ? "" : value;
    if (value != "") {
        switch (format) {
            case "numeric":
                value = addGroupSeparators(value, decimals, soloNumeros);
                break;
            case "percent":
                value = addGroupSeparators(value, decimals) + "%";
                break;
            case "currency":
                value = CurrencySymbol + " " + addGroupSeparators(value, decimals);
                break;
        }
    }

    if (control.is("input")) {
        control.val(value);
    }
    else {
        control.html(value);
    }
};


function GetCaret(element) {
    ///	<summary>
    /// Gets caret (cursor position) from element (can be txtbox or area)
    ///	</summary>
    if (element.selectionStart) {
        return element.selectionStart;
    }
    else if (document.selection) {
        element.focus();
        var r = document.selection.createRange();
        if (r == null) {
            return 0;
        }
        var re = element.createTextRange();
        rc = re.duplicate();
        re.moveToBookmark(r.getBookmark());
        rc.setEndPoint('EndToStart', re);
        return rc.text.length;
    }
    return 0;
}

function GetSelectedText() {
    ///	<summary>
    /// Gets selected text from the browser
    ///	</summary>
    var txt = '';
    if (window.getSelection) {
        txt = window.getSelection();
    }
    else if (document.getSelection) {
        txt = document.getSelection();
    } else if (document.selection) {
        txt = document.selection.createRange().text;
    } else return;
    return txt;
}

function addGroupSeparators(numberStr, decimals, soloNumeros, aceptaCero) {
    numberStr += '';
    if (isNaN(Number(numberStr))) {
        var invalidCharacterPattern = new RegExp("[^\\d\\" + NumberDecimalSeparator + ")]", "g");
        numberStr = numberStr.replace(invalidCharacterPattern, "")
        var replaceExprecion = new RegExp("\\" + NumberGroupSeparator, "g");
        numberStr = numberStr.replace(replaceExprecion, "").replace(NumberDecimalSeparator, ".");
    }
    else {
        numberStr = numberStr.replace(/\,/g, "");
    }


    if (decimals != null && (soloNumeros == 'undefined'   || soloNumeros == null || soloNumeros == false)  && aceptaCero==false) {

        numberStr = parseFloat(numberStr).toFixed(decimals).toString();
        //numberStr = numberStr.toFixed(decimals);
    }
    x = numberStr.split('.');
    integerPart = x[0];
    decimalPart = x.length > 1 ? NumberDecimalSeparator + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
//    while (rgx.test(integerPart)) {
//        integerPart = integerPart.replace(rgx, '$1' + NumberGroupSeparator + '$2');
//    }
    return integerPart + decimalPart;
}
 