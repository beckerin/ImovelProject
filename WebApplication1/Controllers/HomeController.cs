using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication.Engine;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache cache;

        public HomeController(ILogger<HomeController> logger, IMemoryCache cache)
        {
            _logger = logger;
            this.cache=cache;
        }

        public IActionResult Index()
        {
            Filter filter = new()
            {
                Limit = 3,
                Home = true,
                Page = 0,
            };
            filter.Populate();
            return View(filter);
        }

        public ActionResult ListFiltered(IFormCollection collection)
        {
            Filter filter = new()
            {
                Address = collection["Address"],
                Limit = 6,
                Home = false,
                Large = true,
                Option = Filter.ParseOption(collection["Option"]),
            };
            filter.FillStatic(filter);

            return RedirectToAction("List", "Realestate");
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
