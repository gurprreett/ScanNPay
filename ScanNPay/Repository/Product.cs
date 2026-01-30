namespace ScanNPay.Repository
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductShortDesc { get; set; }
        public string? ProductDesc { get; set; }
        public int Points { get; set; }
    }
}
