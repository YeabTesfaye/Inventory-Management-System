using System.Data.Common;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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

    public async Task<(IEnumerable<SupplierDto> suppliers, MetaData metaData)> GetSuppliersAsync(SupplierParameters supplierParameters, bool trackChanges)
    {

        var suppliersWithMetaData = await _repositoryManager.Supplier.GetAllSuppliersAsync(supplierParameters, trackChanges);
        var supplierDto = _mapper.Map<IEnumerable<SupplierDto>>(suppliersWithMetaData);
        return (suppliers: supplierDto, metaData: suppliersWithMetaData.MetaData);
    }

    public async Task UpdateSupplierAsync(Guid supplierId, SupplierForUpdateDto supplier, bool trackChanges)
    {
        var supplierEntity = await GetSupplierAndCheckIfItExists(supplierId, trackChanges);
        _mapper.Map(supplier, supplierEntity);
        await _repositoryManager.SaveAsync();
    }
    public async Task UpdateCustomerAsync(Guid id, CustomerForUpdateDto customer, bool trackChanges)
    {
        // Fetch the customer from the repository with tracking as specified
        var customerEntity = await _repositoryManager.Customer.GetCustomerAsync(id, trackChanges)
                             ?? throw new CustomerNotFoundException(id);

        // Map the DTO to the entity
        _mapper.Map(customer, customerEntity);

        // Save the changes to the database
        await _repositoryManager.SaveAsync();
    }

    private async Task<Supplier> GetSupplierAndCheckIfItExists(Guid supplierId, bool trackChanges)
    {
        var supplier = await _repositoryManager.Supplier.GetSupplierByIdAsync(supplierId, trackChanges)
         ?? throw new SupplierNotFoundException(supplierId);
        return supplier;
    }
}