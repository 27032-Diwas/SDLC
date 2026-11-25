using ShoppingApp.Models;
using ShoppingApp.Repository;

namespace ShoppingApp.Services;

public class InvoiceService(
    IInvoiceRepository invoiceRepository)
    : IInvoiceService
{
    public Invoice GenerateInvoice(
        Order order,
        User user)
    {
        decimal productAmount =
            order.TotalAmount;

        decimal deliveryCharge =
            this.CalculateDeliveryCharge(
                user.Address.State);

        decimal discountAmount = 0;

        decimal finalAmount =
            productAmount +
            deliveryCharge -
            discountAmount;

        Invoice invoice = new()
        {
            InvoiceId = Guid.NewGuid(),
            OrderId = order.OrderId,
            UserId = user.UserId,
            CustomerName = user.Name,
            InvoiceDate = DateTime.Now,
            Items = order.Items,
            ProductAmount = productAmount,
            DeliveryCharge = deliveryCharge,
            DiscountAmount = discountAmount,
            FinalAmount = finalAmount
        };

        invoiceRepository.AddInvoice(
            invoice);

        return invoice;
    }

    public Invoice? GetInvoice(
        Guid orderId)
    {
        return invoiceRepository
            .GetByOrderId(orderId);
    }

    public List<Invoice> GetInvoices(
        Guid userId)
    {
        return invoiceRepository
            .GetByUserId(userId);
    }

    private decimal CalculateDeliveryCharge(
        string state)
    {
        return state.Equals(
            "Tamil Nadu",
            StringComparison.OrdinalIgnoreCase)
            ? 50
            : 100;
    }
}