using System;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class DateDataBindAttribute : ValueDataBindAttribute
    {
        public DateDataBindAttribute(string value)
            : base("")
        {
            Mask = MaskDataBindAttribute.MaskTypes.Date;
            _valueTagName = "dateValue";
            _value = value;

            var tagValue = string.Format("{0}, {1}",
                new ValueDataBindAttribute(value).BuildValueTag(),
                BuildValueTag());

            TagValue = tagValue;
        }
    }
}