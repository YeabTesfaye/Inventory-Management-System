using AutoMapper;
using Contracts;
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

    public IEnumerable<ItemDto> GetAllItems(bool trackChanges)
    {
        try
        {
            var items =
            _repositoryManager.Item.GetAllItems(trackChanges);
             var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);
             return itemsDto;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    public IEnumerable<ItemDto> GetItemsByOrder(Guid orderId)
    {
      var items = _repositoryManager.Item.GetItemsByOrder(orderId);
      var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);
      return itemsDto;
    }

    public IEnumerable<ItemDto> GetItemsByProduct(Guid productId)
    {
        var items = _repositoryManager.Item.GetItemsByProduct(productId);
        var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);
        return itemsDto;
    }
}