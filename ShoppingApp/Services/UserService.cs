using ShoppingApp.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public bool RegisterUser(
        string name,
        string email,
        string phoneNumber,
        string password,
        Address? address)
    {
        User? existingUser =
            this.userRepository.GetUserByEmail(email);

        if (existingUser != null)
        {
            throw new Exception("User already exists.");
        }

        User user = new User(
            Guid.NewGuid(),
            name,
            email,
            phoneNumber,
            password,
            address);

        return this.userRepository.AddUser(user);
    }

    public User? Login(string email, string password)
    {
        User? user = this.userRepository.GetUserByEmail(email);

        if (user == null)
        {
            return null;
        }

        return user.Password == password
            ? user
            : null;
    }

    public void UpdateUser(User user)
    {
        userRepository.UpdateUser(user);
    }
}
