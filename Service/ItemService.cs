using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public sealed class ItemService : IItemService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public ItemService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<ItemDto> items, MetaData metaData)> GetItemsOfOrderAsync(Guid orderId, ItemParameters itemParameters, bool trackChanges)
    {
        await CheckIfOrderExists(orderId, trackChanges: false);
        var itemsWithMetaData =
       await _repositoryManager.Item.GetItemsOfOrderAsync(orderId, itemParameters, trackChanges);
        var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(itemsWithMetaData);
        return (items: itemsDto, metaData: itemsWithMetaData.MetaData);
    }


    public async Task<ItemDto?> GetItemsByProductIdAsync(Guid orderId, Guid productId)
    {
        await CheckIfOrderExists(orderId, trackChanges: false);
        await CheckIfProductExists(productId, trackChanges: false);
        var items = await _repositoryManager.Item.GetItemsByProductIdAsync(productId)
        ?? throw new ProductNotFoundException(productId);
        var itemsDto = _mapper.Map<ItemDto>(items);
        return itemsDto;
    }

    public async Task<ItemDto> CreateItemAsync(Guid orderId, ItemForCreationDto item)
    {
        await CheckIfOrderExists(orderId, trackChanges: true);
        var itemEntity = _mapper.Map<Item>(item);
        _repositoryManager.Item.CreateItem(itemEntity);
        await _repositoryManager.SaveAsync();
        var itemToReturn = _mapper.Map<ItemDto>(itemEntity);
        return itemToReturn;
    }

    public Task DeleteItemAsync(Guid id, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public async Task<ItemDto> GetItemByItemIdAsync(Guid itemId, bool trackChanges)
    {
        var item = await _repositoryManager.Item.GetItemByItemIdAsync(itemId, trackChanges);
        var itemToReturn = _mapper.Map<ItemDto>(item);
        return itemToReturn;
    }

    public async Task DeleteItemByItemIdAsync(Guid id)
    {
        var item = await GetItemAndCheckIfItExists(id, trackChanges: true);
        _repositoryManager.Item.DeleteItem(item);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateItemAsync(Guid orderId, Guid itemId, ItemForUpdateDto item
    , bool orderTrackChanges, bool itemTrackChanges)
    {
        // Check if the order exists
        await CheckIfOrderExists(orderId, orderTrackChanges);

        // Retrieve the item and check if it exists
        var itemEntity = await GetItemAndCheckIfItExists(itemId, itemTrackChanges);


        // Map the DTO to the entity

        _mapper.Map(item, itemEntity);

        // Save the changes to the database
        await _repositoryManager.SaveAsync();
    }


    private async Task<Item> GetItemAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var item = _ = await _repositoryManager.Item.GetItemByItemIdAsync(id, trackChanges)
        ?? throw new ItemNotFoundException(id);
        return item;
    }

    private async Task CheckIfOrderExists(Guid orderId, bool trackChanges)
    {
        _ = await _repositoryManager.Order.GetOrderByIdAsync(orderId, trackChanges)
        ?? throw new OrderNotFoundException(orderId);
    }
    private async Task CheckIfProductExists(Guid productId, bool trackChanges)
    {
        _ = await _repositoryManager.Product.GetProductAsync(productId, trackChanges)
        ?? throw new ProductNotFoundException(productId);
    }


}