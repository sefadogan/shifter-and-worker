using PagedList;
using SAW.BLL.UnitOfWork;
using SAW.Core;
using SAW.DAL.Context;
using SAW.Model.ViewModel;
using SAW.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Areas.Panel.Controllers
{
    [CustomAuthorization]
    public class UserController : Controller
    {
        private IUnitOfWork _uow;

        public UserController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult List(int page = 1)
        {
            User loggedUser = Session["LoggedUser"] as User;

            var userList = _uow.UserManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderByDescending(x => x.CreateDate).ToPagedList(page, 10);
            return View(userList); // TODO : ViewModel.
        }
        public ActionResult Create()
        {
            User loggedUser = Session["LoggedUser"] as User;

            #region Fills VMUserCreate

            List<User> supervisors = _uow.UserManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderBy(x => x.FirstName).ToList();
            List<Department> departments = _uow.DepartmentManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderBy(x => x.Name).ToList();
            List<Role> roles = _uow.RoleManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderBy(x => x.Name).ToList();
            List<WorkingType> workingTypes = _uow.WorkingTypeManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderBy(x => x.Name).ToList();

            VMUserCreate vmUserCreate = VMUserCreate.Parse(supervisors, departments, roles, workingTypes);

            #endregion

            return View(vmUserCreate);
        }
        public ActionResult Edit(int id)
        {
            User loggedUser = Session["LoggedUser"] as User;
            Session["SelectedUserId"] = id; // Created to capture in post method when data is sent.

            User user = _uow.UserManager.Get(x => x.UserId == id);

            if (user == null)
            {
                TempData["ProcessResult"] = "There was an error while viewing the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            List<User> supervisors = _uow.UserManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderBy(x => x.FirstName).ToList();
            List<Department> departments = _uow.DepartmentManager.ListAll(x => x.StoreId == user.StoreId && x.IsActive).OrderBy(x => x.Name).ToList();
            List<Role> roles = _uow.RoleManager.ListAll(x => x.StoreId == user.StoreId && x.IsActive).OrderBy(x => x.Name).ToList();
            List<WorkingType> workingTypes = _uow.WorkingTypeManager.ListAll(x => x.StoreId == user.StoreId && x.IsActive).OrderBy(x => x.Name).ToList();

            VMUserEdit vmUserEdit = VMUserEdit.Parse(user, supervisors, departments, roles, workingTypes);

            return View(vmUserEdit);
        }
        public new ActionResult Profile(int id)
        {
            UserProfileViewModel vmUserProfile = new UserProfileViewModel();
            List<Post> postList = _uow.PostManager.ListAll(x => x.UserId == id);
            if (postList.Count() == 0)
            {
                TempData["ProcessResult"] = "There is no post to display.";
                TempData["AlertType"] = "info";
                return RedirectToAction("List");
            }

            vmUserProfile.PostList = postList;

            User user = _uow.UserManager.Get(x => x.UserId == id);
            vmUserProfile.User = user ?? throw new HttpException(404, "User is not exist.");

            return View(vmUserProfile);
        }
        public ActionResult PersonalProfile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMUserCreate model)
        //[Bind(Include = "FirstName, LastName, Phone, DateOfBirth, Email, About, ImageUrl, DepartmentId, RoleId, WorkingTypeId")]
        {
            User loggedUser = Session["LoggedUser"] as User;

            AppReturn result = _uow.UserManager.Add(
                new User
                {
                    About = model.About,
                    CreateDate = DateTime.Now,
                    DateOfBirth = model.DateOfBirth,
                    DepartmentId = model.DepartmentId,
                    Email = model.Email,
                    IsActive = true,
                    ImageUrl = model.ImageUrl,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Password = "123123qwe", // Default Password.
                    RoleId = model.RoleId,
                    StoreId = loggedUser.StoreId,
                    SupervisorId = model.SupervisorId,
                    WorkingTypeId = model.WorkingTypeId
                });

            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occured while creating user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ProcessResult"] = "User created successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            User loggedUser = Session["LoggedUser"] as User;
            int selectedUserId = (int)Session["SelectedUserId"];

            #region Fills model to update.

            User user = _uow.UserManager.Get(x => x.UserId == selectedUserId);

            user.About = model.About;
            user.DateOfBirth = model.DateOfBirth;
            user.DepartmentId = model.DepartmentId;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Phone = model.Phone;
            user.RoleId = model.RoleId;
            user.WorkingTypeId = model.WorkingTypeId;
            user.SupervisorId = loggedUser.UserId;
            user.ImageUrl = model.ImageUrl;

            #endregion

            var result = _uow.UserManager.Update(user);

            Session.Remove("SelectedUserId");
            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "User update process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ProcessResult"] = "There was an error while updating the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        public ActionResult Delete(int id)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            #region Control whether the user's manager

            List<User> users = _uow.UserManager.ListAll(x => x.UserId != id);
            foreach (var user in users)
            {
                if (user.SupervisorId == id)
                {
                    jsonResult.Data = new
                    {
                        success = false,
                        message = "Could not delete! The user you want to delete, another user's manager."
                    };
                    return jsonResult;
                }
            }

            #endregion

            #region Delete operation

            var result = _uow.UserManager.Delete(id);
            if (!_uow.SaveChanges())
            {
                jsonResult.Data = new
                {
                    success = result,
                    message = result.Message,
                };
                return jsonResult;
            }

            jsonResult.Data = new
            {
                success = result,
                message = result.Message,
            };
            return jsonResult;

            #endregion
        }
    }
}