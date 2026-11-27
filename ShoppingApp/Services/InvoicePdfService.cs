using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
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

        PdfDocument document = new();

        document.Info.Title =
            "Shopping App Invoice";

        PdfPage page =
            document.AddPage();

        XGraphics graphics =
            XGraphics.FromPdfPage(page);

        XFont headerFont =
            new XFont(
                "Arial",
                18,
                XFontStyle.Bold);

        XFont normalFont =
            new XFont(
                "Arial",
                12,
                XFontStyle.Regular);

        XFont boldFont =
            new XFont(
                "Arial",
                12,
                XFontStyle.Bold);

        int y = 40;

        graphics.DrawString(
            "SHOPPING APP INVOICE",
            headerFont,
            XBrushes.Black,
            new XRect(
                0,
                y,
                page.Width,
                20),
            XStringFormats.TopCenter);

        y += 40;

        graphics.DrawString(
            $"Invoice Number : {invoice.InvoiceId}",
            normalFont,
            XBrushes.Black,
            40,
            y);

        y += 20;

        graphics.DrawString(
            $"Customer Name : {invoice.CustomerName}",
            normalFont,
            XBrushes.Black,
            40,
            y);

        y += 20;

        graphics.DrawString(
            $"Date : {invoice.InvoiceDate:dd-MM-yyyy HH:mm}",
            normalFont,
            XBrushes.Black,
            40,
            y);

        y += 40;

        graphics.DrawString(
            "Product",
            boldFont,
            XBrushes.Black,
            40,
            y);

        graphics.DrawString(
            "Qty",
            boldFont,
            XBrushes.Black,
            250,
            y);

        graphics.DrawString(
            "Price",
            boldFont,
            XBrushes.Black,
            320,
            y);

        graphics.DrawString(
            "Total",
            boldFont,
            XBrushes.Black,
            420,
            y);

        y += 20;

        foreach (CartItem item in invoice.Items)
        {
            graphics.DrawString(
                item.ProductName,
                normalFont,
                XBrushes.Black,
                40,
                y);

            graphics.DrawString(
                item.Quantity.ToString(),
                normalFont,
                XBrushes.Black,
                250,
                y);

            graphics.DrawString(
                $"Rs. {item.UnitPrice}",
                normalFont,
                XBrushes.Black,
                320,
                y);

            graphics.DrawString(
                $"Rs. {item.TotalPrice}",
                normalFont,
                XBrushes.Black,
                420,
                y);

            y += 20;
        }

        y += 20;

        graphics.DrawString(
            $"Product Amount : Rs. {invoice.ProductAmount}",
            normalFont,
            XBrushes.Black,
            40,
            y);

        y += 20;

        graphics.DrawString(
            $"Delivery Charge : Rs. {invoice.DeliveryCharge}",
            normalFont,
            XBrushes.Black,
            40,
            y);

        y += 20;

        graphics.DrawString(
            $"Discount : Rs. {invoice.DiscountAmount}",
            normalFont,
            XBrushes.Black,
            40,
            y);

        y += 20;

        graphics.DrawString(
            $"Final Amount : Rs. {invoice.FinalAmount}",
            boldFont,
            XBrushes.Black,
            40,
            y);

        y += 40;

        graphics.DrawString(
            "Thank You For Shopping With Us",
            normalFont,
            XBrushes.Black,
            new XRect(
                0,
                y,
                page.Width,
                20),
            XStringFormats.TopCenter);

        document.Save(filePath);

        return filePath;
    }
}