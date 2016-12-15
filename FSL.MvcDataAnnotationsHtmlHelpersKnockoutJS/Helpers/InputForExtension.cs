using System;
using System.Linq.Expressions;
using System.Web.Mvc;

public static class InputForExtesion
{
    public static MvcHtmlString InputFor<TModel, TValue>(this HtmlHelper<TModel> html,
    Expression<Func<TModel, TValue>> expression, string htmlFieldName)
    {
        if (!string.IsNullOrEmpty(htmlFieldName))
        {
            htmlFieldName = string.Concat(htmlFieldName, "_", ExpressionHelper.GetExpressionText(expression));
        }

        var obj = System.Web.Mvc.Html.EditorExtensions.EditorFor(html, expression, null, htmlFieldName, null);
        return BaseExtension.BuildTag(expression, BaseExtension.ParseHtml(obj));
    }

    public static MvcHtmlString InputFor<TModel, TValue>(this HtmlHelper<TModel> html,
        Expression<Func<TModel, TValue>> expression)
    {
        return InputFor(html, expression, null);
    }
}