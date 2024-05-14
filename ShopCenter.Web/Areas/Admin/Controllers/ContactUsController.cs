using Microsoft.AspNetCore.Mvc;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Domain.ViewModels.ContactUs.Admin;

namespace ShopCenter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        #region IoC
        private IContactUsServices _contactUsServices;

        public ContactUsController(IContactUsServices contactUsServices)
        {
            _contactUsServices = contactUsServices;
        }
        #endregion

        #region Contact Us List
        public IActionResult Index()
        {
            List<ContactUsListViewModel> model = _contactUsServices.GetContactUsList();
            return View(model);
        }
        #endregion

        #region Detail Massage
        public IActionResult Detail(int id)
        {
            ContactUsAdminSideViewModel model = _contactUsServices.GetContactUs(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            _contactUsServices.ChangeContactUsStatus(model.Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Detail(ContactUsAdminSideViewModel contactUs)
        {
            if (!ModelState.IsValid)
            {
                return View(contactUs);
            }

            _contactUsServices.AnswerToContactUs(contactUs.Id, contactUs.Answer);
            return RedirectToAction("index");
        }
        #endregion
    }
}
