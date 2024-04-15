using Microsoft.AspNetCore.Mvc;
using ShopCenter.Application.Security;
using ShopCenter.Application.Services.Interface.User;
using ShopCenter.Domain.ViewModels.User;

namespace ShopCenter.Web.Controllers
{
    public class UserController : Controller
    {
        #region IoC
        private IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        [HttpGet("RegisterOrLogin")]
        public async Task<IActionResult> RegisterOrLogin()
        {
            return View();
        }

        [HttpPost("RegisterOrLogin")]
        public async Task<IActionResult> RegisterOrLogin(RegisterOrLoginViewModel registerOrLogin)
        {
            if(!ModelState.IsValid)
            {
                return View(registerOrLogin);
            }

            if(registerOrLogin.EmailOrPhoneNumber.IsEmail())
            {

            }
            return View();
        } 
    }
}
