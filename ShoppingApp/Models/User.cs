using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Models;

/// <summary>
/// Represents a user account.
/// </summary>
public class User
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="userId">Unique identifier of user.</param>
    /// <param name="name">Name of user.</param>
    /// <param name="email">Email address of user.</param>
    /// <param name="mobileNumber">Mobile number of user.</param>
    /// <param name="password">Password of user.</param>
    /// <param name="address">Address of user.</param>
    public User(
        Guid userId,
        string name,
        string email,
        string mobileNumber,
        string password,
        Address address)
    {
        this.UserId = userId;
        this.Name = name;
        this.Email = email;
        this.MobileNumber = mobileNumber;
        this.Password = password;
        this.Address = address;
        this.CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// Gets the unique identifier of user.
    /// </summary>
    /// <value>
    /// Unique identifier of user.
    /// </value>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the name of user.
    /// </summary>
    /// <value>
    /// Name of user.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of user.
    /// </summary>
    /// <value>
    /// Email address of user.
    /// </value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the mobile number of user.
    /// </summary>
    /// <value>
    /// Mobile number of user.
    /// </value>
    public string MobileNumber { get; set; }

    /// <summary>
    /// Gets or sets the password of user.
    /// </summary>
    /// <value>
    /// Password of user.
    /// </value>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the address of user.
    /// </summary>
    /// <value>
    /// Address of user.
    /// </value>
    public Address Address { get; set; }

    public DateTime CreatedAt { get; set; }
}
