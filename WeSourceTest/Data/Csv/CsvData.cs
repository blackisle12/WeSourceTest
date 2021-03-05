using System;
using TinyCsvParser.Mapping;

namespace WeSourceTest.Data.Csv
{
    public class CsvData
    {
        public DateTime Date { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }

    public class CsvDataMapping : CsvMapping<CsvData>
    {
        public CsvDataMapping() : base()
        {
            MapProperty(0, c => c.Date);
            MapProperty(1, c => c.Country);
            MapProperty(2, c => c.Currency);
            MapProperty(3, c => c.Amount);
        }
    }
}
