using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Models.Common
{
    public class BaseEntities
    {
        public int Id { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
