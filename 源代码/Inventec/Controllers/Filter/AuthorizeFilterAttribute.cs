using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventec.Controllers
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Session["User"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login/Logout",true);
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}