using Business;
using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MotoHut2._0.Collections;
using MotoHut2._0.Models;

namespace MotoHut2._0.Controllers
{
    public class RentController : Controller
    {
        private readonly ILogger<RentController> _logger;
        private readonly IMotorCollection _imotorCollection;
        private readonly IMotor _imotor;
        private readonly IHuurderMotorCollection _ihuurderMotorCollection;
        private readonly IHuurderMotor _ihuurderMotor;

        public RentController(ILogger<RentController> logger, IMotorCollection iMotorCollection, IMotor iMotor, IHuurderMotorCollection iHuurderMotorCollection, IHuurderMotor iHuurderMotor)
        {
            _logger = logger;
            _imotorCollection = iMotorCollection;
            _imotor = iMotor;
            _ihuurderMotorCollection = iHuurderMotorCollection;
            _ihuurderMotor = iHuurderMotor;
        }

        public IActionResult HuurLijst()
        {
            List<SelectListItem> items = new List<SelectListItem>();


            List<MotorViewModel> list1 = new List<MotorViewModel>();
            foreach (var item in _imotorCollection.GetMotorList())
            {
                list1.Add(new MotorViewModel { MotorId = item.MotorId, VerhuurderId = item.VerhuurderId, Bouwjaar = item.Bouwjaar, Prijs = item.Prijs, Model = item.Model, Huurbaar = item.Huurbaar });
                items.Add(new SelectListItem { Text = "" + item.MotorId + ": " + item.Bouwjaar + " " + item.Model + "", Value = item.MotorId.ToString() });
            }

            List<HuurderMotorViewModel> list2 = new List<HuurderMotorViewModel>();
            foreach (var item in _ihuurderMotorCollection.GetHuurderMotorList())
            {
                list2.Add(new HuurderMotorViewModel { HuurderMotorId = item.HuurderMotorId, MotorId = item.MotorId, HuurderId = item.HuurderId, OphaalDatum = item.OphaalDatum, InleverDatum = item.InleverDatum });
            }

            ViewModel viewModel = new ViewModel { HuurderMotorModels = list2, MotorModels = list1, Motors = items };
            return View(viewModel);
        }



        public ActionResult RentMotor(int id, DateTime pickUpDate, DateTime returnDate)
        {


            Motor model = new Motor();

            if (_ihuurderMotor.CheckAvailability(id, pickUpDate, returnDate) && pickUpDate <= returnDate && pickUpDate > DateTime.Now.AddHours(2))
            {
                _imotor.RentMotor(id, pickUpDate, returnDate);
                model = _imotor.GetMotor(id);
            }
            else
            {
                model.MotorId = 0;
            }

            if (returnDate <= pickUpDate)
            {
                model.MotorId = -1;
            }
            else if (pickUpDate < DateTime.Now.AddHours(2))
            {
                model.MotorId = -2;
            }


            MotorViewModel motorViewModel = new MotorViewModel { MotorId = model.MotorId, Bouwjaar = model.Bouwjaar, Model = model.Model, Prijs = model.Prijs };
            HuurderMotorViewModel huurderMotorViewModel = new HuurderMotorViewModel { OphaalDatum = pickUpDate, InleverDatum = returnDate };
            ViewModel viewModel = new ViewModel { MotorViewModel = motorViewModel, HuurderMotorViewModel = huurderMotorViewModel };

            return View(viewModel);
        }
        public ActionResult HuurLijstSelected(int MotorId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in _imotorCollection.GetMotorList())
            {
                items.Add(new SelectListItem { Text = "" + item.MotorId + ": " + item.Bouwjaar + " " + item.Model + "", Value = item.MotorId.ToString() });
            }

            List<HuurderMotorViewModel> list2 = new List<HuurderMotorViewModel>();
            foreach (var item in _ihuurderMotorCollection.GetHuurderMotorListForMotor(MotorId))
            {
                list2.Add(new HuurderMotorViewModel { HuurderMotorId = item.HuurderMotorId, MotorId = item.MotorId, HuurderId = item.HuurderId, OphaalDatum = item.OphaalDatum, InleverDatum = item.InleverDatum, IsGeaccepteerd = item.IsGeaccepteerd, IsGeweigerd = item.IsGeweigerd });
            }

            ViewModel viewModel = new ViewModel { HuurderMotorModels = list2, Motors = items };
            return View(viewModel);

        }

        public ActionResult AcceptRent(int HuurderMotorId, int MotorId, DateTime OphaalDatum, DateTime InleverDatum)
        {
            _ihuurderMotor.AcceptOrDeclineRent(HuurderMotorId, "Accept");

            List<HuurderMotor> list = _ihuurderMotorCollection.GetHuurderMotorListForMotor(MotorId);
            foreach (var item in list)
            {
                if (item.HuurderMotorId != HuurderMotorId)
                {
                    if (item.IsGeaccepteerd == false && item.IsGeweigerd == false)
                    {
                        if (item.OphaalDatum <= InleverDatum && item.InleverDatum >= OphaalDatum)
                        {
                            _ihuurderMotor.AcceptOrDeclineRent(item.HuurderMotorId, "Decline");
                        }
                    }
                }
            }
            return RedirectToAction("HuurLijstSelected");
        }

        public ActionResult DeclineRent(int HuurderMotorId)
        {
            _ihuurderMotor.AcceptOrDeclineRent(HuurderMotorId, "Decline");
            return RedirectToAction("HuurLijstSelected");
        }
    
    }
}
