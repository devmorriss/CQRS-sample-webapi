namespace Domain
{
    public class Receipt
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int CardId { get; set; }
    }
}