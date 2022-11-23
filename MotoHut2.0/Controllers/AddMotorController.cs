using Business.Interface;
using Business;
using Microsoft.AspNetCore.Mvc;
using MotoHut2._0.Collections;

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
            return View();
        }

        public ActionResult AddMotorForm(string txtMerk, int txtBouwjaar, int txtPrijs, string huurbaar)
        {

            if (huurbaar == "Nee")
            {
                _imotorCollection.AddMotor(txtMerk, txtBouwjaar, txtPrijs, false);
            }
            else if (huurbaar == "Ja")
            {
                _imotorCollection.AddMotor(txtMerk, txtBouwjaar, txtPrijs, true);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
