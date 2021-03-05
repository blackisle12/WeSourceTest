using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WeSourceTest.DataAccess;
using WeSourceTest.DataAccess.Entities;

namespace WeSourceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private AppDbContext context { get; }

        public DataController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("MergeDataWithRates")]
        public async Task<IActionResult> MergeDataWithRates()
        {
            await RemoveOldCurrentRatesAsync();

            var currentRates = context.CsvData
                .Join(context.FixerData,
                    csv => new { csv.Date, csv.Currency },
                    fixer => new { fixer.Date, fixer.Currency },
                    (csv, fixer) => new CurrentRate
                    {
                        Date = csv.Date,
                        Country = csv.Country,
                        Currency = csv.Currency,
                        Amount = csv.Amount,
                        Rates = fixer.Rates
                    })
                .ToList();

            await context.AddRangeAsync(currentRates);
            await context.SaveChangesAsync();

            return Ok("Rates has been updated.");
        }

        private async Task RemoveOldCurrentRatesAsync()
        {
            var oldCurrentRateList = context.CurrentRate.AsQueryable();

            context.RemoveRange(oldCurrentRateList);
            await context.SaveChangesAsync();
        }
    }
}
