using IDE_CASHCOUNT.Common.Filters;
using IDE_CASHCOUNT.Common.Security;
using IDE_CASHCOUNT.Models.Entities;
using IDE_CASHCOUNT.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IDE_CASHCOUNT.Controllers
{
    [IDEAuthorize(roles: "Admin")]

    public class UsersController : Controller
    {

        //IDEEntities db = new IDEEntities();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetData()
        {
            using (IDEContext db = new IDEContext())
            {
                List<UserViewModel> list = db.IDE_USERS.Where(a => a.ROLE != "Admin").Select(x => new UserViewModel
                {
                    LAST_LOGIN_TIME = x.LAST_LOGIN_TIME,
                    NAME_SURNAME = x.NAME_SURNAME,
                    USER_NAME = x.USER_NAME,
                    RECORD_ID = x.RECORD_ID
                }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View("AddOrEditPartial", new UserViewModel());
            else
            {
                using (IDEContext db = new IDEContext())
                {
                    UserViewModel model = new UserViewModel();
                    IDE_USERS user = db.IDE_USERS.SingleOrDefault(x => x.RECORD_ID == id && x.ROLE != "A");
                    model.NAME_SURNAME = user.NAME_SURNAME;
                    model.USER_NAME = user.USER_NAME;
                    model.PASSWORD = user.PASSWORD;
                    model.RECORD_ID = user.RECORD_ID;
                    return View("AddOrEditPartial", model);
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(UserViewModel model)
        {
            using (IDEContext db = new IDEContext())
            {
                if (model.RECORD_ID == 0)
                {

                    var sameUser = db.IDE_USERS.Where(a => a.USER_NAME == model.USER_NAME).FirstOrDefault();
                    if (sameUser != null)
                    {
                        //TempData["LoginMessageError"] = "Bu adda istifadəçi artıq mövcüddu!!!";
                        //return View();
                        return Json(new { success = false, message = "Bu adda istifadəçi artıq mövcüddu!!!" }, JsonRequestBehavior.AllowGet);
                    }

                    //insert
                    IDE_USERS user = new IDE_USERS();
                    user.NAME_SURNAME = model.NAME_SURNAME;
                    user.USER_NAME = model.USER_NAME;
                    user.CREATE_DATETIME = DateTime.Now;
                    user.UPDATE_DATETIME = DateTime.Now;
                    user.PASSWORD = Hashing.HasPassword(model.PASSWORD);
                    user.ROLE = "User";
                    user.STATUS = true;
                    db.IDE_USERS.Add(user);
                    db.SaveChanges();

                    return Json(new { success = true, message = user.USER_NAME + " əlavə olundu!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var sameUser = db.IDE_USERS.Where(a => a.USER_NAME == model.USER_NAME && a.RECORD_ID != model.RECORD_ID).FirstOrDefault();
                    if (sameUser != null)
                    {
                        //TempData["LoginMessageError"] = "Bu adda istifadəçi artıq mövcüddu!!!";
                        //return View();
                        return Json(new { success = false, message = "Bu adda istifadəçi artıq mövcüddu!!!" }, JsonRequestBehavior.AllowGet);
                    }
                    //update
                    IDE_USERS user = db.IDE_USERS.SingleOrDefault(x => x.RECORD_ID == model.RECORD_ID);
                    user.NAME_SURNAME = model.NAME_SURNAME;
                    user.USER_NAME = model.USER_NAME;
                    user.UPDATE_DATETIME = DateTime.Now;
                    user.PASSWORD = Hashing.HasPassword(model.PASSWORD);
                    db.SaveChanges();

                    return Json(new { success = true, message = user.USER_NAME + " uğurla yeniləndi!" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (IDEContext db = new IDEContext())
            {
                IDE_USERS user = db.IDE_USERS.Where(x => x.RECORD_ID == id).FirstOrDefault<IDE_USERS>();
                string name = user.USER_NAME;
                db.IDE_USERS.Remove(user);
                db.SaveChanges();
                return Json(new { success = true, message = name + " istifadəçi silindi!" }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}