namespace BizCover.Cars.Common;

public static partial class AutoNumberGenerator
{
    #region Members


    /// <summary>
    /// 
    /// </summary>
    static object _lock = new object();

    /// <summary>
    /// 
    /// </summary>
    static bool _lockTaken = false;

    /// <summary>
    /// 
    /// </summary>
    volatile static int _id = 0;

    /// <summary>
    /// Gets the current integer based value
    /// </summary>
    public static int Id => _id;

    /// <summary>
    /// 
    /// </summary>
    public static Exception? Decrement()
    {
        Exception? err = default;

        try
        {
            if (_lockTaken) _lockTaken = false;

            Monitor.Enter(_lock, ref _lockTaken);

            Interlocked.Decrement(ref _id);
        }
        catch (Exception violation)
        {
            if (_lockTaken)
            {
                Interlocked.Increment(ref _id);
            }

            err = violation;
        }
        finally
        {
            try
            {
                _lockTaken = false;

                Monitor.Exit(_lock);
            }
            catch (Exception violation)
            {
                err = violation;
            }
        }

        return err;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="seed"></param>
    public static Exception? Increment(int seed = -1)
    {
        Exception? err = default;

        if (seed >= 0)
        {
            try
            {
                if (_lockTaken) _lockTaken = false;

                Monitor.Enter(_lock, ref _lockTaken);

                _id = seed;

                Interlocked.Increment(ref _id);
            }
            catch (Exception violation)
            {
                if (_lockTaken)
                {
                    Interlocked.Decrement(ref _id);
                }

                err = violation;
            }
            finally
            {
                try
                {
                    _lockTaken = false;

                    Monitor.Exit(_lock);
                }
                catch (Exception violation)
                {
                    err = violation;
                }
            }
        }
        else
        {
            try
            {
                if (_lockTaken) _lockTaken = false;

                Monitor.Enter(_lock, ref _lockTaken);

                Interlocked.Increment(ref _id);
            }
            catch (Exception violation)
            {
                if (_lockTaken)
                {
                    Interlocked.Decrement(ref _id);
                }

                err = violation;
            }
            finally 
            {
                try
                {
                    _lockTaken = false;

                    Monitor.Exit(_lock);
                }
                catch (Exception violation)
                {
                    err = violation;
                }
            }
        }

        return err;
    }

    #endregion
}
