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
    public class DepartmentController : Controller
    {
        //#region Fields
        //readonly ShifterAndWorkerEntities _context;
        //readonly UnitOfWork _uow;
        //#endregion

        //#region Constructor
        //public DepartmentController()
        //{
        //    _context = new ShifterAndWorkerEntities();
        //    _uow = new UnitOfWork(_context);
        //}
        //#endregion

        private IUnitOfWork _uow;

        public DepartmentController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        #region Actions
        public ActionResult List()
        {
            User loggedUser = Session["LoggedUser"] as User;

            List<Department> departments = _uow.DepartmentManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderByDescending(x => x.CreateDate).ToList();

            VMDepartmentList vmDepartmentList = VMDepartmentList.Synchronize(departments);
            if (vmDepartmentList.Departments.Count() == 0)
            {
                TempData["ProcessResult"] = "There is no department to display.";
                TempData["AlertType"] = "info";
                return View(vmDepartmentList);
            }

            return View(vmDepartmentList);
        }
        public ActionResult Edit(int id)
        {
            User loggedUser = Session["LoggedUser"] as User;

            Session["SelectedDepartmentId"] = id; // Created to capture in post method when data is sent.

            Department department = _uow.DepartmentManager.Get(x => x.DepartmentId == id);
            if (department == null)
            {
                TempData["ProcessResult"] = "There was an error while viewing the department.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            List<Department> allDepartments = _uow.DepartmentManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId);

            VMDepartmentEdit vmDepartmentEdit = VMDepartmentEdit.Parse(department, allDepartments);
            return View(vmDepartmentEdit);
        }
        public ActionResult Create()
        {
            User loggedUser = Session["LoggedUser"] as User;

            List<Department> allDepartments = _uow.DepartmentManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId);

            VMDepartmentCreate vmDepartmentCreate = VMDepartmentCreate.Parse(new Department(), allDepartments);
            return View(vmDepartmentCreate);
        }
        public JsonResult Delete(int id)
        {
            User loggedUser = Session["loggedUser"] as User;
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            #region Check whether the employee is working under the department

            List<User> users = _uow.UserManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId);
            foreach (var user in users)
            {
                if (user.DepartmentId == id)
                {
                    jsonResult.Data = new
                    {
                        success = false,
                        message = "Couldn't delete! There are employees under the department you are trying to delete."
                    };
                    return jsonResult;
                }
            }

            #endregion

            #region Delete department

            var result = _uow.DepartmentManager.Delete(id);
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

            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VMDepartmentEdit model)
        {
            int selectedDepartmentId = Convert.ToInt32(Session["SelectedDepartmentId"]);

            Department department = _uow.DepartmentManager.Get(x => x.DepartmentId == selectedDepartmentId);
            department.Name = model.Name;
            department.MaxEmployees = model.MaxEmployees;
            department.MinEmployees = model.MinEmployees;

            if (department.ParentDepartmentId != model.ParentDepartmentId)
            {
                department.RootLevel = Convert.ToByte(_uow.DepartmentManager.BringParentRootValue(model.ParentDepartmentId) + 1);
                department.ParentDepartmentId = model.ParentDepartmentId;
            }

            var result = _uow.DepartmentManager.Update(department);

            Session.Remove("SelectedDepartmentId");
            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "Department updated successfully.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ProcessResult"] = "An enexpected error occurred while updating the department.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMDepartmentCreate model)
        {
            User loggedUser = Session["LoggedUser"] as User;

            Department department = new Department
            {
                Name = model.Name,
                MaxEmployees = model.MaxEmployees,
                MinEmployees = model.MinEmployees,
                CreateDate = DateTime.Now,
                IsActive = true,
                RootLevel = 1, // Parent departman seçilmişse zaten if bloğunda ona göre doldurma yapılacak, default hali bu.
                StoreId = loggedUser.StoreId
            };

            if (model.ParentDepartmentId != null)
            {
                department.RootLevel = Convert.ToByte(_uow.DepartmentManager.BringParentRootValue(model.ParentDepartmentId) + 1);
                department.ParentDepartmentId = model.ParentDepartmentId;
            }

            var result = _uow.DepartmentManager.Add(department);

            if (_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "Department created successfully.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ProcessResult"] = "An unexpected error occurred while creating the department.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        #endregion
    }
}