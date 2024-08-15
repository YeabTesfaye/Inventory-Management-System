using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICustomerRepository> _cutomerRepository;
    private readonly Lazy<IItemRepository> _itemRepository;
    private readonly Lazy<IOrderRepository> _orderRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly Lazy<ISupplierRepository> _supplierRepository;



    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _cutomerRepository = new Lazy<ICustomerRepository>(() => new
         CustomerRepository(repositoryContext));
        _itemRepository = new Lazy<IItemRepository>(() => new
        ItemRepository(repositoryContext));
        _orderRepository = new Lazy<IOrderRepository>(() => new
        OrderRepository(repositoryContext));
        _productRepository = new Lazy<IProductRepository>(() => new
        ProductRepository(repositoryContext));
        _supplierRepository = new Lazy<ISupplierRepository>(() => new
       SupplierRepository(repositoryContext));

    }
    public ICustomerRepository Customer => _cutomerRepository.Value;

    public IItemRepository Item => _itemRepository.Value;

    public IOrderRepository Order => _orderRepository.Value;

    public IProductRepository Product => _productRepository.Value;

    public ISupplierRepository Supplier => _supplierRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}