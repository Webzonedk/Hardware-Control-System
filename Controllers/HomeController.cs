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
            
            return View(container);

        }

        [HttpPost]
        public IActionResult Confirmation(DataContainer container)
        {
            DbManager manager = new DbManager(configuration);
            DataContainer container2 = new DataContainer();
            int id = 0;
            if (container.CaseID > 0)
            {
                manager.SetData(container);
                id = container.CaseID;
                
            }
            else
            {
                 id = manager.CreateData(container);
            }

            container2 = manager.GetDataFromCaseID(id);

            return View(container2);
        }

        [HttpPost]
        public IActionResult Review(string submit)
        {
            int caseID = Convert.ToInt32(submit);
            DbManager manager = new DbManager(configuration);
            DataContainer container = manager.GetDataFromCaseID(caseID);

            return View(container);
        }

        [HttpPost]
        public IActionResult Create()
        {
            string serial = HttpContext.Session.GetString("tempSerial");
            DataContainer container = new DataContainer();
            container.SerialNumber = serial;
            container.DataLogs.Add(new DataLog());
            return View(container);
        }

        public IActionResult Overview(string selector, string caseId)
        {

            IsLoggedIn = Convert.ToBoolean(HttpContext.Session.GetString("loggedIn"));
            if (IsLoggedIn)
            {
                DbManager manager = new DbManager(configuration);
                List<DataContainer> containers = manager.GetAllData();
                List<DataContainer> sortedList = new List<DataContainer>();
                Debug.WriteLine(selector);
                for (int i = 0; i < containers.Count; i++)
                {
                    switch (selector)
                    {
                        case "Ankommet":
                            if (containers[i].Status == selector)
                            {
                                sortedList.Add(containers[i]);
                            }
                            HttpContext.Session.SetString("status", Convert.ToString("1"));
                            break;
                        case "Igang":
                            if (containers[i].Status == selector)
                            {
                                sortedList.Add(containers[i]);
                            }
                            HttpContext.Session.SetString("status", Convert.ToString("2"));
                            break;
                        case "Defekt":
                            if (containers[i].Status == selector)
                            {
                                sortedList.Add(containers[i]);
                            }
                            HttpContext.Session.SetString("status", Convert.ToString("3"));
                            break;
                        case "OK":
                            if (containers[i].Status == selector)
                            {
                                sortedList.Add(containers[i]);
                            }
                            HttpContext.Session.SetString("status", Convert.ToString("4"));
                            break;
                        case "Afsluttet":
                            if (containers[i].Status == selector)
                            {
                                sortedList.Add(containers[i]);
                            }
                            HttpContext.Session.SetString("status", Convert.ToString("5"));
                            break;
                        default:
                            sortedList.Add(containers[i]);
                            HttpContext.Session.SetString("status", Convert.ToString("0"));
                            break;
                    }
                }


                return View(sortedList);
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
