using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BizCover.Cars.Data.Factory;

using Abstraction;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true)] public sealed partial class RouteParameters<T> : 
    IRouteParameters<T>, IEquatable<IRouteParameters<T>>, IEqualityComparer<IRouteParameters<T>>
    where T : class
{
    #region Shared Properties and Services

    /// <summary>
    /// 
    /// </summary>
    public const string Key = nameof(RouteParameters<T>);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static RouteParameters<T>? Parse(string? content)
    {
        RouteParameters<T>? parameters = default;

        if (!string.IsNullOrWhiteSpace(content))
        {
            JsonSerializerOptions options = new()
            {
                AllowTrailingCommas = false,
                IncludeFields = false,
                MaxDepth = int.MaxValue,
            };

            try
            {
                parameters = JsonSerializer.Deserialize<RouteParameters<T>>(content, options);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    Debug.WriteLine(ex.Message);
                }

                parameters = default;
            }
        }

        return parameters;
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(1)]
    [JsonPropertyName(nameof(List))]
    [Required]
    public List<T> List { get; set; } = [];

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(0)]
    [JsonPropertyName(nameof(Token))]
    [Required]
    public Guid Token { get; set; } = Guid.NewGuid();

    #endregion

    #region IEquatable<IRouteParameters> Members

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(IRouteParameters<T>? other) => IsEqualTo(this, other);

    #endregion

    #region IEqualityComparer<IRouteParameters> Members

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(IRouteParameters<T>? x, IRouteParameters<T>? y) => IsEqualTo(x, y);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int GetHashCode([DisallowNull] IRouteParameters<T> obj) => obj.Token.GetHashCode();

    #endregion

    #region Overrides

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) => IsEqualTo(this, obj as IRouteParameters<T>);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Token.GetHashCode();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => GetProperties(this);

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static bool IsEqualTo(IRouteParameters<T>? x,  IRouteParameters<T>? y)
    {
        if ((x != null) && (y != null))
        {
            return x.Token.ToString().Equals(y.Token.ToString(), StringComparison.Ordinal);
        }

        return (x == null) && (y == null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private static string GetProperties(IRouteParameters<T> parameters)
    {
        JsonSerializerOptions options = new()
        {
            AllowTrailingCommas = false,
            IncludeFields = false,
            MaxDepth = int.MaxValue,
        };

        return JsonSerializer.Serialize(parameters, options);
    }

    #endregion
}
