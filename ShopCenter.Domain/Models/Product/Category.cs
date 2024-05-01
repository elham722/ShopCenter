using ShopCenter.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Models.Product
{
    public class Category:BaseEntities
    {

        [MaxLength(50)]
        public string CategoryTitle { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? ImageUrl { get; set; }

        public int? ParentId { get; set; }



        #region Relations

        [ForeignKey("ParentId")]
        public virtual Category Parent { get; set; }

        public List<Product> Products { get; set; }

        #endregion
    }
}
