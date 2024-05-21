using ShopCenter.Domain.Models.Common;
using ShopCenter.Domain.Models.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Models.User
{
    public class User:BaseEntities
    {
        public int RoleId { get; set; }


        [MaxLength(200)]
        public string? FirstName { get; set; }

       
        [MaxLength(200)]
        public string? LastName { get; set; }

       
        [MaxLength(50)]
        public string? NationalNumber { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(11)]
        public string? PhoneNumber { get; set; }

        public int MessageCode { get; set; }

        [MaxLength(200)]
        [MinLength(8)]
        public string? Password { get; set; }

        public DateTime? BirthDate { get; set; }

        public string ActivationCode { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public string AvatarName { get; set; } = "Default.png";


        #region Relations

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public List<Ticket.Ticket> Tickets { get; set; }

        public List<TicketMassege> TicketMasseges { get; set; }
        #endregion

    }
}
