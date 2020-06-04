using System.Web.Mvc;

namespace MaxsPetCare.Controllers
{
    public class UserAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["UserID"] == null)
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }

    public class AdminAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["AdminID"] == null)
            {
                context.Result = new RedirectResult("/Account/AdminLogin");
            }
        }
    }

    public class IsAuthorized : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["AdminID"] != null)
            {
                context.Result = new RedirectResult("/Admin");
            }
            else if (context.HttpContext.Session["UserID"] != null)
            {
                context.Result = new RedirectResult("/Home");
            }
        }
    }
}