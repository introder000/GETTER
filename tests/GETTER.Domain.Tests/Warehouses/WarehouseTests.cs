using GETTER.Domain.Common;
using GETTER.Domain.Warehouses;

namespace GETTER.Domain.Tests.Warehouses;

public class WarehouseTests
{
    public static IEnumerable<object[]> InvalidNames =>
    [
        ["ww", WarehouseErrors.WarehouseNameLength],
        [new string('A', 51), WarehouseErrors.WarehouseNameLength],
        ["Magazyn  Główny", WarehouseErrors.WarehouseNameDoubleSpaces],
        ["Magazyn@", WarehouseErrors.WarehouseNameInvalidCharacters],
        ["   ", WarehouseErrors.WarehouseNameLength],
        ["", WarehouseErrors.WarehouseNameLength],
        [" ", WarehouseErrors.WarehouseNameLength]
    ];

    public static IEnumerable<object[]> InvalidDescription =>
    [
        [new string('A', 201), WarehouseErrors.WarehouseDescriptionLength],
    ];
    public static IEnumerable<object[]> InvalidAddress =>
    [
        [new string('A', 201), WarehouseErrors.WarehouseAddressLength],
    ];


    [Theory]
    [MemberData(nameof(InvalidNames))]
    public void Create_WithInvalidName_ShouldThrowExpectedError(string name, Error expected)
    {
        Warehouse act() => Warehouse.Create(name, "TEST", WarehouseType.Physical, isActive: true);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected.Code, ex.Error.Code);
    }


    [Fact]
    public void Create_WithValidData_ShouldCreateWarehouse()
    {
        var name = "  Magazyn Główny  ";
        var symbol = "wro-01";

        var warehouse = Warehouse.Create(name, symbol, WarehouseType.Physical, isActive: true);

        Assert.Equal("Magazyn Główny", warehouse.Name);
        Assert.Equal("WRO-01", warehouse.Symbol.Value);
        Assert.True(warehouse.IsActive);
    }

    [Theory]
    [MemberData(nameof(InvalidDescription))]
    public void Create_WithInvalidDescription_ShouldThrowExpectedError(string description, Error expected)
    {
        Warehouse act() => Warehouse.Create("TEST", "TEST", WarehouseType.Physical, isActive: true, description: description);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected.Code, ex.Error.Code);
    }

    [Theory]
    [InlineData("  Opis  ", "Opis")]
    [InlineData("   ", null)]
    [InlineData(null, null)]
    public void Create_WithDescription_ShouldNormalizeDescription(string? input, string? expected)
    {
        var warehouse = Warehouse.Create("TEST", "TEST", WarehouseType.Physical, isActive: true, description: input);

        Assert.Equal(expected, warehouse.Description);
    }


    [Theory]
    [InlineData("  Adres  ", "Adres")]
    [InlineData("   ", null)]
    [InlineData(null, null)]
    public void Create_WithAddress_ShouldNormalizeAddress(string? input, string? expected)
    {
        var warehouse = Warehouse.Create("TEST", "TEST", WarehouseType.Physical, isActive: true, address: input);
        Assert.Equal(expected, warehouse.Address);
    }

    [Theory]
    [MemberData(nameof(InvalidAddress))]
    public void Create_WithInvalidAddress_ShouldThrowExpectedError(string address, Error expected)
    {
        Warehouse act() => Warehouse.Create("TEST", "TEST", WarehouseType.Physical, isActive: true, address: address);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected.Code, ex.Error.Code);
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        var warehouse = Warehouse.Create("TEST", "TEST", WarehouseType.Physical, isActive: false);
        Assert.False(warehouse.IsActive);

        warehouse.Activate();
        Assert.True(warehouse.IsActive);
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        var warehouse = Warehouse.Create("TEST", "TEST", WarehouseType.Physical, isActive: true);
        Assert.True(warehouse.IsActive);

        warehouse.Deactivate();
        Assert.False(warehouse.IsActive);
    }
}