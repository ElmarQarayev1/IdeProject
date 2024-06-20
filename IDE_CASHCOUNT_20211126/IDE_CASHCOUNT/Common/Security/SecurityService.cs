using IDE_CASHCOUNT.Models.Entities;
using System;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Security;

namespace IDE_CASHCOUNT.Common.Security
{
    public class SecurityService
    {
        IDEContext db = new IDEContext();
        public IDE_USERS UserAuth(string username, string password)
        {
            var user = db.IDE_USERS.Where(u => u.USER_NAME == username).FirstOrDefault();
            if (user != null)
            {
                if (Hashing.ValidatePassword(password, user.PASSWORD))
                {
                    user.LAST_LOGIN_TIME = DateTime.Now;
                    user.LAST_ACTITY_TIME = DateTime.Now;
                    user.ACTIVE = true;
                    db.SaveChanges();
                    return user;
                }

            }
            return null;
        }

        public IDE_USERS UserCheck(string username, string password)
        {
            var user = db.IDE_USERS.Where(u => u.USER_NAME == username).FirstOrDefault();
            if (user != null)
            {
                if (Hashing.ValidatePassword(password, user.PASSWORD))
                {
                    
                    return user;
                }

            }
            return null;
        }

        public IDE_USERS GetUserByName(string userName)
        {

            return db.IDE_USERS.Where(u => u.USER_NAME == userName).SingleOrDefault();
        }

        public FormsAuthenticationTicket GetUserAuthTicket()
        {
            var a = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return null;
            var cookieValue = authCookie.Value;

            if (String.IsNullOrWhiteSpace(cookieValue)) return null;
            var ticket = FormsAuthentication.Decrypt(cookieValue);
            return ticket;
        }

        public int GetOnlineUserCount(int timeout,string userName)
        {
            int onlineUserCount = 0;
            var start = DateTime.Now;
            var users = db.IDE_USERS.ToList();
            foreach (var item in users)
            {
                if ((start - Convert.ToDateTime(item.LAST_ACTITY_TIME)).TotalMinutes <= timeout && item.ACTIVE==true && item.USER_NAME!=userName)
                {
                    onlineUserCount++;
                }
            }

            return onlineUserCount;
        }
    }
}