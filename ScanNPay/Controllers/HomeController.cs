using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using ScanNPay.Models;
using ScanNPay.Repository;
using System.Diagnostics;
using System.Linq;
using System.Text;
namespace ScanNPay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomer _customerRep;
        private readonly IProduct _product;
        public HomeController(ICustomer customerRepository, IProduct product) {
            _customerRep = customerRepository;
            _product = product;
        }
        public IActionResult Index()
        {
            LoginViewModel m = new LoginViewModel();
            return View(m);
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel m)
        {
            if (m.btnSubmit == "Get OTP")
            {
                var result = _customerRep.CheckCustomerNGenerateOTP(m.MobileNumber).Result;
                m.IsDisplayOTP = true;
                m.btnSubmit = "Verify OTP";
                return View(m);
            }
            else
            {
                var result = _customerRep.VerifyOTP(m.MobileNumber,m.OneTimePasscode).Result;
                if(result<=0)
                {
                    m.IsDisplayOTP = true;
                    m.btnSubmit = "Verify OTP";
                    TempData["error"] = "Oops! One time password is invalid, please try again.";
                    return View(m);
                }
                var options = new CookieOptions
                {
                    // Persistent for 7 days
                    Expires = DateTimeOffset.UtcNow.AddMinutes(20),

                    // Prevent client-side JS from accessing the cookie
                    HttpOnly = true,

                    // Only transmit over HTTPS
                    Secure = true,

                    // Strict prevents CSRF by only sending the cookie on first-party requests
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append("ScanNPay", m.MobileNumber, options);
                ViewData["isLoggedin"] = true;
                TempData["success"] = "One time password is verified!";
                return RedirectToAction("Welcome");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult KYCStatus()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _customerRep.GetProfile(token).Result;
                var estTypes = _customerRep.GetEstablishmentTypes().Result;
                SelectList EstablishmentTypes = new SelectList(estTypes, "EstId", "Establishment", selectedValue: result.Establishment);
                ProfileViewModel m = new ProfileViewModel { Aadhar = result.Aadhar, Pan = result.Pan, IsKYCComplete = result.IsKycComplete, Name = result.Name, 
                MobileNo = token, EstablishmentType = result.Establishment
                };

                ViewBag.EstablishmentTypes = EstablishmentTypes;
                return View(m);
            }
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult KYCStatus(ProfileViewModel m)
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _customerRep.UpdateProfile(token, m.Name, m.Aadhar, m.Pan, m.IsKYCComplete, m.EstablishmentType).Result;
                TempData["success"] = result;
                return RedirectToAction("KYCStatus");
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult RedeemRewards()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.GetProductCategories().Result;
                return View(result);
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult UPI()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var upiList = _product.GetUPIProviders().Result;
                var result = _product.GetUPIDetails(token).Result;
                ViewBag.UPINo = result.UPINo;
                SelectList upiProviders = new SelectList(upiList, "UPIProviderId", "UPIProvider", selectedValue: result.UPIProvider);
                ViewBag.UPIProviders = upiProviders;
                return View();
            }
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UPI(string upiNo, string upiProvider)
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.UpdateUPI(token, upiNo, upiProvider).Result;
                TempData["success"] = result;
                return RedirectToAction("UPI");
            }
            else
                return RedirectToAction("Index");
        }
        [Route("Home/Products/{ProductCategoryId}")]
        public IActionResult Products(int ProductCategoryId)
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.GetProduct(ProductCategoryId).Result;
                return View(result);
            }
            else
                return RedirectToAction("Index");
        }
        [Route("Home/Addtocart/{ProductId}")]
        public IActionResult Addtocart(int ProductId)
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.Addtocart(token, ProductId).Result;
                return RedirectToAction("ViewCart");
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult ViewCart()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.GetCart(token).Result;
                return View(result);
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult Orders()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.GetOrders(token).Result;
                if (result != null)
                {
                    for (int a = 0; a < result.ToList().Count; a++)
                    {
                        //StringBuilder b = new StringBuilder("<table class=\"table table-bordered table-striped\"><thead><tr><th>Product Name</th><th>Quantity</th><th>Points</th></tr></thead><tbody>");
                        //var subOrdersList = _product.GetOrderDetails(result.ToList()[a].OrderId).Result;
                        //foreach (var subOrder in subOrdersList)
                        //{
                        //    //b.Append("<tr><td>@p.OrderDate</td><td>@p.OrderNo</td><td>@p.TotalOrderValue</td></tr>");
                        //    b.Append("<tr><td>");
                        //    b.Append(subOrder.ProductName);
                        //    b.Append("</td><td>");
                        //    b.Append(subOrder.Quantity);
                        //    b.Append("</td><td>");
                        //    b.Append(subOrder.Points);
                        //    b.Append("</td></tr>");
                        //}
                        //b.Append("</tbody></table>");
                        List<Cart> subOrdersList = (_product.GetOrderDetails(result.ToList()[a].OrderId).Result).ToList();
                        //StringBuilder b = new StringBuilder("Product Name      Quantity           Points\n");
                        //foreach (var subOrder in subOrdersList)
                        //{
                        //    b.Append(subOrder.ProductName + "            " + subOrder.Quantity + "              " + subOrder.Points + "\n");
                        //}
                        //result.ToList()[a].OrderDetails = b.ToString();
                        result.ToList()[a].SubOrders = subOrdersList;
                    }
                }
                return View(result);
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult CheckOut()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.GetCart(token).Result;
                var statesList = _product.GetStates().Result;
                SelectList states = new SelectList(statesList, "StateId", "StateName");
                ViewBag.States = states;
                return View(result);
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult GetCities(int StateId)
        {
            var citiesList = _product.GetCities(StateId).Result;
            var result = new JsonResult(citiesList);
            return result;
        }
        public IActionResult Review(int Total, string Address1, string Address2,string States, string Cities, string Pincode)
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.GetCart(token).Result;
                ReviewOrderViewModel m = new ReviewOrderViewModel();
                m.myCart = result.ToList();
                m.Address1 = Address1;
                m.Address2 = Address2;
                m.Pincode = Pincode;
                m.Total = Total;
                m.Cities = Cities;
                m.States = States;
                var statesList = _product.GetStates().Result;
                SelectList states = new SelectList(statesList, "StateId", "StateName");
                ViewBag.States = states;
                var citiesList = _product.GetCities(Convert.ToInt16(States)).Result;
                SelectList cities = new SelectList(citiesList, "CityId", "CityName");
                ViewBag.Cities = cities;
                return View(m);
            }
            else
                return RedirectToAction("Index");
        }

        public IActionResult ConfirmOrder(ReviewOrderViewModel m)
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.ConfirmOrder(token, m.Total, m.Address1, m.Address2, m.States, m.Cities , m.Pincode).Result;
                if(result.Contains("Congratulations"))
                    TempData["success"] = result;
                else
                    TempData["error"] = result;
                return RedirectToAction("ViewCart");
                //return View();
            }
            else
                return RedirectToAction("Index");
        }

        [Route("Home/DeleteCart/{ProductId}")]
        public IActionResult DeleteCart(int ProductId)
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _product.DeleteCart(token, ProductId).Result;
                return RedirectToAction("ViewCart");
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult Welcome()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _customerRep.GetDashboard(token).Result;
                return View(result);
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult Scan()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                return View();
            }
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Scan(string CouponCode)
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _customerRep.ScanCoupon(token, CouponCode).Result;
                if (result.Contains("credited"))
                {
                    TempData["success"] = result;
                }
                else
                    TempData["error"] = result;
                return View();
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult ScanHistory()
        {
            string? token = Request.Cookies["ScanNPay"];
            if (token != null)
            {
                ViewData["isLoggedin"] = true;
                var result = _customerRep.GetTransactions(token).Result;
                return View(result);
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            ViewData["isLoggedin"] = false;
            Response.Cookies.Delete("ScanNPay");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
