using ShopCenter.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Models.Ticket
{
    public class TicketMassege:BaseEntities
    {
        public string Message { get; set; }

        public int SenderId { get; set; }

        public int TiketId { get; set; }

        #region Relations

        [ForeignKey("SenderId")]
        public User.User User { get; set; }

        [ForeignKey("TiketId")]
        public Ticket Ticket { get; set; }

        #endregion
    }
}
