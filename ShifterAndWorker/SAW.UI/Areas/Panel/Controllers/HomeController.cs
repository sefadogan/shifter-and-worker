using SAW.BLL.UnitOfWork;
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
    public class HomeController : Controller
    {
        //readonly ShifterAndWorkerEntities _context;
        //readonly UnitOfWork _uow;

        //public HomeController()
        //{
        //    _context = new ShifterAndWorkerEntities();
        //    _uow = new UnitOfWork(_context);
        //}

        private IUnitOfWork _uow;

        public HomeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Partials
        public PartialViewResult GetSideBarPartial()
        {
            User user = Session["LoggedUser"] as User;

            int numberOfMails = _uow.MailManager.ListAll(x => x.IsActive && x.IsRead == false && x.IsPermanentDelete == false && x.ReceiverId == user.UserId).Count();

            VMSideBarPartial vmSideBarPartial = new VMSideBarPartial
            {
                User = user,
                NumberOfMails = numberOfMails
            };

            return PartialView("_SideBarPartial", vmSideBarPartial);
        }
        public PartialViewResult GetTopBarPartial()
        {
            User loggedUser = Session["LoggedUser"] as User;

            #region For announce dropdown list

            List<UserAnnouncement> userAnnouncements = _uow.UserAnnouncementManager.ListAll(x => x.UserId == loggedUser.UserId)
                .OrderByDescending(x => x.Announcement.CreateDate).ThenByDescending(x => x.IsRead).ToList();

            #endregion

            #region For mail dropdown list

            List<Mail> mails = _uow.MailManager.ListAll(x => x.IsActive && x.ReceiverId == loggedUser.UserId)
                .OrderByDescending(x => x.CreateDate).ThenByDescending(x => x.IsRead).ToList();

            #endregion

            #region Fills _TopBarViewModel

            VMTopBarPartial vmTopBarPartial = VMTopBarPartial.Synchronize(loggedUser, mails, userAnnouncements); // Parse etmeyi yapamadım o yüzden direkt alıp model view propertylerine eşitledim.

            #endregion

            return PartialView("_TopBarPartial", vmTopBarPartial);
        }
        public PartialViewResult GetQuickViewSideBarPartial()
        {
            User loggedUser = Session["LoggedUser"] as User;

            List<Note> notes = _uow.NoteManager.ListAll(x => x.IsActive && x.UserId == loggedUser.UserId).OrderByDescending(x => x.CreateDate).ToList();

            ViewBag.Notes = VMNote.Parse(notes);

            return PartialView("_QuickViewSideBarPartial");
        }
        public PartialViewResult GetSearchPartial()
        {
            return PartialView("_SearchPartial");
        }
        #endregion
    }
}