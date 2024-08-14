using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

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

    public IEnumerable<ItemDto> GetItemsOfOrder(Guid orderId, bool trackChanges)
    {
        var items =
        _repositoryManager.Item.GetItemsOfOrder(orderId, trackChanges);
        var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);
        return itemsDto;
    }


    public ItemDto? GetItemsByProductId(Guid productId)
    {
        var items = _repositoryManager.Item.GetItemsByProductId(productId)
        ?? throw new ProductNotFoundException(productId);
        var itemsDto = _mapper.Map<ItemDto>(items);
        return itemsDto;
    }

    public ItemDto CreateItem(ItemForCreationDto item)
    {
        var itemEntity = _mapper.Map<Item>(item);
        _repositoryManager.Item.CreateItem(itemEntity);
        _repositoryManager.Save();
        var itemToReturn = _mapper.Map<ItemDto>(itemEntity);
        return itemToReturn;
    }

}