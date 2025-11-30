using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using BizCover.Cars.Models;
using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.Data.Factory;

using Abstraction;
using Enumerations;
using Helpers;

/// <summary>
/// 
/// </summary>
partial class InMemoryContext
{
    #region Database Functions

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <param name="selector"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    [DbFunction(Name = nameof(DatePart), IsBuiltIn = false, Schema = "dbo", IsNullable = true)]
    public int? DatePart(DatePartOffset selector, DateTime dt)
    {
        return DataFunctions.DatePart(selector, dt);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <param name="selector"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    [DbFunction(Name = nameof(DateOffsetPart), IsBuiltIn = true, Schema = "dbo", IsNullable = true)]
    public int? DateOffsetPart(DatePartOffset selector, DateTimeOffset dt)
    {
        return DataFunctions.DateOffsetPart(selector, dt);
    }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="entry"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync<T, A>(T? entry) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) &&
            (await ContainerContext.AddAsync<Car, ICar, InMemoryContext>(Cars, entry as Car, _logger)))
        {
            var count = await SaveChangesAsync();

            return await Task.FromResult(count > 0);
        }


        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public async Task<bool> AddRangeAsync<T, A>(T[]? collection) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (collection != null) &&
            (await ContainerContext.AddRangeAsync<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger)))
        {
            var committed = SaveChanges();

            return await Task.FromResult(committed > 0);
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <returns></returns>
    public async Task<IEnumerable<T>?> AllAsync<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (await ContainerContext.IsTypeReferenceAsync<T, Car>())
        {
            var sequence = Cars as IEnumerable<T>;

            return await Task.FromResult(sequence ?? Enumerable.Empty<T>());
        }

        return await Task.FromResult(Enumerable.Empty<T>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <returns></returns>
    public async Task<bool> AnyAsync<T, A>(Func<T, bool> predicate) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (await ContainerContext.IsTypeReferenceAsync<T, Car>())
        {
            var sequence = Cars as IEnumerable<T>;

            return await Task.FromResult(sequence?.Any(x => (x != null) && predicate(x)) ?? false);
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    public bool BulkDelete<T, A>(IEnumerable<T> collection) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (ContainerContext.IsTypeReference<T, Car>() && (Context != null) &&
            ContainerContext.BulkDelete<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger))
        {
            var committed = Context.SaveChanges();

            return (committed > 0);
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    public async ValueTask<bool> BulkDeleteAsync<T, A>(IEnumerable<T> collection) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (Context != null) &&
            (await ContainerContext.BulkDeleteAsync<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger)))
        {
            var committed = await Context.SaveChangesAsync();

            return await ValueTask.FromResult(committed > 0);
        }

        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    public bool BulkInsert<T, A>(IEnumerable<T> collection) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (ContainerContext.IsTypeReference<T, Car>() && (Context != null) &&
            ContainerContext.BulkInsert<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger))
        {
            return (Context.SaveChanges() > 0);
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    public async ValueTask<bool> BulkInsertAsync<T, A>(IEnumerable<T> collection) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (Context != null) &&
            (await ContainerContext.BulkInsertAsync<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger)))
        {
            var changes = await Context.SaveChangesAsync();

            return await ValueTask.FromResult(changes > 0);
        }

        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public bool BulkInsertOrUpdate<T, A>(IEnumerable<T> collection) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (ContainerContext.IsTypeReference<T, Car>() && (Context != null) &&
            ContainerContext.BulkInsertOrUpdate<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger))
        {
            return (Context.SaveChanges() > 0);
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public async ValueTask<bool> BulkInsertOrUpdateAsync<T, A>(IEnumerable<T> collection)
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (Context != null) &&
            (await ContainerContext.BulkInsertOrUpdateAsync<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger)))
        {
            var changes = await Context.SaveChangesAsync();

            return await ValueTask.FromResult(changes > 0);
        }

        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    public bool BulkInsertOrUpdateOrDelete<T, A>(IEnumerable<T> collection)
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (ContainerContext.IsTypeReference<T, Car>() && (Context != null) &&
            ContainerContext.BulkInsertOrUpdateOrDelete<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger))
        {
            return (Context.SaveChanges() > 0);
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    public async ValueTask<bool> BulkInsertOrUpdateOrDeleteAsync<T, A>(IEnumerable<T> collection)
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (Context != null) &&
            (await ContainerContext.BulkInsertOrUpdateOrDeleteAsync<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger)))
        {
            var changes = await Context.SaveChangesAsync();

            return await ValueTask.FromResult(changes > 0);
        }

        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public bool BulkSaveChanges<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        return (SaveChanges(true) > 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async ValueTask<bool> BulkSaveChangesAsync<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        return await ValueTask.FromResult((await SaveChangesAsync(true)) > 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="entry"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync<T, A>(T? entry) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (Context != null) &&
            (await ContainerContext.DeleteAsync<Car, ICar, InMemoryContext>(Cars, entry as Car, _logger)))
        {
            var changes = await Context.SaveChangesAsync();

            return await ValueTask.FromResult(changes > 0);
        }

        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public async Task<bool> DeleteRangeAsync<T, A>(IEnumerable<T> collection) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (Context != null) &&
            (await ContainerContext.DeleteRangeAsync<Car, ICar, InMemoryContext>(Cars, collection.Cast<Car>(), _logger)))
        {
            var changes = await Context.SaveChangesAsync();

            return await ValueTask.FromResult(changes > 0);
        }

        return await ValueTask.FromResult(false);
    }

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
    public IQueryable<IPartitionKey<T>> DenseRank<T, GroupBy>(
        IQueryable<T> query,
        IEnumerable<Func<T, GroupBy>>? grouping,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class
    {
        return ContainerContext.DenseRank(query, grouping, filter, ordering);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<T?> FirstOrDefaultAsync<T, A>(Func<T, bool> predicate) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        T? entry = default;

        if (await ContainerContext.IsTypeReferenceAsync<T, Car>())
        {
            var sequence = Cars as IEnumerable<T>;

            entry = sequence?.FirstOrDefault(x => x != null && predicate(x)) ?? default;
        }

        return await Task.FromResult(entry);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="expression"></param>
    /// <remarks>
    /// Declared and implemented within DBContext.
    /// </remarks>
    /// <returns></returns>
    public async Task<IQueryable<T>> FromExpressionAsync<T>(Expression<Func<IQueryable<T>>> expression)
    {
        if (await ContainerContext.IsTypeReferenceAsync<T, Car>())
        {
            var condition = expression as Expression<Func<IQueryable<Car>>>;

            if (condition != null)
            {
                var query = FromExpression(condition);

                if (query != null)
                {
                    return await Task.FromResult(query.Cast<T>());
                }
            }
        }

        return await Task.FromResult(Enumerable.Empty<T>().AsQueryable());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T?>> GetAsync<T, A>(Func<T, bool> predicate) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (await ContainerContext.IsTypeReferenceAsync<T, Car>())
        {
            var sequence = Cars as IEnumerable<T>;

            return await Task.FromResult(sequence?.Where(x => (x != null) && predicate(x)) ?? Enumerable.Empty<T>());
        }

        return await Task.FromResult(Enumerable.Empty<T>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public IQueryable<IGrouping<TKey, T>> GroupBy<T, TKey>(IQueryable<T> query, Expression<Func<T, TKey>> expression) where T : class
    {
        if ((query == null) && ContainerContext.IsTypeReference<T, Car>())
        {
            var sequence = Cars as IEnumerable<T>;

            if (sequence != null && expression != null)
            {
                return sequence.GroupBy(expression.Compile()).AsQueryable();
            }
        }

        Func<T, TKey> unconstrained = (T arg) => { return Activator.CreateInstance<TKey>(); };

        return Enumerable.Empty<T>().GroupBy(unconstrained).AsQueryable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IQueryable<IGrouping<TKey, T>>> GroupByAsync<T, TKey>(IQueryable<T> query, Expression<Func<T, TKey>> expression) where T : class
    {
        if ((query == null) && (await ContainerContext.IsTypeReferenceAsync<T, Car>()))
        {
            var sequence = Cars as IEnumerable<T>;

            if (sequence != null && expression != null)
            {
                return await Task.FromResult(sequence.GroupBy(expression.Compile()).AsQueryable());
            }
        }

        if (query != null && expression != null)
        {
            return await Task.FromResult(query.GroupBy(expression));
        }

        Func<T, TKey> unconstrained = (T arg) => { return Activator.CreateInstance<TKey>(); };

        return await Task.FromResult(Enumerable.Empty<T>().GroupBy(unconstrained).AsQueryable());
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<T?> LastOrDefaultAsync<T, A>(Func<T, bool> predicate) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        T? entry = default;

        if (await ContainerContext.IsTypeReferenceAsync<T, Car>())
        {
            var sequence = Cars as IEnumerable<T>;

            entry = sequence?.LastOrDefault(x => x != null && predicate(x)) ?? default;
        }

        return await Task.FromResult(entry);
    }

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
    public IQueryable<IPartitionKey<T>> Rank<T, GroupBy>(
        IQueryable<T> query,
        IEnumerable<Func<T, GroupBy>>? grouping,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class
    {
        return ContainerContext.Rank(query, grouping, filter, ordering);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="filter"></param>
    /// <param name="ordering"></param>
    /// <returns></returns>
    public IQueryable<IPartitionKey<T>> RowNumber<T>(
        IQueryable<T> query,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class
    {
        return ContainerContext.RowNumber(query, filter, ordering);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="entry"></param>
    /// <returns></returns>
    public async ValueTask<EntityEntry<T>?> RemoveAsync<T, A>(T? entry) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (Context != null))
        {
            var component = await ContainerContext.RemoveAsync<Car, ICar, InMemoryContext>(Cars, entry as Car, _logger);

            return await ValueTask.FromResult(component as EntityEntry<T>);
        }

        return await ValueTask.FromResult(default(EntityEntry<T>));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <param name="entry"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync<T, A>(T? entry) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if ((await ContainerContext.IsTypeReferenceAsync<T, Car>()) && (Context != null))
        {
            if (await ContainerContext.UpdateAsync<Car, ICar, InMemoryContext>(Cars, entry as Car, _logger))
            {
                return await ValueTask.FromResult(true);
            }
        }

        return await ValueTask.FromResult(false);
    }

    #endregion
}