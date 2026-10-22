using GETTER.Application.Common;
using GETTER.Domain.Common;
using GETTER.Domain.Warehouses;

namespace GETTER.Application.Warehouses.CreateWarehouse;

public sealed class CreateWarehouseCommandHandler
{
    public Task<Result<Guid>> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
    {
        try
        {
            WarehouseSymbol symbol = WarehouseSymbol.Create(command.Symbol);

            var warehouse = Warehouse.Create(command.Name, symbol, command.Type, command.IsActive, command.Description, command.Address);

            Result<Guid> res = Result<Guid>.Success(warehouse.Id);

            return Task.FromResult(res);
        }
        catch (DomainException ex)
        {
            Result<Guid> res = Result<Guid>.Failure(ex.Error);
            return Task.FromResult(res);
        }
    }
}