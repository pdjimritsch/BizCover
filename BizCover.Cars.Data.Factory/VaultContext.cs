using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BizCover.Cars.Models;

namespace BizCover.Cars.Data.Factory;

using Abstraction;
using Extensions;

/// <summary>
/// 
/// </summary>
[Serializable, ImmutableObject(true)]
public partial class VaultContext : DbContext, IDataContext
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<VaultContext>? _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public VaultContext(
        DbContextOptions<VaultContext> options, ILogger<VaultContext>? logger = null) : base(options)
    {
        Database.AutoSavepointsEnabled = true;
        Database.AutoTransactionBehavior = AutoTransactionBehavior.WhenNeeded;
        Database.SetCommandTimeout(TimeSpan.FromSeconds(180));
        
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connection_string"></param>
    public VaultContext(
        string? connection_string, ILogger<VaultContext>? logger = null) : base()
    {
        Database.AutoSavepointsEnabled = true;
        Database.SetCommandTimeout(TimeSpan.FromSeconds(180));
        Database.SetConnectionString(connection_string);
        _logger = logger;
    }

    #endregion

    #region IDataContext Properties

    /// <summary>
    /// 
    /// </summary>
    public DbContext Context => this;

    /// <summary>
    /// 
    /// </summary>
    public bool IsConnected => Database.CanConnect() &&
        (Database.GetDbConnection().State == System.Data.ConnectionState.Open);

    #endregion

    #region Tables

    /// <summary>
    /// 
    /// </summary>
    public virtual DbSet<Car>? Cars { get; set; } = null!;

    #endregion

    #region Overrides

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configurationBuilder"></param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Initializes the Entity Framework Core container mapper for the schema entities;
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.RegisterServices<VaultContext, IDataContext>(this);

        modelBuilder.Generate();
    }

    #endregion
}
