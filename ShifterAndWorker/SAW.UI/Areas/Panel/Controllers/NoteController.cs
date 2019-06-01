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
    public class NoteController : Controller
    {
        //readonly ShifterAndWorkerEntities _context;
        //readonly UnitOfWork _uow;

        //public NoteController()
        //{
        //    _context = new ShifterAndWorkerEntities();
        //    _uow = new UnitOfWork(_context);
        //}

        private IUnitOfWork _uow;

        public NoteController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult List()
        {
            User loggedUser = Session["LoggedUser"] as User;

            var notes = _uow.NoteManager.ListAll(x => x.IsActive && x.UserId == loggedUser.UserId).OrderByDescending(x => x.CreateDate).ToList();
            List<VMNote> vmNote = VMNote.Parse(notes);

            return View(vmNote);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            User loggedUser = Session["LoggedUser"] as User;

            Session["SelectedNoteId"] = id; // Created to capture in post method when data is sent.

            Note note = _uow.NoteManager.Get(x => x.NoteId == id && x.UserId == loggedUser.UserId);
            if (note == null)
            {
                TempData["ProcessResult"] = "There was an error while viewing the note.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            VMNote vmNote = VMNote.Parse(note);
            return View(vmNote);
        }
        public JsonResult Delete(int id)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var result = _uow.NoteManager.Delete(id);
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

        [HttpPost]
        public ActionResult Create(Note model)
        {
            User loggedUser = Session["LoggedUser"] as User;

            model.UserId = loggedUser.UserId;
            model.CreateDate = DateTime.Now;
            model.IsActive = true;

            _uow.NoteManager.Add(model);
            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An unexpected error occurred while creating the note.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ProcessResult"] = "Note created successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(VMNote model)
        {
            User loggedUser = Session["LoggedUser"] as User;
            int selectedNoteId = Convert.ToInt32(Session["SelectedNoteId"]);

            Note note = _uow.NoteManager.Get(x => x.NoteId == selectedNoteId && x.UserId == loggedUser.UserId);
            note.Title = model.Title;
            note.Body = model.Body;
            note.UpdateDate = DateTime.Now;

            var result = _uow.NoteManager.Update(note);

            Session.Remove("SelectedNoteId");
            if (!_uow.SaveChanges())
            {
                TempData["ProcessResult"] = "An enexpected error occurred while updating the note.";
                TempData["AlertType"] = "danger";
            }

            TempData["ProcessResult"] = "Note updated successfully.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }
    }
}