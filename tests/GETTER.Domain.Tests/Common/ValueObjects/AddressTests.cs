using GETTER.Domain.Common;
using GETTER.Domain.Common.ValueObjects;

namespace GETTER.Domain.Tests.Common.ValueObjects;

public class AddressTests
{
    private static Address CreateAddress(
        string street = "Długa 5",
        string city = "Wrocław",
        string postalCode = "50-001",
        string country = "Polska",
        string countryCode = "PL")
        => Address.Create(street, city, postalCode, country, countryCode);

    public static TheoryData<string, string> InvalidStreet => new()
    {
        { "", ValueObjectsErrors.StreetLength.Code },
        { "   ", ValueObjectsErrors.StreetLength.Code },
        { "abcd", ValueObjectsErrors.StreetLength.Code },
        { new string('a', 256), ValueObjectsErrors.StreetLength.Code },
        { "😀", ValueObjectsErrors.StreetLength.Code },
    };

    public static TheoryData<string, string> InvalidCity => new()
    {
        { "", ValueObjectsErrors.CityLength.Code },
        { "ab", ValueObjectsErrors.CityLength.Code },
        { new string('a', 101), ValueObjectsErrors.CityLength.Code },
        { "😀", ValueObjectsErrors.CityLength.Code },
    };

    public static TheoryData<string, string> InvalidPostalCode => new()
    {
        { "", ValueObjectsErrors.PostalCodeLength.Code },
        { "ab", ValueObjectsErrors.PostalCodeLength.Code },
        { new string('A', 31), ValueObjectsErrors.PostalCodeLength.Code },
        { "50_001", ValueObjectsErrors.PostalCodeInvalidCharacters.Code },
        { "50/001", ValueObjectsErrors.PostalCodeInvalidCharacters.Code },
        { "50-00ą", ValueObjectsErrors.PostalCodeInvalidCharacters.Code },
    };

    public static TheoryData<string, string> InvalidCountry => new()
    {
        { "", ValueObjectsErrors.CountryLength.Code },
        { "Pl", ValueObjectsErrors.CountryLength.Code },
        { new string('a', 101), ValueObjectsErrors.CountryLength.Code },
    };

    public static TheoryData<string, string> InvalidCountryCode => new()
    {
        { "", ValueObjectsErrors.CountryCodeLength.Code },
        { "P", ValueObjectsErrors.CountryCodeLength.Code },
        { "POL", ValueObjectsErrors.CountryCodeLength.Code },
        { "😀", ValueObjectsErrors.CountryCodeLength.Code },
        { "P1", ValueObjectsErrors.CountryCodeInvalid.Code },
        { "1!", ValueObjectsErrors.CountryCodeInvalid.Code },
    };

    [Theory]
    [MemberData(nameof(InvalidStreet))]
    public void Create_WithInvalidStreet_ShouldThrowExpectedError(string input, string expected)
    {
        Address act() => CreateAddress(street: input);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected, ex.Error.Code);
    }

    [Theory]
    [MemberData(nameof(InvalidCity))]
    public void Create_WithInvalidCity_ShouldThrowExpectedError(string input, string expected)
    {
        Address act() => CreateAddress(city: input);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected, ex.Error.Code);
    }

    [Theory]
    [MemberData(nameof(InvalidPostalCode))]
    public void Create_WithInvalidPostalCode_ShouldThrowExpectedError(string input, string expected)
    {
        Address act() => CreateAddress(postalCode: input);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected, ex.Error.Code);
    }

    [Theory]
    [MemberData(nameof(InvalidCountry))]
    public void Create_WithInvalidCountry_ShouldThrowExpectedError(string input, string expected)
    {
        Address act() => CreateAddress(country: input);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected, ex.Error.Code);
    }

    [Theory]
    [MemberData(nameof(InvalidCountryCode))]
    public void Create_WithInvalidCountryCode_ShouldThrowExpectedError(string input, string expected)
    {
        Address act() => CreateAddress(countryCode: input);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected, ex.Error.Code);
    }

    [Fact]
    public void Create_WithValidData_ShouldNormalizeAllFields()
    {
        var address = CreateAddress(
            street: "  Długa   5  ",
            city: "  Wrocław ",
            postalCode: " 50-001 ",
            country: " Polska ",
            countryCode: " pl ");

        Assert.Equal("Długa 5", address.Street);
        Assert.Equal("Wrocław", address.City);
        Assert.Equal("50-001", address.PostalCode);
        Assert.Equal("Polska", address.Country);
        Assert.Equal("PL", address.CountryCode);
    }

    [Theory]
    [InlineData(5, 3, 3, 3)]
    [InlineData(255, 100, 30, 100)]
    public void Create_WithBoundaryLengths_ShouldCreateAddress(int street, int city, int postalCode, int country)
    {
        var address = CreateAddress(
            street: new string('a', street),
            city: new string('a', city),
            postalCode: new string('A', postalCode),
            country: new string('a', country));

        Assert.Equal(street, address.Street.Length);
        Assert.Equal(city, address.City.Length);
        Assert.Equal(postalCode, address.PostalCode.Length);
        Assert.Equal(country, address.Country.Length);
    }

    [Fact]
    public void TwoAddresses_WithSameData_ShouldBeEqual()
    {
        var first = CreateAddress();
        var second = CreateAddress(street: "  Długa 5 ", countryCode: "pl");

        Assert.Equal(first, second);
    }

    [Fact]
    public void TwoAddresses_WithDifferentData_ShouldNotBeEqual()
    {
        var first = CreateAddress();
        var second = CreateAddress(city: "Kraków");

        Assert.NotEqual(first, second);
    }
}
