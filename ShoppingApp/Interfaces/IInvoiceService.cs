using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface IInvoiceService
{
    Invoice GenerateInvoice(
        Order order,
        User user);

    Invoice? GetInvoice(
        Guid orderId);

    List<Invoice> GetInvoices(
        Guid userId);
}