using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using ShoppingApp.Repository;

namespace ShoppingApp.Services;

public class CartService(
    ICartRepository cartRepository,
    IProductRepository productRepository)
    : ICartService
{
    public Cart GetCart(Guid userId)
    {
        return cartRepository.GetByUserId(userId);
    }

    public void AddToCart(
        Guid userId,
        Guid productId,
        int quantity)
    {
        Product? product =
            productRepository.GetProductById(productId);

        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        if (quantity > product.Stock)
        {
            throw new Exception("Insufficient stock.");
        }

        Cart cart =
            cartRepository.GetByUserId(userId);

        CartItem? existingItem =
            cart.Items.FirstOrDefault(
                x => x.ProductId == productId);

        if (existingItem != null)
        {
            if (existingItem.Quantity + quantity >
                product.Stock)
            {
                throw new Exception(
                    "Quantity exceeds stock.");
            }

            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(
                new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = quantity
                });
        }

        cartRepository.Save(cart);
    }

    public void UpdateQuantity(
        Guid userId,
        Guid productId,
        int quantity)
    {
        Cart cart =
            cartRepository.GetByUserId(userId);

        CartItem? item =
            cart.Items.FirstOrDefault(
                x => x.ProductId == productId);

        if (item == null)
        {
            return;
        }

        Product? product =
            productRepository.GetProductById(productId);

        if (product == null)
        {
            return;
        }

        if (quantity > product.Stock)
        {
            throw new InvalidOperationException($"Only {product.Stock} item(s) available in stock.");
        }

        item.Quantity = quantity;

        cartRepository.Save(cart);
    }

    public void RemoveProduct(
        Guid userId,
        Guid productId)
    {
        Cart cart =
            cartRepository.GetByUserId(userId);

        CartItem? item =
            cart.Items.FirstOrDefault(
                x => x.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
        }

        cartRepository.Save(cart);
    }

    public decimal GetTotal(Guid userId)
    {
        Cart cart =
            cartRepository.GetByUserId(userId);

        return cart.CartTotal;
    }
}