using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace BizCover.Cars.Common.Extensions;

/// <summary>
/// 
/// </summary>
public static partial class ClassExtensions
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="visible"></param>
    /// <returns></returns>
    public static TAttribute? GetAttribute<T, TAttribute>(bool visible = true) where TAttribute : Attribute
    {
        if (!HasAttribute<T, TAttribute>(visible)) return default;

        try
        {
            return typeof(T).GetCustomAttribute<TAttribute>(false);
        }
        catch (Exception)
        {
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="property"></param>
    /// <returns></returns>
    public static TAttribute? GetAttribute<TAttribute>(PropertyInfo? property) where TAttribute : Attribute
    {
        if (!HasAttribute<TAttribute>(property)) return default;

        try
        {
            return property?.GetCustomAttribute<TAttribute>(false) ?? default;
        }
        catch (Exception)
        {

        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="visible"></param>
    /// <returns></returns>
    public static bool HasAttribute<T, TAttribute>(bool visible = true) where TAttribute : Attribute
    {
        if (!typeof(T).IsClass) return false;

        if (visible && !typeof(T).IsPublic) return false;

        try
        {
            var attribute = typeof(T).GetCustomAttribute<TAttribute>(false);

            return (attribute != null);
        }
        catch (Exception)
        {
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool HasAttribute<T>(this PropertyInfo? property) where T : Attribute
    {
        if (property == null) return false;

        Attribute[]? attributes = default;

        try
        {
            attributes = (Attribute[])Convert.ChangeType(property.GetCustomAttributes(false), typeof(Attribute[]));
        }
        catch (Exception)
        {
            attributes = null;
        }

        return (attributes != null) && attributes.Any(x => x is T);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool HasAttribute<T, TFirst>(this PropertyInfo? property)
        where T : Attribute
        where TFirst : Attribute
    {
        if (property == null) return false;

        return HasAttribute<T>(property) || HasAttribute<TFirst>(property);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static string NormalizeJson(string json)
    {
        if (string.IsNullOrEmpty(json)) return string.Empty;

        var token = JToken.Parse(json);

        if (token == null) return string.Empty;

        var normalized = NormalizeToken(token);

        return System.Text.Json.JsonSerializer.Serialize(normalized);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static JToken? NormalizeToken(JToken? token)
    {
        if (token == null) return token;

        var component = token as JObject;

        if (component != null)
        {
            var properties = component.Properties()?.ToList() ?? new List<JProperty>();

            properties.Sort((x, y) => x.Name.CompareTo(y.Name));

            var normalized = new JObject();

            foreach (var property in properties)
            {
                if (property != null) normalized.Add(property.Name, NormalizeToken(property.Value));
            }

            return normalized;
        }

        var array = token as JArray;

        if (array != null)
        {
            for (var pos = 0; pos < array.Count; ++pos)
            {
                var property = array[pos];

                property = NormalizeToken(property);

                if (property != null) array[pos] = property;
            }

            return array;
        }

        return token;
    }


    #endregion
}
