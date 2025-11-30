using BizCover.Cars.RuleEngine;

namespace BizCover.Cars.RuleEngine.Abstraction;

public partial interface IRuleRepository
{
    #region Properties


    /// <summary>
    /// 
    /// </summary>
    Exception? Exception { get; }

    /// <summary>
    /// 
    /// </summary>
    List<RuleDescription> List { get; }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
     void Load(string? path, string?  filename);

    #endregion
}
