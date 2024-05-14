using ShopCenter.Application.Convertors;
using ShopCenter.Application.Extensions;
using ShopCenter.Application.Generators;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Application.StaticTools;
using ShopCenter.Core.Senders;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.ContactUs;
using ShopCenter.Domain.ViewModels.ContactUs.Admin;
using ShopCenter.Domain.ViewModels.ContactUs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Application.Services.Implementation
{
    public class ContactUsServices : IContactUsServices
    {
            #region Ioc
            IContactUsRepository _contactUsRepository;
            private IViewRenderService _viewRenderService;
            public ContactUsServices(IContactUsRepository contactUsRepository, IViewRenderService viewRenderService)
            {
                _contactUsRepository = contactUsRepository;
                _viewRenderService = viewRenderService;
            }
            #endregion
            public AddContactUsReturns AddContactUs(ContactUsViewModel contactUs)
            {
                ContactUs ocontactUs = new ContactUs()
                {
                    Email = contactUs.Email,
                    Massage = contactUs.Massage,
                    FullName = contactUs.FullName,
                    Phone = contactUs.Phone,
                    Subject = contactUs.Subject,
                    Status = ContactUsStatus.UnRead
                };

                if (contactUs.AttachFile != null)
                {
                    if (FileTools.FileIsImage(contactUs.AttachFile) || FileTools.FileIsVideo(contactUs.AttachFile))
                    {

                        if (FileTools.FileIsImage(contactUs.AttachFile))
                        {
                            if (!FileTools.FileSizeIsValid(contactUs.AttachFile, FileTypeTitle.Image))
                            {
                                return AddContactUsReturns.FileSizeInvalid;
                            }
                            ocontactUs.FileType = FileTypeTitle.Image;
                        }

                        else//file is video

                        {
                            if (!FileTools.FileSizeIsValid(contactUs.AttachFile, FileTypeTitle.Video))
                            {
                                return AddContactUsReturns.FileSizeInvalid;
                            }
                            ocontactUs.FileType = FileTypeTitle.Video;

                        }

                        string fileName = "";
                        FileTools.AddToHard(contactUs.AttachFile, NameGenerator.GenerateUniqName(), SiteSections.ContactUs, out fileName);
                        ocontactUs.FileName = fileName;

                    }
                    else
                    {
                        return AddContactUsReturns.FileExensionInvalid;
                    }
                }

                AddContactUs(ocontactUs);
                return AddContactUsReturns.Done;

            }

            public ContactUsAdminSideViewModel GetContactUs(int contactUsId)
            {

                ContactUs ocontactUs = _contactUsRepository.GetContactUs(contactUsId);
                if (ocontactUs != null)
                {
                    ContactUsAdminSideViewModel ocontactUsViewModel = new ContactUsAdminSideViewModel()
                    {
                        Id = contactUsId,
                        Email = ocontactUs.Email,
                        FullName = ocontactUs.FullName,
                        Phone = ocontactUs.Phone,
                        Subject = ocontactUs.Subject,
                        Massage = ocontactUs.Massage,
                        FileName = ocontactUs.FileName,
                        FileType = ocontactUs.FileType,
                        Status = ocontactUs.Status,
                        CreateDate = ocontactUs.CreateDate,
                        Answer = ocontactUs.Answer,
                        AnswerDateTime = ocontactUs.AnswerDateTime
                    };

                    return ocontactUsViewModel;

                }
                else
                    return null;
            }

            public List<ContactUsListViewModel> GetContactUsList()
            {
                return _contactUsRepository.GetContactUsList();
            }

            public bool AnswerToContactUs(int contactUsId, string answer)
            {

                ContactUs ocontactUs = _contactUsRepository.GetContactUs(contactUsId);
                if (ocontactUs == null)
                {
                    return false;
                }
                ocontactUs.Answer = answer;
                ocontactUs.AnswerDateTime = DateTime.Now;
                ocontactUs.Status = ContactUsStatus.Answered;

                //send Email
                ContactUsAnswerEmailViewModel emailModel = new ContactUsAnswerEmailViewModel()
                {
                    Answer = answer,
                    CreatDateTime = ocontactUs.CreateDate,
                    Massage = ocontactUs.Massage,
                    Email = ocontactUs.Email,
                    Subject = ocontactUs.Subject.GetDisplayName(),

                };
                string body = _viewRenderService.RenderToStringAsync("Home/_EmailAnswerContactUs", emailModel);
                SendEmail.Send(ocontactUs.Email, "پاسخ پیام شما با موضوع " + ocontactUs.Subject.GetDisplayName(), body);
                UpdateContactUs(ocontactUs);

                return true;
            }

            public bool ChangeContactUsStatus(int contactUsId)
            {
                ContactUs ocontactUs = _contactUsRepository.GetContactUs(contactUsId);

                if (ocontactUs == null)
                {
                    return false;
                }
                switch (ocontactUs.Status)
                {
                    case ContactUsStatus.UnRead:
                        ocontactUs.Status = ContactUsStatus.NotAnswered;
                        break;

                    case ContactUsStatus.NotAnswered:
                        ocontactUs.Status = ContactUsStatus.Answered;
                        break;
                }

                UpdateContactUs(ocontactUs);
                return true;
            }


            #region Utility
            private void UpdateContactUs(ContactUs contactUs)
            {
                _contactUsRepository.Update(contactUs);
                Save();
            }
            private void AddContactUs(ContactUs contactUs)
            {
                _contactUsRepository.Add(contactUs);
                Save();
            }
            private void Save()
            {
                _contactUsRepository.Save();
            }



        #endregion
    }
    
}
