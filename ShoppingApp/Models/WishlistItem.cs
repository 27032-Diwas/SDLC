namespace ShoppingApp.Models;

public class WishlistItem
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public DateTime AddedOn { get; set; }
}