using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface IUserService
{
    bool RegisterUser(
        string name,
        string email,
        string phoneNumber,
        string password,
        Address address);

    User? Login(string email, string password);
}
