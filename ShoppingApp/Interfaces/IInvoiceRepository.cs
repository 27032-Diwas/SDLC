using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public interface IInvoiceRepository
{
    void AddInvoice(Invoice invoice);

    Invoice? GetByOrderId(Guid orderId);

    List<Invoice> GetByUserId(Guid userId);
}