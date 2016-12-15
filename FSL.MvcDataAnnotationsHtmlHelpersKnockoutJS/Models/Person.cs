using FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Models
{
    public sealed class Person
    {
        [Display(Name = "Name")]
        [Required]
        [EnableDataBind("isEnableToEdit")]
        [ValueDataBind("person.Name")]
        public string Name { get; set; }

        [ValueDataBind("person.Id")]
        public string Id { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        [ValueDataBind("person.Email")]
        public string Email { get; set; }

        [Display(Name = "Bith Date")]
        [DateDataBind("person.BithDate")]
        public DateTime? BithDate { get; set; }

        [Display(Name = "Calendar Date")]
        [DatePickerDataBind("person.CalendarDate")]
        public DateTime? CalendarDate { get; set; }

        [Display(Name = "Gender")]
        [OptionsDataBind("genders", "person.Gender", "onGenderClick")]
        public int? Gender { get; set; }
    }
}