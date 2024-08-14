using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class SupplierService  : ISupplierService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public SupplierService(IRepositoryManager repositoryManager, IMapper mapper){
        _repositoryManager = repositoryManager;
        _mapper = mapper;
     }

    public SupplierDto CreateSupplier(SupplierForCreationDto supplier)
    {
        var supplierEntity = _mapper.Map<Supplier>(supplier);
        _repositoryManager.Supplier.CreateSupplier(supplierEntity);
        _repositoryManager.Save();
        var supplierToReturn = _mapper.Map<SupplierDto>(supplierEntity);
        return supplierToReturn;
    }

    public SupplierDto GetSupplierById(Guid supplierId, bool trackChanges)
    {
        var supplier = _repositoryManager.Supplier.GetSupplierById(supplierId,trackChanges);
        var supplierDto = _mapper.Map<SupplierDto>(supplier);
        return supplierDto;
    }

    public IEnumerable<SupplierDto> GetSuppliers(bool trackChanges)
    {
        var suppliers = _repositoryManager.Supplier.GetSuppliers(trackChanges);
        var supplierDto = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        return supplierDto;
    }
}