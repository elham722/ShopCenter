using Microsoft.AspNetCore.Builder;
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
    public class Product:BaseEntities
    {
        [MaxLength(50)]
        public string ProductName { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; } 

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        [MaxLength(100)]
        public string? ImageUrl { get; set; }

        public bool Visible { get; set; } = true;

        public int CategoryId { get; set; }


        #region Relations

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        #endregion
    }
}
