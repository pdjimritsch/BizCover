namespace BizCover.Cars.Configuration.Enumerations;

public enum ConnectionType : byte
{
    SqlServer = 0,
    MySql,
    Memory,
    MongoDB,
    Unknown = byte.MaxValue,
}
