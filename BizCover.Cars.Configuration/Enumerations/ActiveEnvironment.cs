namespace BizCover.Cars.Configuration.Enumerations;

public enum ActiveEnvironment : byte
{
    Development,

    Staging,

    Production,

    Default = Development,

    Unknown = byte.MaxValue,
}
