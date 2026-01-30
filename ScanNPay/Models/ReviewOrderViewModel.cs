using System.ComponentModel.DataAnnotations;

namespace ScanNPay.Models
{
    public class ReviewOrderViewModel
    {
        public List<ScanNPay.Repository.Cart>? myCart {  get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? States { get; set; }
        public string? Cities { get; set; }
        public string? Pincode { get; set; }
        public int Total {  get; set; }

    }
}
