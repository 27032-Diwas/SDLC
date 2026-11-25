using ShoppingApp.Constants;
using ShoppingApp.Interfaces;

namespace ShoppingApp.Services;

public class AdminService
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
}