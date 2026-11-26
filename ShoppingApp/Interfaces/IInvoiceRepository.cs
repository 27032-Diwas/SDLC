using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public interface IInvoiceRepository
{
    /// <summary>
    /// Adds a new invoice to the invoice repository.
    /// </summary>
    /// <param name="invoice">The invoice to be stored.</param>
    void AddInvoice(Invoice invoice);

    /// <summary>
    /// Retrieves an invoice associated with the specified order.
    /// </summary>
    /// <param name="orderId">Identifier of the order whose invoice is requested.</param>
    /// <returns>The matching invoice if found; otherwise, null.</returns>
    Invoice? GetByOrderId(Guid orderId);

    /// <summary>
    /// Retrieves all invoices associated with a specific user.
    /// </summary>
    /// <param name="userId">Identifier of the user whose invoices are requested.</param>
    /// <returns>A list of invoices belonging to the specified user.</returns>
    List<Invoice> GetByUserId(Guid userId);
}