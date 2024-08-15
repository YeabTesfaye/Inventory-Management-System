using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class SupplierService : ISupplierService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public SupplierService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<SupplierDto> CreateSupplierAsync(SupplierForCreationDto supplier)
    {
        var supplierEntity = _mapper.Map<Supplier>(supplier);
        _repositoryManager.Supplier.CreateSupplier(supplierEntity);
        await _repositoryManager.SaveAsync();
        var supplierToReturn = _mapper.Map<SupplierDto>(supplierEntity);
        return supplierToReturn;
    }

    public async Task DeleteSupplierAsync(Guid supplierId, bool trackChanges)
    {
        var supplier = await GetSupplierAndCheckIfItExists(supplierId, trackChanges: false);
        _repositoryManager.Supplier.DeleteSupplier(supplier);
        await _repositoryManager.SaveAsync();

    }

    public async Task<SupplierDto> GetSupplierByIdAsync(Guid supplierId, bool trackChanges)
    {
        var supplier = await _repositoryManager.Supplier.GetSupplierByIdAsync(supplierId, trackChanges);
        var supplierDto = _mapper.Map<SupplierDto>(supplier);
        return supplierDto;
    }

    public async Task<IEnumerable<SupplierDto>> GetSuppliersAsync(bool trackChanges)
    {

        var suppliers = await _repositoryManager.Supplier.GetAllSuppliersAsync(trackChanges);
        var supplierDto = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        return supplierDto;
    }
    private async Task<Supplier> GetSupplierAndCheckIfItExists(Guid supplierId, bool trackChanges)
    {
        var supplier = await _repositoryManager.Supplier.GetSupplierByIdAsync(supplierId, trackChanges)
         ?? throw new SupplierNotFoundException(supplierId);
        return supplier;
    }
}