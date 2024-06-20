using IDE_CASHCOUNT.Common.Filters;
using IDE_CASHCOUNT.Models.Entities;
using IDE_CASHCOUNT.Models.ViewModels.IDE_MIQRA;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace IDE_CASHCOUNT.Controllers
{
    
    [Authorize(Roles ="Admin,User")]
    public class ClientsReportController :Controller
    {
        IDEContext db = new IDEContext();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (IDEContext db = new IDEContext())
            {               
                var list = db.IDE_VIEW_CLIENTS.ToList();
                var jsonResult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }

        public ActionResult Details(string id)
        {
            try
            {
                var pid = new SqlParameter("@CLIENT_CODE", id);

                var data = db.IDE_PROCEDURE_CLIENTS_AKT.SqlQuery("IDE_PROCEDURE_CLIENTS_AKT @CLIENT_CODE", pid).ToList();
                ClientsAktViewModel model = new ClientsAktViewModel()
                {
                    CLIENT_CODE = id,
                    CLIENT_NAME = db.IDE_CLIENT.Where(x => x.CODE == id).FirstOrDefault().NAME_,
                    IDE_PROCEDURE_CLIENTS_AKT = data
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }



    }
}