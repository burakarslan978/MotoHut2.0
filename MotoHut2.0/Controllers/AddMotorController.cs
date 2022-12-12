using Business.Interface;
using Business;
using Microsoft.AspNetCore.Mvc;
using MotoHut2._0.Collections;
using System.Security.Claims;

namespace MotoHut2._0.Controllers
{
    public class AddMotorController : Controller
    {
        private readonly ILogger<AddMotorController> _logger;
        private readonly IMotorCollection _imotorCollection;


        public AddMotorController(ILogger<AddMotorController> logger, IMotorCollection iMotorCollection)
        {
            _logger = logger;
            _imotorCollection = iMotorCollection;

        }
        public IActionResult AddMotor()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult AddMotorForm(string txtMerk, int txtBouwjaar, int txtPrijs, string huurbaar)
        {

            var verhuurderId = User.Claims.Where(c => c.Type == "verhuurderid")
                               .Select(c => c.Value).SingleOrDefault();

            if (huurbaar == "Nee")
            {
                _imotorCollection.AddMotor(txtMerk, txtBouwjaar, txtPrijs, false, Convert.ToInt32(verhuurderId));
            }
            else if (huurbaar == "Ja")
            {
                _imotorCollection.AddMotor(txtMerk, txtBouwjaar, txtPrijs, true, Convert.ToInt32(verhuurderId));
            }

            return RedirectToAction("Index", "Home");
        }


    }
}
