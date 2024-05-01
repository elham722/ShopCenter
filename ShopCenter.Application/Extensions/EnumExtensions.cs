using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum myEnum)
        {
            var enumDispalyName = myEnum.GetType()
                            .GetMember(myEnum.ToString())
                            .FirstOrDefault();

            if (enumDispalyName != null)
                return enumDispalyName.GetCustomAttribute<DisplayAttribute>().GetName();

            return "";
        }

        public static string GetContatUsStatusColor(this ContactUsStatus status)
        {
            switch (status)
            {
                case ContactUsStatus.UnRead:
                    return "orange";
                    break;
                case ContactUsStatus.NotAnswered:
                    return "yellow";
                    break;
                case ContactUsStatus.Answered:
                    return "palegreen";
                    break;

                default:
                    return "";
                    break;
            }
        }

        public static string GetTicketStatusColor(this TicketStatus status)
        {
            switch (status)
            {
                case TicketStatus.Closed:
                    return "orange";
                    break;
                case TicketStatus.NotAnswered:
                    return "yellow";
                    break;
                case TicketStatus.Answered:
                    return "palegreen";
                    break;

                default:
                    return "";
                    break;
            }
        }

    }
}
