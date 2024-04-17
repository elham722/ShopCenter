using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ShopCenter.Application.Generators;
using ShopCenter.Application.Security;
using ShopCenter.Application.Senders;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Domain.ViewModels.User;
using System.Security.Claims;
using ShopCenter.Application.Services.Implementation;
using ShopCenter.Application.Convertors;
using ShopCenter.Core.Senders;


namespace ShopCenter.Web.Controllers
{
    public class UserController : Controller
    {
        #region IoC
        private IUserServices _userServices;
        private IViewRenderService _viewRenderService;
        public UserController(IUserServices userServices, IViewRenderService viewRenderService)
        {
            _userServices = userServices;
            _viewRenderService = viewRenderService;
        }
        #endregion

        #region RegisterOrLogin

        [Route("RegisterOrLogin")]
        public async Task<IActionResult> RegisterOrLogin()
        {
            return View();
        }

        [HttpPost("RegisterOrLogin")]
        public async Task<IActionResult> RegisterOrLogin(RegisterOrLoginViewModel registerOrLogin)
        {
            TempData.Clear();
            if (!ModelState.IsValid)
            {
                return View(registerOrLogin);
            }

            if (registerOrLogin.EmailOrPhoneNumber.IsEmail())
            {
                var user = await _userServices.GetUserByEmail(registerOrLogin.EmailOrPhoneNumber);
                TempData["Email"] = registerOrLogin.EmailOrPhoneNumber;
                TempData.Keep("Email");
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        ModelState.AddModelError("EmailOrPhoneNumber", "حساب کاربری شما فعال نمی باشد لطفا ایمیل خود را بررسی کنید یا ازطریق شماره موبایل وارد شوید");
                        return View(registerOrLogin);
                    }
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Register");
                }
            }
            else
            {
                if (registerOrLogin.EmailOrPhoneNumber.Any(x => char.IsLetter(x)) || registerOrLogin.EmailOrPhoneNumber.Length != 11)
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", "لطفا شماره تلفن خود را بصورت صحیح وارد کنید");
                    return View(registerOrLogin);
                }

                var verificationCode = RandomNumberGenerator.GenerateRendomInteger(10000, 99999);
                var isMessageSent = MessageSender.SendMessage(registerOrLogin.EmailOrPhoneNumber
                                      , "به شاپ سنتر خوش آمدید برای ورود به حساب کاربری خود کد زیر را وارد کنید:" +  verificationCode);

                if (!isMessageSent)
                {
                    ViewBag.MessageDoesntSend = true;
                    return View();
                }
                TempData["VerificationCode"] = verificationCode;
                TempData["PhoneNumber"] = registerOrLogin.EmailOrPhoneNumber;
                return RedirectToAction("Verification");
            }

        }

        #endregion

        #region Register
        [HttpGet("Register")]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            var email = TempData["Email"] as string;
            TempData.Keep("Email");
            _userServices.RegisterUser(email, register);
            return View("SuccessRegister");
        }

        #endregion

        #region Login

        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var email = TempData["Email"] as string;
            var user = await _userServices.IsExistUserForLogin(email, login);
            if (user != null)
            {
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.Email),
                        new Claim("AvatarName",user.AvatarName)
                    };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                    //Remember me
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);
                return View();


            }
            else
            {
                ModelState.AddModelError("Password", "لطفا رمز عبور خود را بصورت صحیح وارد کنید");
                return View(login);
            }
        }

        #endregion

        #region Verification
        [HttpGet("Verification")]
        public async Task<IActionResult> Verification()
        {
            var phone = TempData["PhoneNumber"] as string;
            TempData.Keep("PhoneNumber");
            var user = await _userServices.GetUserByPhoneNumber(phone);
            ViewBag.IsExistUser = user;
            ViewBag.PhoneNumber = phone;
            return View();
        }

        [HttpPost("Verification")]
        public async Task<IActionResult> Verification(VerificationViewModel verification)
        {
            if (!ModelState.IsValid)
            {
                return View(verification);
            }
            var phone = TempData["PhoneNumber"] as string;
            TempData.Keep("PhoneNumber");
            var user = await _userServices.GetUserByPhoneNumber(phone);
            var verificationCode = (int)TempData["VerificationCode"];
            TempData.Keep("VerificationCode");
            if (user == null)
            {


                if (verification.VerificationCode != verificationCode)
                {
                    ModelState.AddModelError("VerificationCode", "کد تایید وارد شده صحیح نمی باشد");
                    return View(verification);
                }
                _userServices.RegisterUserByPhoneNumber(phone);
                return Redirect("/");
            }


            if (verification.VerificationCode != verificationCode)
            {
                ModelState.AddModelError("VerificationCode", "کد تایید وارد شده صحیح نمی باشد");
                return View(verification);
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.PhoneNumber),
                new Claim("AvatarName",user.AvatarName)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(principal, properties);
            return Redirect("/");


        }



        [Route("ResendVerificationMessage")]
        public async Task<IActionResult> ResendVerificationMessage()
        {
            var phoneNumber = TempData["PhoneNumber"] as string;
            TempData.Keep("PhoneNumber");
            var user = await _userServices.GetUserByPhoneNumber(phoneNumber);
            var verificationCode = RandomNumberGenerator.GenerateRendomInteger(10000, 99999);
            var isMessageSent = MessageSender.SendMessage(phoneNumber
                                  , "به شاپ سنتر خوش آمدید برای ورود به حساب کاربری خود کد زیر را وارد کنید:" + " " + verificationCode);

            if (!isMessageSent)
            {
                ViewBag.MessageDoesntSend = true;
                return View();
            }
            TempData["VerificationCode"] = verificationCode;
           
            return Redirect("Verification");
        }

        #endregion

        #region ActiveAccount

        [Route("ActiveAccount/{activeCode}")]
        public async Task<IActionResult> ActiveAccount(string activeCode)
        {
            ViewBag.isActivated = await _userServices.ActiveUserAccount(activeCode);
            return View();
        }

        #endregion

        #region LogOut
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion

        #region ForgetPassword
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(forgetPasswordVM);
            }
            if (forgetPasswordVM.EmailOrPhoneNumber.IsEmail())
            {
                var user = await _userServices.GetUserByEmail(forgetPasswordVM.EmailOrPhoneNumber);
                if (user != null)
                {
                    var body = _viewRenderService.RenderToStringAsync("User/_ResetPasswordEmail", user);
                    SendEmail.Send(user.Email, "بازیابی رمز عبور", body);
                }
                else
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", "کاربری با ایمیل وارد شده وجود ندارد");
                    return View(forgetPasswordVM);
                }

            }
            else
            {
                if (forgetPasswordVM.EmailOrPhoneNumber.Any(x => char.IsLetter(x)) || forgetPasswordVM.EmailOrPhoneNumber.Length != 10)
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", "لطفا شماره تلفن خود را بصورت صحیح وارد کنید");
                    return View(forgetPasswordVM);
                }
                var user = await _userServices.GetUserByPhoneNumber(forgetPasswordVM.EmailOrPhoneNumber);
                if (user != null)
                {
                    var body = _viewRenderService.RenderToStringAsync("User/_ResetPasswordEmail", user);
                    SendEmail.Send(user.Email, "بازیابی رمز عبور", body);

                }
                else
                {
                    ModelState.AddModelError("EmailOrPhoneNumber", "کاربری با ایمیل وارد شده وجود ندارد");
                    return View(forgetPasswordVM);
                }
            }
            ViewBag.EmailSent = true;
            return View();
        }

        #endregion

        #region ResetPassword

        [Route("ResetPassword/{activationCode?}")]
        public async Task<IActionResult> ResetPassword(string activationCode)
        {
            var userByActiveCode = await _userServices.IsExistUserByActivationCode(activationCode);
            if (!userByActiveCode)
            {
                return View("Errors/NotFoundError");
            }
            TempData["ActivationCode"] = activationCode;
            return View();
        }


        [HttpPost("ResetPassword/{activationCode?}")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordVM);
            }
            var activationCode = TempData["ActivationCode"]!.ToString();
            ViewBag.PasswordReseted = await _userServices.ResetUserPassword(activationCode, resetPasswordVM);
            return View("SuccessResetPassword");
        }

        #endregion
    }
}
