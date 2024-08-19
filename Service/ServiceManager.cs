using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
  private readonly Lazy<ICustomerService> _customerService;
  private readonly Lazy<IItemService> _itemService;
  private readonly Lazy<IOrderService> _orderService;
  private readonly Lazy<IProductService> _productService;
  private readonly Lazy<ISupplierService> _supplierService;
  private readonly Lazy<IAuthenticationService> _authenticationService;

  public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper,
  UserManager<User> userManager, IConfiguration configuration)
  {
    _customerService = new Lazy<ICustomerService>(() => new
    CustomerService(repositoryManager, mapper));
    _itemService = new Lazy<IItemService>(() => new
    ItemService(repositoryManager, mapper));
    _orderService = new Lazy<IOrderService>(() => new
    OrderService(repositoryManager, mapper));
    _productService = new Lazy<IProductService>(() => new
    ProductService(repositoryManager, mapper));
    _supplierService = new Lazy<ISupplierService>(() => new
    SupplierService(repositoryManager, mapper));

    _authenticationService = new Lazy<IAuthenticationService>(() =>
     new AuthenticationService(mapper, userManager, configuration));
  }

  public ICustomerService CustomerService => _customerService.Value;

  public IItemService ItemService => _itemService.Value;

  public IOrderService OrderService => _orderService.Value;

  public IProductService ProductService => _productService.Value;

  public ISupplierService SupplierService => _supplierService.Value;

  public IAuthenticationService AuthenticationService => _authenticationService.Value;
}