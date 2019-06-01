using SAW.DAL.Context;
using System;
using System.Collections.Generic;

namespace SAW.Model.ViewModel
{
    public class VMNote
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        public static VMNote Parse(Note model)
        {
            VMNote result = new VMNote
            {
                NoteId = model.NoteId,
                Title = model.Title,
                Body = model.Body,
                CreateDate = model.CreateDate,
                UpdateDate = model.UpdateDate,
                IsActive = model.IsActive,
                UserId = model.UserId
            };

            return result;
        }
        public static List<VMNote> Parse(List<Note> models)
        {
            List<VMNote> results = new List<VMNote>();
            foreach (var model in models)
                results.Add(VMNote.Parse(model));

            return results;
        }
    }
}
