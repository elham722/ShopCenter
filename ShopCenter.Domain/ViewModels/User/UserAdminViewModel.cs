using FoolProof.Core;
using Microsoft.AspNetCore.Http;
using ShopCenter.Domain.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ShopCenter.Domain.ViewModels.User
{
    public class UsersListViewModel
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateUserViewModel
    {
        
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        [RequiredIfEmpty("PhoneNumber", ErrorMessage = "حداقل یک از موارد ایمیل یا شماره همراه را وارد کنید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).*$", ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر و شامل حروف بزرگ و کوچک و عدد باشد")]
        [RequiredIfNotEmpty("Email", ErrorMessage = "در صورت وارد کردن ایمیل رمز عبور را باید وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? LastName { get; set; }

        [Display(Name = "شماره همراه")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(11, ErrorMessage = " لطفا شماره تلفن خود را بصورت صحیح وارد کنید ")]
        [RequiredIfEmpty("Email", ErrorMessage = "حداقل یک از موارد ایمیل یا شماره همراه را وارد کنید")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "فعال سازی حساب کاربری")]
        public bool IsActive { get; set; }

        [Display(Name = "انتخاب کاربر به عنوان ادمین سایت")]
        public bool IsAdmin { get; set; }

        [Display(Name = "عکس کاربر")]
        public IFormFile? UserAvatar { get; set; }

        public string? ActivationCode { get; set; }

        [Display(Name = "ارسال کد فعال سازی برای کاربر")]
        public bool CreateActivationCode { get; set; }


        public Role Role { get; set; }
    }

    public class DetailsUserViewModel
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? NationalNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public string AvatarName { get; set; }
        public DateTime? BirthDate { get; set; }
       
    }

    public class EditUserViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        [RequiredIfEmpty(nameof(PhoneNumber), ErrorMessage = "حداقل یک از موارد ایمیل یا شماره همراه را وارد کنید")]
        public string? Email { get; set; }

        
        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).*$", ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر و شامل حروف بزرگ و کوچک و عدد باشد")]
        [RequiredIfNotEmpty("Email", ErrorMessage = "در صورت وارد کردن ایمیل رمز عبور را باید وارد کنید")]
        public string? Password { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? LastName { get; set; }

        [Display(Name = "شماره همراه")]
        [RequiredIfEmpty(nameof(Email), ErrorMessage = "حداقل یک از موارد ایمیل یا شماره همراه را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(11, ErrorMessage = " لطفا شماره تلفن خود را بصورت صحیح وارد کنید ")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "فعال سازی حساب کاربری")]
        public bool IsActive { get; set; }

        [Display(Name = "انتخاب کاربر به عنوان ادمین سایت")]
        public bool IsAdmin { get; set; }

        public string AvatarName { get; set; }

        [Display(Name = "عکس کاربر")]
        public IFormFile? UserAvatar { get; set; }

     
        [Display(Name = "ارسال کد فعال سازی برای کاربر")]
        public bool CreateActivationCode { get; set; }

        public int RoleId { get; set; }
    }
}
