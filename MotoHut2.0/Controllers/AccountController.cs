using Business;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoHut2._0.Collections;
using System.Net;
using System.Security.Claims;
using Org.BouncyCastle.Crypto.Generators;
using MotoHut2._0.Models;

namespace MotoHut2._0.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser _iuser;
        private readonly IUserCollection _iuserCollection;

        public AccountController(ILogger<HomeController> logger, IUser iUser, IUserCollection iuserCollection)
        {
            _logger = logger;
            _iuser = iUser;
            _iuserCollection = iuserCollection;
        }
        public string GetFromClaim(string claim)
        {
            try
            {
                return User.Claims.Where(c => c.Type == claim)
                               .Select(c => c.Value).SingleOrDefault();
            }
            catch (Exception er)
            {
                return er.ToString();
            }
        }

        public IActionResult Index()
        {
            return View(GetCurrentUserAsViewModel());

        }

        private bool CheckIf18Plus(DateTime dob)
        {
            return (DateTime.Now.Subtract(dob).Days / 365 >= 18);
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

        [HttpPost]
        public async Task<IActionResult> Login(string txtEmail, string txtPassword, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            User verifyUser = _iuser.GetHashedPasswordAndUserId(txtEmail);
            if (verifyUser != null)
            {
                if (BCrypt.Net.BCrypt.Verify(txtPassword, verifyUser.Password))
                {
                    User user = _iuser.GetUserWithId(verifyUser.UserId);

                    var claims = createClaims(user);

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

            }
            ViewBag.ErrorMsg = "Verkeerde combinatie van email en wachtwoord";
            return View("Login");
        }

        public List<Claim> createClaims(User user)
        {
            return new List<Claim>
                {
                    new Claim("email", user.Email),
                    new Claim("userid", user.UserId.ToString()),
                    new Claim("naam", user.Naam.ToString()),
                    new Claim("huurderid", _iuser.GetHuurderId(user.UserId).ToString()),
                    new Claim("verhuurderid", _iuser.GetVerhuurderId(user.UserId).ToString())
                };
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterButton(string txtName, string txtEmail, string txtPassword, DateTime txtBirthdate, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!_iuserCollection.CheckIfEmailExists(txtEmail))
            {
                if (CheckIf18Plus(txtBirthdate))
                {
                    _iuserCollection.AddUser(txtName, txtEmail, BCrypt.Net.BCrypt.HashPassword(txtPassword), txtBirthdate);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMsg = "Je moet minimaal 18 jaar zijn";
                }

            }
            else
            {
                ViewBag.ErrorMsg = "Email bestaat al";
            }


            return View("Register");


        }

        public UserModel GetCurrentUserAsViewModel()
        {
            int id = Convert.ToInt32(GetFromClaim("userid"));
            User user = _iuser.GetUserWithId(id);
            UserModel userModel = new UserModel
            {
                Email = user.Email,
                UserId = user.UserId,
                Naam = user.Naam,
                Geboortedatum = user.Geboortedatum,
                HuurderId = Convert.ToInt32(GetFromClaim("huurderid")),
                VerhuurderId = Convert.ToInt32(GetFromClaim("verhuurderid")),
            };
            return userModel;
        }

        public IActionResult EditAccount()
        {
            return View(GetCurrentUserAsViewModel());
        }

        public ActionResult DeleteAccount()
        {
            int userId = Convert.ToInt32(GetFromClaim("userid"));
            int huurderId = Convert.ToInt32(GetFromClaim("huurderid"));
            int verhuurderId = Convert.ToInt32(GetFromClaim("verhuurderid"));
            _iuserCollection.DeleteUser(userId, huurderId, verhuurderId);
            Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditButton(string txtName, string txtEmail, string txtPassword, DateTime txtBirthdate)
        {
            int id = Convert.ToInt32(GetFromClaim("userid"));
            if (txtEmail == GetFromClaim("email") || !_iuserCollection.CheckIfEmailExists(txtEmail))
            {
                if (CheckIf18Plus(txtBirthdate))
                {
                    _iuser.EditUser(txtName, txtEmail, BCrypt.Net.BCrypt.HashPassword(txtPassword), txtBirthdate, id);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ViewBag.ErrorMsg = "Je moet minimaal 18 jaar zijn";
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Email bestaat al";
            }

            return View("EditAccount", GetCurrentUserAsViewModel());
        }
    }
}
