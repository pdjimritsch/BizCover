using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BizCover.Cars.Models.Abstraction;

/// <summary>
/// Vehicle (registered or unregistered)
/// </summary>
public partial interface ICar : IEquatable<ICar>, IEqualityComparer<ICar>
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    string? Colour { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string? CountryManufactured { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string? Make { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string? Model { get; set; }

    /// <summary>
    /// 
    /// </summary>
    decimal Price { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int Year { get; set; }

    #endregion
}
