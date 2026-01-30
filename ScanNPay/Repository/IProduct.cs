namespace ScanNPay.Repository
{
    public interface IProduct
    {
        Task<IEnumerable<ProductCategories>> GetProductCategories();
        Task<IEnumerable<Product>> GetProduct(int ProductCategoryId);
        Task<string> Addtocart(string MobileNo, int ProductId);
        Task<IEnumerable<Cart>> GetCart(string MobileNo);
        Task<string> DeleteCart(string MobileNo, int ProductId);
        Task<string> ConfirmOrder(string MobileNo, int Total, string Address1, string Address2,string StateId, string CityId, string Pincode);
        Task<IEnumerable<ListOrders>> GetOrders(string MobileNo);
        Task<IEnumerable<Cart>> GetOrderDetails(int OrderId);
        Task<IEnumerable<State>> GetStates();
        Task<IEnumerable<City>> GetCities(int StateId);
        Task<string> UpdateUPI(string MobileNo, string UPINo, string UPIProvider);
        Task<UPIDetails> GetUPIDetails(string MobileNo);
        Task<IEnumerable<UPIProviders>> GetUPIProviders();
    }
}
