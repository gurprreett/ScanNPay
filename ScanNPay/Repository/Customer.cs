namespace ScanNPay.Repository
{
    public class Customer
    {
        public int MemberId { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}