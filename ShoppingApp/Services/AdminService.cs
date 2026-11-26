using ShoppingApp.Constants;
using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Repository;

namespace ShoppingApp.Services;

public class AdminService(
    IProductRepository productRepository,
    ICartRepository cartRepository,
    IWishlistRepository wishlistRepository)
    : IAdminService
{
    public bool Login(
        string email,
        string password)
    {
        return email.Equals(
                   AdminCredentials.Email,
                   StringComparison.OrdinalIgnoreCase)
               &&
               password ==
               AdminCredentials.Password;
    }

    public void DeleteProduct(
        Guid productId)
    {
        productRepository.DeleteProduct(
            productId);

        cartRepository.RemoveProductFromAllCarts(
            productId);

        wishlistRepository.RemoveProductFromAllWishlists(
            productId);
    }

    public void UpdateProduct(
    Product product)
    {
        productRepository.UpdateProduct(
            product);

        cartRepository.UpdateProductInCarts(
            product);
    }
}