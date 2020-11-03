﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RedCrossItCheckingSystem.DAL;
using RedCrossItCheckingSystem.Models;
using Microsoft.AspNetCore.Http;

namespace RedCrossItCheckingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SearchResult(DataContainer container)
        {

            DbManager manager = new DbManager();
            string tempSerial;
            if (container.SerialNumber != null)
            {
                tempSerial = container.SerialNumber;
                HttpContext.Session.SetString("tempSerial", tempSerial);
            }
            else
            {
                tempSerial = "";
                HttpContext.Session.SetString("tempSerial", tempSerial);
            }

            List<DataContainer> containers = manager.GetDataFromserial(container);
            Debug.Write(containers);
            return View(containers);

        }

        [HttpPost]
        public IActionResult Edit(string submit)
        {
            int caseID = Convert.ToInt32(submit);
            DbManager manager = new DbManager();
            DataContainer container = manager.GetDataFromCaseID(caseID);
            return View(container);

        }

        [HttpPost]
        public IActionResult Confirmation(DataContainer container)
        {
            DbManager manager = new DbManager();
            if (container.CaseID > 0)
            {
                manager.SetData(container);

            }
            else
            {
                manager.CreateData(container);
            }


            return View(container);
        }

        [HttpPost]
        public IActionResult Create()
        {
            string serial = HttpContext.Session.GetString("tempSerial");
            DataContainer container = new DataContainer();
            container.SerialNumber = serial;
            return View(container);
        }

        public IActionResult Overview()
        {
            DbManager manager = new DbManager();
            List<DataContainer> containers = manager.GetAllData();
            return View(containers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
