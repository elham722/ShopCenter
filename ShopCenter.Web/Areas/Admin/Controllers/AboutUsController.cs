using Microsoft.AspNetCore.Mvc;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Domain.ViewModels.AboutUs;

namespace ShopCenter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutUsController : Controller
    {
        #region Ctor
        private IAboutUsServices _aboutUsServices;
        public AboutUsController(IAboutUsServices aboutUsServices)
        {
            _aboutUsServices = aboutUsServices;
        }
        #endregion

        [Route("aboutUs")]
        public async  Task<IActionResult> Index()
        {
            AboutUsViewModel aboutUs = await _aboutUsServices.GetAboutUs();
            return View(aboutUs);
        }

        [HttpPost("aboutUs")]
        public async Task<IActionResult> Index(AboutUsViewModel about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }
            var aboutUs = await _aboutUsServices.AddOrEditAboutUs(about);

            return View(aboutUs);
        }
    }
}
