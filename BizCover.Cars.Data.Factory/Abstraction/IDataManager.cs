using System.Data.Common;
using System.Data;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.Data.Factory.Abstraction;

public partial interface IDataManager : IAsyncDisposable, IDisposable
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    bool AutoSavepointsEnabled { get; }

    /// <summary>
    /// 
    /// </summary>
    bool AutoTransactionsEnabled { get; }

    /// <summary>
    /// 
    /// </summary>
    IDataContext? Context { get; }

    /// <summary>
    /// 
    /// </summary>
    IDbContextTransaction? CurrentTransaction { get; }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    EntityEntry<T>? Add<T>(T? entity) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    ValueTask<EntityEntry<T>?> AddAsync<T>(T? entity) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <returns></returns>
    IEnumerable<T> All<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <returns></returns>
    Task<IEnumerable<T>> AllAsync<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    bool Any<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    bool Any<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<bool> AnyAsync<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<bool> AnyAsync<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <returns></returns>
    IDbContextTransaction? BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <returns></returns>
    ValueTask<IDbContextTransaction?> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    bool BulkDelete<T, A>(IEnumerable<T> entities) where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    ValueTask<bool> BulkDeleteAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    bool BulkInsert<T, A>(IEnumerable<T> entities) where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    ValueTask<bool> BulkInsertAsync<T, A>(IEnumerable<T> entities)
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    void BulkInsertOrUpdate<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    ValueTask BulkInsertOrUpdateAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    void BulkInsertOrUpdateOrDelete<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    ValueTask BulkInsertOrUpdateOrDeleteAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    bool BulkSaveChanges<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    ValueTask<bool> BulkSaveChangesAsync<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool Close();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<bool> CloseAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    void Commit();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    ValueTask CommitAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    void CommitTransaction(IDbContextTransaction? transaction);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    ValueTask CommitTransactionAsync(IDbContextTransaction? transaction);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="savepoint"></param>
    void CreateSavepoint(string? savepoint);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="savepoint"></param>
    void CreateSavepoint(IDbContextTransaction? transaction, string? savepoint);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="savepoint"></param>
    ValueTask CreateSavepointAsync(string? savepoint);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="savepoint"></param>
    ValueTask CreateSavepointAsync(IDbContextTransaction? transaction, string? savepoint);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="GroupBy"></typeparam>
    /// <param name="query"></param>
    /// <param name="grouping"></param>
    /// <param name="filter"></param>
    /// <param name="ordering"></param>
    /// <returns></returns>
    IQueryable<IPartitionKey<T>> DenseRank<T, GroupBy>(
        IQueryable<T> query,
        IEnumerable<Func<T, GroupBy>>? grouping,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    int Execute(string? statement);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<int> ExecuteAsync(string? statement, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    int ExecuteNonQuery
    (
        string? statement,
        CommandType commandType = CommandType.Text,
        int timeout = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    ValueTask<int> ExecuteNonQueryAsync
    (
        string? statement,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="behavior"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    IDataReader? ExecuteReader
    (
        string? statement,
        CommandBehavior behavior = CommandBehavior.SingleResult | CommandBehavior.SequentialAccess,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="behavior"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    ValueTask<IDataReader?> ExecuteReaderAsync
    (
        string? statement,
        CommandBehavior behavior = CommandBehavior.SingleResult | CommandBehavior.SequentialAccess,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    object? ExecuteScalar
    (
        string? statement,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<object?> ExecuteScalarAsync
    (
        string? statement,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    );

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statement"></param>
    /// <returns></returns>
    IEnumerable<T>? ExecuteScript<T>(string? statement = null) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statement"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<T>?> ExecuteScriptAsync<T>(string? statement = null) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    T? First<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    T? First<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    T? FirstEntry<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<T?> FirstAsync<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<T?> FirstAsync<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<T?> FirstEntryAsync<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? FirstOrDefault<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    T? FirstOrDefault<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    T? FirstEntryOrDefault<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ValueTask<T?> FirstOrDefaultAsync<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<T?> FirstOrDefaultAsync<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<T?> FirstEntryOrDefaultAsync<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string GetTableName<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    IQueryable<IGrouping<TKey, T>> GroupBy<T, TKey>(IQueryable<T> query, Expression<Func<T, TKey>> expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool IsClosed();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<bool> IsClosedAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    bool IsEmpty<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? Last<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    T? Last<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    T? LastEntry<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ValueTask<T?> LastAsync<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<T?> LastAsync<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<T?> LastEntryAsync<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    T? LastOrDefault<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    T? LastOrDefault<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    T? LastEntryOrDefault<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<T?> LastOrDefaultAsync<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<T?> LastOrDefaultAsync<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<T?> LastEntryOrDefaultAsync<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    List<T> List<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    List<T> List<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    List<T> ListWhere<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<List<T>> ListAsync<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<List<T>> ListAsync<T>(Func<T, bool>? condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<List<T>> ListWhereAsync<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool Open();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<bool> OpenAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="GroupBy"></typeparam>
    /// <param name="query"></param>
    /// <param name="grouping"></param>
    /// <param name="filter"></param>
    /// <param name="ordering"></param>
    /// <returns></returns>
    IQueryable<IPartitionKey<T>> Rank<T, GroupBy>(
        IQueryable<T> query,
        IEnumerable<Func<T, GroupBy>>? grouping,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="grouping"></param>
    /// <param name="filter"></param>
    /// <param name="ordering"></param>
    /// <returns></returns>
    IQueryable<IPartitionKey<T>> RowNumber<T>(
        IQueryable<T> query,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statement"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    IQueryable<T> Query<T>(string? statement, Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<IQueryable<T>> QueryAsync<T>(Expression<Func<T, bool>> expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statement"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<IQueryable<T>> QueryAsync<T>(string? statement, Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    bool Remove<T, A>(T? entity) where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    ValueTask<bool> RemoveAsync<T, A>(T? entity) where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    void Rollback();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    void RollbackTransaction(IDbContextTransaction? transaction);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask RollbackAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    ValueTask RollbackTransactionAsync(IDbContextTransaction? transaction);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="savepoint"></param>
    void RollbackTransactionToSavepoint(IDbContextTransaction? transaction, string? savepoint);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="savepoint"></param>
    /// <returns></returns>
    ValueTask RollbackTransactionToSavepointAsync(IDbContextTransaction? transaction, string? savepoint);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="preserveAllChanges"></param>
    /// <returns></returns>
    int SaveChanges(bool preserveAllChanges = true);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="preserveAllChanges"></param>
    /// <returns></returns>
    ValueTask<int> SaveChangesAsync(bool preserveAllChanges = true);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    DbSet<T>? Set<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ValueTask<DbSet<T>?> SetAsync<T>() where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    bool Some<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<bool> SomeAsync<T>(Expression<Func<T, bool>>? expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    EntityEntry<T>? Update<T>(T? entity) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    ValueTask<EntityEntry<T>?> UpdateAsync<T>(T? entity) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    IDbContextTransaction? UseTransaction(DbTransaction? transaction, Guid? guid = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    ValueTask<IDbContextTransaction?> UseTransactionAsync(DbTransaction? transaction, Guid? guid = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    IEnumerable<T> Where<T>(Func<T, bool> condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IEnumerable<T> WhereAny<T>(Expression<Func<T, bool>> expression) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<T>> WhereAsync<T>(Func<T, bool> condition) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<T>> WhereAnyAsync<T>(Expression<Func<T, bool>> expression) where T : class;

    #endregion
}
