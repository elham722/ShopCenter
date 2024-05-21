using ShopCenter.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Domain.Models.Ticket
{
    public class Ticket : BaseEntities
    {
        public int OwnerId { get; set; }

        public string Title { get; set; }

        public TicketSections TicketSection { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public TicketPriorities TicketPriority { get; set; }

        public bool IsReadByAdmin { get; set; }

        public bool IsReadByOwners { get; set; }

        public string? AttachmentFile { get; set; }
        public FileTypeTitle? FileType { get; set; }


        #region Relations

        [ForeignKey("OwnerId")]
        public User.User User { get; set; }

        public List<TicketMassege> TicketMasseges { get; set; }
        #endregion
    }
}
