using System;
namespace TheMoney.Shared.Entities
{
    public class MonetaryTransaction : EntityBase
    {
        public string OwnerEmail { get; set; }
        public string DataSource { get; set; }
        public string Currency { get; set; }
        public float Value { get; set; }
        public DateTime TransactionTimestamp { get; set; }
        public DateTime ImportTimestamp { get; set; }
    }
}
