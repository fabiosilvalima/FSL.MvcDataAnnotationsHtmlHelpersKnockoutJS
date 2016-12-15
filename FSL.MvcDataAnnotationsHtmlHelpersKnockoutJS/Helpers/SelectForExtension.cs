using System;
using System.Linq.Expressions;
using System.Web.Mvc;

public static class SelectForExtension
{
    public static MvcHtmlString SelectFor<TModel, TValue>(this HtmlHelper<TModel> html,
        Expression<Func<TModel, TValue>> expression)
    {
        var ex = (MemberExpression)expression.Body;
        TagBuilder tag = new TagBuilder("select");
        var member = ex.Member;
        var className = member.ReflectedType.Name;
        var fieldName = member.Name;
        tag.MergeAttribute("id", string.Concat(className, "_", fieldName));
        tag.MergeAttribute("name", string.Concat(className, ".", fieldName));

        return BaseExtension.BuildTag(expression, tag, TagRenderMode.Normal);
    }
}