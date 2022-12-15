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
                List<MotorViewModel> motorViewModel = new List<MotorViewModel>();
                List<HuurderMotorViewModel> huurderMotorViewModel = new List<HuurderMotorViewModel>();
                foreach (var item in _imotorCollection.GetMotorListForVerhuurder(Convert.ToInt32(GetFromClaim("verhuurderid"))))
                {
                    motorViewModel.Add(ConvertMotorToMotorViewModel(item));
                    huurderMotorViewModel.AddRange(GetHuurderMotorViewModelForMotor(item.MotorId)); ;

                }

                ViewModel viewModel = new ViewModel 
                { 
                    HuurderMotorModels = huurderMotorViewModel,
                    MotorModels = motorViewModel,
                    Motors = GetCurrentUserSelectItemsViewModel()
                };
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RentMotor(int id, DateTime pickUpDate, DateTime returnDate)
        {
            Motor motor = new Motor();
            int TotalDays = 0;
            if (_ihuurderMotor.CheckAvailability(id, pickUpDate, returnDate) && pickUpDate <= returnDate && pickUpDate > DateTime.Now.AddHours(2))
            {
                motor = _imotor.GetMotor(id);
                _imotor.RentMotor(id, pickUpDate, returnDate, motor.Prijs, Convert.ToInt32(GetFromClaim("huurderid")));
                TotalDays = CalculateDays(pickUpDate, returnDate);
            }
            else
            {
                motor.MotorId = 0;
            }

            if (returnDate <= pickUpDate)
            {
                motor.MotorId = -1;
            }
            else if (pickUpDate < DateTime.Now.AddHours(2))
            {
                motor.MotorId = -2;
            }

            MotorViewModel motorViewModel = ConvertMotorToMotorViewModel(motor);
            HuurderMotorViewModel huurderMotorViewModel = new HuurderMotorViewModel { OphaalDatum = pickUpDate, InleverDatum = returnDate, Prijs = motor.Prijs, Dagen = TotalDays, TotaalPrijs = motor.Prijs * TotalDays };
            ViewModel viewModel = new ViewModel 
            { 
                MotorViewModel = motorViewModel,
                HuurderMotorViewModel = huurderMotorViewModel
            };

            return View(viewModel);
        }
        public ActionResult HuurLijstSelected(int MotorId)
        {
            ViewModel viewModel = new ViewModel { 
                HuurderMotorModels = GetHuurderMotorViewModelForMotor(MotorId),
                Motors = GetCurrentUserSelectItemsViewModel(),
                MotorViewModel = ConvertMotorToMotorViewModel(_imotor.GetMotor(MotorId)) };
            return View(viewModel);

        }

        private List<SelectListItem> GetCurrentUserSelectItemsViewModel()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in _imotorCollection.GetMotorListForVerhuurder(Convert.ToInt32(GetFromClaim("verhuurderid"))))
            {
                items.Add(new SelectListItem 
                { Text = "" + item.MotorId + ": " + item.Bouwjaar + " " 
                + item.Model + "", Value = item.MotorId.ToString() });
            }
            return items;
        }

        private MotorViewModel ConvertMotorToMotorViewModel(Motor motor)
        {
            if(motor != null)
            {
                MotorViewModel viewModel = new MotorViewModel
                {
                    MotorId = motor.MotorId,
                    VerhuurderId = motor.VerhuurderId,
                    Bouwjaar = motor.Bouwjaar,
                    Prijs = motor.Prijs,
                    Model = motor.Model,
                    Huurbaar = motor.Huurbaar
                };
                return viewModel;
            }
            return new MotorViewModel();
            
        }

        private List<HuurderMotorViewModel> GetHuurderMotorViewModelForMotor(int motorId)
        {
            List<HuurderMotorViewModel> list = new List<HuurderMotorViewModel>();
            foreach (var item in _ihuurderMotorCollection.GetHuurderMotorListForMotor(motorId))
            {
                int dagen = CalculateDays(item.OphaalDatum, item.InleverDatum);
                list.Add(ConvertItemToHuurderMotorViewModel(item));
            }
            return list;
        }

        private HuurderMotorViewModel ConvertItemToHuurderMotorViewModel(HuurderMotor huurderMotor)
        {
            int dagen = CalculateDays(huurderMotor.OphaalDatum, huurderMotor.InleverDatum);
            HuurderMotorViewModel viewModel = new HuurderMotorViewModel
            {
                HuurderId = huurderMotor.HuurderId,
                HuurderMotorId = huurderMotor.HuurderMotorId,
                HuurderNaam = _iuser.GetNameWithId(_iuser.GetUserIdWithHuurderId(huurderMotor.HuurderId)),
                HuurderLeeftijd = GetAgeFromDoB(_iuser.GetDoBWithId(_iuser.GetUserIdWithHuurderId(huurderMotor.HuurderId))),
                MotorId = huurderMotor.MotorId,
                OphaalDatum = huurderMotor.OphaalDatum,
                InleverDatum = huurderMotor.InleverDatum,
                IsGeaccepteerd = huurderMotor.IsGeaccepteerd,
                Prijs = huurderMotor.Prijs,
                Dagen = dagen,
                TotaalPrijs = huurderMotor.Prijs * dagen
            };
            return viewModel;
        }

        public int CalculateDays(DateTime ophaalDatum, DateTime inleverDatum)
        {
            return Convert.ToInt32(Math.Ceiling((inleverDatum - ophaalDatum).TotalDays));
        }

        public ActionResult AcceptRent(int HuurderMotorId, int MotorId, DateTime OphaalDatum, DateTime InleverDatum)
        {
            _ihuurderMotor.AcceptOrDeclineRent(HuurderMotorId, true);

            DeclineOverlappingRents(HuurderMotorId, MotorId, OphaalDatum, InleverDatum);
            
            return RedirectToAction("HuurLijstSelected", new { MotorId = MotorId });
        }

        public void DeclineOverlappingRents(int huurderMotorId, int motorId, DateTime ophaalDatum, DateTime inleverDatum)
        {
            List<HuurderMotor> list = _ihuurderMotorCollection.GetHuurderMotorListForMotor(motorId);
            foreach (var item in list)
            {
                if (CheckIfOverlaps(item, huurderMotorId, ophaalDatum, inleverDatum))
                {
                    _ihuurderMotor.AcceptOrDeclineRent(item.HuurderMotorId, false);
                }
            }
        }

        private bool CheckIfOverlaps(HuurderMotor huurderMotor, int huurderMotorId, DateTime ophaalDatum, DateTime inleverDatum)
        {
            return (huurderMotor.HuurderMotorId != huurderMotorId
                    && huurderMotor.IsGeaccepteerd != true
                    && huurderMotor.OphaalDatum <= inleverDatum
                    && huurderMotor.InleverDatum >= ophaalDatum);
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
        public int GetAgeFromDoB(DateTime dob)
        {
            return DateTime.Now.Subtract(dob).Days / 365;
        }

    }
}
