using IDE_CASHCOUNT.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace IDE_CASHCOUNT.Common.Filters
{
    public class MyAutorizeAttribute:AuthorizeAttribute
    {
        private readonly string[] allowedRoles;

        public MyAutorizeAttribute(params string[] roles)
        {
            this.allowedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string a = httpContext.Application["SessionCount"].ToString();
            IPrincipal user = httpContext.User;
            SecurityService service = new SecurityService();
            bool autorize = false;
            if (user.Identity.IsAuthenticated)
            {
                if (allowedRoles.Count() > 0)
                {
                    var dbUser = service.GetUserByName(user.Identity.Name);
                    if (dbUser == null) { return autorize; }
                    foreach (string role in allowedRoles)
                    {
                        if (dbUser.ROLE.Equals(role)) { autorize = true; }
                    }
                }
                else return autorize = true;
            }
           
            return autorize;
        }

        //public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        //{
        //    var user = filterContext.HttpContext.User;
        //    if (user == null || !user.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(
        //            new System.Web.Routing.RouteValueDictionary {
        //            { "controller", "Security" }, { "action", "Login" }
        //           });
        //    }
        //}

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Security" }, { "action", "Login" }
                   });
                //filterContext.Result = new ViewResult() { ViewName = "UnAutorize" };
            }
            else {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}