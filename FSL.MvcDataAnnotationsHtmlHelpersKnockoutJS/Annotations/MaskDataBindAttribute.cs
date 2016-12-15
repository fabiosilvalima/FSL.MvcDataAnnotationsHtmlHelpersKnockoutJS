using System;
using System.Text;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class MaskDataBindAttribute : DataBindAttribute
    {
        public enum MaskTypes
        {
            None,
            Date,
            Phone,
            Zipcode,
            Ip,
            Time
        }

        public MaskDataBindAttribute(string maskObservable)
            : base("")
        {
            _maskObservable = maskObservable;
            TagValue = MontarTagValue();
        }

        public MaskDataBindAttribute(MaskTypes mask)
            : base("")
        {
            _maskType = mask;
            switch (mask)
            {
                case MaskTypes.Date: _mask = "99/99/9999"; break;
                case MaskTypes.Phone: _mask = "(99) 9999-9999"; break;
                case MaskTypes.Zipcode: _mask = "99999-999"; break;
                case MaskTypes.Ip: _mask = "999.999.999.99"; break;
                case MaskTypes.Time: _mask = "99:99"; break;
            }
            TagValue = MontarTagValue();
        }

        public MaskDataBindAttribute(string mask, MaskTypes maskType)
            : base("")
        {
            _mask = mask;
            _maskType = maskType;
            TagValue = MontarTagValue();
        }

        public MaskDataBindAttribute(MaskTypes tipo, string value)
            : this(tipo)
        {
            Value = value;
        }

        private string _maskObservable;
        private string _mask;
        private MaskTypes _maskType;
        public string Value { get; set; }
        public int? Precision { get; set; }
        public long? MaxLeftDigits { get; set; }

        public string MontarTagValue()
        {
            var sb = new StringBuilder();
            sb.Append("mask: { mascara: ");
            if (!string.IsNullOrEmpty(_mask))
            {
                sb.AppendFormat("'{0}'", _mask);
            }
            else
            {
                sb.AppendFormat("{0}", !string.IsNullOrEmpty(_maskObservable) ? _maskObservable : "''");
            }
            sb.AppendFormat(", tipo: '{0}'", _maskType.ToString());

            if (Precision.HasValue)
            {
                sb.AppendFormat(", precision: {0}", Precision.Value.ToString());
            }

            if (MaxLeftDigits.HasValue)
            {
                sb.AppendFormat(", maxLeftDigits: {0}", MaxLeftDigits.Value.ToString());
            }

            if (!string.IsNullOrEmpty(Value))
            {
                sb.AppendFormat(", value: {0}", Value);
            }

            sb.Append(" }");

            return sb.ToString();
        }
    }
}