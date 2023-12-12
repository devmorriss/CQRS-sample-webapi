
namespace Domain
{
    public class DiscountedQCard : QCard
    {
        public DiscountedQCard() : base()
        {
            Id = nextId++;
            Amount = 500;
            CardType = "SENIOR";
        }
        public int TripCount { get; set; }

        protected override DateTime CalculateExpiryDate() => PurchasedDate.AddYears(3);
        protected override double CalculateFareAmount() => 10.00;
    }
}