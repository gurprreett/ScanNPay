namespace ScanNPay.Repository
{
    public class Profile
    {
        public string Name { get; set; }
        public string Aadhar { get; set; }
        public string Pan { get; set; }
        public bool IsKycComplete { get; set; }

        public string? Establishment { get; set; }
    }
}
