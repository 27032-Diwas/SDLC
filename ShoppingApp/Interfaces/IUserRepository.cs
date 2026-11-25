using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface IUserRepository
{
    bool AddUser(User user);
    User? GetUserByEmail(string email);
}
