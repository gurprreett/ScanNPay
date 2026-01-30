
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using static System.Net.WebRequestMethods;

namespace ScanNPay.Repository
{
    public class CustomerRepository : ICustomer
    {
        private readonly dbContext _context;
        public CustomerRepository(dbContext context) => _context = context;

        public async Task<int> CheckCustomerNGenerateOTP(string MobileNo)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "GenerateOTP";
            var parameters = new { mobileNo = MobileNo };

            var result = await connection.ExecuteScalarAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> CreateCustomer(Customer customer)
        {
            var query = "INSERT INTO tbl_Customers (MobileNo,IsActive,CreatedDate) VALUES (@MobileNo, @IsActive, GETDATE())";
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteScalarAsync<int>(query, customer);
            return result;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var query = "SELECT * FROM tbl_Customers where IsActive = 1";
            using var connection = _context.CreateConnection();
            var customer = await connection.QueryAsync<Customer>(query);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomer(string mobileNo)
        {
            var query = "SELECT * FROM tbl_Customers where IsActive = 1 and MobileNo = @MobileNo";
            using var connection = _context.CreateConnection();
            var customer = await connection.QueryAsync<Customer>(query, new { MobileNo = mobileNo });
            return customer; 
        }

        public async Task<Dashboard> GetDashboard(string MobileNo)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "GetDashborad";
            var parameters = new { mobileNo = MobileNo };

            var result = await connection.QuerySingleAsync<Dashboard>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<EstablishmentType>> GetEstablishmentTypes()
        {
            var query = "SELECT EstId,Establishment FROM tbl_Master_EstablishmentType where IsActive = 1";
            using var connection = _context.CreateConnection();
            var customer = await connection.QueryAsync<EstablishmentType>(query, new { });
            return customer;
        }

        public async Task<Profile> GetProfile(string MobileNo)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "getProfile";
            var parameters = new { mobileNo = MobileNo };

            var result = await connection.QuerySingleAsync<Profile>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(string MobileNo)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "ListTransactions";
            var parameters = new { mobileNo = MobileNo };

            var result = await connection.QueryAsync<Transaction>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<string> ScanCoupon(string MobileNo, string CouponCode)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "ScanCoupon";
            var parameters = new { mobileNo = MobileNo, couponCode = CouponCode };

            string? result = await connection.ExecuteScalarAsync<string>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<string> UpdateProfile(string MobileNo, string Name, string Aadhar, string Pan, bool IsKycComplete, string EstablishmentType)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "updateProfile";
            var parameters = new { mobileNo = MobileNo, name = Name, aadhar = Aadhar, pan = Pan, isKYCComplete = IsKycComplete, establishmentType = EstablishmentType };

            string? result = await connection.ExecuteScalarAsync<string>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> VerifyOTP(string MobileNo, string OTP)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "verifyOTP";
            var parameters = new { mobileNo = MobileNo, oTP = OTP };

            var result = await connection.ExecuteScalarAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
