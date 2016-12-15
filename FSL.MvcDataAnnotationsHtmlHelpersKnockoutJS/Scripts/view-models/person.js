var personViewModel = function () {

    var _vm = null,

    createComputed = function () {
        _vm.isEnableToEdit = ko.computed(function () {
            return (_vm.person.Name() || '').length > 2;
        }, _vm);
    },

    init = function (model) {
        _vm = {
            person: ko.mapping.fromJS(model.Person),
            genders: [
                { Id: 0, Name: 'Select...' },
                { Id: 1, Name: 'Masc' },
                { Id: 2, Name: 'Fem' }
            ]
        };

        createComputed();

        var ctx = $('#person').get(0);
        ko.applyBindings(_vm, ctx);
    }

    return {
        init: init
    }

}();