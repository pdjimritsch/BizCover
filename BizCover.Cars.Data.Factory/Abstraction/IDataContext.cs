using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

using BizCover.Cars.Models;
using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.Data.Factory.Abstraction;

/// <summary>
/// 
/// </summary>
public partial interface IDataContext : IDataFunctions
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    DbContext? Context { get; }

    /// <summary>
    /// 
    /// </summary>
    DatabaseFacade? Database { get; }

    /// <summary>
    /// 
    /// </summary>
    bool IsConnected {  get; }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    DbSet<T>? Set<T>() where T : class;

    #endregion

    #region Tables

    /// <summary>
    /// Vehicle collection
    /// </summary>
    DbSet<Car>? Cars { get; set; }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entry"></param>
    /// <remarks>
    /// Declared and implemented within DBContext
    /// </remarks>
    /// <returns></returns>
    EntityEntry<T> Add<T>(T entry) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <remarks>
    /// Declared and implemented within DBContext
    /// </remarks>
    /// <returns></returns>
    EntityEntry Add(object entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entry"></param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    ValueTask<EntityEntry<T>> AddAsync<T>(T entry, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    void AddRange(params object[] entities);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    void AddRange(IEnumerable<object> entities);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    Task AddRangeAsync(params object[] entities);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// Declared and implemented within DBContext.
    /// <returns></returns>
    EntityEntry<T> Attach<T>(T entity) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    EntityEntry Attach(object entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    void AttachRange(params object[] entities);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    void AttachRange(IEnumerable<object> entities);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    bool BulkDelete<T, A>(IEnumerable<T> entities) where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    ValueTask<bool> BulkDeleteAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    bool BulkInsert<T, A>(IEnumerable<T> entities) where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    ValueTask<bool> BulkInsertAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    bool BulkInsertOrUpdate<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    ValueTask<bool> BulkInsertOrUpdateAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    bool BulkInsertOrUpdateOrDelete<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    ValueTask<bool> BulkInsertOrUpdateOrDeleteAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    bool BulkSaveChanges<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    ValueTask<bool> BulkSaveChangesAsync<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey;

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
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    EntityEntry<T> Entry<T>(T entity) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    EntityEntry Entry(object entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entityType"></param>
    /// <param name="keyValues"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    object? Find(Type entityType, params object?[]? keyValues);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keyValues"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    T? Find<T>(params object?[]? keyValues) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keyValues"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    ValueTask<T?> FindAsync<T>(params object?[]? keyValues) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keyValues"></param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    ValueTask<T?> FindAsync<T>(object?[]? keyValues, CancellationToken cancellationToken)
        where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entityType"></param>
    /// <param name="keyValues"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    ValueTask<object?> FindAsync(Type entityType, params object?[]? keyValues);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entityType"></param>
    /// <param name="keyValues"></param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    ValueTask<object?> FindAsync(Type entityType, object[]? keyValues, CancellationToken cancellationToken);

    /// <summary>
    /// Declared within EntityFramework Core
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="expression"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    IQueryable<T> FromExpression<T>(Expression<Func<IQueryable<T>>> expression);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="expression"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    Task<IQueryable<T>> FromExpressionAsync<T>(Expression<Func<IQueryable<T>>> expression);

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
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<IQueryable<IGrouping<TKey, T>>> GroupByAsync<T, TKey>(IQueryable<T> query, Expression<Func<T, TKey>> expression) where T : class;

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
    /// <param name="entry"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    EntityEntry<T> Remove<T>(T entry) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entry"></param>
    /// <returns></returns>
    ValueTask<EntityEntry<T>?> RemoveAsync<T, A>(T entry) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    EntityEntry Remove(object entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    void RemoveRange(params object[] entities);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    void RemoveRange(IEnumerable<object> entities);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    int SaveChanges();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <returns></returns>
    int SaveChanges(bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam 
    /// <param name="entity"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    EntityEntry<T> Update<T>(T entity) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    EntityEntry Update(object entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    void UpdateRange(params object[] entities);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    void UpdateRange(IEnumerable<object> entities);

    #endregion
}
