using System.ComponentModel;

namespace BizCover.Cars.Models;

[ImmutableObject(true)] public sealed partial class VehicleComparer : IComparer<Car>
{
    #region IComparer<Car> Members

    /// <summary>
    /// Sorts the vehicle by the vehicle key identifier.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(Car? x, Car? y)
    {
        if (x != null && y != null)
        {
            return x.Id < y.Id ? -1 : 1;
        }
        else if (x == null && y != null)
        {
            return 1;
        }
        else if (x != null && y == null)
        {
            return -1;
        }

        return 0;
    }

    #endregion
}
