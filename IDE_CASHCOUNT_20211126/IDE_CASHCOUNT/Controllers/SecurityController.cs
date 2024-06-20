using IDE_CASHCOUNT.Common.Security;
using IDE_CASHCOUNT.Models.Entities;
using IDE_CASHCOUNT.Models.ViewModels;
using IDEProject.Common.Utils;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IDE_CASHCOUNT.Controllers
{
    public class SecurityController : Controller
    {
        SecurityService service = new SecurityService();

        // GET: Security

        IDEContext db = new IDEContext();
        public ActionResult Login()
        {
            //int userCount = Convert.ToInt32(HttpContext.Application["SessionCount"].ToString());
            var admin = db.IDE_USERS.Where(u => u.ROLE == "Admin").ToList();
            if (admin.Count == 0)
            {
                IDE_USERS user = new IDE_USERS();
                user.CREATE_DATETIME = DateTime.Now;
                user.UPDATE_DATETIME = DateTime.Now;
                user.NAME_SURNAME = "Admin";
                user.USER_NAME = "admin";
                user.ROLE = "Admin";
                user.STATUS = true;
                user.PASSWORD = Hashing.HasPassword("1234");
                db.IDE_USERS.Add(user);
                db.SaveChanges();
            }
            return View(new UserViewModel());
            // return View("~/Views/Security/Login.cshtml",new UserViewModel());
        }

        //https://www.red-gate.com/simple-talk/dotnet/asp-net/tracking-online-users/
        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            string licenseKey = EncryptDecrypt.Encrypt(HttpContext.Request.Url.Host + ":" + DeviceProparties.GetMacAddress(), "ide2018Soft");

            string url = ConfigurationManager.AppSettings["LinUrl"]+ "WebSerivce.asmx/AuthLicenseControl" + "?mac=" + DeviceProparties.GetMacAddress() + "&license=" + licenseKey.Replace("+", "%2B");
            var client = new WebClient();
            string content = "";
            try
            {
                content = client.DownloadString(url).ToString();
            }
            catch (Exception)
            {
                ViewBag.LoginMessage = "Xəta baş verdi.Lisenziya oxuna bilmedi!";
                return View(user);
            }

            if (content.Split(',')[0] == "insert_license")
            {
                ViewBag.LoginLicense = licenseKey + "," + DeviceProparties.GetMacAddress();
                return View(user);
            }

            if (content.Split(',')[0] == "has_license")
            {

                if (content.Split(',')[2].ToLower() == "false")
                {
                    ViewBag.LoginLicense = licenseKey + "," + DeviceProparties.GetMacAddress() + "," + HttpContext.Request.Url.Host;
                    return View(user);
                }

                Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
                //Edit
                if (objAppsettings != null)
                {
                    objAppsettings.Settings["FirmName"].Value = content.Split(',')[3];
                    objConfig.Save();
                }

                //int online= db.IDE_USERS.Where(u => u.ACTIVE == true).Count();

                //int activeUsersCount = (int)HttpContext.Items["activeUsers"];

                //string online = Convert.ToString(HttpContext.Application["SiteVisitedCounter"]);


                IDE_USERS userCheck = service.UserCheck(user.USER_NAME, user.PASSWORD);
                if (userCheck != null)
                {
                    if (userCheck.STATUS == false)
                    {
                        ViewBag.LoginMessage = userCheck.USER_NAME + " İstifadəçi aktiv deyil";
                        return View(user);
                    }

                    int online = service.GetOnlineUserCount(Convert.ToInt32(content.Split(',')[4]), user.USER_NAME);

                    //string activeUsersCount = HttpContext.Application["TotalOnlineUsers"].ToString();
                    //= OnlineVisitorsContainer.Visitors.Values.OrderByDescending(x => x.SessionStarted).ToList();
                    if (online >= Convert.ToInt32(content.Split(',')[1]))
                    {
                        ViewBag.LoginMessage = "Sessiyada istifadəçi limiti dolub!Sessiyada mövcud istifadəçi sayı:" + online + "/" + Convert.ToInt32(content.Split(',')[1]);
                        return View(user);
                    }


                    IDE_USERS userAuth = service.UserAuth(user.USER_NAME, user.PASSWORD);
                    int timeout = Convert.ToInt32(content.Split(',')[4]);
                    var authTicket = new FormsAuthenticationTicket(1, userAuth.USER_NAME, DateTime.Now, DateTime.Now.AddMinutes(timeout), true, "");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }

                //var account = db.IDE_USERS.Where(u => u.USER_NAME == user.USER_NAME && u.STATUS==true).FirstOrDefault();
                //if (account != null)
                //{
                //    if (Hashing.ValidatePassword(user.PASSWORD, account.PASSWORD))
                //    {

                //        var authTicket = new FormsAuthenticationTicket(1, account.USER_NAME, DateTime.Now, DateTime.Now.AddMinutes(20), true, "");
                //        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                //        Response.Cookies.Add(cookie);
                //        account.LAST_LOGIN_TIME = DateTime.Now;
                //        account.LAST_ACTITY_TIME = DateTime.Now;
                //        db.SaveChanges();


                //        return RedirectToAction("Index", "Home");

                //    }
                //    else
                //    {
                //        ViewBag.LoginMessage = "*İstifadəçi adı və ya şifrə düzgün daxil edilməmişdir!";
                //        return View(user);
                //    }
                //}

                ViewBag.LoginMessage = "*İstifadəçi adı və ya şifrə düzgün daxil edilməmişdir!";
                return View(user);
            }

            ViewBag.LoginLicense = licenseKey + "," + DeviceProparties.GetMacAddress() + "," + HttpContext.Request.Url.Host;
            return View(user);

        }

        public ActionResult Logout()
        {
            string UserName = service.GetUserAuthTicket().Name;
            var user = db.IDE_USERS.Where(x => x.USER_NAME == UserName).FirstOrDefault();
            user.ACTIVE = false;
            db.SaveChanges();

            Session.Abandon();
            FormsAuthentication.SignOut();

            Session.Abandon(); return RedirectToAction("Index", "Home");
        }
    }
}