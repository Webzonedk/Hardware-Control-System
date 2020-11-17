using System;
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
        public bool IsLoggedIn;
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            IsLoggedIn = Convert.ToBoolean(HttpContext.Session.GetString("loggedIn"));

            if (IsLoggedIn)
            {
                return View();
            }
            else
            {
                return View("LoginError");
            }

        }
        [HttpPost]
        public IActionResult SearchResult(DataContainer container)
        {

            IsLoggedIn = Convert.ToBoolean(HttpContext.Session.GetString("loggedIn"));
            if (IsLoggedIn)
            {
                DbManager manager = new DbManager(configuration);
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
            else
            {
                return View("LoginError");
            }


        }

        [HttpPost]
        public IActionResult Edit(string submit)
        {
            int caseID = Convert.ToInt32(submit);
            DbManager manager = new DbManager(configuration);
            DataContainer container = manager.GetDataFromCaseID(caseID);


            container.DataLogs.Add(new DataLog());
            Debug.WriteLine(container.DataLogs[container.DataLogs.Count - 1]);
            return View(container);

        }

        [HttpPost]
        public IActionResult Confirmation(DataContainer container)
        {
            Debug.WriteLine(container.DataLogs.Count);
            DbManager manager = new DbManager(configuration);
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
            container.DataLogs.Add(new DataLog());
            Debug.Write(container.DataLogs.Count);
            return View(container);
        }

        public IActionResult Overview()
        {
            IsLoggedIn = Convert.ToBoolean(HttpContext.Session.GetString("loggedIn"));
            if (IsLoggedIn)
            {
                DbManager manager = new DbManager(configuration);
                List<DataContainer> containers = manager.GetAllData();
                return View(containers);
            }
            else
            {
                return View("LoginError");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CheckUser(UserData user)
        {
            DbManager manager = new DbManager(configuration);
            bool validation = manager.UserLogin(user);

            if (validation)
            {
                IsLoggedIn = true;
                HttpContext.Session.SetString("loggedIn", Convert.ToString(IsLoggedIn));
                return View("Index");
            }
            else
            {
                IsLoggedIn = false;
                HttpContext.Session.SetString("loggedIn", Convert.ToString(IsLoggedIn));
                return View("Login");
            }

        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            bool logout = false;
            HttpContext.Session.SetString("loggedIn", Convert.ToString(logout));
            return View("Login");
        }
    }
}
