using ShoppingApp.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Services;

public class ProductService(
    IProductRepository productRepository)
    : IProductService
{
    public List<Product> GetAllProducts()
    {
        return productRepository.GetAllProducts();
    }

    public Product? GetProductById(Guid productId)
    {
        return productRepository.GetProductById(productId);
    }

    public List<Product> SearchByName(string name)
    {
        return productRepository.SearchByName(name);
    }

    public List<Product> SearchByCategory(string category)
    {
        return productRepository.SearchByCategory(category);
    }

    public List<Product> SearchByPriceRange(
        decimal minPrice,
        decimal maxPrice)
    {
        return productRepository
            .SearchByPriceRange(
                minPrice,
                maxPrice);
    }

    public List<string> GetCategories()
    {
        return productRepository.GetCategories();
    }

    public void AddProduct(Product product)
    {
        productRepository.AddProduct(product);
    }

    public void UpdateProduct(Product product)
    {
        productRepository.UpdateProduct(product);
    }

    public void DeleteProduct(Guid productId)
    {
        productRepository.DeleteProduct(
            productId);
    }

    public void ImportProducts(string csvPath)
    {
        if (!File.Exists(csvPath))
        {
            throw new FileNotFoundException(
                "CSV file not found.");
        }

        List<Product> existingProducts =
            productRepository.GetAllProducts();

        List<Product> productsToImport = [];

        string[] lines =
            File.ReadAllLines(csvPath);

        if (lines.Length <= 1)
        {
            throw new Exception(
                "CSV file contains no product data.");
        }

        foreach (string line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] values =
                line.Split(',');

            if (values.Length < 5)
            {
                continue;
            }

            string name =
                values[0].Trim();

            string description =
                values[1].Trim();

            string category =
                values[2].Trim();

            decimal price =
                decimal.Parse(values[3].Trim());

            int stock =
                int.Parse(values[4].Trim());

            bool exists =
                existingProducts.Any(
                    product =>
                        product.Name.Equals(
                            name,
                            StringComparison.OrdinalIgnoreCase)
                        &&
                        product.Category.Equals(
                            category,
                            StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                continue;
            }

            Product product = new()
            {
                ProductId = Guid.NewGuid(),
                Name = name,
                Description = description,
                Category = category,
                Price = price,
                Stock = stock
            };

            productsToImport.Add(product);
        }

        if (!productsToImport.Any())
        {
            throw new Exception(
                "No new products found to import.");
        }

        productRepository.AddProducts(
            productsToImport);
    }

    public string ExportProducts(string exportPath)
    {
        List<Product> products =
            productRepository.GetAllProducts();

        List<string> lines =
        [
            "Name,Description,Category,Price,Stock"
        ];

        foreach (Product product in products)
        {
            lines.Add(
                $"{product.Name}," +
                $"{product.Description}," +
                $"{product.Category}," +
                $"{product.Price}," +
                $"{product.Stock}");
        }

        string? directory =
            Path.GetDirectoryName(exportPath);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllLines(
            exportPath,
            lines);

        return exportPath;
    }
}
