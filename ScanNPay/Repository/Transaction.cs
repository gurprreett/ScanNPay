namespace ScanNPay.Repository
{
    public class Transaction
    {
        public int TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? ProductName { get; set; }
        public int Points { get; set; }
        public string? CouponCode { get; set; }
        public string? TransactionType { get; set; }
    }
}