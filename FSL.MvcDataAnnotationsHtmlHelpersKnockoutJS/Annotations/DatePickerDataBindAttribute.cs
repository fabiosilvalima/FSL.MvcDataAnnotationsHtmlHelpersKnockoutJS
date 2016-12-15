using System;
using System.Text;
using System.Threading;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DatePickerDataBindAttribute : DataBindAttribute
    {
        public DatePickerDataBindAttribute(string value)
            : this(value, "dd/mm/yy")
        {

        }

        public DatePickerDataBindAttribute(string value, string formato)
            : base(value)
        {
            _cultura = Thread.CurrentThread.CurrentCulture.Name;
            _formato = formato;
        }
        
        private string _cultura = "";
        private string _formato = "";
        
        public override void InitData()
        {
            base.InitData();
        }
        
        public override string GetValue()
        {
            var value = TagValue;
            var date = new DateDataBindAttribute(value);
            var sb = new StringBuilder();
            sb.Append(date.GetValue());
            sb.Append(", datePicker: { ");
            sb.AppendFormat("formatDate: '{0}', culture: '{1}', value: {2}", _formato, _cultura, value);
            sb.Append(" }");

            return sb.ToString();
        }
    }
}