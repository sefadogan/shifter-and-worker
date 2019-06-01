using SAW.BLL.UnitOfWork;
using SAW.Core;
using SAW.DAL.Context;
using SAW.Model.ViewModel;
using SAW.UI.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Areas.Panel.Controllers
{
    [CustomAuthorization]
    public class ShiftController : Controller
    {
        private IUnitOfWork _uow;
        private ShifterAndWorkerEntities _context;

        public ShiftController(IUnitOfWork uow)
        {
            _uow = uow;
            _context = new ShifterAndWorkerEntities();
        }

        public ActionResult Create()
        {
            User loggedUser = Session["LoggedUser"] as User;

            var users = _uow.UserManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderBy(x => x.FirstName).ToList();
            var departments = _uow.DepartmentManager.ListAll(x => x.IsActive && x.StoreId == loggedUser.StoreId).OrderBy(x => x.Name).ToList();
            var breaks = _uow.BreakManager.ListAll(x => x.IsActive).OrderByDescending(x => x.StartTime).ToList();

            ViewData["Users"] = VMUser.Parse(users);
            ViewData["Departments"] = VMDepartment.Parse(departments);
            ViewData["Breaks"] = VMBreak.Parse(breaks);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMShift model, int[] breakIds)
        {
            #region Logged user

            User loggedUser = Session["LoggedUser"] as User;

            #endregion

            #region Begin transaction

            using (DbContextTransaction dbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    #region Creation shift

                    Shift shift = new Shift
                    {
                        Date = model.Date,
                        StartTime = model.StartTime,
                        EndTime = model.EndTime,
                        UserId = model.UserId,
                        StoreId = loggedUser.StoreId,
                        DepartmentId = model.DepartmentId,
                        CreateDate = DateTime.Now,
                        IsActive = true,
                        IsNotify = false
                    };
                    _uow.ShiftManager.Add(shift);

                    #endregion

                    #region Creation related table between shift and break 

                    for (int i = 0; i < breakIds.Length; i++)
                    {
                        ShiftBreak shiftBreak = new ShiftBreak
                        {
                            BreakId = breakIds[i],
                            ShiftId = shift.ShiftId
                        };
                        _uow.ShiftBreakManager.Add(shiftBreak);
                    }

                    #endregion

                    #region Try to save changes and commit transaction

                    _uow.SaveChanges();
                    dbTran.Commit();

                    #endregion
                }
                catch (Exception e)
                {
                    dbTran.Rollback();

                    TempData["ProcessResult"] = "An unexpected error occured while creating shift.";
                    TempData["AlertType"] = "danger";
                    return RedirectToAction("Create");

                }
            }

            #endregion

            #region Return success result message with TempData

            TempData["ProcessResult"] = "Shift created successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("Create");

            #endregion
        }
    }
}