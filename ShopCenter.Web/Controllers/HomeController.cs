using Microsoft.AspNetCore.Mvc;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Web.Models;
using System.Diagnostics;

namespace ShopCenter.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAboutUsServices _aboutUsServices;
        public HomeController(ILogger<HomeController> logger,IAboutUsServices aboutUsServices)
        {
            _aboutUsServices = aboutUsServices;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpGet("About")]
        public async  Task<IActionResult> AboutUsSite()
        {
            var about = await _aboutUsServices.GetAboutUs();
            return View(about);
        }
    }
}
