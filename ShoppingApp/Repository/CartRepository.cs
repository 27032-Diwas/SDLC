using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using System.Text.Json;

namespace ShoppingApp.Repository;

public class CartRepository : ICartRepository
{
    private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Carts.json");

    public Cart GetByUserId(Guid userId)
    {
        List<Cart> carts = this.GetAllCarts();

        Cart? cart =
            carts.FirstOrDefault(
                c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId
            };

            carts.Add(cart);
            this.SaveAll(carts);
        }

        return cart;
    }

    public void Save(Cart cart)
    {
        List<Cart> carts = this.GetAllCarts();

        int index =
            carts.FindIndex(
                c => c.UserId == cart.UserId);

        if (index >= 0)
        {
            carts[index] = cart;
        }
        else
        {
            carts.Add(cart);
        }

        this.SaveAll(carts);
    }

    public void Clear(Guid userId)
    {
        List<Cart> carts = this.GetAllCarts();

        Cart? cart =
            carts.FirstOrDefault(
                c => c.UserId == userId);

        if (cart != null)
        {
            cart.Items.Clear();
            this.SaveAll(carts);
        }
    }

    public void RemoveProductFromAllCarts(
    Guid productId)
    {
        List<Cart> carts =
            this.GetAllCarts();

        foreach (Cart cart in carts)
        {
            cart.Items.RemoveAll(
                item => item.ProductId == productId);
        }

        this.SaveAll(carts);
    }

    public void UpdateProductInCarts(
    Product product)
    {
        List<Cart> carts =
            this.GetAllCarts();

        foreach (Cart cart in carts)
        {
            CartItem? item =
                cart.Items.FirstOrDefault(
                    cartItem =>
                        cartItem.ProductId ==
                        product.ProductId);

            if (item != null)
            {
                item.ProductName =
                    product.Name;

                item.UnitPrice =
                    product.Price;
            }
        }

        this.SaveAll(carts);
    }

    private List<Cart> GetAllCarts()
    {

        string? directoryPath = Path.GetDirectoryName(this.FilePath);

        if (!string.IsNullOrEmpty(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "[]");
        }

        string json = File.ReadAllText(FilePath);

        return JsonSerializer.Deserialize<List<Cart>>(json)
               ?? [];
    }

    private void SaveAll(List<Cart> carts)
    {
        string json =
            JsonSerializer.Serialize(
                carts,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

        File.WriteAllText(FilePath, json);
    }
}