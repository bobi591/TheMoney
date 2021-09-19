using System;
using TheMoney.Shared.Entities.Dimensions;

namespace TheMoney.Shared.Entities
{
    public class MonetaryTransaction : EntityBase
    {
        public string OwnerEmail { get; set; }

        [Dimension("Data source")]
        public string DataSource { get; set; }

        [Dimension("Currency")]
        public string Currency { get; set; }

        [Measure("Value")]
        public float Value { get; set; }

        [Dimension("Date")]
        public DateTime TransactionTimestamp { get; set; }

        public DateTime ImportTimestamp { get; set; }
    }
}
