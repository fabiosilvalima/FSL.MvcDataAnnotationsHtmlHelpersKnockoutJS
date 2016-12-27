using FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Models
{
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

        [Display(Name = "Salary")]
        [NoDataValTag]
        [DoubleDataBind("person.Salary")]
        [CssClassTag("form-control")]
        public double? Salary { get; set; }
    }
}