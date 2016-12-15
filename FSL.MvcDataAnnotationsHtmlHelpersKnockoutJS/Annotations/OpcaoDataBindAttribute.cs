using System;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class OptionsDataBindAttribute : DataBindAttribute
    {
        public OptionsDataBindAttribute(string options, string value, bool usarSomenteValue)
            : base("")
        {
            _usarSomenteValue = usarSomenteValue;
            _options = options;
            _value = value;
            TagValue = MontarTagValue();
        }
        
        public OptionsDataBindAttribute(string options, string value)
            : base("")
        {
            _options = options;
            _value = value;
            TagValue = MontarTagValue();
        }
        
        public OptionsDataBindAttribute(string options, string value, string evento)
            : base("")
        {
            _options = options;
            _value = value;
            _evento = evento;
            TagValue = MontarTagValue();
        }

        private bool _usarSomenteValue;
        private string _options;
        private string _value;
        private string _evento;

        public override string OriginalValue
        {
            get
            {
                return _value;
            }
        }

        public string MontarTagValue()
        {
            var options = "options: {0}";
            var optionsText = "optionsText: 'Name'";
            var value = "value: {1}";
            if (!_usarSomenteValue)
            {
                var optionsValue = "optionsValue:'Id'";
                var str = string.Concat(options, ", ", optionsText, ", ", optionsValue, ", ", value);
                if (!string.IsNullOrEmpty(_evento))
                {
                    str = string.Concat(str, ", ", "event: [ change: {2} ]");
                    str = string.Format(str, _options, _value, _evento)
                        .Replace("[", "{")
                        .Replace("]", "}");
                    return str;
                }

                return string.Format(str, _options, _value);
            }
            else
            {
                return string.Format("options: {0}, optionsText: 'Name', value: {1}", _options, _value);
            }
        }
    }
}