using IDE_CASHCOUNT.Common.Security;
using IDE_CASHCOUNT.Models.Entities;
using System;
using System.Linq;
using System.Web.Http;

namespace IDE_CASHCOUNT.Controllers.WebApi
{
    [RoutePrefix("api/ApiSecurity")]
    public class ApiSecurityController : ApiController
    {
        IDEContext dbLogin = new IDEContext();
        [HttpGet]
        [Route("login")]
        public IHttpActionResult login(string name, string password)
        {
            var account = dbLogin.IDE_USERS.Where(u => u.USER_NAME == name &&  u.ROLE=="Api").FirstOrDefault();
            if (account != null)
            {
                if (Hashing.ValidatePassword(password, account.PASSWORD))
                {
                    account.LAST_LOGIN_TIME = DateTime.Now;
                    account.API_KEY = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                    dbLogin.SaveChanges();

                    return Ok(account.API_KEY);

                }
                else
                {
                    return BadRequest("authorization_faild");
                }
            }

            return BadRequest("authorization_faild");
        }

        [HttpGet]
        [Route("keycheck")]
        public IHttpActionResult apicheck(string apiKey)
        {

            var account = dbLogin.IDE_USERS.Where(u => u.API_KEY == apiKey).FirstOrDefault();
            if (account != null)
            {
                return Ok("apiKey_ok");
            }

            return BadRequest("apiKey_faild");

        }
    }
}
