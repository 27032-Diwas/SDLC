namespace ShoppingApp.Repository;

using ShoppingApp.Interfaces;
using ShoppingApp.Models;
using System.Text.Json;

public class UserRepository : IUserRepository
{
    private readonly string filePath = "Data/Users.json";

    public bool AddUser(User user)
    {
        List<User> users = this.GetAllUsers();

        users.Add(user);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(users, options);
        File.WriteAllText(this.filePath, json);

        return true;
    }

    public User? GetUserByEmail(string email)
    {
        List<User> users = this.GetAllUsers();

        return users.FirstOrDefault(
            u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    private List<User> GetAllUsers()
    {
        if (!File.Exists(this.filePath))
        {
            File.WriteAllText(this.filePath, "[]");
        }

        string json = File.ReadAllText(this.filePath);

        return JsonSerializer.Deserialize<List<User>>(json)
               ?? new List<User>();
    }
}
