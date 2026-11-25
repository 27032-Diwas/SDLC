namespace ShoppingApp.Models;

public class CartItem
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice =>
        this.UnitPrice * this.Quantity;
}
