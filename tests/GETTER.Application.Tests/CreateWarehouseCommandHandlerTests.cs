using GETTER.Application.Tests.Fakes;
using GETTER.Application.Warehouses.CreateWarehouse;
using GETTER.Domain.Warehouses;

namespace GETTER.Application.Tests;

public class CreateWarehouseCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldReturnSuccess()
    {
        CreateWarehouseCommandHandler handler = new(new FakeWarehouseRepository(), new FakeUnitOfWork());

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
        var repository = new FakeWarehouseRepository();
        var unitOfWork = new FakeUnitOfWork();
        var handler = new CreateWarehouseCommandHandler(repository, unitOfWork);

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
        Assert.Empty(repository.Items);
        Assert.Equal(0, unitOfWork.SaveChangesCallCount);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldAddWarehouseAndSaveChanges()
    {
        var repository = new FakeWarehouseRepository();
        var unitOfWork = new FakeUnitOfWork();
        var handler = new CreateWarehouseCommandHandler(repository, unitOfWork);

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
        Assert.Single(repository.Items);
        Assert.Equal(1, unitOfWork.SaveChangesCallCount);
        Assert.Equal(repository.Items[0].Id, result.Value);
    }
    [Fact]
    public async Task Handle_WithExistingSymbol_ShouldReturnFailureAndNotSave()
    {
        var repository = new FakeWarehouseRepository();
        var unitOfWork = new FakeUnitOfWork();
        var handler = new CreateWarehouseCommandHandler(repository, unitOfWork);

        repository.Add(Warehouse.Create("Existing", WarehouseSymbol.Create("MAIN"), WarehouseType.Physical, true));

        var result = await handler.Handle(new CreateWarehouseCommand()
        {
            Name = "Main",
            Symbol = "Main",
            Type = WarehouseType.Physical,
            IsActive = true,
            Description = null,
            Address = null
        }, CancellationToken.None);

        Assert.True(result.IsFail);
        Assert.NotNull(result.Error);
        Assert.Equal(WarehouseErrors.WarehouseSymbolAlreadyExists.Code, result.Error.Code);
        Assert.Single(repository.Items);
        Assert.Equal(0, unitOfWork.SaveChangesCallCount);
    }
}
