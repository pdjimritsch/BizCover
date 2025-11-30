using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

using BizCover.Cars.Models;
using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.Data.Factory.Helpers;

using Abstraction;
using Enumerations;

/// <summary>
/// 
/// </summary>
public static partial class ContainerContext
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static async Task<bool> AddAsync<T, A, TContext>(DbSet<T>? container, T? entry, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        if (container != null && entry != null)
        {
            try
            {
                var component = container?.Add(entry);

                if ((component?.State ?? EntityState.Unchanged) == EntityState.Added)
                {
                    return await Task.FromResult(true);
                }
            }
            catch (Exception violation)
            {
                logger?.LogError(violation, violation.Message);
            }
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static async Task<bool> AddRangeAsync<T, A, TContext>(DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        if (collection != null && container != null)
        {
            try
            {
                container?.AddRange(collection.ToArray());

                return await Task.FromResult(true);
            }
            catch (Exception violation)
            {
                logger?.LogError(violation, violation.Message);
            }
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool BulkDelete<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        if (container != null && collection != null && collection.Any())
        {
            try
            {
                container.RemoveRange(collection);

                return true;
            }
            catch (DbUpdateConcurrencyException violation)
            {
                logger?.LogError(violation.Message, violation);
            }
            catch (DbUpdateException violation)
            {
                logger?.LogError(violation.Message, violation);
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static async Task<bool> BulkDeleteAsync<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        if (container != null && collection != null && collection.Any())
        {
            try
            {
                container.RemoveRange(collection);

                return await Task.FromResult(true);
            }
            catch (DbUpdateConcurrencyException violation)
            {
                logger?.LogError(violation.Message, violation);
            }
            catch (DbUpdateException violation)
            {
                logger?.LogError(violation.Message, violation);
            }
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool BulkInsert<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        try
        {
            if (container != null && collection != null && collection.Any())
            {
                container.AddRange(collection);

                return true;
            }
        }
        catch (DbUpdateConcurrencyException violation)
        {
            logger?.LogError(violation.Message, violation);
        }
        catch (DbUpdateException violation)
        {
            logger?.LogError(violation.Message, violation);
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static async Task<bool> BulkInsertAsync<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        try
        {
            if (container != null && collection != null && collection.Any())
            {
                await container.AddRangeAsync(collection);

                return await Task.FromResult(true);
            }
        }
        catch (DbUpdateConcurrencyException violation)
        {
            logger?.LogError(violation.Message, violation);
        }
        catch (DbUpdateException violation)
        {
            logger?.LogError(violation.Message, violation);
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool BulkInsertOrUpdate<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        try
        {
            if (container != null && collection != null && collection.Any())
            {
                foreach (var entry in collection)
                {
                    if (container.Any(x => x != entry && x.Id == entry.Id))
                    {
                        container.Update(entry);
                    }
                    else if (!container.Any(x => entry.Id > 0 && x.Id == entry.Id))
                    {
                        container.Add(entry);
                    }
                }

                return true;
            }
        }
        catch (DbUpdateConcurrencyException violation)
        {
            logger?.LogError(violation.Message, violation);
        }
        catch (DbUpdateException violation)
        {
            logger?.LogError(violation.Message, violation);
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static async Task<bool> BulkInsertOrUpdateAsync<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        try
        {
            if (container != null && collection != null && collection.Any())
            {
                foreach (var entry in collection)
                {
                    if (container.Any(x => x != entry && x.Id == entry.Id))
                    {
                        container.Update(entry);
                    }
                    else if (!container.Any(x => entry.Id > 0 && x.Id == entry.Id))
                    {
                        container.Add(entry);
                    }
                }

                return await ValueTask.FromResult(true);
            }
        }
        catch (DbUpdateConcurrencyException violation)
        {
            logger?.LogError(violation.Message, violation);
        }
        catch (DbUpdateException violation)
        {
            logger?.LogError(violation.Message, violation);
        }

        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool BulkInsertOrUpdateOrDelete<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        try
        {
            if (container != null && collection != null && collection.Any())
            {
                foreach (var entry in collection)
                {
                    if (container.Any(x => x != entry && x.Id == entry.Id))
                    {
                        container.Update(entry);
                    }
                    else if (!container.Any(x => entry.Id > 0 && x.Id == entry.Id))
                    {
                        container.Add(entry);
                    }
                    else if (container.Any(x => x == entry && x.Id == entry.Id))
                    {
                        container.Remove(entry);
                    }
                }

                return true;
            }
        }
        catch (DbUpdateConcurrencyException violation)
        {
            logger?.LogError(violation.Message, violation);
        }
        catch (DbUpdateException violation)
        {
            logger?.LogError(violation.Message, violation);
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static async Task<bool> BulkInsertOrUpdateOrDeleteAsync<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        try
        {
            if (container != null && collection != null && collection.Any())
            {
                foreach (var entry in collection)
                {
                    if (container.Any(x => x != entry && x.Id == entry.Id))
                    {
                        container.Update(entry);
                    }
                    else if (!container.Any(x => entry.Id > 0 && x.Id == entry.Id))
                    {
                        container.Add(entry);
                    }
                    else if (container.Any(x => x == entry && x.Id == entry.Id))
                    {
                        container.Remove(entry);
                    }
                }

                return await Task.FromResult(true);
            }
        }
        catch (DbUpdateConcurrencyException violation)
        {
            logger?.LogError(violation.Message, violation);
        }
        catch (DbUpdateException violation)
        {
            logger?.LogError(violation.Message, violation);
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="container"></param>
    /// <param name="entry"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static async Task<bool> DeleteAsync<T, A, TContext>
        (DbSet<T>? container, T? entry, ILogger<TContext>? logger)
        where T : class, IDataPrimaryKey
        where TContext : DbContext
    {
        if (container != null && entry != null)
        {
            try
            {
                var component = container.Remove(entry);

                if (component != null && component.State == EntityState.Deleted)
                {
                    return await Task.FromResult(true);
                }
            }
            catch (Exception violation)
            {
                logger?.LogError(violation, violation.Message);
            }
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="container"></param>
    /// <param name="collection"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static async Task<bool> DeleteRangeAsync<T, A, TContext>
        (DbSet<T>? container, IEnumerable<T> collection, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        if (container != null && collection != null)
        {
            try
            {
                container.RemoveRange(collection);

                return await Task.FromResult(true);
            }
            catch (Exception violation)
            {
                logger?.LogError(violation, violation.Message);
            }
        }

        return await Task.FromResult(false);
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
    public static IQueryable<IPartitionKey<T>> DenseRank<T, GroupBy>(
        IQueryable<T> query,
        IEnumerable<Func<T, GroupBy>>? grouping,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class
    {
        // sort the query with the preferred sequence ordering predicate.

        IOrderedEnumerable<T> sequence = Partition(query, ordering);

        // filter the applied partitioning using the filter predicate

        var repository = WherePartition(sequence, filter);

        // apply row number sequencing

        ulong index = 0;

        List<IPartitionKey<T>> response = new();

        foreach (var entry in repository)
        {
            if (entry != null)
            {
                ++index;

                var key = new PartitionKey<T> { KeyEntry = entry, Position = index };

                if (response.Any())
                {
                    IEnumerable<T> previous = [response.Last().KeyEntry];

                    IEnumerable<T> container = [entry];

                    if (grouping != null && grouping.Any())
                    {
                        var equality = true;

                        int groupNumber = 0;

                        foreach (var group in grouping)
                        {
                            groupNumber++;

                            var groupx = previous.GroupBy(group);

                            var groupy = container.GroupBy(group);

                            if (groupx != null && groupy != null)
                            {
                                if (groupx.First() != null && groupy.First() != null)
                                {
                                    var firstx = groupx.First();

                                    var firsty = groupy.First();

                                    if (firstx != null && firsty != null)
                                    {
                                        var keyx = firstx.Key;

                                        var keyy = firsty.Key;

                                        if (keyx != null && keyy != null)
                                        {
                                            equality = keyx.Equals(keyy);

                                            if (equality && groupNumber == 1)
                                            {
                                                continue;
                                            }
                                            else if (groupNumber > 1)
                                            {
                                                key.Position = response.Last().Position + 1;

                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            equality = false;

                            break;
                        }

                        if (equality)
                        {
                            response.Add(key);
                        }
                        else
                        {
                            index = 1;

                            key.Position = 1;

                            response.Add(key);
                        }
                    }
                    else
                    {
                        index = 1;

                        key.Position = index;

                        response.Add(key);
                    }
                }
                else
                {
                    response.Add(key);
                }
            }
        }

        return response.AsQueryable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="GroupBy"></typeparam>
    /// <param name="sequence"></param>
    /// <param name="grouping"></param>
    /// <returns></returns>
    public static IEnumerable<T> GroupPartition<T, GroupBy>(
        IEnumerable<T> sequence, IEnumerable<Func<T, GroupBy>>? grouping)
        where T : class
    {
        if (sequence == null)
        {
            return Enumerable.Empty<T>();
        }

        if (grouping == null)
        {
            return sequence;
        }

        var segment = grouping.First();

        if (segment != null)
        {
            var grouped = sequence.GroupBy(segment);

            foreach (var group in grouping.Skip(1) ?? Enumerable.Empty<Func<T, GroupBy>>())
            {
                var container = sequence.GroupBy(group);

                foreach (var entry in container)
                {
                    grouped = grouped.Append(entry);
                }
            }

            sequence = grouped.SelectMany(x => x);
        }

        return sequence;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCompare"></typeparam>
    /// <returns></returns>
    public static bool IsTypeReference<T, TCompare>()
    {
        if (ReferenceEquals(typeof(T), typeof(TCompare)))
        {
            return true;
        }

        if (typeof(T).IsAssignableFrom(typeof(TCompare)))
        {
            return true;
        }

        if (typeof(T).IsAssignableTo(typeof(TCompare)))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCompare"></typeparam>
    /// <returns></returns>
    public static async Task<bool> IsTypeReferenceAsync<T, TCompare>()
    {
        if (ReferenceEquals(typeof(T), typeof(TCompare)))
        {
            return await Task.FromResult(true);
        }

        if (typeof(T).IsAssignableFrom(typeof(TCompare)))
        {
            return await Task.FromResult(true);
        }

        if (typeof(T).IsAssignableTo(typeof(TCompare)))
        {
            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="ordering"></param>
    /// <returns></returns>
    public static IOrderedEnumerable<T> Partition<T>(
        IQueryable<T> query,
        IEnumerable<IOrdering<T>>? ordering
    )
    where T : class
    {
        List<T> container = [.. query.AsEnumerable()];

        List<(SortDirection, Func<T, object>)> expressions = new();

        // register the provided sorting predicates

        foreach (var order in ordering ?? Enumerable.Empty<IOrdering<T>>())
        {
            if (order.SortBy != null && order.Direction == SortDirection.Ascending)
            {
                expressions.Add((SortDirection.Ascending, order.SortBy));
            }
            else if (order.SortBy != null && order.Direction == SortDirection.Descending)
            {
                expressions.Add((SortDirection.Descending, order.SortBy));
            }
        }

        // sort the container from the sequence of ordering predicates

        var sequencing = expressions.Any() && (expressions.FirstOrDefault().Item1 == SortDirection.Ascending)
            ? container.OrderBy(expressions.FirstOrDefault().Item2)
            : expressions.Any() && (expressions.FirstOrDefault().Item1 == SortDirection.Descending)
            ? container.OrderByDescending(expressions.FirstOrDefault().Item2)
            : container.OrderBy(x => true);

        foreach (var expr in expressions.Skip(1))
        {
            if (expr.Item1 == SortDirection.Ascending)
            {
                sequencing = sequencing.ThenBy(expr.Item2);
            }
            else if (expr.Item1 == SortDirection.Descending)
            {
                sequencing = sequencing.ThenByDescending(expr.Item2);
            }
        }

        return sequencing;
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
    public static IQueryable<IPartitionKey<T>> Rank<T, GroupBy>(
        IQueryable<T> query,
        IEnumerable<Func<T, GroupBy>>? grouping,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class
    {
        // sort the query with the preferred sequence ordering predicate.

        IOrderedEnumerable<T> sequence = Partition(query, ordering);

        // filter the applied partitioning using the filter predicate

        var repository = WherePartition(sequence, filter);

        // apply row number sequencing

        ulong index = 0;

        List<IPartitionKey<T>> response = new();

        foreach (var entry in repository)
        {
            if (entry != null)
            {
                ++index;

                var key = new PartitionKey<T> { KeyEntry = entry, Position = index };

                if (response.Any())
                {
                    IEnumerable<T> previous = [ response.Last().KeyEntry ];

                    IEnumerable<T> container = [entry];

                    if (grouping != null && grouping.Any())
                    {
                        var equality = true;

                        int groupNumber = 0;

                        foreach (var group in grouping)
                        {
                            groupNumber++;

                            var groupx = previous.GroupBy(group);

                            var groupy = container.GroupBy(group);

                            if (groupx != null && groupy != null)
                            {
                                if (groupx.First() != null && groupy.First() != null)
                                {
                                    var firstx = groupx.First();

                                    var firsty = groupy.First();

                                    if (firstx != null && firsty != null)
                                    {
                                        var keyx = firstx.Key;

                                        var keyy = firsty.Key;

                                        if (keyx != null && keyy != null)
                                        {
                                            equality = keyx.Equals(keyy);

                                            if (equality && groupNumber == 1)
                                            {
                                                continue;
                                            }
                                            else if (equality && groupNumber > 1)
                                            {
                                                key.Position = response.Last().Position;

                                                ++index;

                                                break;
                                            }
                                            else if (!equality && groupNumber > 1)
                                            {
                                                key.Position = response.Last().Position;

                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            equality = false;

                            break;
                        }

                        if (equality)
                        {
                            response.Add(key);
                        }
                        else
                        {
                            index = 1;

                            key.Position = 1;

                            response.Add(key);
                        }
                    }
                    else
                    {
                        index = 1;

                        key.Position = index;

                        response.Add(key);
                    }
                }
                else
                {
                    response.Add(key);
                }
            }
        }

        return response.AsQueryable();
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
    public static IQueryable<IPartitionKey<T>> RowNumber<T>(
        IQueryable<T> query,
        Expression<Func<T, bool>>? filter,
        IEnumerable<IOrdering<T>>? ordering)
        where T : class
    {
        // sort the query with the preferred sequence ordering predicate.

        IOrderedEnumerable<T> sequence = Partition(query, ordering);

        // filter the applied partitioning using the filter predicate

        var repository = WherePartition(sequence, filter);

        // apply row number sequencing

        ulong index = 0;

        List<IPartitionKey<T>> response = new();

        foreach (var entry in repository)
        {
            if (entry != null)
            {
                ++index;

                response.Add(new PartitionKey<T> { KeyEntry = entry, Position = index });
            }
        }

        return response.AsQueryable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="container"></param>
    /// <param name="entry"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static async ValueTask<EntityEntry<T>?> 
        RemoveAsync<T, A, TContext>(DbSet<T>? container, T? entry, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        try
        {
            if (container != null && entry != null)
            {
                var component = container.Remove(entry);

                if (component != null &&
                    (component.State == EntityState.Deleted || component.State == EntityState.Unchanged))
                {
                    return await ValueTask.FromResult(component);
                }
            }
        }
        catch (Exception violation)
        {
            logger?.LogError(violation, violation.Message);
        }

        return await ValueTask.FromResult(default(EntityEntry<T>));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="container"></param>
    /// <param name="entry"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static async Task<bool> 
        UpdateAsync<T, A, TContext>(DbSet<T>? container, T? entry, ILogger<TContext>? logger)
        where T : class, A, IDataPrimaryKey, IEquatable<A>
        where TContext : DbContext
    {
        if (container != null && entry != null)
        {
            try
            {
                var component = container.Update(entry);

                if (component != null &&
                    (component.State == EntityState.Unchanged || component.State == EntityState.Modified))
                {
                    return await Task.FromResult(true);
                }
            }
            catch (Exception violation)
            {
                logger?.LogError(violation, violation.Message);
            }
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sequence"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static IEnumerable<T> WherePartition<T>(
        IOrderedEnumerable<T> sequence, 
        Expression<Func<T, bool>>? filter)
        where T : class
    {
        if (sequence == null)
        {
            return Enumerable.Empty<T>();
        }

        if (filter == null)
        {
            return sequence.AsEnumerable();
        }

        Func<T, bool> constraint = filter.Compile();

        return sequence.Where(x => constraint.Invoke(x));
    }

    #endregion
}