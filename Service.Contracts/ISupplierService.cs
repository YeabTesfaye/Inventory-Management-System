using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ISupplierService
{
    public IEnumerable<SupplierDto> GetSuppliers(bool trackChanges);
    public SupplierDto CreateSupplier(SupplierForCreationDto supplier);
    public SupplierDto GetSupplierById(Guid supplierId, bool trackChanges);
  
}