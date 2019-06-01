using FluentValidation;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.ValidationRules.FluentValidation
{
    public class NoteValidator : AbstractValidator<Note>
    {
        public NoteValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(100);
            RuleFor(x => x.Title).MinimumLength(2);

            RuleFor(x => x.Body).NotEmpty();
            RuleFor(x => x.Body).MinimumLength(2);
        }
    }
}
