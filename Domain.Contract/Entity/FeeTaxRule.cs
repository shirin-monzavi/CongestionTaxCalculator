namespace Domain.Contract.Entity
{
    public class FeeTaxRule
    {
        public Guid Id { get; set; }

        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }

        public int Amount { get; set; }
    }
}
