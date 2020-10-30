using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedCrossItCheckingSystem.DAL;
using RedCrossItCheckingSystem.Models;

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
        public IActionResult Edit(DataContainer container)
        {
            
            DbManager manager = new DbManager();
            DataContainer returnData = new DataContainer();
            returnData = manager.GetData(container);
           
            return View(returnData);
          
        }

        public void test(object sender, EventArgs e)
        {
            Debug.Write("this works");
        }

        [HttpPost]
        public IActionResult Confirmation(DataContainer container)
        {
            DbManager manager = new DbManager();
            manager.SetData(container);

            
            return View(container);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
