namespace GETTER.Domain.Common.ValueObjects;

public sealed record Address
{
    public string Street { get; }
    public string City { get; }
    public string PostalCode { get; }
    public string Country { get; }
    public string CountryCode { get; }
    private Address(string street, string city, string postalCode, string country, string countryCode)
    {
        Street = street;
        City = city;
        PostalCode = postalCode;
        Country = country;
        CountryCode = countryCode;
    }
    public static Address Create(string street, string city, string postalCode, string country, string countryCode)
    {
        return new Address(
            street: ValidateText(StringNormalizer.Normalize(street), 5, 255, ValueObjectsErrors.StreetLength),
            city: ValidateText(StringNormalizer.Normalize(city), 3, 100, ValueObjectsErrors.CityLength),
            postalCode: ValidatePostalCode(StringNormalizer.Normalize(postalCode)),
            country: ValidateText(StringNormalizer.Normalize(country), 3, 100, ValueObjectsErrors.CountryLength),
            countryCode: ValidateCountryCode(StringNormalizer.Normalize(countryCode)));
    }

    private static string ValidateText(string? text, int minLength, int maxLength, Error error)
    {
        if (text is null || text.Length < minLength || text.Length > maxLength)
            throw new DomainException(error);

        return text;
    }

    private static string ValidatePostalCode(string? code)
    {
        if (code is null || code.Length < 3 || code.Length > 30)
            throw new DomainException(ValueObjectsErrors.PostalCodeLength);

        if (!code.All(c => char.IsAsciiLetterOrDigit(c) || c == ' ' || c == '-'))
            throw new DomainException(ValueObjectsErrors.PostalCodeInvalidCharacters);
        return code;
    }
    private static string ValidateCountryCode(string? countryCode)
    {
        var value = countryCode?.ToUpperInvariant();

        if (value is null || value.Length != 2)
            throw new DomainException(ValueObjectsErrors.CountryCodeLength);

        if (!value.All(char.IsAsciiLetter))
            throw new DomainException(ValueObjectsErrors.CountryCodeInvalid);

        return value;
    }
}