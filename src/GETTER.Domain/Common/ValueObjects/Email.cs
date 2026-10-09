namespace GETTER.Domain.Common.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(ValueObjectsErrors.EmailLength);

        value = value.Trim();

        if (value.Length is < 8 or > 255)
            throw new DomainException(ValueObjectsErrors.EmailLength);

        if (!value.Contains('@') || !value[(value.IndexOf('@') + 1)..].Contains('.'))
            throw new DomainException(ValueObjectsErrors.EmailInvalidCharacters);

        return new Email(value.ToLowerInvariant());
    }


}

