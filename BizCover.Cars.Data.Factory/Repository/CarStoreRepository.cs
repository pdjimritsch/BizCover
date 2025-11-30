using System.Text.Json;

using BizCover.Cars.Common;
using BizCover.Cars.Models;
using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.Data.Factory.Repository;

using Abstraction;
using System.Runtime.CompilerServices;

/// <summary>
/// 
/// </summary>
public partial class CarStoreRepository : ICarStoreRepository 
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private static object _lock = new object();

    /// <summary>
    /// 
    /// </summary>
    private static bool _lockToken = false;

    /// <summary>
    /// Backing store
    /// </summary>
    private CarStore? _dictionary;

    /// <summary>
    /// 
    /// </summary>
    private string? _filename;

    /// <summary>
    /// 
    /// </summary>
    private Exception? _error;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    public CarStoreRepository() : base()
    {
        _dictionary = default;

        _error = default;

        _filename = default;
    }

    #endregion

    #region Index Based Properties

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public ICar? this[int index]
    {
        get 
        {
            ICar? vehicle = default;

            (string? content, Exception? error) outcome = GetFileContent(_filename);

            var content = outcome.content;

            _error = outcome.error;

            if (_error == null)
            {
                GetDescriptions(content);

                if ((_dictionary == null) || (index < 0))
                {
                    return vehicle;
                }
                else if ((_dictionary.Cars?.Count ?? 0) == 0)
                {
                    return vehicle;
                }
                else if (index >= (_dictionary?.Cars?.Count ?? 0))
                {
                    return vehicle;
                }

                vehicle = _dictionary?.Cars?[index] ?? default;
            }

            return vehicle;
        }
    }

    #endregion

    #region CarFileRepository Members

    /// <summary>
    /// Registers the vehicle synchronously into the backing storage.
    /// </summary>
    /// <param name="car">
    /// Nomiated vehicle
    /// </param>
    /// <returns>
    /// Vehicle identity
    /// </returns>
    public int Add(ICar? car)
    {
        int id = int.MinValue;

        try
        {
            if (_lockToken) _lockToken = false;

            Monitor.Enter(_lock, ref _lockToken);

            if (_dictionary != null && car != null)
            {
                var count = _dictionary.Cars.Count;

                /* vehicle identifier */
                car.Id = id = ++count;

                var vehicle = Car.TransForm(car);

                _dictionary.Cars.Add(vehicle);

                /* save the vehice within the dictionary */
                _error = Serialize(_filename, _dictionary);

                /* retrieves the vehicle dictionary into _dictionary */
                if (_error == null)
                {
                    Deserialize(_filename);
                }
            }
        }
        catch (Exception ex)
        {
            _error = ex;
        }
        finally
        {
            try
            {
                _lockToken = false;

                Monitor.Exit(_lock);
            }
            catch (Exception violation)
            {
                _error = violation;
            }
        }

        return id;
    }

    /// <summary>
    /// Registers the vehicle asynchronously into the backing storage.
    /// </summary>
    /// <param name="car">
    /// Nomiated vehicle
    /// </param>
    /// <returns>
    /// Vehicle identity
    /// </returns>
    public async Task<int> AddAsync(ICar? car)
    {
        int id = int.MinValue;

        try
        {
            if (_lockToken) _lockToken = false;

            Monitor.Enter(_lock, ref _lockToken);

            if (_dictionary != null && car != null)
            {
                var count = _dictionary.Cars.Count;

                /* vehicle identifier */
                car.Id = id = ++count;

                _dictionary.Cars.Add(Car.TransForm(car));

                /* save the vehice within the dictionary */
                Serialize(_filename, _dictionary);

                /* retrieves the vehicle dictionary into _dictionary */
                Deserialize(_filename);
            }
        }
        catch (Exception ex)
        {
            _error = ex;
        }
        finally
        {
            try
            {
                _lockToken = false;

                Monitor.Exit(_lock);
            }
            catch (Exception violation)
            {
                _error = violation;
            }
        }

        return await Task.FromResult(id);
    }

    /// <summary>
    /// Retrieves the vehicle details synchronously from the provided vehicle identity.
    /// </summary>
    /// <param name="id">
    /// Vehicle identity
    /// </param>
    /// <returns>
    /// Vehicle from the ensuing repository.
    /// </returns>
    public ICar? Get(int id)
    {
        var collection = GetAll();

        return collection.FirstOrDefault(v => v.Id == id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ICar?> GetAsync(int id)
    {
        var collection = await GetAllAsync();

        return await Task.FromResult(collection.FirstOrDefault(v => v.Id == id));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<ICar> GetAll()
    {
        List<ICar> collection = [];

        try
        {
            if (_lockToken) _lockToken = false;

            Monitor.Enter(_lock, ref _lockToken);

            if (_dictionary != null)
            {
                var sequence = _dictionary.GetSequence();

                collection = sequence.ToList();
            }
        }
        catch (Exception violation)
        {
            _error = violation;
        }
        finally 
        {
            try
            {
                _lockToken = false;

                Monitor.Exit(_lock);
            }
            catch (Exception violation)
            {
                _error = violation;
            }
        }

        return collection;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<ICar>> GetAllAsync()
    {
        var collection = GetAll();

        return await Task.FromResult(collection);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    public bool Update(ICar? car)
    {
        _error = null;

        try
        {
            if (_lockToken) _lockToken= false;

            Monitor.Enter(_lock, ref _lockToken);

            if (_dictionary != null && car != null && 
                !string.IsNullOrEmpty(_filename) && File.Exists(_filename))
            {
                /* retrieves the vehicle dictionary into _dictionary */
                Deserialize(_filename);

                if (_dictionary != null && _dictionary.Cars.Count > 0)
                {
                    var vehicle = _dictionary.Cars.FirstOrDefault(v => v.Id == car.Id);

                    if (vehicle != null)
                    {
                        _dictionary.Cars.Remove(vehicle);
                        _dictionary.Cars.Add(Car.TransForm(car));
                        _dictionary.Cars.Sort(new VehicleComparer());

                        _error = Serialize(_filename, _dictionary);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _error = ex;
        }
        finally 
        {
            try
            {
                _lockToken = false;

                Monitor.Exit(_lock);
            }
            catch (Exception violation)
            {
                _error = violation;
            }
        }

        return (_error == null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(ICar? car) => await Task.FromResult(Update(car));

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    public void Load(string? path, string? filename)
    {
        (string? directory, Exception? error) response = GetDomainDirectory(path);

        if (string.IsNullOrEmpty(response.directory))
        {
            _error = response.error;

            return;
        }

        _filename = GetResourcePath(response.directory, filename);

        (string? content, Exception? error) outcome = GetFileContent(_filename);

        var content = outcome.content;

        _error = outcome.error;

        if (_error == null)
        {
            GetDescriptions(content);
        }
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static (string?, Exception?) GetDomainDirectory(string? path)
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;

        Exception? error = default;

        if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
        {
            path = Path.Combine(directory, path);
        }
        else if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
        {
            DirectoryInfo? reference = default;

            try
            {
                reference = new DirectoryInfo(path);
            }
            catch (Exception violation)
            {
                error = violation;
                reference = default;
            }

            if (reference?.Exists ?? false)
            {
                path = reference.FullName;
            }
        }

        return (path, error);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private static (string?, Exception?) GetFileContent(string? filename)
    {
        string? content = default;

        Exception? error = null;

        if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
        {
            try
            {
                content = File.ReadAllText(filename, System.Text.Encoding.UTF8);
            }
            catch (Exception violation)
            {
                error = violation;
                content = default;
            }

            content = content.RemoveWhiteSpaces();
        }

        return (content, error);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="filename"></param>
    /// <returns></returns>
    private static string? GetResourcePath(string? directory, string? filename)
    {
        directory = directory.RemoveWhiteSpaces();

        filename = filename.RemoveWhiteSpaces();

        if (!string.IsNullOrEmpty(directory) &&
                    Directory.Exists(directory) &&
                    !string.IsNullOrEmpty(filename))
        {
            return Path.Combine(directory, filename.Trim());
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="sequence"></param>
    private static Exception? Serialize(string? filename, CarStore? dictionary)
    {
        Exception? error = default;

        if (!string.IsNullOrEmpty(filename) && (dictionary != null) && (dictionary.Cars.Count > 0))
        {
            try
            {
                if (_lockToken) _lockToken = false;

                Monitor.Enter(_lock, ref _lockToken);
                
                if (File.Exists(filename))
                {
                    using FileStream stream = File.Open(filename, FileMode.Truncate, FileAccess.Write, FileShare.Write);

                    Utf8JsonWriter writer = new(stream);

                    JsonSerializer.Serialize(writer, dictionary, typeof(CarStore));

                    writer.Flush();

                    stream.Flush();

                    stream.Close();
                }
                else /* recommended filename does not exist */
                {
                    using FileStream stream = File.Create(filename);

                    JsonSerializerOptions options = new()
                    {
                        AllowTrailingCommas = false,
                        AllowOutOfOrderMetadataProperties = true,
                        IncludeFields = false,
                        PropertyNameCaseInsensitive = true,
                        WriteIndented = true,
                        MaxDepth = int.MaxValue,
                    };

                    JsonSerializer.Serialize(stream, dictionary, typeof(CarStore), options);

                    stream.Flush();

                    stream.Close();
                }

            }
            catch (Exception violation)
            {
                error = violation;
            }
            finally
            {
                try
                {
                    _lockToken = false;

                    Monitor.Exit(_lock);
                }
                catch (Exception violation)
                {
                    error = violation;
                }
            }
        }

        return error;
    }

    /// <summary>
    /// Extracts the car store dictionary from the provided filename
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private void Deserialize(string? filename)
    {
        (string? content, Exception? error) outcome = GetFileContent(filename);

        var content = outcome.content;

        _error = outcome.error;

        if (_error == null) 
        { 
            GetDescriptions(content); 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    private void GetDescriptions(string? content)
    {
        content = content.RemoveWhiteSpaces();

        if (!string.IsNullOrEmpty(content))
        {
            try
            {
                if (_lockToken) _lockToken = false;

                Monitor.Enter(_lock, ref _lockToken);

                JsonSerializerOptions options = new()
                {
                    AllowTrailingCommas = false,
                    AllowOutOfOrderMetadataProperties = true,
                    IncludeFields = false,
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    MaxDepth = int.MaxValue,
                };

                _dictionary = JsonSerializer.Deserialize<CarStore>(content, options);
            }
            catch (Exception violation)
            {
                _dictionary = new CarStore();

                _error = violation;
            }
            finally
            {
                try
                {
                    _lockToken = false;

                    Monitor.Exit(_lock);
                }
                catch (Exception violation)
                {
                    _error = violation;
                }
            }
        }
    }

    #endregion
}
