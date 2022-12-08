using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoHut2._0.Collections;
using System.Net;

namespace MotoHut2._0.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser _iuser;
        public const string SessionKeyUserId = "_UserId";
        public const string SessionKeyUserName = "_UserName";

        public LoginController(ILogger<HomeController> logger, IUser iUser)
        {
            _logger = logger;
            _iuser = iUser;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginForm(string txtEmail, string txtPassword)
        {
            if(_iuser.CheckLogin(txtEmail, txtPassword))
            {
                ViewBag.ErrorMsg = "Success";
                
                HttpContext.Session.SetInt32(SessionKeyUserId, _iuser.GetUserIdWithLogin(txtEmail, txtPassword));
            } else
            {
                ViewBag.ErrorMsg = "Fout";
                return View("Index");
            }
  


            return RedirectToAction("Index", "Home");
        }
    }
}
