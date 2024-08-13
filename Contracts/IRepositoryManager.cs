namespace Contracts;

 public interface IRepositoryManager
{
     ICustomerRepository Customer { get;  }
     IItemRepository Item { get;  }
     IOrderRepository Order { get;  }
     IProductRepository Product { get;  }
     ISupplierRepository Supplier { get;  }
}