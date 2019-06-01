using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Filters
{
    public class CustomAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["LoggedUser"] == null)
            {
                //HttpContext.Current.Session["requestUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller", "Account"},
                    {"Action", "Login"}
                });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}