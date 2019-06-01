using SAW.BLL.UnitOfWork;
using SAW.DAL.Context;
using SAW.Model.ViewModel;
using SAW.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Areas.Panel.Controllers
{
    [CustomAuthorization]
    public class AnnouncementController : Controller
    {
        //readonly ShifterAndWorkerEntities _context;
        //readonly UnitOfWork _uow;

        //public AnnouncementController()
        //{
        //    _context = new ShifterAndWorkerEntities();
        //    _uow = new UnitOfWork(_context);
        //}

        private IUnitOfWork _uow;

        public AnnouncementController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult List(int page =1)
        {
            User loggedUser = Session["LoggedUser"] as User;

            #region Fill announcements for manager or worker.

            VMAnnouncementList vmAnnouncementList = new VMAnnouncementList();
            if (loggedUser.Role.Name == "Manager")
            {
                List<Announcement> announcements = _uow.AnnouncementManager
                    .ListAll(x => x.IsActive && x.CreatorId == loggedUser.UserId)
                    .OrderByDescending(x => x.CreateDate).ToList();

                vmAnnouncementList = VMAnnouncementList.Parse(announcements, loggedUser);
            }

            else // It means logged user is worker. If so fill announcements for worker.
            {
                List<UserAnnouncement> userAnnouncements = _uow.UserAnnouncementManager
                    .ListAll(x => x.Announcement.IsActive && x.UserId == loggedUser.UserId)
                    .OrderByDescending(x => x.Announcement.CreateDate).ToList();

                vmAnnouncementList = VMAnnouncementList.Parse(userAnnouncements, loggedUser);
            }

            #endregion

            #region Control of announcements count for alert.

            if (vmAnnouncementList.Announcements.Count() == 0)
            {
                TempData["ProcessResult"] = "There is no announcement to display.";
                TempData["AlertType"] = "info";
                return View(vmAnnouncementList);
            }

            #endregion

            return View(vmAnnouncementList);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedAnnouncementId"] = id; // Created to capture in post method when data is sent.

            Announcement announcement = _uow.AnnouncementManager.Get(x => x.AnnouncementId == id);
            if (announcement == null)
            {
                TempData["ProcessResult"] = "There was an error while viewing the announcement.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            VMAnnouncementEdit vmAnnouncementEdit = VMAnnouncementEdit.Parse(announcement);
            return View(vmAnnouncementEdit);
        }
        
        public JsonResult Delete(int id)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var result = _uow.AnnouncementManager.Delete(id);
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
        public ActionResult Detail(int id)
        {
            var announcement = _uow.AnnouncementManager.Get(x => x.AnnouncementId == id);
            announcement.UserAnnouncements.FirstOrDefault().IsRead = true;
            if (!_uow.SaveChanges())
                throw new Exception("An unexpected error has occured.");

            var creator = _uow.UserManager.Get(x => x.UserId == announcement.CreatorId);

            VMAnnouncementDetail vmAnnouncementDetail = VMAnnouncementDetail.Parse(announcement, creator);
            return View(vmAnnouncementDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Announcement model)
        {
            User loggedUser = Session["LoggedUser"] as User;

            #region Creating announce

            model.CreateDate = DateTime.Now;
            model.CreatorId = loggedUser.UserId;
            model.IsActive = true;

            _uow.AnnouncementManager.Add(model);

            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occurred while creating the announcement.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            #endregion

            #region Creating announcements for all users in the store

            List<User> users = _uow.UserManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId);
            foreach (var user in users)
            {
                UserAnnouncement userAnnouncement = new UserAnnouncement
                {
                    AnnouncementId = model.AnnouncementId,
                    UserId = user.UserId,
                    IsRead = false
                };

                _uow.UserAnnouncementManager.Add(userAnnouncement);
            }

            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "Announcement created and successfully sent to all users.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ProcessResult"] = "An unexpected error occurred while sending the announcement to all users.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            #endregion

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VMAnnouncementEdit model)
        {
            int selectedAnnouncementId = Convert.ToInt32(Session["SelectedAnnouncementId"]);

            Announcement announcement = _uow.AnnouncementManager.Get(x => x.AnnouncementId == selectedAnnouncementId);
            announcement.Title = model.Title;
            announcement.Body = model.Body;
            announcement.IsImportant = model.IsImportant;

            var result = _uow.AnnouncementManager.Update(announcement);

            Session.Remove("SelectedAnnouncementId");
            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "Announcement updated successfully.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ProcessResult"] = "An enexpected error occurred while updating the announcement.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
    }
}