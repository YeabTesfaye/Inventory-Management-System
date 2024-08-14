using AutoMapper;
using Contracts;
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

    public IEnumerable<SupplierDto> GetSuppliers(bool trackChanges)
    {
        var suppliers = _repositoryManager.Supplier.GetSuppliers(trackChanges);
        var supplierDto = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        return supplierDto;
    }
}