using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

using BizCover.Cars.Common;
using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.Data.Factory;

using Abstraction;
using Extensions;
using Helpers;

/// <summary>
/// Database content manager
/// </summary>
[ImmutableObject(true)] public sealed partial class DataManager : IDataManager
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public static readonly DateTime MinimumDate = new DateTime(1753, 1, 1).Date;

    #endregion

    #region Members

    /// <summary>
    /// Database context
    /// </summary>
    private readonly IDataContext? _context;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public DataManager(IDataContext? context) : base()
    {
        _context = context;
    }

    #endregion

    #region IDataManager Properties

    /// <summary>
    /// 
    /// </summary>
    public bool AutoSavepointsEnabled => _context?.Database?.AutoSavepointsEnabled ?? false;

    /// <summary>
    /// 
    /// </summary>
    public bool AutoTransactionsEnabled => _context?.Database?.AutoTransactionBehavior == AutoTransactionBehavior.Always;

    /// <summary>
    /// 
    /// </summary>
    public IDataContext? Context => _context;

    /// <summary>
    /// 
    /// </summary>
    public IDbContextTransaction? CurrentTransaction => _context?.Database?.CurrentTransaction;

    #endregion

    #region IDataManager Functions

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public EntityEntry<T>? Add<T>(T? entity) where T : class
    {
        if ((_context != null) && (entity != null))
        {
            return _context.Add(entity);
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async ValueTask<EntityEntry<T>?> AddAsync<T>(T? entity) where T : class
    {
        if ((_context != null) && (entity != null))
        {
            return await _context.AddAsync(entity);
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <returns></returns>
    public IEnumerable<T> All<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            var qry = _context.Set<T>() as IQueryable<T>;

            if (qry != null)
            {
                return qry;
            }
        }

        return Enumerable.Empty<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <returns></returns>
    public async Task<IEnumerable<T>> AllAsync<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            return await Task.FromResult(_context.Set<T>()?.AsEnumerable() ?? Enumerable.Empty<T>());
        }

        return await Task.FromResult(Enumerable.Empty<T>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public bool Any<T>() where T : class
    {
        if (_context != null)
        {
            return _context.Set<T>()?.Any() ?? false;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public bool Any<T>(Func<T, bool>? condition) where T : class
    {
        if (_context != null)
        {
            if (condition != null)
            {
                return _context.Set<T>()?.Any(x => condition(x)) ?? false;
            }

            return _context.Set<T>()?.Any() ?? false;
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<bool> AnyAsync<T>() where T : class
    {
        if (_context != null && _context.Set<T>() != null)
        {
            var collection = _context.Set<T>();

            if (collection != null)
            {
                return await collection.AnyAsync();
            }
        }
        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<bool> AnyAsync<T>(Func<T, bool>? condition) where T : class
    {
        if (_context != null)
        {
            var collection = _context.Set<T>();

            if (collection != null && condition != null)
            {
                return await collection.AnyAsync();
            }
            else if (collection != null)
            {
                return await collection.AnyAsync();
            }
        }

        return await ValueTask.FromResult(false);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <returns></returns>
    public IDbContextTransaction? BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (_context != null && _context.Database != null)
        {
            return _context.Database.BeginTransaction(isolationLevel);
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <returns></returns>
    public async ValueTask<IDbContextTransaction?> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (_context != null && _context.Database != null)
        {
            return await _context.Database.BeginTransactionAsync(isolationLevel);
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public bool BulkDelete<T, A>(IEnumerable<T> entities) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            return _context.BulkDelete<T, A>(entities);
        }

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async ValueTask<bool> BulkDeleteAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            return await _context.BulkDeleteAsync<T, A>(entities);
        }

        return await Task.FromResult(false);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public bool BulkInsert<T, A>(IEnumerable<T> entities) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            return _context.BulkInsert<T, A>(entities);
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async ValueTask<bool> BulkInsertAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            return await _context.BulkInsertAsync<T, A>(entities);
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public void BulkInsertOrUpdate<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            _context.BulkInsertOrUpdate<T, A>(entities);

            _context.BulkSaveChanges<T, A>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async ValueTask BulkInsertOrUpdateAsync<T, A>(IEnumerable<T> entities) 
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null) 
        { 
            await _context.BulkInsertOrUpdateAsync<T, A>(entities);

            await _context.BulkSaveChangesAsync<T, A>();
        }

        await Task.Yield();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public void BulkInsertOrUpdateOrDelete<T, A>(IEnumerable<T> entities)
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null) 
        { 
            _context.BulkInsertOrUpdateOrDelete<T, A>(entities);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async ValueTask BulkInsertOrUpdateOrDeleteAsync<T, A>(IEnumerable<T> entities)
        where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            await _context.BulkInsertOrUpdateOrDeleteAsync<T, A>(entities);
        }

        await Task.Yield();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    public bool BulkSaveChanges<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            return _context.BulkSaveChanges<T, A>();
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    public async ValueTask<bool> BulkSaveChangesAsync<T, A>() where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        if (_context != null)
        {
            return await _context.BulkSaveChangesAsync<T, A>();
        }

        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool Close()
    {
        if (_context != null && _context.Database != null && 
            ( _context.Database.GetDbConnection().State != ConnectionState.Closed ) )
        {
            _context.Database.CloseConnection();

            return _context.Database.GetDbConnection().State == ConnectionState.Closed;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> CloseAsync()
    {
        if (_context != null && _context.Database != null && 
            ( _context.Database.GetDbConnection().State != ConnectionState.Closed ) )
        {
            await _context.Database.CloseConnectionAsync();

            return _context.Database.GetDbConnection().State == ConnectionState.Closed;
        }

        return await ValueTask.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    public void Commit()
    {
        if (_context != null && _context.Database != null && ( _context.Database.CurrentTransaction != null ))
        {
            _context.Database.CommitTransaction();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    public async ValueTask CommitAsync()
    {
        if (_context != null && _context.Database != null)
        {
            await _context.Database.CommitTransactionAsync();
        }

        await Task.Yield();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    public void CommitTransaction(IDbContextTransaction? transaction)
    {
        if (transaction != null) 
        { 
            transaction.Commit();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    public async ValueTask CommitTransactionAsync(IDbContextTransaction? transaction)
    {
        if (transaction != null)
        {
            await transaction.CommitAsync();
        }

        await Task.Yield();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="savepoint"></param>
    public void CreateSavepoint(string? savepoint)
    {
        if (_context != null && !string.IsNullOrEmpty(savepoint) && 
            ( _context.Database != null ) && !_context.Database.AutoSavepointsEnabled)
        {
            if (_context.Database.CurrentTransaction != null && !_context.Database.AutoSavepointsEnabled)
            {
                _context.Database.CurrentTransaction.CreateSavepoint(savepoint.Trim());
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="savepoint"></param>
    public void CreateSavepoint(IDbContextTransaction? transaction, string? savepoint)
    {
        if (transaction != null && !string.IsNullOrEmpty(savepoint) && 
            (_context != null) && ( _context.Database != null ) && !_context.Database.AutoSavepointsEnabled)
        {
            transaction.CreateSavepoint(savepoint.Trim());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="savepoint"></param>
    public async ValueTask CreateSavepointAsync(string? savepoint)
    {
        if (_context != null && !string.IsNullOrEmpty(savepoint) && 
            ( _context.Database != null ) && !_context.Database.AutoSavepointsEnabled)
        {
            if (_context.Database.CurrentTransaction != null && !_context.Database.AutoSavepointsEnabled)
            {
                await _context.Database.CurrentTransaction.CreateSavepointAsync(savepoint.Trim());
            }
        }

        await Task.Yield();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="savepoint"></param>
    public async ValueTask CreateSavepointAsync(IDbContextTransaction? transaction, string? savepoint)
    {
        if (transaction != null && !string.IsNullOrEmpty(savepoint) &&
            (_context != null) && ( _context.Database != null ) && !_context.Database.AutoSavepointsEnabled)
        {
            await transaction.CreateSavepointAsync(savepoint.Trim());
        }

        await Task.Yield();
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
    /// <param name="statement"></param>
    /// <returns></returns>
    public int Execute(string? statement)
    {
        if ((_context != null) && (_context.Database != null) && !string.IsNullOrEmpty(statement))
        {
            var formatted = FormattableStringFactory.Create(statement.Trim());

            return _context.Database.ExecuteSqlInterpolated(formatted);
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public async ValueTask<int> ExecuteAsync(string? statement, CancellationToken cancellationToken = default)
    {
        if (_context != null && _context.Database != null && !string.IsNullOrEmpty(statement))
        {
            var formatted = FormattableStringFactory.Create(statement);

            return await _context.Database.ExecuteSqlInterpolatedAsync(formatted, cancellationToken);
        }

        return await ValueTask.FromResult(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="commandType"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public int ExecuteNonQuery
    (
        string? statement,
        CommandType commandType = CommandType.Text,
        int timeout = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    )
    {
        if (_context != null && _context.Database != null && !string.IsNullOrEmpty(statement))
        {
            using var command = _context.Database.GetDbConnection().CreateCommand();

            if ((transaction == null) && (_context.Database.CurrentTransaction != null))
            {
                command.Transaction = _context.Database.CurrentTransaction.GetDbTransaction();
            }
            else if ((transaction != null) && 
                (_context.Database.CurrentTransaction == null) && 
                (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always))
            {
                command.Transaction = transaction;
            }

            command.CommandText = statement;

            command.CommandType = commandType;

            command.CommandTimeout = timeout;

            if (parameters?.Any() ?? false)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            return command.ExecuteNonQuery();
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async ValueTask<int> ExecuteNonQueryAsync
    (
        string? statement,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    )
    {
        return await ValueTask.FromResult(ExecuteNonQuery(
            statement, commandType, commandTimeoutInSeconds, transaction, parameters));
    }

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
    public IDataReader? ExecuteReader
    (
        string? statement,
        CommandBehavior behavior = CommandBehavior.SingleResult | CommandBehavior.SequentialAccess,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    )
    {
        if (_context is not null && _context.Database is not null)
        {
            using DbCommand command = _context.Database.GetDbConnection().CreateCommand();

            if ((transaction is null) && (_context.Database.CurrentTransaction is not null))
            {
                command.Transaction = _context.Database.CurrentTransaction.GetDbTransaction();
            }
            else if ((transaction is not null) && 
                (_context.Database.CurrentTransaction is null) &&
                (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always))
            {
                command.Transaction = transaction;
            }

            command.CommandText = statement;

            command.CommandType = commandType;

            command.CommandTimeout = commandTimeoutInSeconds;

            if (parameters?.Any() ?? false)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            return command.ExecuteReader(behavior);
        }

        return default;
    }

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
    public async ValueTask<IDataReader?> ExecuteReaderAsync
    (
        string? statement,
        CommandBehavior behavior = CommandBehavior.SingleResult | CommandBehavior.SequentialAccess,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    )
    {
        return await ValueTask.FromResult(
            ExecuteReader(statement, behavior, commandType, commandTimeoutInSeconds, transaction, parameters));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public object? ExecuteScalar
    (
        string? statement,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    )
    {
        if ((_context is not null) && (_context.Database is not null) &&
            !string.IsNullOrEmpty(statement))
        {
            using DbCommand command = _context.Database.GetDbConnection().CreateCommand();

            if ((transaction is null) && (_context.Database.CurrentTransaction is not null))
            {
                command.Transaction = _context.Database.CurrentTransaction.GetDbTransaction();
            }
            else if ((transaction is not null) && (_context.Database.CurrentTransaction is null))
            {
                command.Transaction = transaction;
            }

            command.CommandText = statement;

            command.CommandType = commandType;

            command.CommandTimeout = commandTimeoutInSeconds;

            if (parameters?.Any() ?? false)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            return command.ExecuteScalar();
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeoutInSeconds"></param>
    /// <param name="transaction"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async ValueTask<object?> ExecuteScalarAsync
    (
        string? statement,
        CommandType commandType = CommandType.Text,
        int commandTimeoutInSeconds = 60,
        DbTransaction? transaction = null,
        IEnumerable<DbParameter>? parameters = null
    )
    {
        return await ValueTask.FromResult(
            ExecuteScalar(statement, commandType, commandTimeoutInSeconds, transaction, parameters));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statement"></param>
    /// <returns></returns>
    public IEnumerable<T>? ExecuteScript<T>(string? statement = null) where T : class
    {
        IEnumerable<T>? sequence = default;

        if (statement is null)
        {
            return sequence;
        }

        try
        {
            var query = Query<T>(statement, null);

            sequence = query?.AsEnumerable() ?? Enumerable.Empty<T>();
        }
        catch (Exception)
        {
            throw;
        }

        return sequence;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statement"></param>
    /// <returns></returns>
    public async ValueTask<IEnumerable<T>?> ExecuteScriptAsync<T>(string? statement = null) where T : class
    {
        IEnumerable<T>? sequence = default;

        if (statement is null)
        {
            return await ValueTask.FromResult(sequence);
        }

        try
        {
            var query = Query<T>(statement, null);

            sequence = query?.AsEnumerable() ?? Enumerable.Empty<T>();
        }
        catch (Exception)
        {
            throw;
        }

        return await ValueTask.FromResult(sequence);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? First<T>() where T : class
    {
        try
        {
            return List<T>().First();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? First<T>(Func<T, bool>? condition) where T : class
    {
        if (condition is not null)
        {
            return List(condition).First();
        }
        return First<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? FirstEntry<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (expression is not null)
        {
            return ListWhere(expression).First();
        }

        return First<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> FirstAsync<T>() where T : class
    {
        return await ValueTask.FromResult((await ListAsync<T>()).First());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> FirstAsync<T>(Func<T, bool>? condition) where T : class
    {
        return await ValueTask.FromResult((await ListAsync(condition)).First());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> FirstEntryAsync<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        return await ValueTask.FromResult((await ListWhereAsync(expression)).First());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T? FirstOrDefault<T>() where T : class
    {
        return List<T>().FirstOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? FirstOrDefault<T>(Func<T, bool>? condition) where T : class
    {
        return List(condition).FirstOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? FirstEntryOrDefault<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (expression is null)
        {
            return List<T>().FirstOrDefault();
        }

        var condition = expression.Compile();

        return List(condition).FirstOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async ValueTask<T?> FirstOrDefaultAsync<T>() where T : class
    {
        return await ValueTask.FromResult((await ListAsync<T>()).FirstOrDefault());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> FirstOrDefaultAsync<T>(Func<T, bool>? condition) where T : class
    {
        return await ValueTask.FromResult((await ListAsync(condition)).FirstOrDefault());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> FirstEntryOrDefaultAsync<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        return await ValueTask.FromResult((await ListWhereAsync(expression)).FirstOrDefault());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public string GetTableName<T>() where T : class
    {
        if (typeof(T).HasAttribute<TableAttribute>())
        {
            var attribute = TypeHelper.GetAttribute<TableAttribute>(typeof(T));

            if (attribute != null) return attribute.Name;
        }

        return string.Empty;
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
        if (_context != null)
        {
            return _context.GroupBy(query, expression);
        }

        if (query != null)
        {
            return query.GroupBy(expression);
        }

        return Enumerable.Empty<T>().GroupBy(expression.Compile()).AsQueryable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsClosed()
    {
        if (_context is not null && _context.Database is not null)
        {
            return _context.Database.GetDbConnection().State == ConnectionState.Closed;
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> IsClosedAsync()
    {
        if (_context is not null && _context.Database is not null)
        {
            return await ValueTask.FromResult(_context.Database.GetDbConnection().State == ConnectionState.Closed);
        }

        return true;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool IsEmpty<T>() where T : class
    {
        if (_context != null)
        {
            return !(_context.Set<T>()?.Any() ?? false);
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? Last<T>() where T : class
    {
        return List<T>().Last();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? Last<T>(Func<T, bool>? condition) where T : class
    {
        return List(condition).Last();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? LastEntry<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (expression is not null)
        {
            var condition = expression.Compile();

            return List(condition).Last();
        }
        return List<T>().Last();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> LastAsync<T>() where T : class
    {
        return await ValueTask.FromResult((await ListAsync<T>()).Last());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> LastAsync<T>(Func<T, bool>? condition) where T : class
    {
        return await ValueTask.FromResult((await ListAsync(condition)).Last());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> LastEntryAsync<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (expression is not null)
        {
            var condition = expression.Compile();

            return await ValueTask.FromResult((await ListAsync<T>(condition)).Last());
        }

        return await ValueTask.FromResult((await ListAsync<T>()).Last());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? LastOrDefault<T>() where T : class
    {
        return List<T>().LastOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public T? LastOrDefault<T>(Func<T, bool>? condition) where T : class
    {
        return List(condition).LastOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public T? LastEntryOrDefault<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (expression == null)
        {
            return List<T>().LastOrDefault();
        }

        var condition = expression.Compile();

        return List(condition).LastOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> LastOrDefaultAsync<T>() where T : class
    {
        return await ValueTask.FromResult((await ListAsync<T>()).LastOrDefault());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<T?> LastOrDefaultAsync<T>(Func<T, bool>? condition) where T : class
    {
        return await ValueTask.FromResult((await ListAsync(condition)).LastOrDefault());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async ValueTask<T?> LastEntryOrDefaultAsync<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (expression == null)
        {
            return await ValueTask.FromResult((await ListAsync<T>()).LastOrDefault());
        }

        var condition = expression.Compile();

        return await ValueTask.FromResult((await ListAsync(condition)).LastOrDefault());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<T> List<T>() where T : class
    {
        if (_context != null)
        {
            return _context.Set<T>()?.ToList() ?? new List<T>();
        }

        return new List<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public List<T> List<T>(Func<T, bool>? condition) where T : class
    {
        if (_context != null)
        {
            if (condition == null)
            {
                return _context.Set<T>()?.ToList() ?? new List<T>();
            }

            return _context.Set<T>()?.AsEnumerable().Where(x => condition(x)).ToList() ?? new List<T>();
        }

        return new List<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<List<T>> ListAsync<T>(Func<T, bool>? condition) where T : class
    {
        if (_context != null)
        {
            var collection = _context.Set<T>(); 

            if (collection != null && condition == null)
            {
                return await collection.ToListAsync();
            }
            else if (collection != null && condition != null)
            {
                return await collection.Where(x => condition(x)).ToListAsync();
            }
        }

        return await ValueTask.FromResult(new List<T>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public List<T> ListWhere<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (_context != null && expression == null)
        {
            return List<T>();
        }
        else if (_context != null && expression != null)
        {
            var condition = expression.Compile();

            var collection = _context.Set<T>();

            if (collection != null && condition != null)
            {
                return collection.Where(x => condition(x)).ToList();
            }
        }

        return new List<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<List<T>> ListAsync<T>() where T : class
    {
        if (_context != null)
        {
            return await ValueTask.FromResult(List<T>());
        }

        return await ValueTask.FromResult(new List<T>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async ValueTask<List<T>> ListWhereAsync<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (_context != null)
        {
            var collection = _context.Set<T>();

            if (collection != null && expression == null)
            {
                return await collection.ToListAsync<T>();
            }
            else if (collection != null && expression != null)
            {
                var condition = expression.Compile();

                return await collection.Where(x => condition(x)).ToListAsync();
            }
        }

        return await ValueTask.FromResult(new List<T>());
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool Open()
    {
        if (_context != null && _context.Database != null)
        {
            _context.Database.OpenConnection();

            return _context.Database.GetDbConnection().State == ConnectionState.Open;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> OpenAsync()
    {
        return await ValueTask.FromResult(Open());
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
        if (_context != null)
        {
            return _context.Rank(query, grouping, filter, ordering);
        }

        return new List<PartitionKey<T>>().AsQueryable();
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
        if (_context != null)
        {
            return _context.RowNumber(query, filter, ordering);
        }

        return new List<PartitionKey<T>>().AsQueryable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class
    {
        if (_context != null)
        {
            var collection = _context.Set<T>();

            if (collection != null && expression == null)
            {
                return collection.AsQueryable();
            }
            else if (collection != null && expression != null)
            {
                var condition = expression.Compile();

                return collection.Where(x => condition(x)).AsQueryable();
            }
        }

        return Enumerable.Empty<T>().AsQueryable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statement"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IQueryable<T> Query<T>(string? statement, Expression<Func<T, bool>>? expression) where T : class
    {
        if (_context != null && string.IsNullOrEmpty(statement))
        {
            var collection = _context.Set<T>();

            if (collection != null && expression == null)
            {
                return collection.AsQueryable();
            }
            else if (collection != null && expression != null)
            {
                var condition = expression.Compile();

                return collection.Where(x => condition(x)).AsQueryable();
            }
        }
        else if (_context != null && !string.IsNullOrEmpty(statement))
        {
            var sequence = (IEnumerable<T>?)ExecuteScalar(statement);

            if (sequence != null && expression == null)
            {
                return sequence.AsQueryable();
            }
            else if (sequence != null && expression != null)
            {
                var condition = expression.Compile();

                return sequence.Where(x => condition(x)).AsQueryable();
            }
        }

        return Enumerable.Empty<T>().AsQueryable();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async ValueTask<IQueryable<T>> QueryAsync<T>(Expression<Func<T, bool>> expression) where T : class
    {
        return await ValueTask.FromResult(Query<T>(expression));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statement"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async ValueTask<IQueryable<T>> QueryAsync<T>(string? statement, Expression<Func<T, bool>>? expression) where T : class
    {
        return await ValueTask.FromResult(Query(statement, expression));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Remove<T, A>(T? entity) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        var approved = false;

        if (entity is null) return approved;

        if (_context is not null && _context.Database is not null)
        {
            var tracker = _context.Remove(entity);

            var committed = _context.SaveChanges();

            if (tracker is not null)
            {
                approved = (committed > 0) &&
                    (tracker.State == EntityState.Detached) ||
                    (tracker.State == EntityState.Deleted) ||
                    (tracker.State == EntityState.Unchanged);

                if (approved && _context.Database.AutoTransactionBehavior == AutoTransactionBehavior.Always)
                {
                    _context.Database.CommitTransaction();   
                }
                else if (approved && 
                    (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always) &&
                    (_context.Database.CurrentTransaction != null))
                {
                    _context.Database.CurrentTransaction.Commit();
                }
                else if (!approved && _context.Database.AutoTransactionBehavior == AutoTransactionBehavior.Always)
                {
                    _context.Database.RollbackTransaction();
                }
                else if (!approved &&
                    (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always) &&
                    (_context.Database.CurrentTransaction != null))
                {
                    _context.Database.CurrentTransaction.Rollback();
                }
            }
        }

        return approved;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async ValueTask<bool> RemoveAsync<T, A>(T? entity) where T : class, A, IEquatable<A>, IDataPrimaryKey
    {
        return await ValueTask.FromResult(Remove<T, A>(entity));
    }

    /// <summary>
    /// 
    /// </summary>
    public void Rollback()
    {
        if (_context is not null && _context.Database is not null)
        {
            if (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always )
            {
                _context.Database.RollbackTransaction();
            }
            else if (_context.Database.CurrentTransaction is not null && 
                _context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always)
            {
                _context.Database.RollbackTransaction();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask RollbackAsync()
    {
        if (_context is not null && _context.Database is not null)
        {
            if (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always)
            {
                await _context.Database.RollbackTransactionAsync();
            }
            else if (_context.Database.CurrentTransaction is not null &&
                _context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always)
            {
                await _context.Database.RollbackTransactionAsync();
            }
        }

        await Task.Yield();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    public void RollbackTransaction(IDbContextTransaction? transaction)
    {
        if (transaction != null && _context != null && _context.Database != null)
        {
            if (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always)
            {
                transaction.Rollback();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async ValueTask RollbackTransactionAsync(IDbContextTransaction? transaction)
    {
        if (transaction != null && _context != null && _context.Database != null)
        {
            if (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always)
            {
                await transaction.RollbackAsync();
            }
        }

        await Task.Yield();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="savepoint"></param>
    public void RollbackTransactionToSavepoint(IDbContextTransaction? transaction, string? savepoint)
    {
        if (transaction is null && _context is not null && _context.Database is not null)
        {
            if (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always)
            {
                _context.Database.RollbackTransaction();
            }
        }
        else if (transaction is not null && string.IsNullOrEmpty(savepoint))
        {
            if (_context is not null &&
                _context.Database is not null && 
                (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always))
            {
                transaction.Rollback();
            }
        }
        else if (transaction is not null && !string.IsNullOrEmpty(savepoint))
        {
            if (_context is not null &&
                _context.Database is not null && 
                (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always))
            {
                transaction.RollbackToSavepoint(savepoint);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="savepoint"></param>
    /// <returns></returns>
    public async ValueTask RollbackTransactionToSavepointAsync(IDbContextTransaction? transaction, string? savepoint)
    {
        if (transaction is null && _context is not null && _context.Database is not null)
        {
            if (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always)
            {
                await _context.Database.RollbackTransactionAsync();
            }
        }
        else if (transaction is not null && string.IsNullOrEmpty(savepoint))
        {
            if (_context is not null &&
                _context.Database is not null &&
                (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always))
            {
                await transaction.RollbackAsync();
            }
        }
        else if (transaction is not null && !string.IsNullOrEmpty(savepoint))
        {
            if (_context is not null &&
                _context.Database is not null &&
                (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always))
            {
                await transaction.RollbackToSavepointAsync(savepoint.Trim());
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="preserveAllChanges"></param>
    /// <returns></returns>
    public int SaveChanges(bool preserveAllChanges = true)
    {
        if (_context is not null)
        {
            return _context.SaveChanges(preserveAllChanges);
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="preserveAllChanges"></param>
    /// <returns></returns>
    public async ValueTask<int> SaveChangesAsync(bool preserveAllChanges = true)
    {
        if (_context is not null)
        {
            return await _context.SaveChangesAsync(preserveAllChanges);
        }

        return await ValueTask.FromResult(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public DbSet<T>? Set<T>() where T : class
    {
        if (_context != null && _context.Context != null)
        {
            return _context.Context.Set<T>();
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async ValueTask<DbSet<T>?> SetAsync<T>() where T : class
    {
        return await ValueTask.FromResult(Set<T>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public bool Some<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        if (_context != null)
        {
            var collection = _context.Set<T>();

            if (collection != null && expression == null)
            {
                return true;
            }
            else if (collection != null && expression != null)
            {
                var condition = expression.Compile();

                return collection.Any(x => condition(x));
            }
        }

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<bool> SomeAsync<T>(Expression<Func<T, bool>>? expression) where T : class
    {
        return await ValueTask.FromResult(Some<T>(expression));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public EntityEntry<T>? Update<T>(T? entity) where T : class
    {
        if (entity is null) return default;

        if (_context is not null && _context.Database is not null)
        {
            var transaction = _context.Database.BeginTransaction();

            var tracker = _context.Update(entity);

            var committed = _context.SaveChanges(true);

            if (committed > 0 && (transaction == null) && 
                (_context.Database.AutoTransactionBehavior == AutoTransactionBehavior.Always))
            {
                _context.Database.CommitTransaction();

                return tracker;
            }
            else if (committed > 0 && (transaction != null))
            {
                transaction.Commit();

                return tracker;
            }
            else if (committed <= 0 && (transaction == null))
            {
                _context.Database.RollbackTransaction();
            }
            else if (committed <= 0 && (transaction != null))
            {
                transaction.Rollback();
            }
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async ValueTask<EntityEntry<T>?> UpdateAsync<T>(T? entity) where T : class
    {
        if (entity is null) return default;

        if (_context is not null && _context.Database is not null)
        {
            var transaction = _context.Database.BeginTransaction();

            var tracker = _context.Update(entity);

            var committed = _context.SaveChanges(true);

            if (committed > 0 && (transaction == null) &&
                (_context.Database.AutoTransactionBehavior == AutoTransactionBehavior.Always))
            {
                _context.Database.CommitTransaction();

                return await ValueTask.FromResult(tracker);
            }
            else if (committed > 0 && (transaction != null))
            {
                transaction.Commit();

                return await ValueTask.FromResult(tracker);
            }
            else if (committed <= 0 && (transaction == null))
            {
                _context.Database.RollbackTransaction();
            }
            else if (committed <= 0 && (transaction != null))
            {
                transaction.Rollback();
            }
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    public IDbContextTransaction? UseTransaction(DbTransaction? transaction, Guid? guid = null)
    {
        if (_context is not null && _context.Database is not null && (transaction is not null) &&
            (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always))
        {
            if (guid is null)
            {
                return _context.Database.UseTransaction(transaction);
            }
            else if (guid.Value == Guid.Empty)
            {
                return _context.Database.UseTransaction(transaction);
            }
            else
            {
                return _context.Database.UseTransaction(transaction, guid.Value);
            }
        }
        else if (_context is not null && _context.Database is not null && (transaction is not null) &&
            (_context.Database.AutoTransactionBehavior == AutoTransactionBehavior.Always))
        {
            return _context.Database.CurrentTransaction;
        }

        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async ValueTask<IDbContextTransaction?> UseTransactionAsync(DbTransaction? transaction, Guid? guid = null)
    {
        if (_context is not null && _context.Database is not null && (transaction is not null) &&
            (_context.Database.AutoTransactionBehavior != AutoTransactionBehavior.Always))
        {
            if (guid is null)
            {
                return await _context.Database.UseTransactionAsync(transaction);
            }
            else if (guid.Value == Guid.Empty)
            {
                return await _context.Database.UseTransactionAsync(transaction);
            }
            else
            {
                return await _context.Database.UseTransactionAsync(transaction, guid.Value);
            }
        }
        else if (_context is not null && _context.Database is not null && (transaction is not null) &&
            (_context.Database.AutoTransactionBehavior == AutoTransactionBehavior.Always))
        {
            return await ValueTask.FromResult(_context.Database.CurrentTransaction);
        }

        return default;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public IEnumerable<T> Where<T>(Func<T, bool> condition) where T : class
    {
        if (_context != null)
        {
            var collection = _context.Set<T>();

            if (collection != null && condition == null)
            {
                return collection;
            }
            else if (collection != null && condition != null)
            {
                return collection.Where(x => condition(x));
            }
        }

        return Enumerable.Empty<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IEnumerable<T> WhereAny<T>(Expression<Func<T, bool>> expression) where T : class
    {
        if (_context != null && expression == null)
        {
            return Enumerable.Where(_context.Set<T>() ?? Enumerable.Empty<T>(), (x) => true);
        }
        else if (_context != null && expression != null)
        {
            var condition = expression.Compile();

            return Enumerable.Where(_context.Set<T>()?.Where(x => condition(x)) ?? Enumerable.Empty<T>(), (x) => true);
        }

        return Enumerable.Empty<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async ValueTask<IEnumerable<T>> WhereAsync<T>(Func<T, bool> condition) where T : class
    {
        return await ValueTask.FromResult(Where(condition));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async ValueTask<IEnumerable<T>> WhereAnyAsync<T>(Expression<Func<T, bool>> expression) where T : class
    {
        return await ValueTask.FromResult(WhereAny<T>(expression));
    }

    #endregion

    #region IAsyncDisposable Members

    /// <summary>
    /// 
    /// </summary>
    public async ValueTask DisposeAsync() => await DisposeAsync(true);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    public async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing && (_context != null) && (_context.Context != null))
        {
            await _context.Context.DisposeAsync();
        }

        await Task.Yield();
    }

    #endregion

    #region IDisposable Members

    /// <summary>
    /// 
    /// </summary>
    public void Dispose() => Dispose(true);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    public void Dispose(bool disposing)
    {
        if (disposing && (_context != null) && (_context.Context != null))
        {
            _context.Context.Dispose();
        }
    }

    #endregion
}
