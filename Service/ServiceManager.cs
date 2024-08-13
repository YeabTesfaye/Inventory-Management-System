using Contracts;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<CustomerService> _customerService;
    private readonly Lazy<ItemService> _itemService;
    private readonly Lazy<OrderService> _orderService;
    private readonly Lazy<ProductService> _productService;
    private readonly Lazy<SupplierService> _supplierService;

   public ServiceManager(IRepositoryManager repositoryManager){
      _customerService = new Lazy<CustomerService>(() => new 
      CustomerService(repositoryManager));
      _itemService = new Lazy<ItemService>(() => new
      ItemService(repositoryManager));
      _orderService = new Lazy<OrderService>(() => new
      OrderService(repositoryManager));
      _productService = new Lazy<ProductService>(() => new
      ProductService(repositoryManager));
      _supplierService = new Lazy<SupplierService>(() => new
      SupplierService(repositoryManager));
    }

    public ICustomerService CustomerService => _customerService.Value;

    public IItemService ItemService => _itemService.Value;

    public IOrderService OrderService =>_orderService.Value;

    public IProductService ProductService => _productService.Value ;

    public ISupplierService SupplierService => _supplierService.Value;
}