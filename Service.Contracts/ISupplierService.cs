using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ISupplierService
{
    public IEnumerable<SupplierDto> GetSuppliers();
}