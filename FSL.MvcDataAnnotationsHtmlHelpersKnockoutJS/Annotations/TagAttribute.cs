using System;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class TagAttribute : Attribute
    {
        public TagAttribute(string tagName, string tagValue)
            : base()
        {
            TagName = tagName;
            TagValue = tagValue;
            InitData();
        }

        public TagAttribute(string tagValue)
            : base()
        {
            TagValue = tagValue;
            InitData();
        }

        public string TagName { get; set; }
        public string TagValue { get; set; }
        public virtual string OriginalValue { get { return TagValue; } }

        public virtual string GetValue()
        {
            return TagValue;
        }

        public string GetTagName()
        {
            return TagName;
        }
        public virtual void InitData()
        {

        }
    }
}