using IDE_CASHCOUNT.Common.Filters;
using IDE_CASHCOUNT.Common.Security;
using IDE_CASHCOUNT.Models.Entities;
using IDE_CASHCOUNT.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace IDE_CASHCOUNT.Controllers
{
    [IDEAuthorize(roles: "Admin,User")]
    public class ProfilSettingsController : Controller
    {
        SecurityService service = new SecurityService();
        IDEContext db = new IDEContext();
        // GET: ProfilSettings

        ProfilSettingsViewModel vm = new ProfilSettingsViewModel();
        private void GetUserData()
        {
            //int id = Convert.ToInt32(EncryptDecrypt.Decrypt(HttpContext.Request.Cookies["userID"].Value, "shams99IDESOFTMMC"));
            if (service.GetUserAuthTicket() != null)
            {
                string userName = service.GetUserAuthTicket().Name;
                var user = db.IDE_USERS.Where(x => x.USER_NAME == userName).SingleOrDefault();
                vm.USER_NAME = user.USER_NAME;
                vm.NAME_SURNAME = user.NAME_SURNAME;
                vm.PASSWORD = user.PASSWORD;
                vm.RECORD_ID = user.RECORD_ID;
            }
            
        
        }
        public ActionResult Index()
        {
            GetUserData();
            return View(vm);
        }


        [HttpPost]
        public ActionResult Update(ProfilSettingsViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("Index", user);
            }

            if (user.RECORD_ID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var account = db.IDE_USERS.Where(i => i.RECORD_ID == user.RECORD_ID).FirstOrDefault();
            if (account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sameUser = db.IDE_USERS.Where(a => a.USER_NAME == user.USER_NAME && a.RECORD_ID != user.RECORD_ID).FirstOrDefault();
            if (sameUser != null)
            {
                //TempData["LoginMessageError"] = "Bu adda istifadəçi artıq mövcüddu!!!";
                //return View();
                ViewBag.UserSettingsMessage = "Bu adda istifadəçi artıq mövcüddu!!!";
                return View("Index", user);
            }

            //update main table
            account.NAME_SURNAME = user.NAME_SURNAME;
            account.USER_NAME = user.USER_NAME;
            user.UPDATE_DATETIME = DateTime.Now;
            account.PASSWORD = Hashing.HasPassword(user.PASSWORD); ;
            db.SaveChanges();
            ViewBag.UserSettingsSuccess = "Uğurla dəyişdirildi!";
            return View("Index", user);
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult UploadLogo(HttpPostedFileBase LogoImage)
        {
            GetUserData();
            List<string> acceptExtentions = new List<string>() { ".jpeg", ".png", ".jpg", ".tiff", ".gif", ".ico" };

            try
            {

                // Default folder    
                if (LogoImage != null)
                {
                    var files = Directory.EnumerateFiles(Server.MapPath("~/Content/webFiles/logo/"));

                    string filename = "";

                    foreach (var file in files)
                    {

                        System.IO.File.Delete(file);

                    }

                    filename = Path.GetFileNameWithoutExtension(LogoImage.FileName);
                    string extension = Path.GetExtension(LogoImage.FileName);
                    if (!acceptExtentions.Contains(extension)) { ViewBag.UserSettingsMessage = "Xəta baş verdi. Seçdiyiniz file formatı düzgün deyil!"; return View("Index", vm); }


                    filename = "logo" + extension;

                    filename = Path.Combine(Server.MapPath("~/Content/webFiles/logo/"), filename);
                    LogoImage.SaveAs(filename);

                    Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                    AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
                    //Edit
                    if (objAppsettings != null)
                    {
                        objAppsettings.Settings["LogoUrl"].Value = "logo" + extension;
                        objConfig.Save();
                    }


                    ViewBag.UserSettingsSuccess = "Sizin loqonuz yükləndi!Təbriklər!";
                }



            }
            catch (Exception)
            {
                ViewBag.UserSettingsMessage = "File yüklənə bilmədi.Yüklənmə zamanı xəta baş verdi";
            }


            return View("Index", vm);
        }

    }
}