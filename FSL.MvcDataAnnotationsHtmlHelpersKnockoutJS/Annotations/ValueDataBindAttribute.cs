using System;
using System.Text;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class ValueDataBindAttribute : DataBindAttribute
    {
        public ValueDataBindAttribute(string value)
            : base("")
        {
            Mask = MaskDataBindAttribute.MaskTypes.None;
            _valueTagName = "value";
            _value = value;
            TagValue = BuildValueTag();
        }

        protected virtual int? Precision { get; set; }
        protected string _valueTagName;
        protected string _value;
        protected MaskDataBindAttribute.MaskTypes Mask { get; set; }
        protected long? MaxLeftDigits { get; set; }

        public string BuildValueTag()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}", _valueTagName, _value);

            if (Mask != MaskDataBindAttribute.MaskTypes.None)
            {
                var mask = new MaskDataBindAttribute(Mask, _value);
                mask.Precision = Precision;
                mask.MaxLeftDigits = MaxLeftDigits;
                var mascara = mask.MontarTagValue();

                sb.AppendFormat(", {0}", mascara);
            }

            return sb.ToString();
        }
    }
}