using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using DrugAbuseReportingSystem.Models;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;
using Color = iText.Kernel.Colors.Color;
using Document = iText.Layout.Document;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using iText.Bouncycastle;


namespace DrugAbuseReportingSystem.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ReportService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<int> CreateReportAsync(Report report)
        {
            if (report.ImageFile != null && report.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + report.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await report.ImageFile.CopyToAsync(fileStream);
                }

                report.ImagePath = "~/" + uniqueFileName;
            }

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report.ReportId;
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _context.Reports.OrderByDescending(r => r.ReportDate).ToListAsync();
        }

        public async Task<Report?> GetReportByIdAsync(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task UpdateReportAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReportAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                if (!string.IsNullOrEmpty(report.ImagePath))
                {
                    var filePath = Path.Combine(_environment.WebRootPath, report.ImagePath.TrimStart('/'));
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Report>> SearchReportsAsync(string searchTerm, DateTime? fromDate, DateTime? toDate, DrugType? drugType, SeverityLevel? severity)
        {
            var query = _context.Reports.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(r => r.Location.Contains(searchTerm) || r.Description.Contains(searchTerm));
            }

            if (fromDate.HasValue)
            {
                query = query.Where(r => r.ReportDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(r => r.ReportDate <= toDate.Value);
            }

            if (drugType.HasValue)
            {
                query = query.Where(r => r.DrugType == drugType.Value);
            }

            if (severity.HasValue)
            {
                query = query.Where(r => r.Severity == severity.Value);
            }

            return await query.OrderByDescending(r => r.ReportDate).ToListAsync();
        }

        public async Task<byte[]> GeneratePdfReportAsync(IEnumerable<Report> reports)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Initialize PDF writer
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Add title
                var title = new Paragraph("Drug Abuse Reports")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(18);
                document.Add(title);

                // Add generation date
                var date = new Paragraph($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm}")
                    .SetFontSize(10)
                    .SetMarginBottom(20);
                document.Add(date);

                // Create table
                var table = new Table(6, true)
                    .SetWidth(UnitValue.CreatePercentValue(100));

                // Add headers with styling
                var headerFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                var headerBackground = new DeviceRgb(63, 81, 181); // Primary color

                table.AddHeaderCell(new Cell().Add(new Paragraph("Date").SetFont(headerFont))
                    .SetBackgroundColor(headerBackground)
                    .SetFontColor(ColorConstants.WHITE));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Location").SetFont(headerFont))
                    .SetBackgroundColor(headerBackground)
                    .SetFontColor(ColorConstants.WHITE));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Drug Type").SetFont(headerFont))
                    .SetBackgroundColor(headerBackground)
                    .SetFontColor(ColorConstants.WHITE));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Severity").SetFont(headerFont))
                    .SetBackgroundColor(headerBackground)
                    .SetFontColor(ColorConstants.WHITE));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Status").SetFont(headerFont))
                    .SetBackgroundColor(headerBackground)
                    .SetFontColor(ColorConstants.WHITE));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Description").SetFont(headerFont))
                    .SetBackgroundColor(headerBackground)
                    .SetFontColor(ColorConstants.WHITE));

                // Add data rows
                foreach (var report in reports)
                {
                    table.AddCell(new Cell().Add(new Paragraph(report.ReportDate.ToShortDateString())));
                    table.AddCell(new Cell().Add(new Paragraph(report.Location)));
                    table.AddCell(new Cell().Add(new Paragraph(report.DrugType.ToString())));

                    // Add severity with color coding
                    var severityCell = new Cell().Add(new Paragraph(report.Severity.ToString()));
                    severityCell.SetBackgroundColor(GetSeverityColor(report.Severity));
                    table.AddCell(severityCell);

                    // Add status with color coding
                    var statusCell = new Cell().Add(new Paragraph(report.Status.ToString()));
                    statusCell.SetBackgroundColor(GetStatusColor(report.Status));
                    table.AddCell(statusCell);

                    // Add truncated description
                    var description = report.Description.Length > 50
                        ? report.Description.Substring(0, 47) + "..."
                        : report.Description;
                    table.AddCell(new Cell().Add(new Paragraph(description)));
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }

        private Color GetSeverityColor(SeverityLevel severity)
        {
            return severity switch
            {
                SeverityLevel.Low => new DeviceRgb(46, 125, 50),       // Green
                SeverityLevel.Medium => new DeviceRgb(255, 193, 7),    // Amber
                SeverityLevel.High => new DeviceRgb(198, 40, 40),       // Red
                SeverityLevel.Critical => new DeviceRgb(33, 33, 33),    // Dark grey
                _ => new DeviceRgb(189, 189, 189)                      // Grey
            };
        }

        private Color GetStatusColor(ReportStatus status)
        {
            return status switch
            {
                ReportStatus.Pending => new DeviceRgb(158, 158, 158),  // Grey
                ReportStatus.UnderReview => new DeviceRgb(2, 136, 209), // Blue
                ReportStatus.Resolved => new DeviceRgb(56, 142, 60),    // Green
                ReportStatus.Dismissed => new DeviceRgb(33, 33, 33),    // Dark grey
                _ => new DeviceRgb(255, 255, 255)                      // White
            };
        }

        public async Task<byte[]> GenerateExcelReportAsync(IEnumerable<Report> reports)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reports");
                var currentRow = 1;

                // Header
                worksheet.Cell(currentRow, 1).Value = "Date";
                worksheet.Cell(currentRow, 2).Value = "Location";
                worksheet.Cell(currentRow, 3).Value = "Drug Type";
                worksheet.Cell(currentRow, 4).Value = "Severity";
                worksheet.Cell(currentRow, 5).Value = "Status";
                worksheet.Cell(currentRow, 6).Value = "Description";

                // Data
                foreach (var report in reports)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = report.ReportDate.ToShortDateString();
                    worksheet.Cell(currentRow, 2).Value = report.Location;
                    worksheet.Cell(currentRow, 3).Value = report.DrugType.ToString();
                    worksheet.Cell(currentRow, 4).Value = report.Severity.ToString();
                    worksheet.Cell(currentRow, 5).Value = report.Status.ToString();
                    worksheet.Cell(currentRow, 6).Value = report.Description;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}