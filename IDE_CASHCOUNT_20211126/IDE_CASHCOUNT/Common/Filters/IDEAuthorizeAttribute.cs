using IDE_CASHCOUNT.Common.Security;
using IDE_CASHCOUNT.Models.Entities;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDE_CASHCOUNT.Common.Filters
{
    public class IDEAuthorizeAttribute : AuthorizeAttribute
    {
        SecurityService service = new SecurityService();
        IDEContext db = new IDEContext();
        private readonly string[] allowedroles;

        public IDEAuthorizeAttribute(string roles)
        {
            this.allowedroles = roles.Split(',');
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;

            if (service.GetUserAuthTicket() != null)
            {
                string userName = service.GetUserAuthTicket().Name;
                var user = db.IDE_USERS.Where(x => x.USER_NAME == userName && x.STATUS == true).FirstOrDefault();
                if (user != null && allowedroles.Contains(user.ROLE))
                {
                    user.LAST_ACTITY_TIME = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
            }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                            { "controller", "Security" }, { "action", "Login" }
               });
        }




        //SecurityService service = new SecurityService();
        //IDEContext db = new IDEContext();
        //private readonly string[] allowedroles;

        //public IDEAuthorizeAttribute(params string[] roles)
        //{
        //    this.allowedroles = roles;
        //}
        //protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    bool authorize = false;
        //    foreach (var role in allowedroles)
        //    {
        //        var user = db.IDE_USERS.Where(x => x.USER_NAME == service.GetUserAuthTicket().Name && x.ROLE==role && x.STATUS==true);
        //        if (user.Count() > 0)
        //        {
        //            authorize = true;
        //        }
        //    }
        //    return authorize;
        //}

        //public void OnAuthorization(AuthorizationContext filterContext)
        //{

        //    var user = filterContext.HttpContext.User;
        //    if (user == null || !user.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = new HttpUnauthorizedResult();
        //    }

        //}
    }

    //public class IDAuthorizeAttribute : FilterAttribute
    //{
    //    private string[] allowedUsers = new string[] { };
    //    private string[] allowedRoles = new string[] { };

    //    public IDAuthorizeAttribute()
    //    { }

    //    public IDAuthorizeAttribute(string Roles)
    //    {
    //        allowedRoles = Roles.Split(',');
    //    }

    //    //protected override bool AuthorizeCore(HttpContextBase httpContext)
    //    //{
    //    //    if (!String.IsNullOrEmpty(base.Users))
    //    //    {
    //    //        allowedUsers = base.Users.Split(new char[] { ',' });
    //    //        for (int i = 0; i < allowedUsers.Length; i++)
    //    //        {
    //    //            allowedUsers[i] = allowedUsers[i].Trim();
    //    //        }
    //    //    }
    //    //    if (!String.IsNullOrEmpty(base.Roles))
    //    //    {
    //    //        allowedRoles = base.Roles.Split(new char[] { ',' });
    //    //        for (int i = 0; i < allowedRoles.Length; i++)
    //    //        {
    //    //            allowedRoles[i] = allowedRoles[i].Trim();
    //    //        }
    //    //    }

    //    //    return httpContext.Request.IsAuthenticated &&
    //    //         User(httpContext) && Role(httpContext);
    //    //}

    //    //private bool User(HttpContextBase httpContext)
    //    //{
    //    //    if (allowedUsers.Length > 0)
    //    //    {
    //    //        return allowedUsers.Contains(httpContext.User.Identity.Name);
    //    //    }
    //    //    return true;
    //    //}

    //    private bool Role(HttpContextBase httpContext)
    //    {
    //        if (allowedRoles.Length > 0)
    //        {
    //            for (int i = 0; i < allowedRoles.Length; i++)
    //            {
    //                if (httpContext.User.IsInRole(allowedRoles[i]))
    //                    return true;
    //            }
    //            return false;
    //        }
    //        return true;
    //    }


    //    public void OnAuthentication(AuthenticationContext filterContext)
    //    {
    //        var user = filterContext.HttpContext.User;
    //        if (user == null || !user.Identity.IsAuthenticated)
    //        {
    //            filterContext.Result = new HttpUnauthorizedResult();
    //        }
    //    }

    //    public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
    //    {
    //        var user = filterContext.HttpContext.User;
    //        if (user == null || !user.Identity.IsAuthenticated)
    //        {
    //            filterContext.Result = new RedirectToRouteResult(
    //                new System.Web.Routing.RouteValueDictionary {
    //                { "controller", "Security" }, { "action", "Login" }
    //               });
    //        }
    //    }
}
