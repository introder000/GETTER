using GETTER.Application.Warehouses.CreateWarehouse;
using GETTER.Domain.Warehouses;

namespace GETTER.Application.Tests;

public class CreateWarehouseCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldReturnSuccess()
    {
        CreateWarehouseCommandHandler handler = new();

        var result = await handler.Handle(new CreateWarehouseCommand()
        {
            Name = "Main",
            Symbol = "Main",
            Type = WarehouseType.Physical,
            IsActive = true,
            Description = null,
            Address = null
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
    }

    [Fact]
    public async Task Handle_WithInvalidSymbol_ShouldReturnFailure()
    {
        CreateWarehouseCommandHandler handler = new();

        var result = await handler.Handle(new CreateWarehouseCommand()
        {
            Name = "Main",
            Symbol = "g",
            Type = WarehouseType.Physical,
            IsActive = true,
            Description = null,
            Address = null
        }, CancellationToken.None);

        Assert.True(result.IsFail);
        Assert.NotNull(result.Error);
        Assert.Equal(WarehouseErrors.WarehouseSymbolLength.Code, result.Error.Code);
    }
}
