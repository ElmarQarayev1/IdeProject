using IDE_CASHCOUNT.Common.Filters;
using IDE_CASHCOUNT.Models.Entities;
using System.Linq;
using System.Web.Mvc;


namespace IDE_CASHCOUNT.Controllers
{
    [IDEAuthorize(roles:"Admin,User")]
    public class HomeController : Controller
    {
        IDEContext db = new IDEContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalesmansListPartial() {
            return PartialView();
        }

        public ActionResult ClientsListPartial()
        {
            return PartialView();
        }
        public ActionResult SaleControlClientsListPartial()
        {
            return PartialView();
        }

        public ActionResult WarehouseListPartial()
        {
            return PartialView();
        }

        public ActionResult GetPartialsData(string model)
        {  
            using (IDEContext db = new IDEContext())
            {
                if (model == "CLIENT")
                {
                   var list = (from c in db.IDE_CLIENT

                                select new
                                {
                                    RECORD_ID = c.RECORD_ID,
                                    CODE = c.CODE,
                                    NAME = c.NAME_,
                                }).ToList();
                
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
                }

                else if (model == "SALESMAN")
                {
                    var list = (from c in db.IDE_SLSMAN

                                select new
                                {
                                    RECORD_ID = c.RECORD_ID,
                                    CODE = c.CODE,
                                    NAME = c.NAME
                                }).ToList();

                    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
                }
                else if (model == "WAREHOUSE")
                {
                    var list = db.IDE_VIEW_WAREHOUSES.ToList();
                    var jsonResult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (model == "SaleControlClients")
                {
                    var list = db.IDE_VIEW_SALECONTROL_CLIENTS.ToList();
                    var jsonResult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                   
                }
            }
            return View("Index");
        }
    }
}

