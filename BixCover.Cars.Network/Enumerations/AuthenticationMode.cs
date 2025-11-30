namespace BizCover.Cars.Network.Enumerations;

/// <summary>
/// 
/// </summary>
public static partial class AuthenticationMode
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public static readonly ushort None = 0;

    /// <summary>
    /// 
    /// </summary>
    public static readonly ushort Basic = 1;

    /// <summary>
    /// 
    /// </summary>
    public static readonly ushort Cookie = 2;

    /// <summary>
    /// 
    /// </summary>
    public static readonly ushort UseLocalApi = 3;

    /// <summary>
    /// 
    /// </summary>
    public static readonly ushort UseIdentityWebApi = 4;

    /// <summary>
    /// 
    /// </summary>
    public static readonly ushort UseJwtBearer = 5;

    /// <summary>
    /// 
    /// </summary>
    public static readonly ushort UseOpenIdConnect = 6;

    /// <summary>
    /// 
    /// </summary>
    public static readonly ushort OAuth2 = 7;

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authentication_mode"></param>
    /// <returns></returns>
    public static ushort Parse(string? authentication_mode)
    {
        if (string.IsNullOrEmpty(authentication_mode))
        {
            return AuthenticationMode.None;
        }

        if (authentication_mode.Equals(nameof(Basic), StringComparison.InvariantCultureIgnoreCase))
        {
            return AuthenticationMode.Basic;
        }

        if (authentication_mode.Equals(nameof(Cookie), StringComparison.InvariantCultureIgnoreCase))
        {
            return AuthenticationMode.Cookie;
        }

        if (authentication_mode.Equals(nameof(UseLocalApi), StringComparison.InvariantCultureIgnoreCase))
        {
            return AuthenticationMode.UseLocalApi;
        }

        if (authentication_mode.Equals(nameof(UseIdentityWebApi), StringComparison.InvariantCultureIgnoreCase))
        {
            return AuthenticationMode.UseIdentityWebApi;
        }

        if (authentication_mode.Equals(nameof(UseJwtBearer), StringComparison.InvariantCultureIgnoreCase))
        {
            return AuthenticationMode.UseJwtBearer;
        }

        if (authentication_mode.Equals(nameof(UseOpenIdConnect), StringComparison.InvariantCultureIgnoreCase))
        {
            return AuthenticationMode.UseOpenIdConnect;
        }

        if (authentication_mode.Equals(nameof(OAuth2), StringComparison.InvariantCultureIgnoreCase))
        {
            return AuthenticationMode.OAuth2;
        }

        return AuthenticationMode.None;
    }

    #endregion
}
