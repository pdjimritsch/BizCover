using System.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Configuration;
using Abstraction;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true)]
public sealed class MongoDatabaseOption : IMongoDatabaseOption
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public const string SectionName = @"MongoDatabase";

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="serviceProvider"></param>
    public MongoDatabaseOption(IConfiguration? configuration, IServiceProvider? serviceProvider = null) : base()
    {
        Configuration = configuration;

        Database = configuration?.GetValue<string?>($"{SectionName}:{nameof(Database)}");

        Enable = configuration?.GetValue<bool?>($"{SectionName}:{nameof(Enable)}");

        Uri = configuration?.GetValue<string?>($"{SectionName}:{nameof(Uri)}");

        ServiceProvider = serviceProvider;
    }

    #endregion
    
    #region IMongoDatabaseOption Members

    /// <summary>
    /// 
    /// </summary>
    public IConfiguration? Configuration { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string? Database { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public bool? Enable { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider? ServiceProvider { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string? Uri { get; private set; } = null!;

    #endregion
}
