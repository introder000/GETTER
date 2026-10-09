namespace GETTER.Domain.Common.ValueObjects;

public sealed record Phone
{
    public string Value { get; }

    private Phone(string value)
    {
        Value = value;
    }
    public static Phone Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(ValueObjectsErrors.PhoneLength);

        value = value.Trim();

        if (!value.All(c => char.IsAsciiDigit(c) || c is ' ' or '+' or '-' or '(' or ')'))
            throw new DomainException(ValueObjectsErrors.PhoneInvalidCharacters);

        if (value.LastIndexOf('+') > 0)
            throw new DomainException(ValueObjectsErrors.PhoneInvalidCharacters);

        value = value.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

        if (value.Length is < 9 or > 16)
            throw new DomainException(ValueObjectsErrors.PhoneLength);

        return new Phone(value);
    }
}

