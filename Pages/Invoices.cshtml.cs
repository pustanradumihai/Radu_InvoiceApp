using System.Reflection.Metadata;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.IO.Font.Constants;

namespace RadocInvoice.Pages
{
    public class InvoicesModel : PageModel
    {
        private readonly AppDbContext _context;

        public InvoicesModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoices { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty]
        public int InvoiceId { get; set; }

        public async Task<IActionResult> OnGetAsync(string downloadFile)
        {
            if (!string.IsNullOrEmpty(downloadFile) && System.IO.File.Exists(downloadFile))
            {
                var fileBytes = await System.IO.File.ReadAllBytesAsync(downloadFile);
                System.IO.File.Delete(downloadFile); // Clean up the temporary file
                return File(fileBytes, "application/pdf", $"invoice_{InvoiceId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            }

            var query = _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.Service)
                .Include(i => i.Doctor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(i => i.Client.Name.ToLower().Contains(SearchTerm.ToLower()) || i.Doctor.Name.ToLower().Contains(SearchTerm.ToLower()));
            }

            Invoices = await query.ToListAsync();

            ViewData["SearchTerm"] = SearchTerm;

            return Page();
        }

        public async Task<IActionResult> OnPostGeneratePdf()
        {
            var filePath = Path.GetTempFileName() + ".pdf";

            using (var writer = new PdfWriter(filePath))
            {
                var pdf = new PdfDocument(writer);
                var document = new iText.Layout.Document(pdf);

                var myInvoice = _context.Invoices
                    .Where(i => i.Id == InvoiceId)
                    .Include(i => i.Client)
                    .Include(i => i.Service)
                    .Include(i => i.Doctor)
                    .FirstOrDefault();

                if (myInvoice == null)
                {
                    return NotFound();
                }

                // Add client and doctor details
                var detailsTable = new Table(new float[] { 1, 1 }).UseAllAvailableWidth();
                detailsTable.AddCell(new Cell().Add(new Paragraph($"Client name: {myInvoice.Client.Name}\nClient's SSN: {myInvoice.Client.SocialSecurityNumber}"))
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                detailsTable.AddCell(new Cell().Add(new Paragraph($"Doctor name: {myInvoice.Doctor.Name}\nDoctor's practice license nr.: {myInvoice.Doctor.PracticeLicenseNumber}"))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER));

                document.Add(detailsTable);

                // Add more space after the details table
                document.Add(new Paragraph("\n\n"));

                // Add invoice title with date
                document.Add(new Paragraph($"Invoice nr. {myInvoice.Id.ToString().Substring(2)} / {myInvoice.Date:dd.MM.yyyy}")
                     .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                     .SetFontSize(24)
                     .SetUnderline());

                // Add more space between the tables
                document.Add(new Paragraph("\n\n"));

                // Itemized Table for Services
                var table = new Table(4).UseAllAvailableWidth();

                // Adding headers and aligning them to the center
                table.AddHeaderCell(new Cell().Add(new Paragraph("Service name")).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Quantity")).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Price")).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Total")).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                // Adding itemized rows and aligning each cell's content to the center
                table.AddCell(new Cell().Add(new Paragraph($"{myInvoice.Service.Name} ({myInvoice.Service.Details})")).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddCell(new Cell().Add(new Paragraph(myInvoice.Quantity.ToString())).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddCell(new Cell().Add(new Paragraph(myInvoice.Price.ToString("C", new System.Globalization.CultureInfo("ro-RO")))).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddCell(new Cell().Add(new Paragraph((myInvoice.Quantity * myInvoice.Price).ToString("C", new System.Globalization.CultureInfo("ro-RO")))).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                document.Add(table);

                document.Add(new Paragraph("\n"));

                // Add total
                var totalAmount = myInvoice.Quantity * myInvoice.Price;
                document.Add(new Paragraph($"Total Amount (19% VAT Included): {(totalAmount + totalAmount * 0.19m):0.00} RON")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFontSize(14)
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY));

         

                // Încarcă imaginea semnăturii (stampilei)
                string stampPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/stampila.png");
                iText.IO.Image.ImageData stampImageData = iText.IO.Image.ImageDataFactory.Create(stampPath);
                iText.Layout.Element.Image stampImage = new iText.Layout.Element.Image(stampImageData)
                    .SetFixedPosition(200, 290) // Poziția X și Y (ajustabilă)
                    .SetWidth(300) // Lățimea imaginii
                    .SetHeight(150); // Înălțimea imaginii

                // Adăugarea imaginii stampilei în document
                document.Add(stampImage);

                // --- End adăugare stampilă ---

                // Add footer at the bottom of the document
                var footer = new Paragraph("Generated by Radu's Invoice App - Confidential")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ITALIC))
                    .SetFontSize(10)
                    .SetFontColor(ColorConstants.GRAY);

                document.ShowTextAligned(footer, 297, 20, pdf.GetNumberOfPages(), iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.BOTTOM, 0);

                document.Close();
            }

            return RedirectToPage("./Invoices", new { downloadFile = filePath });
        }
    }
}

