using System.ComponentModel.DataAnnotations;

namespace ScanNPay.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? MobileNumber { get; set; }
        public bool IsDisplayOTP { get; set; } = false;
        public string? OneTimePasscode { get; set; }
        [Required]
        public string? btnSubmit { get; set; } = "Get OTP";
    }
}
