
using Dapper;
using System.Data;

namespace ScanNPay.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly dbContext _context;
        public ProductRepository(dbContext context) => _context = context;

        public async Task<string> Addtocart(string MobileNo, int ProductId)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "Addtocart";
            var parameters = new { mobileNo = MobileNo, productId = ProductId };

            string? result = await connection.ExecuteScalarAsync<string>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<string> ConfirmOrder(string MobileNo, int Total, string Address1, string Address2, string StateId, string CityId, string Pincode)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "confirmOrder";
            var parameters = new { mobileNo = MobileNo, total = Total, address1 = Address1, address2 = Address2, stateId = StateId, cityId = CityId ,pincode = Pincode };

            string? result = await connection.ExecuteScalarAsync<string>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<string> DeleteCart(string MobileNo, int ProductId)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "deleteCart";
            var parameters = new { mobileNo = MobileNo, productId = ProductId };

            string? result = await connection.ExecuteScalarAsync<string>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<Cart>> GetCart(string MobileNo)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "getCart";
            var parameters = new { mobileNo = MobileNo };
            var result = await connection.QueryAsync<Cart>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<City>> GetCities(int StateId)
        {
            var query = "select CityId,CityName from tbl_Master_Cities where isactive = 1 and StateId = @stateId";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<City>(query, new { stateId = StateId });
            return result;
        }

        public async Task<IEnumerable<Cart>> GetOrderDetails(int OrderId)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "getOrderDetails";
            var parameters = new { orderId = OrderId };
            var result = await connection.QueryAsync<Cart>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<ListOrders>> GetOrders(string MobileNo)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "getOrders";
            var parameters = new { mobileNo = MobileNo };
            var result = await connection.QueryAsync<ListOrders>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProduct(int ProductCategoryId)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "getProducts";
            var parameters = new { productCategoryId = ProductCategoryId};
            var result = await connection.QueryAsync<Product>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<ProductCategories>> GetProductCategories()
        {
            using var connection = _context.CreateConnection();
            var procedureName = "getProductCategories";
            var parameters = new {  };
            var result = await connection.QueryAsync<ProductCategories>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<State>> GetStates()
        {
            var query = "select StateId,StateName from tbl_Master_States where isactive = 1";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<State>(query);
            return result;
        }

        public async Task<UPIDetails> GetUPIDetails(string MobileNo)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "getUPIDetails";
            var parameters = new { mobileNo = MobileNo};
            var result = await connection.QuerySingleAsync<UPIDetails>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<UPIProviders>> GetUPIProviders()
        {
            var query = "select UPIProviderId,UPIProvider from tbl_Master_UPIProviders where IsActive = 1";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<UPIProviders>(query);
            return result;
        }

        public async Task<string> UpdateUPI(string MobileNo, string UPINo, string UPIProvider)
        {
            using var connection = _context.CreateConnection();
            var procedureName = "updateUPIDetails";
            var parameters = new { mobileNo = MobileNo, uPINo = UPINo, uPIProvider = UPIProvider };

            string? result = await connection.ExecuteScalarAsync<string>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
