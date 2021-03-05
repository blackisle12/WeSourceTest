using System;
using System.ComponentModel.DataAnnotations;

namespace WeSourceTest.DataAccess.Entities
{
    public class CsvData
    {
        public int CsvDataID { get; set; }

        public DateTime Date { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        [DataType("decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}
