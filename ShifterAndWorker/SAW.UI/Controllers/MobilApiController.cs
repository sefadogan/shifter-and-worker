using SAW.BLL.UnitOfWork;
using SAW.UI.Models;
using System.Web.Mvc;

namespace SAW.UI.Controllers
{
    public class MobilApiController : Controller
    {
        private IUnitOfWork _uow;
        private const string token = "saw-api-xd-bx-ipa-was";


        public MobilApiController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public JsonResult UserLogin(string email, string password, string accessToken) // Test
        {
            if (token != accessToken)
                return MobilApiReturn.Unauthorized();

            var user = _uow.UserManager.Get(x => x.Email == email);
            if (user == null)
                return MobilApiReturn.NotFound("There is no user with this email in our system.");

            if (user.Password != password)
                return MobilApiReturn.InvalidOperation("Your password is incorrect.");

            return MobilApiReturn.Successful();
        }
    }
}