namespace Service.Contracts;

public interface IServiceManager
{
    ICustomerService CustomerService { get; }
    IItemService ItemService { get; }
    IOrderService OrderService { get; }
    IProductService ProductService { get; }
    ISupplierService SupplierService { get; }
    
}