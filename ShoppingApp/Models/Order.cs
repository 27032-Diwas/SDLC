namespace ShoppingApp.Models;

public class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public List<CartItem> Items { get; set; } = [];

    public decimal TotalAmount { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; }
}