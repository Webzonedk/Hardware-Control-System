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
        //public bool field for checking state of login & field for configuration 
        public bool IsLoggedIn;
        private readonly IConfiguration configuration;

        // constructor of homecontroller
        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }


        // Action event to return view to index page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // action event to check if user is logged in and return view to search page
        // returns to error page if not logged in
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

        //action event returns page of search results
        //model of search results are returned to the view if any exists
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

        // action event returns data from the database and returns the edit view with the data model
        //takes in a string of case id, data is collected based on case id.
        [HttpPost]
        public IActionResult Edit(string submit)
        {
            int caseID = Convert.ToInt32(submit);
            DbManager manager = new DbManager(configuration);
            DataContainer container = manager.GetDataFromCaseID(caseID);


            container.DataLogs.Add(new DataLog());
            
            return View(container);

        }

        // action event returns the confirmation page after adding data to the database
        // the view takes in data from the database after uploading.
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

        // action event returns Review page with data from database.
        [HttpPost]
        public IActionResult Review(string submit)
        {
            int caseID = Convert.ToInt32(submit);
            DbManager manager = new DbManager(configuration);
            DataContainer container = manager.GetDataFromCaseID(caseID);

            return View(container);
        }

        // action event creates new data model and returns the create page with the data model.
        //serial number is collected though Session.
        [HttpPost]
        public IActionResult Create()
        {
            string serial = HttpContext.Session.GetString("tempSerial");
            DataContainer container = new DataContainer();
            container.SerialNumber = serial;
            container.DataLogs.Add(new DataLog());
            return View(container);
        }

        // action event returns a list of data from database
        // the list is filtered before returned to the overview page
        public IActionResult Overview(string selector, string caseId)
        {

            IsLoggedIn = Convert.ToBoolean(HttpContext.Session.GetString("loggedIn"));
            if (IsLoggedIn)
            {
                DbManager manager = new DbManager(configuration);
                List<DataContainer> containers = manager.GetAllData();
                List<DataContainer> sortedList = new List<DataContainer>();
                
                // data is added to the sortedlist when the status matches the selector string
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

        // action event returns error page when not logged in
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // action event sends user input to database and checks for validation
        // returns index page if validation is ok
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

        // action event returns the login page to the view
        public IActionResult Login()
        {
            return View();
        }

        // action event return to login page and sets the session to logout
        public IActionResult Logout()
        {
            bool logout = false;
            HttpContext.Session.SetString("loggedIn", Convert.ToString(logout));
            return View("Login");
        }
    }
}
