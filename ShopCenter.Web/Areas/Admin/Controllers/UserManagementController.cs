using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.OpenApi.Extensions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ShopCenter.Application.Services.Implementation;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Domain.ViewModels.User;
using System.Web.Helpers;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagementController : Controller
    {
        #region Ctor
        private IUserServices _userServices;
        private IPermissionService _permissionService;
        public UserManagementController(IUserServices userServices, IPermissionService permissionService)
        {
            _userServices = userServices;
            _permissionService = permissionService;
        }
        #endregion

        #region ListUser
        [Route("usermanagment")]
        public async Task<IActionResult> Index(string fullName, string email, string phoneNumber)
        {
            List<UsersListViewModel> model = await _userServices.GetUsersList();

            if (!string.IsNullOrEmpty(fullName))
            {
                model = model.Where(u => u.FullName.Contains(fullName)).ToList();
            }
            if (!string.IsNullOrEmpty(email))
            {
                model = model.Where(u => u.Email.Contains(email)).ToList();
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                model = model.Where(u => u.PhoneNumber != null && u.PhoneNumber.Contains(phoneNumber)).ToList();
            }
            return View(model);
        }

        #endregion

        #region AddNewUser

        [HttpGet("createuser")]
        public async Task<IActionResult> CreateUser()
        {
            ViewData["Roles"] = await _permissionService.GetAllRoles();
            return PartialView("_AddNewUser");
        }


        [HttpPost("submitaddusermodal")]
        public IActionResult SubmitCreateUser(int roleId, CreateUserViewModel newUser)
        {
            var resultRoleId = _permissionService.IsExistRoleById(roleId);
            if (resultRoleId)
            {
                var result = _userServices.AddUserByAdmin(newUser, roleId);
                switch (result)
                {
                    case AddOrEditUserByAdminResult.DuplicateEmail:
                        return new JsonResult(new { status = "DuplicateEmail" });

                    case AddOrEditUserByAdminResult.DuplicatePhoneNumber:
                        return new JsonResult(new { status = "DuplicatePhoneNumber" });

                    case AddOrEditUserByAdminResult.ImageSizeInvalid:
                        return new JsonResult(new { status = "ImageSizeInvalid" });

                    case AddOrEditUserByAdminResult.ImageExensionInvalid:
                        return new JsonResult(new { status = "ImageExensionInvalid" });

                    case AddOrEditUserByAdminResult.Done:
                        return new JsonResult(new { status = "Success" });
                }
            }
            return new JsonResult(new { status = "Error" });
        }



        #endregion

        #region EditUser

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int UserId)
        {
            var details = await _userServices.ShowDetailsForEditUserByAdmin(UserId);
            ViewData["Roles"] = await _permissionService.GetAllRoles();
            if (details == null)
            {
                return NotFound();
            }
            return PartialView("_EditUser", details);
        }


        [HttpPost("SubmitEditUser")]
        public IActionResult SubmitEditUser(int roleId, EditUserViewModel user)
        {
            var resultRoleId = _permissionService.IsExistRoleById(roleId);
            if (resultRoleId)
            {
                var result = _userServices.EditUserByAdmin(user, roleId);
                switch (result)
                {
                    case AddOrEditUserByAdminResult.UserNotFound:
                        return new JsonResult(new { status = "UserNotFound" });

                    case AddOrEditUserByAdminResult.DuplicateEmail:
                        return new JsonResult(new { status = "DuplicateEmail" });

                    case AddOrEditUserByAdminResult.DuplicatePhoneNumber:
                        return new JsonResult(new { status = "DuplicatePhoneNumber" });

                    case AddOrEditUserByAdminResult.ImageSizeInvalid:
                        return new JsonResult(new { status = "ImageSizeInvalid" });

                    case AddOrEditUserByAdminResult.ImageExensionInvalid:
                        return new JsonResult(new { status = "ImageExensionInvalid" });

                    case AddOrEditUserByAdminResult.Done:
                        return new JsonResult(new { status = "Success" });
                }
            }
            return new JsonResult(new { status = "Error" });
        }


        #endregion


        #region DetailsUser

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int UserId)
        {
            var detail = await _userServices.GetDetailsUserForShow(UserId);
            return PartialView("_DetailsUser", detail);
        }

        #endregion


        #region DeleteUser

        [HttpGet("delete-user")]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            var result = await _userServices.DeleteUser(UserId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }
    }

    #endregion

    #region SearchUser
   


    #endregion
}
