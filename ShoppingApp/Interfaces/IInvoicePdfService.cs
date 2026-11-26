using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface IInvoicePdfService
{
    /// <summary>
    /// Generates a PDF invoice for the specified invoice details.
    /// </summary>
    /// <param name="invoice">The invoice containing customer, order, and payment information.</param>
    /// <returns>The file path of the generated PDF invoice.</returns>
    string GenerateInvoicePdf(Invoice invoice);
}