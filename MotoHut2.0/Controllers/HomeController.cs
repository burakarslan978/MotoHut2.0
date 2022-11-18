using Microsoft.AspNetCore.Mvc;
using MotoHut2._0.Models;
using System.Diagnostics;
using Dal;
using Business;
using MotoHut2._0.Collections;
using Business.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoHut2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMotorCollection _imotorCollection;
        private readonly IMotor _imotor;
        private readonly IHuurderMotorCollection _ihuurderMotorCollection;
        private readonly IHuurderMotor _ihuurderMotor;

        public HomeController(ILogger<HomeController> logger, IMotorCollection iMotorCollection, IMotor iMotor, IHuurderMotorCollection iHuurderMotorCollection, IHuurderMotor iHuurderMotor)
        {
            _logger = logger;
            _imotorCollection = iMotorCollection;
            _imotor = iMotor;
            _ihuurderMotorCollection = iHuurderMotorCollection;
            _ihuurderMotor = iHuurderMotor;
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

        //[ActionName("RentMotor")]
        //[Route("RentMotor/Home/{id:int}")]
        //public IActionResult RentMotor1(int id)
        //{
        //    //_imotor.RentMotor(id, pickUpDate, returnDate);

        //    Motor model = _imotor.GetMotor(id);

        //    MotorViewModel viewModel = new MotorViewModel { MotorId = model.MotorId, Bouwjaar = model.Bouwjaar, Model = model.Model, Prijs = model.Prijs};
            
        //    return View(viewModel);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddMotor()
        {
            return View(); 
        }

        public IActionResult HuurLijst()
        {
            List<SelectListItem> items = new List<SelectListItem>();


            List<MotorViewModel> list1 = new List<MotorViewModel>();
            foreach (var item in _imotorCollection.ConvertDataToView())
            {
                list1.Add(new MotorViewModel { MotorId = item.MotorId, VerhuurderId = item.VerhuurderId, Bouwjaar = item.Bouwjaar, Prijs = item.Prijs, Model = item.Model, Status = item.Status });
                items.Add(new SelectListItem { Text = ""+item.MotorId+": "+item.Bouwjaar+" "+item.Model+"", Value = item.MotorId.ToString() });
            }

            List<HuurderMotorViewModel> list2 = new List<HuurderMotorViewModel>();
            foreach (var item in _ihuurderMotorCollection.GetHuurderMotorList())
            {
                list2.Add(new HuurderMotorViewModel { HuurderMotorId = item.HuurderMotorId, MotorId = item.MotorId, HuurderId = item.HuurderId, OphaalDatum = item.OphaalDatum, InleverDatum = item.InleverDatum});
            }
            
            ViewModel viewModel = new ViewModel { HuurderMotorModels = list2, MotorModels = list1, Motors = items };
            return View(viewModel);
        }
        public ActionResult AddMotorForm(string txtMerk, int txtBouwjaar, int txtPrijs)
        {
            _imotor.AddMotor(txtMerk, txtBouwjaar, txtPrijs);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RentMotor(int id, DateTime pickUpDate, DateTime returnDate)
        {
            _imotor.RentMotor(id, pickUpDate, returnDate);

            Motor model = _imotor.GetMotor(id);

            MotorViewModel viewModel = new MotorViewModel { MotorId = model.MotorId, Bouwjaar = model.Bouwjaar, Model = model.Model, Prijs = model.Prijs };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult HuurLijstSelected(int MotorId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in _imotorCollection.ConvertDataToView())
            { 
                items.Add(new SelectListItem { Text = "" + item.MotorId + ": " + item.Bouwjaar + " " + item.Model + "", Value = item.MotorId.ToString() });
            }

            List<HuurderMotorViewModel> list2 = new List<HuurderMotorViewModel>();
            foreach (var item in _ihuurderMotorCollection.GetHuurderMotorListForMotor(MotorId))
            {
                list2.Add(new HuurderMotorViewModel { HuurderMotorId = item.HuurderMotorId, MotorId = item.MotorId, HuurderId = item.HuurderId, OphaalDatum = item.OphaalDatum, InleverDatum = item.InleverDatum, IsGeaccepteerd = item.IsGeaccepteerd, IsGeweigerd = item.IsGeweigerd });
            }

            ViewModel viewModel = new ViewModel { HuurderMotorModels = list2, Motors = items};
            return View(viewModel);
            
        }

        public ActionResult AcceptRent(int HuurderMotorId)
        {
            _ihuurderMotor.AcceptOrDeclineRent(HuurderMotorId, "Accept");
            return RedirectToAction("HuurLijst");
        }

        public ActionResult DeclineRent(int HuurderMotorId)
        {
            _ihuurderMotor.AcceptOrDeclineRent(HuurderMotorId, "Decline");
            return RedirectToAction("HuurLijst");
        }
    }
}