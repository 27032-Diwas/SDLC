namespace ShoppingApp.Models;

public class Address
{
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
    public string AddressLine1 { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
}
