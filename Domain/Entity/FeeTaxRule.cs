namespace Domain.Entity
{
    public class FeeTaxRule
    {
        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }

        public int Amount { get; set; }
    }
}
