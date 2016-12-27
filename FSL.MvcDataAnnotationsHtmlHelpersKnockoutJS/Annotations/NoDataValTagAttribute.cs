using System;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NoDataValTagAttribute : TagAttribute
    {
        public NoDataValTagAttribute()
            : base("data-val", "false")
        {

        }
    }
}