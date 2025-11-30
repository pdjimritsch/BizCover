using System.ComponentModel;

using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Configuration;
using Abstraction;

[ImmutableObject(true)]
public sealed partial class Subscribers : ISubscribers
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public const string SectionName = nameof(Subscribers);

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="provider"></param>
    public Subscribers(IConfiguration? configuration, IServiceProvider? provider = null) : base()
    {
        ApiSubscriber = configuration?.GetValue<string?>($"{SectionName}:{nameof(ApiSubscriber)}");

        ApiTimeout = configuration?.GetValue<int?>($"{SectionName}:{nameof(ApiTimeout)}");

        Configuration = configuration;

        ServiceProvider = provider;

        Trigger = configuration?.GetValue<string?>($"{SectionName}:{nameof(Trigger)}");

        TriggerTimeout = configuration?.GetValue<int?>($"{SectionName}:{nameof(TriggerTimeout)}");
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public string? ApiSubscriber { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public int? ApiTimeout { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public IConfiguration? Configuration { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider? ServiceProvider { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string? Trigger { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public int? TriggerTimeout { get; private set; } = null!;

    #endregion

}
