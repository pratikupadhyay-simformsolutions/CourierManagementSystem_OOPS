using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CourierManagementSystem.Data
{
    public class ReportService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ReportService> _logger;

        public ReportService(ApplicationDbContext dbContext, ILogger<ReportService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<byte[]> GenerateReportAsync()
        {
            try
            {
                List<Item> items = await _dbContext.Item.ToListAsync();

                // Generate the report using the items data
                StringBuilder reportBuilder = new StringBuilder();
                reportBuilder.AppendLine("Item Name, Category, Description, Price, Stock Item, Date of Adding Item");

                foreach (Item item in items)
                {
                    reportBuilder.AppendLine($"{item.Name}, {item.Category}, {item.Description}, {item.Price}, {item.CourierItem}, {item.DateofAddingItem}");
                }
                _logger.LogInformation("report generated successfully");
                // Convert the report to a byte array
                byte[] reportBytes = Encoding.UTF8.GetBytes(reportBuilder.ToString());

                return reportBytes;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the report generation
                _logger.LogError(ex, "Error generating report");
                throw;
            }
        }

        public async Task<bool> DownloadReportAsync(string filePath)
        {
            try
            {
                byte[] reportBytes = await GenerateReportAsync();

                if (reportBytes == null)
                    return false;

                await File.WriteAllBytesAsync(filePath, reportBytes);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading report: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DownloadMultipleReportsAsync(string directoryPath, int count)
        {
            try
            {
                if (count <= 0)
                    return false;

                for (int i = 1; i <= count; i++)
                {
                    string fileName = $"Report{i}.csv";
                    string filePath = Path.Combine(directoryPath, fileName);

                    bool result = await DownloadReportAsync(filePath);

                    if (!result)
                    {
                        Console.WriteLine($"Error downloading report {i}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading multiple reports: {ex.Message}");
                return false;
            }
        }
    }
}
