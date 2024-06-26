﻿using Microsoft.AspNetCore.Mvc;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Domain.ViewModels.ContactUs.Admin;

namespace ShopCenter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        #region Ctor
        private IContactUsServices _contactUsServices;

        public ContactUsController(IContactUsServices contactUsServices)
        {
            _contactUsServices = contactUsServices;
        }
        #endregion

        #region Contact Us List
        public async Task<IActionResult> Index()
        {
            List<ContactUsListViewModel> model =await _contactUsServices.GetContactUsList();
            return View(model);
        }
        #endregion

        #region Detail Massage
        public async Task<IActionResult> Detail(int id)
        {
            ContactUsAdminSideViewModel model =await _contactUsServices.GetContactUs(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            await _contactUsServices.ChangeContactUsStatus(model.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(ContactUsAdminSideViewModel contactUs)
        {
            if (!ModelState.IsValid)
            {
                return View(contactUs);
            }

          await  _contactUsServices.AnswerToContactUs(contactUs.Id, contactUs.Answer);
            return RedirectToAction("index");
        }
        #endregion
    }
}
