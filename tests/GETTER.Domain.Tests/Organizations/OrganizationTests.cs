using GETTER.Domain.Common;
using GETTER.Domain.Common.ValueObjects;
using GETTER.Domain.Organizations;

namespace GETTER.Domain.Tests.Organizations;

public class OrganizationTests
{
    private static readonly Phone ValidPhone = Phone.Create("+48123456789");
    private static readonly Email ValidEmail = Email.Create("firma@firma.pl");
    private static readonly Address ValidAddress = Address.Create("Długa 5", "Wrocław", "50-001", "Polska", "PL");

    public static TheoryData<string, string> InvalidNames => new()
    {
        { "", OrganizationErrors.InvalidName.Code },
        { "   ", OrganizationErrors.InvalidName.Code },
        { "ab", OrganizationErrors.InvalidName.Code },
        { new string('A', 256), OrganizationErrors.InvalidName.Code },
        { "😀", OrganizationErrors.InvalidName.Code },
    };

    [Theory]
    [MemberData(nameof(InvalidNames))]
    public void Create_WithInvalidName_ShouldThrowExpectedError(string name, string expected)
    {
        Organization act() => Organization.Create(name, ValidPhone, ValidEmail, ValidAddress);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected, ex.Error.Code);
    }

    [Theory]
    [InlineData("  Firma  ", "Firma")]
    [InlineData("Firma   Sp. z o.o.", "Firma Sp. z o.o.")]
    [InlineData("Firma 😀 ABC", "Firma ABC")]
    public void Create_WithName_ShouldNormalizeName(string input, string expected)
    {
        var organization = Organization.Create(input, ValidPhone, ValidEmail, ValidAddress);

        Assert.Equal(expected, organization.Name);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(255)]
    public void Create_WithBoundaryNameLength_ShouldCreateOrganization(int length)
    {
        var organization = Organization.Create(new string('A', length), ValidPhone, ValidEmail, ValidAddress);

        Assert.Equal(length, organization.Name.Length);
    }

    [Fact]
    public void Create_WithNullPhone_ShouldThrowArgumentNullException()
    {
        static Organization act() => Organization.Create("Firma", null!, ValidEmail, ValidAddress);

        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Create_WithNullEmail_ShouldThrowArgumentNullException()
    {
        static Organization act() => Organization.Create("Firma", ValidPhone, null!, ValidAddress);

        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Create_WithNullAddress_ShouldThrowArgumentNullException()
    {
        static Organization act() => Organization.Create("Firma", ValidPhone, ValidEmail, null!);

        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Create_WithRequiredData_ShouldCreateOrganizationWithoutOptionalIdentifiers()
    {
        var organization = Organization.Create("  Firma  ", ValidPhone, ValidEmail, ValidAddress);

        Assert.NotEqual(Guid.Empty, organization.Id);
        Assert.Equal("Firma", organization.Name);
        Assert.Same(ValidPhone, organization.Phone);
        Assert.Same(ValidEmail, organization.Email);
        Assert.Same(ValidAddress, organization.Address);
        Assert.Null(organization.Nip);
        Assert.Null(organization.Regon);
        Assert.Null(organization.Krs);
        Assert.Null(organization.Bdo);
        Assert.Null(organization.Eori);
    }

    [Fact]
    public void Create_WithOptionalIdentifiers_ShouldAssignThem()
    {
        var nip = Nip.Create("1234567890", "PL");
        var regon = Regon.Create("123456789");
        var krs = Krs.Create("0000123456");
        var bdo = Bdo.Create("000012345");
        var eori = Eori.Create("PL1234567890");

        var organization = Organization.Create("Firma", ValidPhone, ValidEmail, ValidAddress, nip, regon, krs, bdo, eori);

        Assert.Same(nip, organization.Nip);
        Assert.Same(regon, organization.Regon);
        Assert.Same(krs, organization.Krs);
        Assert.Same(bdo, organization.Bdo);
        Assert.Same(eori, organization.Eori);
    }

    [Fact]
    public void Create_CalledTwice_ShouldGenerateDifferentIds()
    {
        var first = Organization.Create("Firma", ValidPhone, ValidEmail, ValidAddress);
        var second = Organization.Create("Firma", ValidPhone, ValidEmail, ValidAddress);

        Assert.NotEqual(first.Id, second.Id);
    }
}
