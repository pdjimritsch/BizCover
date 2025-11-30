using System;
using System.Collections.Generic;
using System.Text;

namespace BizCover.Cars.Data.Factory.Abstraction;

public partial interface IRouteParameters<T> where T : class
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    List<T> List { get; set; }

    /// <summary>
    /// 
    /// </summary>
    Guid Token { get; set; }

    #endregion
}
