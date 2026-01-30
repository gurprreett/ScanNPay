namespace ScanNPay.Repository
{
    public class Dashboard
    {
        public int Earned { get; set; }
        public int Burned { get; set; }
        public int Balance { get; set; }
        public int Scans { get; set; }
        public string? LastScanDate { get; set; }
        public string? LastPayOutDate { get; set; }
    }
}