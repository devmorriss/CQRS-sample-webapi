namespace Domain
{
    public class QCard
    {
        public QCard()
        {
            Id = nextId++;
            Amount = 100;
            CardType = "STANDARD";
        }

        protected static int nextId = 1; // Static counter to generate unique IDs
        public int Id { get; set; }
        public string CardType { get; set; }
        public double Amount { get; set; }

        public DateTime PurchasedDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate => CalculateExpiryDate();
        protected virtual DateTime CalculateExpiryDate() => PurchasedDate.AddYears(5);

        public double FareAmount => CalculateFareAmount();
        protected virtual double CalculateFareAmount() => 15.00;
        public bool IsExit { get; set; } = false;

    }
}