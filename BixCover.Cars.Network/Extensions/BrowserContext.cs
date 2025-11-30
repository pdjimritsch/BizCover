namespace BizCover.Cars.Network.Extensions;

public static partial class BrowserContext
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="browser_url"></param>
    /// <param name="is_authenticated"></param>
    /// <returns></returns>
    public static string? GetBrowserUrl(this string? browser_url, ref bool is_authenticated)
    {
        if (is_authenticated && !string.IsNullOrEmpty(browser_url))
        {
            var start_pos = browser_url.IndexOf("https://", StringComparison.InvariantCultureIgnoreCase);

            var end_pos = browser_url.IndexOf("http://", StringComparison.InvariantCultureIgnoreCase);

            if (start_pos >= 0 && end_pos >= 0 && start_pos < end_pos)
            {
                browser_url = browser_url.Substring(start_pos, end_pos - start_pos).Trim();
            }
            else if (start_pos >= 0 && end_pos >= 0 && start_pos > end_pos)
            {
                browser_url = browser_url.Substring(start_pos).Trim();
            }
            else if (start_pos >= 0 && end_pos < 0)
            {
                browser_url = browser_url.Trim();
            }
            else if (start_pos < 0 && end_pos >= 0)
            {
                browser_url = browser_url.Substring(end_pos).Trim();
                is_authenticated = false;
            }
        }
        else if (!string.IsNullOrEmpty(browser_url))
        {
            var start_pos = browser_url.IndexOf("https://", StringComparison.InvariantCultureIgnoreCase);

            var end_pos = browser_url.IndexOf("http://", StringComparison.InvariantCultureIgnoreCase);

            if (start_pos >= 0 && end_pos >= 0 && start_pos < end_pos)
            {
                browser_url = browser_url.Substring(end_pos).Trim();
            }
            else if (start_pos >= 0 && end_pos >= 0 && start_pos > end_pos)
            {
                browser_url = browser_url.Substring(end_pos, start_pos - end_pos).Trim();
            }
            else if (start_pos < 0 && end_pos >= 0)
            {
                browser_url = browser_url.Trim();
            }
            else if (start_pos >= 0 && end_pos < 0)
            {
                browser_url = browser_url.Substring(start_pos).Trim();
                is_authenticated = true;
            }
        }

        return browser_url;
    }

    #endregion
}
