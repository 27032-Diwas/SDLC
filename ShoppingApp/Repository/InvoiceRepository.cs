using System.Text.Json;
using ShoppingApp.Models;

namespace ShoppingApp.Repository;

public class InvoiceRepository
    : IInvoiceRepository
{
    private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Invoices.json");

    public void AddInvoice(
        Invoice invoice)
    {
        List<Invoice> invoices =
            this.GetAllInvoices();

        invoices.Add(invoice);

        this.SaveAll(invoices);
    }

    public Invoice? GetByOrderId(
        Guid orderId)
    {
        return this.GetAllInvoices()
            .FirstOrDefault(
                invoice =>
                    invoice.OrderId == orderId);
    }

    public List<Invoice> GetByUserId(
        Guid userId)
    {
        return this.GetAllInvoices()
            .Where(invoice =>
                invoice.UserId == userId)
            .ToList();
    }

    private List<Invoice> GetAllInvoices()
    {

        string? directoryPath = Path.GetDirectoryName(this.FilePath);

        if (!string.IsNullOrEmpty(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "[]");
        }

        string json =
            File.ReadAllText(FilePath);

        return JsonSerializer
            .Deserialize<List<Invoice>>(json)
            ?? [];
    }

    private void SaveAll(
        List<Invoice> invoices)
    {
        string json =
            JsonSerializer.Serialize(
                invoices,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

        File.WriteAllText(
            FilePath,
            json);
    }
}