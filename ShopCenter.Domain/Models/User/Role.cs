using ShopCenter.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Models.User
{
    public class Role : BaseEntities
    {
       
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string RoleTiltle { get; set; }

        public bool IsDefaultForNewUsers { get; set; }

        public List<User> Users { get; set; }

    }
}
