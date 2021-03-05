using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeSourceTest.DataAccess.Entities
{
    public class CurrentRate
    {
        public int CurrentRateID { get; set; }

        public DateTime Date { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public decimal Rates { get; set; }

        [NotMapped]
        public decimal AmountEur
        {
            get
            {
                return Amount / Rates;
            }
        }

        [NotMapped]
        public string CountryGroup
        {
            get
            {
                if (new List<string> { "Austria", "Italy", "Belgium", "Latvia" }.Contains(Country))
                {
                    return "EU";
                }
                else if (new List<string> { "Chile", "Qatar", "United Arab Emirates", "United States of America" }.Contains(Country))
                {
                    return "ROW";
                }
                else
                {
                    return Country;
                }
            }
        }
    }
}
