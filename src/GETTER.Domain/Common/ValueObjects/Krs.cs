namespace GETTER.Domain.Common.ValueObjects;

public sealed record Krs
{
    public string Value { get; }

    private Krs(string value)
    {
        Value = value;
    }

    public static Krs Create(string value)
    {
        return new(value);
    }
}
