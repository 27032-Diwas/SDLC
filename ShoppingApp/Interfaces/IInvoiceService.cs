using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface IInvoiceService
{
    /// <summary>
    /// Generates an invoice for the specified order and user.
    /// </summary>
    /// <param name="order">The order for which the invoice is generated.</param>
    /// <param name="user">The user associated with the order.</param>
    /// <returns>The generated invoice.</returns>
    Invoice GenerateInvoice(Order order, User user);

    /// <summary>
    /// Retrieves the invoice associated with the specified order.
    /// </summary>
    /// <param name="orderId">Identifier of the order whose invoice is requested.</param>
    /// <returns>The matching invoice if found; otherwise, null.</returns>
    Invoice? GetInvoice(Guid orderId);

    /// <summary>
    /// Retrieves all invoices associated with a specific user.
    /// </summary>
    /// <param name="userId">Identifier of the user whose invoices are requested.</param>
    /// <returns>A list of invoices belonging to the specified user.</returns>
    List<Invoice> GetInvoices(Guid userId);
}