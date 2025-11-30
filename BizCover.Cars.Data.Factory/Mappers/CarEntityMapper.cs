using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BizCover.Cars.Models;


namespace BizCover.Cars.Data.Factory.Mappers;

/// <summary>
/// 
/// </summary>
public static partial class CarEntityMapper
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void Build(EntityTypeBuilder<Car>? builder)
    {
        builder?.ToTable(nameof(Car), x => 
        {
            x.HasComment("BizCover Vehicle Insurance");
            x.ExcludeFromMigrations(true);
        });

        builder?.Property(x => x.Id).IsRequired(true).HasColumnType("INT NOT NULL").HasColumnOrder(1);
        builder?.Property(x => x.Make).IsRequired(false).IsUnicode(true).HasColumnType("NVARCHAR(30) NULL").HasMaxLength(50).HasColumnOrder(2);
        builder?.Property(x => x.Model).IsRequired(false).IsUnicode(true).HasColumnType("NVARCHAR(30) NULL").HasMaxLength(50).HasColumnOrder(3);
        builder?.Property(x => x.Year).IsRequired(true).HasColumnType("INT NOT NULL").HasColumnOrder(4);
        builder?.Property(x => x.CountryManufactured).IsRequired(false).IsUnicode(true).HasColumnType("NVARCHAR(100) NULL").HasMaxLength(100).HasColumnOrder(5);
        builder?.Property(x => x.Colour).IsRequired(false).IsUnicode(true).HasColumnType("NVARCHAR(50) NULL").HasMaxLength(50).HasColumnOrder(6);
        builder?.Property(x => x.Price).IsRequired(true).HasColumnType("MONEY NOT NULL").HasColumnOrder(7);
    }

    #endregion
}
