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
            street: ValidateText(street, 5, 255, ValueObjectsErrors.StreetLength),
            city: ValidateText(city, 3, 100, ValueObjectsErrors.CityLength),
            postalCode: ValidatePostalCode(postalCode),
            country: ValidateText(country, 3, 100, ValueObjectsErrors.CountryLength),
            countryCode: ValidateCountryCode(countryCode));
    }

    private static string ValidateText(string? text, int minLength, int maxLength, Error error)
    {
        text = StringNormalizer.Normalize(text) ?? throw new DomainException(error);

        if (text.Length < minLength || text.Length > maxLength)
            throw new DomainException(error);

        return text;
    }

    private static string ValidatePostalCode(string? code)
    {
        code = StringNormalizer.Normalize(code) ?? throw new DomainException(ValueObjectsErrors.PostalCodeLength);

        if (code.Length < 3 || code.Length > 30)
            throw new DomainException(ValueObjectsErrors.PostalCodeLength);

        if (!code.All(c => char.IsAsciiLetterOrDigit(c) || c == ' ' || c == '-'))
            throw new DomainException(ValueObjectsErrors.PostalCodeInvalidCharacters);

        return code;
    }
    private static string ValidateCountryCode(string? countryCode)
    {
        countryCode = StringNormalizer.Normalize(countryCode) ?? throw new DomainException(ValueObjectsErrors.CountryCodeLength);
        countryCode = countryCode.ToUpperInvariant();

        if (countryCode.Length != 2)
            throw new DomainException(ValueObjectsErrors.CountryCodeLength);

        if (!countryCode.All(char.IsAsciiLetter))
            throw new DomainException(ValueObjectsErrors.CountryCodeInvalid);

        return countryCode;
    }
}