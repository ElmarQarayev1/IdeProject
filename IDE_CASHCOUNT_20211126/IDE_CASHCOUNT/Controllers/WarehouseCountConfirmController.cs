using IDE_CASHCOUNT.Models.Entities;
using IDE_CASHCOUNT.Models.Entities.Procedurs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDE_CASHCOUNT.Controllers
{
    public class WarehouseCountConfirmController : Controller
    {
        // GET: WarehouseCountConfirm
        public static string p_c_begin_date , p_c_end_date, p_c_confirm_date, p_c_onhand_date, p_wh, p_item_count_status;
        public static List<IDE_PROCEDURE_WAREHOUSE_COUNT_CALC_ITEMS> list;
        [HttpGet]
        public ActionResult Index(string c_begin_date , string c_end_date, string c_confirm_date, string c_onhand_date, string whtesdiq, string item_count_status)
        {
            p_c_begin_date = DateTime.Parse(c_begin_date).ToString("yyyy-MM-dd");
            p_c_end_date = DateTime.Parse(c_end_date).ToString("yyyy-MM-dd");
            p_c_confirm_date = DateTime.Parse(c_confirm_date).ToString("yyyy-MM-dd");
            p_c_onhand_date = DateTime.Parse(c_onhand_date).ToString("yyyy-MM-dd");
            p_wh = whtesdiq;
            p_item_count_status = item_count_status;


            return View();
        }

        public ActionResult GetData()
        {
            using (IDEContext db = new IDEContext())
            {
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                // var list = db.IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS.ToList();
                //var list = db.IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS.Where(l => l.DATE_ >= dtTo).ToList();
                var pid1 = new SqlParameter("@c_begin_date", p_c_begin_date);
                var pid2 = new SqlParameter("@c_end_date", p_c_end_date);
                var pid3 = new SqlParameter("@c_onhand_date", p_c_onhand_date);
                var pid4 = new SqlParameter("@wh", p_wh);
                var pid5 = new SqlParameter("@item_count_status", p_item_count_status);
                  list = db.IDE_PROCEDURE_WAREHOUSE_COUNT_CALC_ITEMS.SqlQuery("IDE_PROCEDURE_WAREHOUSE_COUNT_CALC_ITEMS @c_begin_date,@c_end_date,@c_onhand_date,@wh,@item_count_status", pid1, pid2, pid3, pid4, pid5).ToList();
                var jsonResult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }

        public ActionResult SetSayimSenedleri()
        {
            try
            {
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                using (IDEContext db = new IDEContext())
                {
                    var pid1 = new SqlParameter("@c_begin_date", p_c_begin_date);
                    var pid2 = new SqlParameter("@c_end_date", p_c_end_date);
                    var pid3 = new SqlParameter("@c_onhand_date", p_c_onhand_date);
                   
                    var pid4 = new SqlParameter("@wh", p_wh);
                    var pid5 = new SqlParameter("@item_count_status", p_item_count_status);
                    var pid6 = new SqlParameter("@c_confirm_date", p_c_confirm_date);

                    var list = db.IDE_PROCEDURE_WAREHOUSE_COUNT_SAVE.SqlQuery("IDE_PROCEDURE_WAREHOUSE_COUNT_SAVE @c_begin_date,@c_end_date,@c_onhand_date,@c_confirm_date,@wh,@item_count_status", pid1, pid2, pid3,pid6, pid4, pid5).ToList();
                    return Json(new { success = false, responseText = list }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception )
            {
                return Json(new { success = false, responseText = "Göndərilən məlumatlarda problem yarandı.Filtrasıya edilmədi!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}