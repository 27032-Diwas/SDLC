using ShoppingApp.Models;

namespace ShoppingApp.Services;

public interface IInvoicePdfService
{
    string GenerateInvoicePdf(Invoice invoice);
}