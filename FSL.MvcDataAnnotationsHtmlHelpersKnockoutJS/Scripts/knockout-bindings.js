$(document).ready(function () {

    ko.bindingHandlers.mask = {

        init: function (element, valueAccessor, allBindingsAccessor) {
            ko.bindingHandlers.mask.processar(element, valueAccessor, allBindingsAccessor);
        },

        update: function (element, valueAccessor, allBindingsAccessor) {
            var mask = valueAccessor();
            ko.bindingHandlers.mask.createMask(element, ko.bindingHandlers.mask.getObservable(mask.mascara));
        },

        getObservable: function (valor) {
            if ($.isFunction(valor)) {
                var res = valor();
                return $.isFunction(res) ? res() : res;
            } else {
                return valor;
            }
        },

        processar: function (element, valueAccessor, allBindingsAccessor) {
            var mask = valueAccessor();
            var maxLeftDigits = mask.hasOwnProperty('maxLeftDigits') ? mask.maxLeftDigits : null;
            var options = null;
            if (maxLeftDigits !== null) {
                options = {
                    maxLeftDigits: mask.maxLeftDigits
                };
            }

            var mascara = ko.bindingHandlers.mask.getObservable(mask.mascara);
            ko.bindingHandlers.mask.createMask(element, mascara);
        },

        createMask: function (element, mask) {
            $(element).mask(mask);
        }
    };

    ko.bindingHandlers.dateValue = {

        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            ko.bindingHandlers.dateValue.processar($(element).val(), element, valueAccessor, allBindingsAccessor);
            $(element).change(function () {
                ko.bindingHandlers.dateValue.processar($(this).val(), element, valueAccessor, allBindingsAccessor);
            });
        },

        update: function (element, valueAccessor, allBindingsAccessor) {
            $(element).change();
        },

        processar: function (value, element, valueAccessor, allBindingsAccessor) {
            var formattedValue = ko.bindingHandlers.dateValue.formatDate(value);
            var acessor = valueAccessor();
            acessor(formattedValue);
            $(element).val(formattedValue);
        },

        formatDate: function (data) {
            if (data && data.length > 10) {
                data = data.substr(0, 10);
                if (data.indexOf('-') !== -1) {
                    var s = data.split('-');
                    data = s[2] + '/' + s[1] + '/' + s[0];
                } else {
                    //var s = data.split('/');
                    //var d = new Date(parseInt(s[2]), parseInt(s[1]) - 1, parseInt(s[0]));
                }
            }

            return data;
        }
    };

});