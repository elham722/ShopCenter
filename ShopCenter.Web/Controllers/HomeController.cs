using Microsoft.AspNetCore.Mvc;
using ShopCenter.Application.Extensions;
using ShopCenter.Application.Services.Implementation;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Domain.ViewModels.ContactUs.Client;
using ShopCenter.Web.Models;
using System.Diagnostics;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Ctor
        private readonly ILogger<HomeController> _logger;
        private IAboutUsServices _aboutUsServices;
        private IContactUsServices _contactUsService;

        public HomeController(ILogger<HomeController> logger,IAboutUsServices aboutUsServices, IContactUsServices contactUsService)
        {
            _aboutUsServices = aboutUsServices;
            _logger = logger;
            _contactUsService = contactUsService;
        }
        #endregion
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


        #region Contact Us

        public IActionResult ContactUS()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUS(ContactUsViewModel contactUs)
        {
            if (!ModelState.IsValid)
            {
                return View(contactUs);
            }
            var result = _contactUsService.AddContactUs(contactUs);
            if (result == AddContactUsReturns.Done)
            {
                return RedirectToAction("index");
            }
            ModelState.AddModelError("AttachFile", result.GetDisplayName());

            return View(contactUs);
        }

        #endregion
    }
}
