using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScanNPay.Repository
{
    public interface ICustomer
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<IEnumerable<Customer>> GetCustomer(string MobileNo);
        Task<int> CreateCustomer(Customer customer);
        Task<int> CheckCustomerNGenerateOTP(string MobileNo);
        Task<int> VerifyOTP(string MobileNo, string OTP);
        Task<string> ScanCoupon(string MobileNo, string CouponCode);
        Task<IEnumerable<Transaction>> GetTransactions(string MobileNo);
        Task<Dashboard> GetDashboard(string MobileNo);
        Task<string> UpdateProfile(string MobileNo, string Name, string Aadhar, string Pan, bool IsKycComplete, string EstablishmentType);
        Task<Profile> GetProfile(string MobileNo);
        Task<IEnumerable<EstablishmentType>> GetEstablishmentTypes();
    }
}
