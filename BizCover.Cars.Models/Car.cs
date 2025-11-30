using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

using BizCover.Cars.Common;

namespace BizCover.Cars.Models;
using Abstraction;

[Serializable, ImmutableObject(true)]
[Table(nameof(Car), Schema = "dbo")]
public sealed partial record class Car : ICar, IDataPrimaryKey
{
    #region Shared Services

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static ICar? Parse(string? content)
    {
        ICar? car = default;

        if (!string.IsNullOrEmpty(content))
        {
            JsonSerializerOptions options = new()
            {
                AllowTrailingCommas = false,
                IncludeFields = false,
                MaxDepth = int.MaxValue,
            };

            try
            {
                car = JsonSerializer.Deserialize<ICar>(content, options);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    Debug.WriteLine(ex.Message);
                }

                car = default;
            }
        }

        return car;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    public static Car TransForm(ICar? car)
    {
        if (car != null)
        {
            return new Car
            {
                Colour = car.Colour,
                CountryManufactured = car.CountryManufactured,
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Price = car.Price,
                Year = car.Year,
            };
        }
        return new Car();
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(5)]
    [JsonPropertyName("colour")]
    [Column(nameof(Colour), TypeName = "NVARCHAR(30) NULL", Order = 5)]
    [StringLength(30)]
    public string? Colour { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(4)]
    [JsonPropertyName("countryManufactured")]
    [Column(nameof(CountryManufactured), TypeName = "NVARCHAR(60) NULL", Order = 4)]
    [StringLength(60)]
    public string? CountryManufactured { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(1)]
    [JsonPropertyName("id")]
    [JsonRequired]
    [Column(nameof(Id), TypeName = "INT NOT NULL", Order = 1)]
    public int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(2)]
    [JsonPropertyName("make")]
    [Column(nameof(Make), TypeName = "NVARCHAR(30) NULL", Order = 2)]
    [StringLength(30)]
    public string? Make { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(3)]
    [JsonPropertyName("model")]
    [Column(nameof(Model), TypeName = "NVARCHAR(30) NULL", Order = 3)]
    [StringLength(30)]
    public string? Model { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(6)]
    [JsonPropertyName("price")]
    [JsonRequired]
    [JsonConverter(typeof(StringToDecimalConverter))]
    [Column(nameof(Price), TypeName = "MONEY NOT NULL", Order = 6)]
    public decimal Price { get; set; } = 0m;

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(6)]
    [JsonPropertyName("year")]
    [JsonRequired]
    [Column(nameof(Year), TypeName = "INT NOT NULL", Order = 6)]
    public int Year { get; set; } = 0;

    #endregion

    #region IEquatable<ICar> Members

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    public bool Equals(ICar? car) => IsEqualTo(this, car);

    #endregion

    #region IEqualityComparer<ICar> Members

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(ICar? x, ICar? y) => IsEqualTo(x, y);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    public int GetHashCode(ICar? car) => car?.GetHashCode() ?? Id.GetHashCode();

    #endregion

    #region Overrides

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => GetProperties(this);

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="visitor"></param>
    /// <returns></returns>
    private static string GetProperties(ICar car)
    {
        JsonSerializerOptions options = new()
        {
            AllowTrailingCommas = false,
            IncludeFields = false,
            MaxDepth = int.MaxValue,
        };

        return JsonSerializer.Serialize(car, options);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static bool IsEqualTo(ICar? x, ICar? y)
    {
        return true;
    }

    #endregion

}
