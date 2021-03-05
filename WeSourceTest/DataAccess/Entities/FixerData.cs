using System;
using System.ComponentModel.DataAnnotations;

namespace WeSourceTest.DataAccess.Entities
{
    public class FixerData
    {
        public int FixerDataID { get; set; }

        public DateTime Date { get; set; }

        public string Currency { get; set; }

        [DataType("decimal(18,2)")]
        public decimal Rates { get; set; }
    }
}
