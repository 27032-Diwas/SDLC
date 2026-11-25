namespace ShoppingApp.Interfaces;

public interface IAdminService
{
    bool Login(
        string email,
        string password);
}