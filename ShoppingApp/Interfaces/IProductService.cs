using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface IProductService
{
    /// <summary>
    /// Retrieves all products available in the catalog.
    /// </summary>
    /// <returns>A list of all products.</returns>
    List<Product> GetAllProducts();

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="productId">Identifier of the product to retrieve.</param>
    /// <returns>The matching product if found; otherwise, null.</returns>
    Product? GetProductById(Guid productId);

    /// <summary>
    /// Searches for products whose names match the specified search term.
    /// </summary>
    /// <param name="name">The product name or keyword to search for.</param>
    /// <returns>A list of matching products.</returns>
    List<Product> SearchByName(string name);

    /// <summary>
    /// Retrieves products belonging to the specified category.
    /// </summary>
    /// <param name="category">The category to search for.</param>
    /// <returns>A list of products in the specified category.</returns>
    List<Product> SearchByCategory(string category);

    /// <summary>
    /// Retrieves products within the specified price range.
    /// </summary>
    /// <param name="minPrice">The minimum price of the range.</param>
    /// <param name="maxPrice">The maximum price of the range.</param>
    /// <returns>A list of products within the specified price range.</returns>
    List<Product> SearchByPriceRange(decimal minPrice, decimal maxPrice);

    /// <summary>
    /// Retrieves all available product categories.
    /// </summary>
    /// <returns>A list of product category names.</returns>
    List<string> GetCategories();

    /// <summary>
    /// Adds a new product to the catalog.
    /// </summary>
    /// <param name="product">The product to be added.</param>
    void AddProduct(Product product);

    /// <summary>
    /// Updates the details of an existing product.
    /// </summary>
    /// <param name="product">The product containing the updated information.</param>
    void UpdateProduct(Product product);

    /// <summary>
    /// Removes a product from the catalog.
    /// </summary>
    /// <param name="productId">Identifier of the product to remove.</param>
    void DeleteProduct(Guid productId);

    void ImportProducts(string csvPath);

    string ExportProducts(string exportPath);
}