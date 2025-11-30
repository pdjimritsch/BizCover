namespace BizCover.Cars.Network.Abstraction;

/// <summary>
/// 
/// </summary>
public partial interface IBasicAuthenticationService
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<IUserAccount?> AuthenticateAsync(string? username, string? password);

    #endregion
}
