using SAW.BLL.UnitOfWork;
using SAW.Core;
using SAW.DAL.Context;
using SAW.Model.ViewModel;
using SAW.Model.ViewModel.Partials;
using SAW.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Areas.Panel.Controllers
{
    [CustomAuthorization]
    public class MailController : Controller
    {
        //readonly ShifterAndWorkerEntities _context;
        //readonly UnitOfWork _uow;

        //public MailController()
        //{
        //    _context = new ShifterAndWorkerEntities();
        //    _uow = new UnitOfWork(_context);
        //}

        private IUnitOfWork _uow;

        public MailController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Inbox()
        {
            User user = Session["LoggedUser"] as User;

            VMMailInbox vmMailInbox = new VMMailInbox
            {
                Mails = _uow.MailManager.ListAll(x => x.IsActive && x.IsPermanentDelete == false && x.ReceiverId == user.UserId).OrderByDescending(x => x.CreateDate).ToList()
            };

            if (vmMailInbox.Mails.Count() == 0)
            {
                TempData["ProcessResult"] = "There is no mail to display.";
                TempData["AlertType"] = "info";
                return View(vmMailInbox);
            }

            return View(vmMailInbox);
        }
        
        public ActionResult Send()
        {
            return View();
        }
        public ActionResult Trash()
        {
            User user = Session["LoggedUser"] as User;

            VMMailDeleted vmMailDeleted = new VMMailDeleted
            {
                Mails = _uow.MailManager.ListAll(x => !x.IsActive && !x.IsPermanentDelete && x.ReceiverId == user.UserId).OrderByDescending(x => x.CreateDate).ToList()
            };

            if (vmMailDeleted.Mails.Count() == 0)
            {
                TempData["ProcessResult"] = "There is no deleted mail to display.";
                TempData["AlertType"] = "info";
                return View(vmMailDeleted);
            }

            return View(vmMailDeleted);
        }
        
        public JsonResult Delete(int id)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var result = _uow.MailManager.Delete(id);
            if (_uow.SaveChanges())
            {
                jsonResult.Data = new
                {
                    success = result,
                    message = result.Message,
                };
                return jsonResult;
            }
            else
            {
                jsonResult.Data = new
                {
                    success = result.Success,
                    message = result.Message,
                };
                return jsonResult;
            }
        }
        public JsonResult PermanentDelete(int id)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            var result = _uow.MailManager.PermanentDelete(id);
            if (_uow.SaveChanges())
            {
                jsonResult.Data = new
                {
                    success = result,
                    message = result.Message
                };
                return jsonResult;
            }
            else
            {
                jsonResult.Data = new
                {
                    success = result.Success,
                    message = result.Message
                };
                return jsonResult;
            }
        }
        public ActionResult Detail(int id)
        {
            // Huzurlarınızdaa, iyyyyğğrenç bir kod bloğu.

            User loggedUser = Session["LoggedUser"] as User;

            Mail detailedMail = _uow.MailManager.Get(x => x.MailId == id);
            if (detailedMail == null)
            {
                TempData["ProcessResult"] = "An error has occured when bringing mail.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Inbox");
            }

            List<VMMailDetail> hierarchicalMails = new List<VMMailDetail>();
            List<Mail> allMails = _uow.MailManager.ListAll(x => x.IsActive && (x.SenderId == loggedUser.UserId || x.ReceiverId == loggedUser.UserId));

            var isDone = false;
            while (!isDone)
            {
                if (detailedMail.ParentMailId == null)
                    isDone = true;
                else
                    detailedMail = _uow.MailManager.BringById(x => x.MailId == detailedMail.ParentMailId);

                if (!detailedMail.IsRead)
                {
                    detailedMail.IsRead = true;
                    detailedMail.ReadDate = DateTime.Now;
                    _uow.MailManager.Update(detailedMail);
                    _uow.SaveChanges();
                }
            }

            VMMailDetail parentMail = VMMailDetail.Parse(detailedMail, detailedMail.User);

            FillChildMail(parentMail, detailedMail.MailId, allMails);
            hierarchicalMails.Add(parentMail);

            return View(hierarchicalMails);
        }
        public ActionResult SendAnswerEmail(int mailId, string body)
        {
            User loggedUser = Session["LoggedUser"] as User;

            Mail mail = _uow.MailManager.Get(x => x.MailId == mailId);

            Mail newMail;
            if (mail.SenderId == loggedUser.UserId) // En son cevap veren giriş yapmış olan kullanıcı ise (kendi kendine cevaplamasın diye).
            {
                newMail = new Mail
                {
                    Subject = mail.Subject,
                    Body = body,
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    IsActive = true,
                    IsPermanentDelete = false,
                    SenderId = loggedUser.UserId,
                    ReceiverId = mail.ReceiverId,
                    ParentMailId = mail.MailId
                };
            }
            else // En son cevap veren karşı taraf ise.
            {
                newMail = new Mail
                {
                    Subject = mail.Subject,
                    Body = body,
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    IsActive = true,
                    IsPermanentDelete = false,
                    SenderId = loggedUser.UserId,
                    ReceiverId = mail.SenderId,
                    ParentMailId = mail.MailId
                };
            }

            AppReturn result = _uow.MailManager.Add(newMail);
            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "Mail sent successfuly.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Inbox");
            }
            else
            {
                TempData["ProcessResult"] = "An error has occured when sending mail.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Inbox");
            }
        }
        public ActionResult MoveDeletedMailToInbox(int id)
        {
            var mail = _uow.MailManager.Get(x => x.MailId == id);
            mail.IsActive = true;

            AppReturn result = _uow.MailManager.Update(mail);
            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "Mail moved to inbox.";
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["ProcessResult"] = "An error has occured when moving email to inbox.";
                TempData["AlertType"] = "danger";
            }

            return RedirectToAction("Trash");
        }
        public ActionResult MarkAsRead(int id)
        {
            var mail = _uow.MailManager.Get(x => x.MailId == id);

            if (mail == null)
            {
                TempData["ProcessResult"] = "We could not find the requested email.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Inbox");
            }
            else
            {
                if (mail.IsRead == false)
                {
                    mail.IsRead = true;
                    mail.ReadDate = DateTime.Now;
                    _uow.MailManager.Update(mail);
                    _uow.SaveChanges();
                    TempData["ProcessResult"] = "Mail marked as seen successfully.";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("Inbox");
                }
                else
                {
                    TempData["ProcessResult"] = "Mail already marked as seen.";
                    TempData["AlertType"] = "info";
                    return RedirectToAction("Inbox");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(string email, string subject, string body)
        {
            User loggedUser = Session["LoggedUser"] as User;

            var receiverUser = _uow.UserManager.Get(x => x.Email == email); // bringing for receiverId property when creating a new mail.
            if (receiverUser == null)
            {
                TempData["ProcessResult"] = "There is no such a user in the system.";
                TempData["AlertType"] = "danger";
                return View();
            }

            AppReturn result = _uow.MailManager.Add(new Mail
            {
                Subject = subject,
                Body = body,
                CreateDate = DateTime.Now,
                SenderId = loggedUser.UserId,
                IsActive = true,
                ReceiverId = receiverUser.UserId,
                IsPermanentDelete = false,
                IsRead = false
            });
            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "Mail sent successfully.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Inbox");
            }
            else
            {
                TempData["ProcessResult"] = "An error has occured when sending mail.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Inbox");
            }
        }

        public void FillChildMail(VMMailDetail hierarchicalMails, int parentMailId, List<Mail> allMails)
        {
            foreach (var item in allMails.Where(c => c.ParentMailId == parentMailId).ToList())
            {
                VMMailDetail childMail = VMMailDetail.Parse(item, item.User);

                hierarchicalMails.ChildMails.Add(childMail);

                FillChildMail(hierarchicalMails, childMail.MailId, allMails);
            }
        }

        #region Partial Views

        public PartialViewResult GetSideBarMailPartial()
        {
            User user = Session["LoggedUser"] as User;

            int numberOfMails = _uow.MailManager.ListAll(x => x.IsActive && x.IsRead == false && x.IsPermanentDelete == false && x.ReceiverId == user.UserId).Count();

            VMSideBarMailPartial vm = new VMSideBarMailPartial
            {
                NumberOfMails = numberOfMails
            };

            return PartialView("_SideBarMailPartial", vm);
        }

        #endregion
    }
}