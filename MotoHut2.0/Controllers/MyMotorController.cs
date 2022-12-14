using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using MotoHut2._0.Collections;
using MotoHut2._0.Models;

namespace MotoHut2._0.Controllers
{
    public class MyMotorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMotorCollection _imotorCollection;
        private readonly IHuurderMotorCollection _ihuurderMotorCollection;

        public MyMotorController(ILogger<HomeController> logger, IMotorCollection iMotorCollection, IHuurderMotorCollection iHuurderMotorCollection)
        {
            _logger = logger;
            _imotorCollection = iMotorCollection;
            _ihuurderMotorCollection = iHuurderMotorCollection;
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
            int VerhuurderId = Convert.ToInt32(GetFromClaim("verhuurderid"));

            List<MotorViewModel> list = new List<MotorViewModel>();
            foreach (var item in _imotorCollection.GetMotorListForVerhuurder(VerhuurderId))
            {
                list.Add(new MotorViewModel { MotorId = item.MotorId, VerhuurderId = VerhuurderId, Bouwjaar = item.Bouwjaar, Prijs = item.Prijs, Model = item.Model, Huurbaar = item.Huurbaar });
            }
            ViewModel viewModel = new ViewModel { MotorModels = list };
            return View(viewModel);

        }

        public ActionResult DeleteMotor(int id)
        {
            _ihuurderMotorCollection.DeleteHuurderMotorForMotor(id);
            _imotorCollection.DeleteMotor(id);
            return RedirectToAction("Index", "MyMotor");
        }
    }
}
