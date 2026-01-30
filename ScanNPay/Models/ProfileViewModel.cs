using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ScanNPay.Models
{
    public class ProfileViewModel
    {
        public string? MobileNo {  get; set; }
        [Required]
        public string? Aadhar { get; set; }
        public bool IsAadharVerified { get; set; } = false;
        [Required]
        public string? Pan {  get; set; }
        public bool IsPanVerified { get; set; } = false;
        [Required]
        public bool IsKYCComplete { get; set; } = true;
        public string? Name { get; set; }
        //public List<SelectListItem>? EstablishmentTypeArray { get; set; }

        public string? EstablishmentType { get; set; }

    }
}
