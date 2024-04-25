using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ShopCenter.Domain.Models.Common
{
    public class Enums
    {
        public enum ContactUsSubjectTitle
        {

            [Display(Name = "پیشنهاد")]
            Suggestion,

            [Display(Name = "انتقاد")]
            Criticism,

            [Display(Name = "پیگیری سفارش")]
            OrderTracking,

            [Display(Name = "خدمات پس از فروش")]
            AfterSalesService,

            [Display(Name = "گارانتی")]
            Warranty,

            [Display(Name = "مدیریت")]
            Management,

            [Display(Name = "مالی")]
            Financial,

            [Display(Name = "سایر موضوعات")]
            Others,
        }

        public enum ContactUsStatus
        {
            [Display(Name = "خوانده نشده")]
            UnRead,

            [Display(Name = "پاسخ داده نشده")]
            NotAnswered,

            [Display(Name = "پاسخ داده شده")]
            Answered,         
        }

        public enum FileTypeTitle
        {
            [Display(Name = "عکس")]
            Image,

            [Display(Name = "ویدیو")]
            Video
        }

        public enum SiteSections
        {
            User,
            ContactUs,
            AboutUs,
            Product,
            Ticket

        }

        public enum UserEditByAdminReturns
        {
            [Display(Name = "ایمیل وارد شده تکراری میباشد")]
            DuplicateEmail,
            [Display(Name = "کاربر یافت نشد")]
            UserNotFound,
            [Display(Name = "حجم عکس  باید کمتر از 2 مگا بایت باشد.")]
            ImageSizeInvalid,
            [Display(Name = "پسوند عکس  نامعتبر است")]
            ImageExensionInvalid,
            [Display(Name = "تغییرات با موفقیت اعمال شد")]
            Done
        }

        public enum AddOrEditUserByAdminResult
        {
            [Display(Name = "ایمیل وارد شده تکراری میباشد")]
            DuplicateEmail,
            [Display(Name = "شماره تماس وارد شده تکراری میباشد")]
            DuplicatePhoneNumber,
            [Display(Name = "حجم عکس  باید کمتر از 2 مگا بایت باشد")]
            ImageSizeInvalid,
            [Display(Name = "پسوند عکس نامعتبر است")]
            ImageExensionInvalid,
            [Display(Name = "کاربر با موفقیت ثبت شد")]
            Done,
            UserNotFound

        }

        public enum AddContactUsReturns
        {
    
            [Display(Name = "حجم فایل وارد شده نامعتبر است")]
            FileSizeInvalid,
            [Display(Name = "پسوند فایل وارد شده نامعتبر است")]
            FileExensionInvalid,
            [Display(Name = "پیام با موفقیت ارسال شد")]
            Done
        }

        public enum TicketSections
        {
            [Display(Name = "بخش را وارد کنید")]
            None,

            [Display(Name ="فنی")]
            Technical,

            [Display(Name = "پیشنهاد")]
            Suggestion,

            [Display(Name = "انتقاد")]
            Criticism,

            [Display(Name = "پیگیری سفارش")]
            OrderTracking,

            [Display(Name = "خدمات پس از فروش")]
            AfterSalesService,

            [Display(Name = "گارانتی")]
            Warranty,

            [Display(Name = "مدیریت")]
            Management,

            [Display(Name = "مالی")]
            Financial,

            [Display(Name = "سایر موضوعات")]
            Others,
        }

        public enum TicketStatus
        {
           

            [Display(Name = "پاسخ داده نشده")]
            NotAnswered,

            [Display(Name = "پاسخ داده شده")]
            Answered,

            [Display(Name = "بسته شده")]
            Closed
        }

        public enum TicketPriorities 
        {
            [Display(Name = "اولویت را وارد کنید")]
            None,
            [Display(Name = "عادی")]
            Normal,

            [Display(Name = "مهم")]
            Important,

            [Display(Name = "خیلی مهم")]
            VeryImportant,

        }

        public enum FinalAddFileForTicket
        {

            [Display(Name = "حجم فایل وارد شده نامعتبر است")]
            FileSizeInvalid,
            [Display(Name = "پسوند فایل وارد شده نامعتبر است")]
            FileExensionInvalid,
            [Display(Name = "پیام با موفقیت ارسال شد")]
            Done
        }

        public enum AddProductReturns
        {
            [Display(Name ="لطفا یک دسته بندی انتخاب کنید")]
            CategoryInvalid,
            [Display(Name = "حجم عکس  باید کمتر از 2 مگا بایت باشد")]
            FileSizeInvalid,
            [Display(Name = "پسوند فایل وارد شده نامعتبر است")]
            FileExensionInvalid,
            [Display(Name = "محصول با موفقیت ثبت شد")]
            Done
        }

        public enum EditProductReturns
        {
            [Display(Name = "محصول یافت نشد")]
            NotFound,
            [Display(Name = "لطفا یک دسته بندی انتخاب کنید")]
            CategoryInvalid,
            [Display(Name = "حجم عکس  باید کمتر از 2 مگا بایت باشد")]
            FileSizeInvalid,
            [Display(Name = "پسوند فایل وارد شده نامعتبر است")]
            FileExensionInvalid,
            [Display(Name = "محصول با موفقیت ویرایش شد")]
            Done
        }

        public enum BannerDirection
        {
            [Display(Name = "")]
            Top1,
            [Display(Name = "")]
            Top2,
            [Display(Name = "")]
            TopRight,
            [Display(Name = "")]
            TopLeft,
            [Display(Name = "")]
            BottomLeft1,
            [Display(Name = "")]
            BottomLeft2,
            [Display(Name = "")]
            BottomRight1,
            [Display(Name = "")]
            BottomRight2,
            [Display(Name = "")]
            BottomLeft,
            [Display(Name = "")]
            BottomRight
        }
        public enum DiscountType
        {
            [Display(Name = " انتخاب کنید")]
            None,

            [Display(Name = "تخفیف کلی")]
            Total,

            [Display(Name ="کاربر")]
            User,

            [Display(Name = "محصول")]
            Product,

            [Display(Name = "دسته بندی")]
            Category,
            
        }

        public enum AddDiscountReturns
        {
            [Display(Name = "  خطایی هنگام ثبت رخ داده است")]
            Faild,

            [Display(Name = "  کد تخفیف با موفقیت ثبت شد")]
            Done,
        }
    }
}
