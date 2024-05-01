using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.Senders
{
    public class MessageSender
    {
        public static bool SendMessage(string phoneNumber, string messageText)
        {
            try
            {
                //var sender = "10008663";
                var receptor = phoneNumber;
                var message = messageText;
               //var api = new Kavenegar.KavenegarApi("32313449354152452F676C484158786B4A646933645546704561316334507461664C4E4566356D616773413D");
                //api.Send(sender, receptor, message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
