namespace ShoppingApp.Models;

/// <summary>
/// Represents a user's address information.
/// </summary>
public class Address
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class.
    /// </summary>
    /// <param name="addressLine1">The primary address line, such as house number and street name.</param>
    /// <param name="district">The district of the address.</param>
    /// <param name="state">The state of the address.</param>
    /// <param name="pincode">The postal code of the address.</param>
    public Address(
        string addressLine1,
        string district,
        string state,
        string pincode)
    {
        AddressLine1 = addressLine1;
        District = district;
        State = state;
        Pincode = pincode;
    }

    /// <summary>
    /// Gets or sets the primary address line.
    /// </summary>
    public string AddressLine1 { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the district of the address.
    /// </summary>
    public string District { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the state of the address.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the postal code of the address.
    /// </summary>
    public string Pincode { get; set; } = string.Empty;
}
