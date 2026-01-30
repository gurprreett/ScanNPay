namespace ScanNPay.Repository
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public int Points { get; set; }
    }
}
