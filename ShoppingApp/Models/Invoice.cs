namespace ShoppingApp.Models;

public class Invoice
{
    public Guid InvoiceId { get; set; }

    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public string CustomerName { get; set; }

    public DateTime InvoiceDate { get; set; }

    public List<CartItem> Items { get; set; } = [];

    public decimal ProductAmount { get; set; }

    public decimal DeliveryCharge { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal FinalAmount { get; set; }
}