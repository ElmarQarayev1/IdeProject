using IDE_CASHCOUNT.Models.Entities;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace IDE_CASHCOUNT.Common.WebApi
{
    public class APIAutorizeAttribute : AuthorizeAttribute
    {
        IDEContext db = new IDEContext();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var actionRoles = Roles;
            var userName = HttpContext.Current.User.Identity.Name;
            var user = db.IDE_USERS.FirstOrDefault(a => a.USER_NAME == userName);
            if (user != null && actionRoles.Contains(user.ROLE))
            {
                //base.OnAuthorization(actionContext);
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

        }
    }
}