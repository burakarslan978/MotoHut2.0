﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IUser _iuser;



        public HomeController(ILogger<HomeController> logger, IMotorCollection iMotorCollection, IUser iUser)
        {
            _logger = logger;
            _imotorCollection = iMotorCollection;
            _iuser = iUser;
        }

        public IActionResult Index()
        {
            List<MotorViewModel> list = new List<MotorViewModel>();
            foreach(var item in _imotorCollection.GetMotorList())
            {
                list.Add(new MotorViewModel { MotorId = item.MotorId, VerhuurderId = item.VerhuurderId, Bouwjaar = item.Bouwjaar, Prijs = item.Prijs, Model = item.Model, Huurbaar = item.Huurbaar, VerhuurderNaam =  _iuser.GetNameWithId(_iuser.GetUserIdWithVerhuurderId(item.VerhuurderId)) });
            }
            ViewModel viewModel = new ViewModel { MotorModels = list};
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }




    }
}