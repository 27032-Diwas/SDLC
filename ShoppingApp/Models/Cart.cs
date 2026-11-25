namespace ShoppingApp.Models;

public class Cart
{
    public Guid UserId { get; set; }

    public List<CartItem> Items { get; set; } = [];

    public decimal CartTotal =>
        this.Items.Sum(item => item.TotalPrice);
}