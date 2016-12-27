using System;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class DoubleDataBindAttribute : ValueDataBindAttribute
    {
        public DoubleDataBindAttribute(string value, int decimais, long maxLeftDigits = -1)
            : base("")
        {
            _decimais = decimais;
            MontarDataBind(value, maxLeftDigits);
        }
        
        public DoubleDataBindAttribute(string value, string decimais, long maxLeftDigits = -1)
            : base("")
        {
            _decimaisObservable = decimais;
            MontarDataBind(value, maxLeftDigits);
        }

        public DoubleDataBindAttribute(string value, long maxLeftDigits = -1)
            : base("")
        {
            MontarDataBind(value, maxLeftDigits);
        }
        
        protected override int? Precision
        {
            get
            {
                return _decimais;
            }
            set
            {
                if (value.HasValue)
                {
                    _decimais = value.Value;
                }
            }
        }
        
        private void MontarDataBind(string value, long maxLeftDigits = -1)
        {
            Mask = MaskDataBindAttribute.MaskTypes.Currency;
            _valueTagName = "doubleValue";
            _value = value;
            MaxLeftDigits = maxLeftDigits.Equals(-1) ?
                new long?() :
                maxLeftDigits;

            var tagValue = string.Format("{0}, {1}",
                new ValueDataBindAttribute(value).BuildValueTag(),
                base.BuildValueTag());

            if (_decimais > 0)
            {
                tagValue = string.Format("{0}, {1}", tagValue,
                    new DataBindAttribute(string.Format("positions: {0}", _decimais.ToString())).TagValue);
            }

            if (!string.IsNullOrEmpty(_decimaisObservable))
            {
                tagValue = string.Format("{0}, {1}", tagValue,
                    new DataBindAttribute(string.Format("decimais: {0}", _decimaisObservable)).TagValue);
            }

            TagValue = tagValue;
        }

        private int _decimais = 0;
        private string _decimaisObservable = "";
    }
}