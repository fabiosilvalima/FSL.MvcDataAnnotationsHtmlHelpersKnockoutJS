using FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Annotations;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

public static class BaseExtension
{
    public enum FieldTypes
    {
        InputHidden,
        Anyone
    }

    public static string CapturarExpression<TModel, TValue>(this Expression<Func<TModel, TValue>> expression)
    {
        var ex = expression.ToString();
        var i = ex.IndexOf('.');

        return ex.Substring(i + 1, ex.Length - i - 1);
    }
    
    public static T CapturarModelMetadata<TModel, TValue, T>(HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
    {
        var metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
        var model = (T)metaData.Model;

        return model;
    }
    
    public static MvcHtmlString BuildTag<TModel, TValue>(Expression<Func<TModel, TValue>> expression, TagBuilder tag, TagRenderMode renderMode)
    {
        return BuildTag(expression, tag, renderMode, FieldTypes.Anyone);
    }

    public static MvcHtmlString BuildTag<TModel, TValue>(Expression<Func<TModel, TValue>> expression, TagBuilder tag, TagRenderMode renderMode, FieldTypes fieldType)
    {
        return BuildTag(expression, ParseHtml(tag.ToString(renderMode)), fieldType);
    }

    public static MvcHtmlString BuildTag<TModel, TValue>(Expression<Func<TModel, TValue>> expression, HtmlDocument doc)
    {
        return BuildTag(expression, doc, FieldTypes.Anyone);
    }

    public static MvcHtmlString BuildTag<TModel, TValue>(Expression<Func<TModel, TValue>> expression, HtmlDocument doc, FieldTypes fieldType)
    {
        AddCustomAttributes(expression, doc, fieldType);
        NormalizeAttributesHtml5(expression, doc, fieldType);

        return MvcHtmlString.Create(doc.DocumentNode.OuterHtml);
    }

    public static void AddCustomAttributes<TModel, TValue>(Expression<Func<TModel, TValue>> expression, HtmlDocument doc, FieldTypes fieldType)
    {
        var ex = (MemberExpression)expression.Body;
        var attributes = ex.Expression.Type.GetProperty(ex.Member.Name).GetCustomAttributes(false).AsParallel();
        foreach (Attribute attribute in attributes)
        {
            if (attribute is TagAttribute)
            {
                if (doc.DocumentNode.FirstChild != null)
                {
                    var att = attribute as TagAttribute;
                    if (TransformDataBindAttributeFieldType(doc, fieldType, attribute, att))
                    {
                        continue;
                    }

                    AddAttribute(doc.DocumentNode.FirstChild.Attributes, att.GetTagName(), att.GetValue(), fieldType);
                }
            }
        }
    }

    private static bool TransformDataBindAttributeFieldType(HtmlDocument doc, FieldTypes fieldType, Attribute attribute, TagAttribute att)
    {
        if (attribute is OptionsDataBindAttribute && fieldType == FieldTypes.InputHidden)
        {
            AddAttribute(doc.DocumentNode.FirstChild.Attributes, att.GetTagName(), string.Format("value: {0}", att.OriginalValue), fieldType);
            return true;
        }

        return false;
    }
    
    public static HtmlDocument ParseHtml(MvcHtmlString obj)
    {
        return ParseHtml(obj.ToHtmlString());
    }
    
    public static HtmlDocument ParseHtml(string str)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(str);

        if (doc.DocumentNode != null
            && doc.DocumentNode.FirstChild != null
            && doc.DocumentNode.FirstChild.Attributes != null
            && doc.DocumentNode.FirstChild.Attributes.Contains("name"))
        {
            var name = doc.DocumentNode.FirstChild.Attributes["name"];
            if (name.Value.IndexOf(".").Equals(-1)) name.Value = name.Value.Replace("_", ".");
        }

        return doc;
    }
    
    public static void AddAttribute(HtmlAttributeCollection atributos, string key, string value, FieldTypes fieldType)
    {
        if (atributos.Contains(key))
        {
            atributos[key].Value += ", " + value;
        }
        else
        {
            atributos.Add(key, value);
        }
    }
    
    public static void AddAttribute(TagBuilder tag, string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            tag.MergeAttribute(key, value);
        }
    }
            
    private static void NormalizeAttributesHtml5<TModel, TValue>(Expression<Func<TModel, TValue>> expression, HtmlDocument doc, FieldTypes fieldType)
    {
        if (doc == null
            || doc.DocumentNode == null
            || doc.DocumentNode.FirstChild == null) return;
        
        if (doc.DocumentNode.FirstChild.Attributes.Contains("type"))
        {
            var valueTypes = new List<string>()
                {
                    "datetime", "date", "datetime-local", "number", "time"
                };

            var attType = doc.DocumentNode.FirstChild.Attributes["type"];
            if (!string.IsNullOrEmpty(attType.Value)
                && valueTypes.Contains(attType.Value.ToLower()))
            {
                attType.Value = "text";
            }
        }
        
        if (doc.DocumentNode.FirstChild.Attributes.Contains("data-val"))
        {
            var attDataVal = doc.DocumentNode.FirstChild.Attributes["data-val"];
            if (!string.IsNullOrEmpty(attDataVal.Value)
                && !attDataVal.Value.IndexOf(',').Equals(-1))
            {
                attDataVal.Value = "false";
            }
        }
    }
}