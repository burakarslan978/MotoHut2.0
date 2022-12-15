using Business;
using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MotoHut2._0.Collections;
using MotoHut2._0.Models;
using System.Reflection;

namespace MotoHut2._0.Controllers
{
    public class RentController : Controller
    {
        private readonly ILogger<RentController> _logger;
        private readonly IMotorCollection _imotorCollection;
        private readonly IMotor _imotor;
        private readonly IHuurderMotorCollection _ihuurderMotorCollection;
        private readonly IHuurderMotor _ihuurderMotor;
        private readonly IUser _iuser;

        public RentController(ILogger<RentController> logger, IMotorCollection iMotorCollection, IMotor iMotor, IHuurderMotorCollection iHuurderMotorCollection, IHuurderMotor iHuurderMotor, IUser iUser)
        {
            _logger = logger;
            _imotorCollection = iMotorCollection;
            _imotor = iMotor;
            _ihuurderMotorCollection = iHuurderMotorCollection;
            _ihuurderMotor = iHuurderMotor;
            _iuser = iUser;
        }

        public IActionResult HuurLijst()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<SelectListItem> items = new List<SelectListItem>();

                List<MotorViewModel> list1 = new List<MotorViewModel>();
                List<HuurderMotorViewModel> list2 = new List<HuurderMotorViewModel>();
                foreach (var item in _imotorCollection.GetMotorListForVerhuurder(Convert.ToInt32(GetFromClaim("verhuurderid"))))
                {
                    list1.Add(new MotorViewModel { MotorId = item.MotorId, VerhuurderId = item.VerhuurderId, Bouwjaar = item.Bouwjaar, Prijs = item.Prijs, Model = item.Model, Huurbaar = item.Huurbaar });
                    items.Add(new SelectListItem { Text = "" + item.MotorId + ": " + item.Bouwjaar + " " + item.Model + "", Value = item.MotorId.ToString() });
                    foreach(var huurderMotor in _ihuurderMotorCollection.GetHuurderMotorListForMotor(item.MotorId))
                    {
                        int dagen = CalculateDays(huurderMotor.OphaalDatum, huurderMotor.InleverDatum);
                        list2.Add(new HuurderMotorViewModel
                        {
                            HuurderId = huurderMotor.HuurderId,
                            HuurderNaam = _iuser.GetNameWithId(_iuser.GetUserIdWithHuurderId(huurderMotor.HuurderId)),
                            HuurderLeeftijd = GetAge(_iuser.GetDoBWithId(_iuser.GetUserIdWithHuurderId(huurderMotor.HuurderId))),
                            HuurderMotorId = huurderMotor.HuurderMotorId,
                            MotorId = item.MotorId,
                            OphaalDatum = huurderMotor.OphaalDatum,
                            InleverDatum = huurderMotor.InleverDatum,
                            IsGeaccepteerd = huurderMotor.IsGeaccepteerd,
                            Prijs = huurderMotor.Prijs,
                            Dagen = dagen,
                            TotaalPrijs = item.Prijs * dagen
                        }); ;
                    }
                }

                ViewModel viewModel = new ViewModel { HuurderMotorModels = list2, MotorModels = list1, Motors = items };
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RentMotor(int id, DateTime pickUpDate, DateTime returnDate)
        {
            Motor model = new Motor();
            int TotalDays = 0;
            if (_ihuurderMotor.CheckAvailability(id, pickUpDate, returnDate) && pickUpDate <= returnDate && pickUpDate > DateTime.Now.AddHours(2))
            {
                model = _imotor.GetMotor(id);
                _imotor.RentMotor(id, pickUpDate, returnDate, model.Prijs, Convert.ToInt32(GetFromClaim("huurderid")));
                TotalDays = CalculateDays(pickUpDate, returnDate);
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
            HuurderMotorViewModel huurderMotorViewModel = new HuurderMotorViewModel { OphaalDatum = pickUpDate, InleverDatum = returnDate, Prijs = model.Prijs, Dagen = TotalDays, TotaalPrijs = model.Prijs * TotalDays };
            ViewModel viewModel = new ViewModel { MotorViewModel = motorViewModel, HuurderMotorViewModel = huurderMotorViewModel };

            return View(viewModel);
        }
        public ActionResult HuurLijstSelected(int MotorId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            MotorViewModel motorViewModel = new MotorViewModel();
            foreach (var item in _imotorCollection.GetMotorListForVerhuurder(Convert.ToInt32(GetFromClaim("verhuurderid"))))
            {
                items.Add(new SelectListItem { Text = "" + item.MotorId + ": " + item.Bouwjaar + " " + item.Model + "", Value = item.MotorId.ToString() });
                if(item.MotorId == MotorId)
                {
                    motorViewModel.Model = item.Model;
                    motorViewModel.Bouwjaar = item.Bouwjaar;
                }
            }

            List<HuurderMotorViewModel> list2 = new List<HuurderMotorViewModel>();
            foreach (var item in _ihuurderMotorCollection.GetHuurderMotorListForMotor(MotorId))
            {
                int dagen = CalculateDays(item.OphaalDatum, item.InleverDatum);
                list2.Add(new HuurderMotorViewModel
                {
                    HuurderId = item.HuurderId,
                    HuurderMotorId = item.HuurderMotorId,
                    HuurderNaam = _iuser.GetNameWithId(_iuser.GetUserIdWithHuurderId(item.HuurderId)),
                    HuurderLeeftijd = GetAge(_iuser.GetDoBWithId(_iuser.GetUserIdWithHuurderId(item.HuurderId))),
                    MotorId = item.MotorId,
                    OphaalDatum = item.OphaalDatum,
                    InleverDatum = item.InleverDatum,
                    IsGeaccepteerd = item.IsGeaccepteerd,
                    Prijs = item.Prijs,
                    Dagen = dagen,
                    TotaalPrijs = item.Prijs * dagen
                });
            }

            ViewModel viewModel = new ViewModel { HuurderMotorModels = list2, Motors = items, MotorViewModel = motorViewModel };
            return View(viewModel);

        }

        public int CalculateDays(DateTime ophaalDatum, DateTime inleverDatum)
        {
            return Convert.ToInt32(Math.Ceiling((inleverDatum - ophaalDatum).TotalDays));
        }

        public ActionResult AcceptRent(int HuurderMotorId, int MotorId, DateTime OphaalDatum, DateTime InleverDatum)
        {
            _ihuurderMotor.AcceptOrDeclineRent(HuurderMotorId, true);

            List<HuurderMotor> list = _ihuurderMotorCollection.GetHuurderMotorListForMotor(MotorId);
            foreach (var item in list)
            {
                if (item.HuurderMotorId != HuurderMotorId)
                {
                    if (item.IsGeaccepteerd == false)
                    {
                        if (item.OphaalDatum <= InleverDatum && item.InleverDatum >= OphaalDatum)
                        {
                            _ihuurderMotor.AcceptOrDeclineRent(item.HuurderMotorId, false);
                        }
                    }
                }
            }
            return RedirectToAction("HuurLijstSelected");
        }

        public ActionResult DeclineRent(int HuurderMotorId)
        {
            _ihuurderMotor.AcceptOrDeclineRent(HuurderMotorId, false);
            return RedirectToAction("HuurLijstSelected");
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
        public int GetAge(DateTime dob)
        {
            return DateTime.Now.Subtract(dob).Days / 365;
        }

    }
}
