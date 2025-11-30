namespace BizCover.Cars.Data.Factory;

using Enumerations;

/// <summary>
/// 
/// </summary>
public static partial class DataFunctions
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public readonly static string DatePartName = @"DatePart";

    /// <summary>
    /// 
    /// </summary>
    public readonly static string DateOffsetPartName = @"DateOffsetPart";

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static int? DatePart(DatePartOffset offset, DateTime dt)
    {
        switch (offset)
        {
            case DatePartOffset.Year: return dt.Year;

            case DatePartOffset.Quarter:
                {
                    int quarter = dt.Month / 4;

                    return quarter + 1;
                }

            case DatePartOffset.DayOfYear: return dt.DayOfYear;

            case DatePartOffset.Day: return dt.Day;

            case DatePartOffset.Week:
                {
                    int yearday = dt.DayOfYear;

                    return yearday / 7;
                }

            case DatePartOffset.WeekDay: return (int)dt.DayOfWeek;

            case DatePartOffset.Hour: return dt.Hour;

            case DatePartOffset.Minute: return dt.Minute;

            case DatePartOffset.Second: return dt.Second;

            case DatePartOffset.Millisecond: return dt.Millisecond;

            case DatePartOffset.Microsecond: return dt.Microsecond;

            case DatePartOffset.NanoSecond: return dt.Nanosecond;

            case DatePartOffset.IsoWeek:
                {
                    if (DateTime.Compare(dt.Date, new DateTime(dt.Year, 12, 28).Date) <= 0)
                    {
                        return dt.DayOfYear / 7;
                    }
                    return 1;
                }
        }
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static int? DateOffsetPart(DatePartOffset offset, DateTimeOffset dt)
    {
        switch (offset)
        {
            case DatePartOffset.Year: return dt.Year;

            case DatePartOffset.Quarter:
                {
                    int quarter = dt.Month / 4;

                    return quarter + 1;
                }

            case DatePartOffset.DayOfYear: return dt.DayOfYear;

            case DatePartOffset.Day: return dt.Day;

            case DatePartOffset.Week:
                {
                    int yearday = dt.DayOfYear;

                    return yearday / 7;
                }

            case DatePartOffset.WeekDay: return (int)dt.DayOfWeek;

            case DatePartOffset.Hour: return dt.Hour;

            case DatePartOffset.Minute: return dt.Minute;

            case DatePartOffset.Second: return dt.Second;

            case DatePartOffset.Millisecond: return dt.Millisecond;

            case DatePartOffset.Microsecond: return dt.Microsecond;

            case DatePartOffset.NanoSecond: return dt.Nanosecond;

            case DatePartOffset.IsoWeek:
                {
                    if (DateTime.Compare(dt.Date, new DateTime(dt.Year, 12, 28).Date) <= 0)
                    {
                        return dt.DayOfYear / 7;
                    }
                    return 1;
                }
        }
        return null;
    }

    #endregion
}
