namespace BizCover.Cars.Network.Abstraction;

/// <summary>
/// 
/// </summary>
public interface ISoapBody<TResponse> : IEquatable<ISoapBody<TResponse>>
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    string? ActionName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    bool Encoded { get; }

    /// <summary>
    /// 
    /// </summary>
    string? Header { get; }

    /// <summary>
    /// 
    /// </summary>
    TResponse? Response { get; set; }

    /// <summary>
    /// 
    /// </summary>
    IDictionary<string, object?> Parameters { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string? Trailer { get; }

    #endregion
}
