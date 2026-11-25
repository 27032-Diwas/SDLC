using System.Text.Json;
using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public class WishlistRepository
    : IWishlistRepository
{
    private const string FilePath =
        "Data/Wishlists.json";

    public List<WishlistItem> GetByUserId(
        Guid userId)
    {
        return this.GetAll()
            .Where(item =>
                item.UserId == userId)
            .ToList();
    }

    public void Add(WishlistItem item)
    {
        List<WishlistItem> wishlists =
            this.GetAll();

        bool exists =
            wishlists.Any(x =>
                x.UserId == item.UserId &&
                x.ProductId == item.ProductId);

        if (!exists)
        {
            wishlists.Add(item);
        }

        this.SaveAll(wishlists);
    }

    public void Remove(
        Guid userId,
        Guid productId)
    {
        List<WishlistItem> wishlists =
            this.GetAll();

        WishlistItem? item =
            wishlists.FirstOrDefault(
                x => x.UserId == userId &&
                     x.ProductId == productId);

        if (item != null)
        {
            wishlists.Remove(item);
        }

        this.SaveAll(wishlists);
    }

    private List<WishlistItem> GetAll()
    {
        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "[]");
        }

        string json =
            File.ReadAllText(FilePath);

        return JsonSerializer
            .Deserialize<List<WishlistItem>>(json)
            ?? [];
    }

    private void SaveAll(
        List<WishlistItem> wishlists)
    {
        string json =
            JsonSerializer.Serialize(
                wishlists,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

        File.WriteAllText(
            FilePath,
            json);
    }
}