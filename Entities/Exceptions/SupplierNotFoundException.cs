namespace Entities.Exceptions;

public sealed class SupplierNotFoundException : NotFoundException
{
    public SupplierNotFoundException(Guid supplierId)
: base($"The supplier with id: {supplierId} doesn't exist in the database.")
    {
    }

}