using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeSourceTest.DataAccess;

namespace WeSourceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixerController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private AppDbContext context { get; }

        public FixerController(IConfiguration configuration, AppDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        [HttpGet("GetTimeSeriesRates/{startDate}/{endDate}")]
        public async Task<IActionResult> GetTimeSeriesRates(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return BadRequest("End date should be higher than Start date.");
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(@$"{configuration["Fixer:BaseUrl"]}/timeseries?access_key={configuration["Fixer:AccessKey"]}&base=EUR&start_date={startDate.ToString("yyyy-MM-dd")}&end_date={endDate.ToString("yyyy-MM-dd")}");

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest("Invalid request.");
                }

                await RemoveOldFixerDataAsync();

                var content = response.Content.ReadAsStringAsync();

                //deserialize the content
                //save content in sql db
            }

            return Ok($"Fixer result from {startDate} to {endDate} has been imported to the database.");
        }

        private async Task RemoveOldFixerDataAsync()
        {
            var oldFixerDataList = context.FixerData.AsQueryable();

            context.RemoveRange(oldFixerDataList);
            await context.SaveChangesAsync();
        }
    }
}
