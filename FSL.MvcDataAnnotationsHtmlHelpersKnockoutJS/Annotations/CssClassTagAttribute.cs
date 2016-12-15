using System;
namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    public class CssClassTagAttribute : TagAttribute
    {
        public CssClassTagAttribute(String tagValue)
            : base("class", tagValue)
        {

        }
    }
}