using Microsoft.AspNetCore.Mvc;
using MotoHut2._0.Models;
using System.Diagnostics;
using Dal;
using Business;
using MotoHut2._0.Collections;

namespace MotoHut2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMotorCollection _imotorCollection;
        private readonly IMotor _imotor;

        public HomeController(ILogger<HomeController> logger, IMotorCollection iMotorCollection, IMotor iMotor)
        {
            _logger = logger;
            _imotorCollection = iMotorCollection;
            _imotor = iMotor;
        }

        public IActionResult Index()
        {
            List<MotorViewModel> list = new List<MotorViewModel>();
            foreach(var item in _imotorCollection.ConvertDataToView())
            {
                list.Add(new MotorViewModel { MotorId = item.MotorId, VerhuurderId = item.VerhuurderId, Bouwjaar = item.Bouwjaar, Prijs = item.Prijs, Model = item.Model, Status = item.Status });
            }
            ViewModel viewModel = new ViewModel { MotorModels = list};
            return View(viewModel);
        }

        [ActionName("RentMotor")]
        //[Route("RentMotor/Home/{id:int}")]
        public IActionResult RentMotor(int id)
        {
            _imotor.RentMotor(id);

            Motor model = _imotor.GetMotor(id);

            MotorViewModel viewModel = new MotorViewModel { MotorId = model.MotorId, Bouwjaar = model.Bouwjaar, Model = model.Model, Prijs = model.Prijs};
            
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddMotor()
        {
            return View(); 
        }
        public ActionResult AddMotorForm(string txtMerk, int txtBouwjaar, int txtPrijs)
        {
            _imotor.AddMotor(txtMerk, txtBouwjaar, txtPrijs);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}