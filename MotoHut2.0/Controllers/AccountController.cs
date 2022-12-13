using Business;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoHut2._0.Collections;
using System.Net;
using System.Security.Claims;
using Org.BouncyCastle.Crypto.Generators;

namespace MotoHut2._0.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser _iuser;

        public AccountController(ILogger<HomeController> logger, IUser iUser)
        {
            _logger = logger;
            _iuser = iUser;

        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string txtEmail, string txtPassword, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            string hashedPassword = EncodePasswordToBase64(txtPassword);
           

            if (_iuser.CheckLogin(txtEmail, hashedPassword))
            {
                int userId = _iuser.GetUserIdWithLogin(txtEmail, hashedPassword);

                var claims = new List<Claim>
                {
                    new Claim("email", txtEmail),
                    new Claim("userid", userId.ToString()),
                    new Claim("naam", _iuser.GetNameWithId(userId)),
                    new Claim("huurderid", _iuser.GetHuurderId(userId).ToString()),
                    new Claim("verhuurderid", _iuser.GetVerhuurderId(userId).ToString())
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "naam", "userid")));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            ViewBag.ErrorMsg = "Verkeerde combinatie van email en wachtwoord";
            return View("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterButton(string txtName, string txtEmail, string txtPassword, DateTime txtBirthdate, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            string hashedPassword = EncodePasswordToBase64(txtPassword);

            _iuser.AddUser(txtName, txtEmail, hashedPassword, txtBirthdate); 
     
            return RedirectToAction("Index", "Home");
        }
    }
}
