using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using System.Text.Json;

namespace ShoppingApp.Repository;
public class ProductRepository : IProductRepository
{
    private const string FilePath = "Data/Products.json";

    public List<Product> GetAllProducts()
    {
        string json = File.ReadAllText(FilePath);

        return JsonSerializer.Deserialize<List<Product>>(json)
               ?? [];
    }

    public Product? GetProductById(Guid productId)
    {
        return this.GetAllProducts()
            .FirstOrDefault(x =>
                x.ProductId == productId);
    }

    public List<Product> SearchByName(string name)
    {
        return this.GetAllProducts()
            .Where(x =>
                x.Name.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Product> SearchByCategory(string category)
    {
        return this.GetAllProducts()
            .Where(x =>
                x.Category.Equals(
                    category,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Product> SearchByPriceRange(
        decimal minPrice,
        decimal maxPrice)
    {
        return this.GetAllProducts()
            .Where(x =>
                x.Price >= minPrice &&
                x.Price <= maxPrice)
            .ToList();
    }

    public void UpdateProduct(Product updatedProduct)
    {
        List<Product> products =
            this.GetAllProducts();

        int index =
            products.FindIndex(x =>
                x.ProductId ==
                updatedProduct.ProductId);

        if (index >= 0)
        {
            products[index] = updatedProduct;
        }

        string json =
            JsonSerializer.Serialize(
                products,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

        File.WriteAllText(
            FilePath,
            json);
    }
}
