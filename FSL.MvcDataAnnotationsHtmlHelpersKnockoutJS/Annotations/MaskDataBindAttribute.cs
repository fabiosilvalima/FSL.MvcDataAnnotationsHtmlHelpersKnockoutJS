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
            Currency,
            Phone,
            Cellphone,
            Zipcode,
            Ip,
            Time,
            Cpf
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
            _mask = mask;
            switch (mask)
            {
                case MaskTypes.Date: _mascara = "99/99/9999"; break;
                case MaskTypes.Phone: _mascara = "(99) 9999-9999"; break;
                case MaskTypes.Cellphone: _mascara = "(99) 9999-9999?9"; break;
                case MaskTypes.Zipcode: _mascara = "99999-999"; break;
                case MaskTypes.Currency: _mascara = ""; break;
                case MaskTypes.Ip: _mascara = "999.999.999.99"; break;
                case MaskTypes.Time: _mascara = "99:99"; break;
                case MaskTypes.Cpf: _mascara = "999.999.999-99"; break;
            }
            TagValue = MontarTagValue();
        }

        public MaskDataBindAttribute(string mascara, MaskTypes tipo)
            : base("")
        {
            _mascara = mascara;
            _mask = tipo;
            TagValue = MontarTagValue();
        }

        public MaskDataBindAttribute(MaskTypes tipo, string value)
            : this(tipo)
        {
            Value = value;
        }

        private string _maskObservable;
        private string _mascara;
        private MaskTypes _mask;
        public string Value { get; set; }
        public int? Precision { get; set; }
        public long? MaxLeftDigits { get; set; }

        public string MontarTagValue()
        {
            var sb = new StringBuilder();
            sb.Append("mask: { mascara: ");
            if (!string.IsNullOrEmpty(_mascara))
            {
                sb.AppendFormat("'{0}'", _mascara);
            }
            else
            {
                sb.AppendFormat("{0}", !string.IsNullOrEmpty(_maskObservable) ? _maskObservable : "''");
            }
            sb.AppendFormat(", tipo: '{0}'", _mask.ToString());

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