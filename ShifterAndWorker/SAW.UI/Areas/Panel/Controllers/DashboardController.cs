using SAW.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Areas.Panel.Controllers
{
    [CustomAuthorization]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}