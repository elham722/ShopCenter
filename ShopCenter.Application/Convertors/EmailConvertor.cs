using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.Convertors
{
    public class EmailConvertor
    {
        public static string FixEmail(string email)
        {
            return email.ToLower().Replace(" ", "");
        }
    }
}
