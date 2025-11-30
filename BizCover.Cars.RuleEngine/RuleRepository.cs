using System.Text.Json;

using BizCover.Cars.Common;

namespace BizCover.Cars.RuleEngine;

using Abstraction;

/// <summary>
/// 
/// </summary>
public partial class RuleRepository : IRuleRepository
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private Exception? _violation;

    /// <summary>
    /// Backing store
    /// </summary>
    private RuleDescriptions? _descriptions;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    public RuleRepository() : base()
    {
        _descriptions = default;

        _violation = null;
    }

    #endregion

    #region IFileRepository Members

    /// <summary>
    /// 
    /// </summary>
    public Exception? Exception => _violation;

    /// <summary>
    /// 
    /// </summary>
    public List<RuleDescription> List => _descriptions?.List ?? [];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public void Load(string? path, string? filename)
    {
         (string? directory, Exception? error) response = GetDomainDirectory(path);

        if (string.IsNullOrEmpty(response.directory))
        {
            _violation = response.error;

            return;
        }

        (string? content, Exception? error) outcome = GetFileContent(response.directory, filename);

        var content = outcome.content;

        _violation = outcome.error;

        GetDescriptions(content);
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static (string?, Exception?) GetDomainDirectory(string? path)
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;

        Exception? error = default;

        if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
        {
            path = Path.Combine(directory, path);
        }
        else if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
        {
            DirectoryInfo? reference = default;

            try
            {
                reference = new DirectoryInfo(path);
            }
            catch (Exception violation)
            {
                error = violation;
                reference = default;
            }

            if (reference?.Exists ?? false)
            {
                path = reference.FullName;
            }
        }

        return (path, error);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="filename"></param>
    /// <returns></returns>
    private static (string?, Exception?) GetFileContent(string? directory, string? filename)
    {
        string? content = default;

        Exception? error = null;

        directory = directory.RemoveWhiteSpaces();

        filename = filename.RemoveWhiteSpaces();

        if (!string.IsNullOrEmpty(directory) && 
            Directory.Exists(directory) &&
            !string.IsNullOrEmpty(filename))
        {
            var resource = Path.Combine(directory, filename.Trim());

            try
            {
                content = File.ReadAllText(resource);
            }
            catch (Exception violation)
            {
                error = violation;
                content = default;
            }

            content = content.RemoveWhiteSpaces();

        }

        return (content, error);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    private void GetDescriptions(string? content)
    {
        content = content.RemoveWhiteSpaces();

        if (!string.IsNullOrEmpty(content))
        {
            try
            {
                JsonSerializerOptions options = new()
                {
                    AllowTrailingCommas = false,
                    AllowOutOfOrderMetadataProperties = true,
                    IncludeFields = false,
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    MaxDepth = int.MaxValue,
                };

                _descriptions = JsonSerializer.Deserialize<RuleDescriptions>(content, options);

            }
            catch (Exception violation)
            {
                _violation = violation;
            }
        }
    }

    #endregion
}
