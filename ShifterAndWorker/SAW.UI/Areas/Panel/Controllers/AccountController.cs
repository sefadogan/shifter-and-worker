using SAW.BLL.UnitOfWork;
using SAW.Core;
using SAW.DAL.Context;
using SAW.Model.ViewModel;
using SAW.UI.Code;
using SAW.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Areas.Panel.Controllers
{
    public class AccountController : Controller
    {
        private IUnitOfWork _uow;

        public AccountController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Login()
        {
            if (Session["LoggedUser"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(new VMAccountLogin());
        }

        [CustomAuthorization]
        public ActionResult Logout()
        {
            Session.Remove("LoggedUser");
            return RedirectToAction("Login", "Account");
        }

        [CustomAuthorization]
        public ActionResult LockScreen()
        {
            Session["LockedUser"] = Session["LoggedUser"];
            ViewBag.UserFirstName = (Session["LockedUser"] as User).FirstName;

            Session.Remove("LoggedUser");
            return View();
        }

        [HttpPost]
        public ActionResult LockScreen(string password)
        {
            if (!(Session["LockedUser"] is User lockedUser))
                return View("Login");

            if (password != lockedUser.Password)
            {
                TempData["ProcessResult"] = "You entered password is not matching in our system.";
                TempData["AlertType"] = "danger";
                ViewBag.UserFirstName = lockedUser.FirstName;
                return View();
            }

            Session["LoggedUser"] = lockedUser;
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public JsonResult SendPassword(string email)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            #region User control

            User user = _uow.UserManager.Get(x => x.Email == email);

            if (user == null)
            {
                jsonResult.Data = new
                {
                    success = false,
                    message = "You are not registered in our system or have used a faulty email. Please check and try again."
                };
                return jsonResult;
            }

            #endregion

            #region Send email and return result

            AppReturn result = EmailTemplateManager.SendEmailForForgottenPassword(user);
            if (result.Success)
            {
                jsonResult.Data = new
                {
                    success = true,
                    message = result.Message
                };
                return jsonResult;
            }
            else
            {
                jsonResult.Data = new
                {
                    success = false,
                    message = result.Message
                };
                return jsonResult;
            }

            #endregion
        }

        [HttpPost]
        public ActionResult Login(VMAccountLogin model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = _uow.UserManager.Get(x => x.Email == model.Email);
            if (user == null)
            {
                TempData["ProcessResult"] = "There is no such user in our system.";
                TempData["AlertType"] = "warning";
                return View(model);
            }
            else if (user.Password != model.Password)
            {
                TempData["ProcessResult"] = "Your user information is incorrect. Please try again.";
                TempData["AlertType"] = "danger";
                return View(model);
            }

            user.LastLogin = DateTime.Now;
            _uow.UserManager.Update(user);
            _uow.SaveChanges();

            Session["LoggedUser"] = user;

            return RedirectToAction("Index", "Dashboard");
        }

        [CustomAuthorization]
        public ActionResult Settings(int id)
        {
            Session["SelectedUserId"] = id; // Created to capture in post method when data is sent.

            User user = _uow.UserManager.Get(x => x.UserId == id);
            VMAccountSettings vmAccountSettings = VMAccountSettings.Parse(user);

            return View(vmAccountSettings);
        }

        [HttpPost]
        [CustomAuthorization]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(VMAccountSettings model)
        {
            User loggedUser = Session["LoggedUser"] as User;
            int selectedUserId = (int)Session["SelectedUserId"];

            #region Fills model to update.

            User user = _uow.UserManager.Get(x => x.UserId == selectedUserId);

            user.About = model.About;
            user.DateOfBirth = model.DateOfBirth;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Phone = model.Phone;
            user.ImageUrl = model.ImageUrl;

            #endregion

            var result = _uow.UserManager.Update(user);
            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "Your user information has been successfully updated.";
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["ProcessResult"] = "There was an error while updating the user informations.";
                TempData["AlertType"] = "danger";
            }

            Session.Remove("SelectedUserId");
            return RedirectToAction("Settings");
        }
    }
}