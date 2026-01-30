namespace ScanNPay.Repository
{
    public class ListOrders
    {
        public int OrderId { get; set; }
        public int OrderNo { get; set; }
        public string? OrderDate { get; set; }
        public int TotalOrderValue { get; set; }
        public int Items { get; set; }
        public string? OrderDetails { get; set; }

        public List<Cart>? SubOrders { get; set; }
    }
}
