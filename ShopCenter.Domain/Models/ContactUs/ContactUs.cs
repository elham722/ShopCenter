using ShopCenter.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Domain.Models.ContactUs
{
    public class ContactUs:BaseEntities
    {
        [MaxLength(100)]
        public string? FullName { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(13)]
        public string? Phone { get; set; }

        public string Massage { get; set; }

        public string? Answer { get; set; }

        public DateTime? AnswerDateTime { get; set; }

        [MaxLength(100)]
        public ContactUsSubjectTitle Subject { get; set; }

        public string? FileName { get; set; }

        public FileTypeTitle? FileType { get; set; }

        public ContactUsStatus Status { get; set; }
    }
}
