using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

using BizCover.Cars.Common;
using BizCover.Cars.Common.Extensions;
using BizCover.Cars.Models;

namespace BizCover.Cars.Data.Factory.Extensions;

using Abstraction;
using Enumerations;
using Mappers;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// https://www.entityframeworktutorial.net/
/// https://www.entityframeworktutorial.net/efcore/install-entity-framework-core.aspx
/// </remarks>
public static partial class ContextExtensions
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="opts"></param>
    /// <param name="configuration"></param>
    /// <param name="server_name"></param>
    public static void ConfigureServer(this DbContextOptionsBuilder opts, IConfiguration? configuration, string? server_name)
    {
        if (configuration != null && !string.IsNullOrEmpty(server_name))
        {
            var connection_string = configuration.GetConnectionString(server_name);

            if (!string.IsNullOrEmpty(connection_string))
            {
                opts.UseSqlServer(connection_string, builder => GetOptions(builder));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void Generate(this ModelBuilder? modelBuilder)
    {
        if (modelBuilder != null)
        {
            modelBuilder.Entity<Car>(CarEntityMapper.Build);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableReference<T>() where T : class
    {
        var databaseTable = typeof(T).Name;

        TableAttribute? attribute;

        try
        {
            attribute = typeof(T).GetAttribute<TableAttribute>(false);
        }
        catch (Exception)
        {
            attribute = default;
        }

        var schema = string.Empty;

        if (attribute != null) schema = attribute.Schema;

        return string.IsNullOrEmpty(schema) ? databaseTable : $"{schema}.{databaseTable}";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="context"></param>
    /// <remarks>
    /// https://learn.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping
    /// </remarks>
    public static void RegisterServices<Context, IContext>(this ModelBuilder? modelBuilder, Context? context)
        where Context : DbContext, IContext
        where IContext : IDataFunctions
    {
        if (modelBuilder != null && context != null)
        {
            var mi = typeof(IContext).GetRuntimeMethod(DataFunctions.DatePartName, new[] { typeof(DatePartOffset), typeof(DateTime) });
            if (mi != null)
            {
                modelBuilder.HasDbFunction(mi).HasName(DataFunctions.DatePartName);
            }

            var mi2 = typeof(IContext).GetRuntimeMethod(DataFunctions.DatePartName, new[] { typeof(DatePartOffset), typeof(DateTimeOffset) });
            if (mi2 != null)
            {
                modelBuilder.HasDbFunction(mi2).HasName(DataFunctions.DateOffsetPartName);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="activate"></param>
    public static void SetIdentityInsert<T>(IDataContext? context, bool activate = true)
    {
        if ((context == null) || !typeof(T).IsPublic) return;

        if (ClassExtensions.HasAttribute<T, TableAttribute>(typeof(T).IsPublic))
        {
            var attribute = ClassExtensions.GetAttribute<T, TableAttribute>(typeof(T).IsPublic);

            if (attribute == null) return;

            string statement;

            if (string.IsNullOrEmpty(attribute.Schema))
            {
                statement = string.Concat($"SET IDENTITY_INSERT { attribute.Name } ", activate ? "ON" : "OFF", ";");
            }
            else
            {
                statement = string.Concat($"SET IDENTITY_INSERT { attribute.Schema }.{ attribute.Name } ", activate ? "ON" : "OFF", ";");
            }

            var formatted = FormattableStringFactory.Create(statement);

            context.Database?.ExecuteSqlInterpolated(formatted);
        }
    }

    #endregion

    #region Functions

    /// <summary>
    /// Assigns the SQL server options
    /// </summary>
    static void GetOptions(SqlServerDbContextOptionsBuilder builder)
    {
        builder.EnableRetryOnFailure(5);
        builder.CommandTimeout(180);
        builder.MigrationsHistoryTable("Migrations", "dbo");
        builder.UseRelationalNulls();
    }

    #endregion
}
