# FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS

**MVC Data Annotations and HtmlHelpers for KnockoutJS**

In this article I will show you how to create data annotations for MVC models and combine with HtmlHelpers to render all attributes of KnockoutJS in input fields.

UPDATES:
---
**Dez, 27 - 2016**
- DoubleDataBind - Currency/Double values for input
- NoDataValTag - Add data-val="false" attribute in input

---

What is in the source code?
---

#### <i class="icon-file"></i> FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS

- 1 - MVC application.
- 2 - DataBind Attribute classes for those KnockoutJS annotations.
- 2.1 - EnableDataBind: enables ou disables a field.
- 2.2 - MaskDataBind: input masks for a field.
- 2.3 - DateDataBind: transforms an input into a date mask field.
- 2.4 - OptionsDataBind: configure a select field with options.
- 3 - HtmlHelper to render an input with those annotations.
- 3.1 - InputFor: renders an input for KnockoutJS.
- 3.2 - SelectFor: renders a select for KnockoutJS.

> **Remarks:**

> - I created a MVC application application in Visual Studio 2015. 
> - Use the same virtual directory from this article

---

What is the goal?
---

- KnockoutJS is javascript plugin with Model-View-View-Model (MVVM) pattern. When your data model's state changes, your UI updates automatically. Like AngularJS it's provides a full control for a view and his model.
- In MVC there are a lot of Data Annotations for using in our Models. The goal here is to create a custom one's for KnockoutJS.

**Assumptions:**

- You need to create a virtual directory "FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS" in your IIS for that application.


Code:
---

All fields above are "handled" by KnockoutJS.

I configured some data annotations for Person model below.

**Models/Person.cs**
```csharp
public sealed class Person
    {
        [Display(Name = "Name")]
        [Required]
        [ValueDataBind("person.Name")]
        [CssClassTag("form-control")]
        public string Name { get; set; }

        [ValueDataBind("person.Id")]
        public string Id { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        [EnableDataBind("isEnableToEdit")]
        [ValueDataBind("person.Email")]
        [CssClassTag("form-control")]
        public string Email { get; set; }

        [Display(Name = "Bith Date")]
        [DateDataBind("person.BithDate")]
        [CssClassTag("form-control")]
        public DateTime? BithDate { get; set; }

        [Display(Name = "Gender")]
        [EnableDataBind("isEnableToEdit")]
        [OptionsDataBind("genders", "person.Gender")]
        [CssClassTag("form-control")]
        public int? Gender { get; set; }
    }
```

n HTML the usage is something like that:

**Views/Person/Index.cshtml**
```html
@Html.InputFor(x => x.Person.Name)
@Html.InputFor(x => x.Person.BithDate)
@Html.SelectFor(x => x.Person.Gender)
@Html.InputFor(x => x.Person.Email)
```

**It will render:**

```html
<input class="text-box single-line, form-control" data-val="true" data-val-required="The field Name is required." id="Person_Name" name="Person.Name" type="text" value="" data-bind="value: person.Name">

<input class="text-box single-line, form-control" data-val="true" data-val-date="The field Bith Date must be a date." id="Person_BithDate" name="Person.BithDate" type="text" value="" data-bind="value: person.BithDate, dateValue: person.BithDate, mask: { mascara: '99/99/9999', tipo: 'Date', value: person.BithDate }">

<select id="Person_Gender" name="Person.Gender" data-bind="enable: isEnableToEdit, options: genders, optionsText: 'Name', optionsValue:'Id', value: person.Gender" class="form-control"></select>

<input class="text-box single-line, form-control" data-val="true" data-val-email="O campo Email não é um endereço de email válido." data-val-required="The field Email is required" id="Person_Email" name="Person.Email" type="email" value="" data-bind="enable: isEnableToEdit, value: person.Email">
```

The javascript file bellow it's to control some data in KnockoutJS for the frontend. The "isEnableToEdit" computed function below it's used in EnableDataBind annotation in MVC Person model.

**Scripts/view-models/person.js**

```javascript
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
```

**REMARKS: I will update that repository with new Annotations and Html Helpers features.**

---------

References:
---

- More at my blog click here [fabiosilvalima.net][1];

Licence:
---

- Licence MIT


  [1]: http://fabiosilvalima.net
