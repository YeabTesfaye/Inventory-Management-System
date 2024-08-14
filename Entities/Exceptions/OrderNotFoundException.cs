namespace Entities.Exceptions;

public sealed class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid orderId) : base($"The order with id: {orderId} doesn't exist in the database.")
    {
    }

}