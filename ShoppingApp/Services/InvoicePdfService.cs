using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ShoppingApp.Models;

namespace ShoppingApp.Services;

public class InvoicePdfService
    : IInvoicePdfService
{
    public string GenerateInvoicePdf(
        Invoice invoice)
    {
        string folderPath = "Invoices";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath =
            Path.Combine(
                folderPath,
                $"Invoice_{invoice.InvoiceId}.pdf");

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);

                page.Header()
                    .Text("SHOPPING APP INVOICE")
                    .FontSize(20)
                    .Bold();

                page.Content()
                    .Column(column =>
                    {
                        column.Item().Text(
                            $"Invoice Number : {invoice.InvoiceId}");

                        column.Item().Text(
                            $"Customer Name : {invoice.CustomerName}");

                        column.Item().Text(
                            $"Date : {invoice.InvoiceDate}");

                        column.Item().PaddingVertical(10);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Product");
                                header.Cell().Text("Qty");
                                header.Cell().Text("Price");
                                header.Cell().Text("Total");
                            });

                            foreach (CartItem item in invoice.Items)
                            {
                                table.Cell().Text(item.ProductName);

                                table.Cell().Text(
                                    item.Quantity.ToString());

                                table.Cell().Text(
                                    $"₹{item.UnitPrice}");

                                table.Cell().Text(
                                    $"₹{item.TotalPrice}");
                            }
                        });

                        column.Item().PaddingTop(20);

                        column.Item().Text(
                            $"Product Amount : ₹{invoice.ProductAmount}");

                        column.Item().Text(
                            $"Delivery Charge : ₹{invoice.DeliveryCharge}");

                        column.Item().Text(
                            $"Discount : ₹{invoice.DiscountAmount}");

                        column.Item().Text(
                            $"Final Amount : ₹{invoice.FinalAmount}")
                            .Bold()
                            .FontSize(16);
                    });

                page.Footer()
                    .AlignCenter()
                    .Text("Thank You For Shopping With Us");
            });
        })
        .GeneratePdf(filePath);

        return filePath;
    }
}
