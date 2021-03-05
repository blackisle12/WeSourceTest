using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using WeSourceTest.Data.Csv;
using WeSourceTest.DataAccess;

namespace WeSourceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvController : ControllerBase
    {
        private AppDbContext context { get; }


        public CsvController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ParseData")]
        public async Task<IActionResult> ParseData()
        {
            await RemoveOldCsvDataAsync();

            var csvParserOptions = new CsvParserOptions(true, ',');
            var csvParser = new CsvParser<CsvData>(csvParserOptions, new CsvDataMapping());

            for (var i = 1; i<=3; i++)
            {
                var records = csvParser.ReadFromFile($"data/data{i}.csv", Encoding.UTF8);

                var csvDataList = records
                    .AsEnumerable()
                    .Select(r => new DataAccess.Entities.CsvData
                    {
                        Date = r.Result.Date,
                        Country = r.Result.Country,
                        Currency = r.Result.Currency,
                        Amount = r.Result.Amount
                    })
                    .ToList();

                await context.AddRangeAsync(csvDataList);
            }

            await context.SaveChangesAsync();

            return Ok("Csv files in /Data folder has been imported to the database.");
        }

        private async Task RemoveOldCsvDataAsync()
        {
            var oldCsvDataList = context.CsvData.AsQueryable();

            context.RemoveRange(oldCsvDataList);
            await context.SaveChangesAsync();
        }
    }
}
