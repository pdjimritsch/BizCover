using System.Data;

namespace BizCover.Cars.RuleEngine;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class Rule<T>
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly T? _instance;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instanceType"></param>
    public Rule(T? instance) : base()
    {
        _instance = instance;
    }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validations"></param>
    public void Validate(params (dynamic Constraint, bool Enabled)[] validations)
    {
        foreach (var validation in validations)
        {
            if (validation.Constraint.Condition && validation.Enabled)
            {
                validation.Constraint.Effect(_instance);
            }
        }
    }

    #endregion
}
