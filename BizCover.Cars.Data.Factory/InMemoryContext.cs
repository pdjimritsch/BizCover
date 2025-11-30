using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BizCover.Cars.Models;

namespace BizCover.Cars.Data.Factory;

using Abstraction;
using Extensions;

[Serializable, ImmutableObject(true)]
public partial class InMemoryContext : DbContext, IDataContext
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<InMemoryContext>? _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public InMemoryContext(ILogger<InMemoryContext>? logger = null) : base()
    {
        Database.AutoSavepointsEnabled = true;
        Database.IsInMemory();
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public InMemoryContext(
        DbContextOptions<InMemoryContext> options, ILogger<InMemoryContext>? logger = null) : base(options)
    {
        Database.AutoSavepointsEnabled = true;
        Database.IsInMemory();
        _logger = logger;
    }

    #endregion

    #region IDataContext Properties

    /// <summary>
    /// 
    /// </summary>
    public DbContext? Context => this;

    /// <summary>
    /// 
    /// </summary>
    public bool IsConnected
    {
        get
        {
            if (Context != null &&  Context.Database != null)
            {
                return Context.Database.GetDbConnection().State == System.Data.ConnectionState.Open;
            }
            return false;
        }
    }

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
    /// Configures the database context to be employed as in-memory.
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "VaultContext", options =>
        {
            options.EnableNullChecks();
        });
    }

    /// <summary>
    /// Initializes the Entity Framework Core container mapper for the schema entities;
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Generate();
    }

    #endregion
}
