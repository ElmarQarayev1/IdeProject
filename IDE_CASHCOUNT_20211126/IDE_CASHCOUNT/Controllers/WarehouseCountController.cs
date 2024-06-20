using IDE_CASHCOUNT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDE_CASHCOUNT.Controllers
{
    public class WarehouseCountController : Controller
    {
        // GET: WarehouseCount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            IDEContext context = new IDEContext();
            var data = context.IDE_VIEW_WAREHOUSES.ToList();
            return View();
        }

        public ActionResult GetData()
        {
            using (IDEContext db = new IDEContext())
            {
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                 var list = db.IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS.ToList();
                //var list = db.IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS.Where(l => l.DATE_ >= dtTo).ToList();
                var jsonResult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }

        public ActionResult GetFilterData(string begin_date, string end_date, string warehouses)
        {
            try
            {
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                using (IDEContext db = new IDEContext())
                {
                    var list = db.IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS.ToList();

                    if (begin_date != "" && end_date != "")
                    {
                        DateTime dt_begin = DateTime.ParseExact(begin_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dt_end = DateTime.ParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        list = list.Where(a => Convert.ToDateTime(a.DATE_) >= dt_begin.Date && Convert.ToDateTime(a.DATE_) <= dt_end.Date).ToList();

                    }                   

                    if (warehouses != "")
                    {
                        List<string> arr_slsm = warehouses.Split(new char[] { ',' }).ToList();
                        list = list.Where(l => arr_slsm.Contains(l.WH_CODE.ToString())).ToList();
                    }
                   
                    return Json(new { success = true, data = list }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception )
            {
                return Json(new { success = false, responseText = "Göndərilən məlumatlarda problem yarandı.Filtrasıya edilmədi!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFilterTesdiqleData(string c_begin_date, string c_end_date, string c_confirm_date , string c_onhand_date, string wh  )
        {

           

            try
            {
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                using (IDEContext db = new IDEContext())
                {


                    var pid = new SqlParameter("@RECORD_ID", "6");
                    var list = db.IDE_PROCEDURE_WAREHOUSE_COUNT_LINE.SqlQuery("IDE_PROCEDURE_WAREHOUSE_COUNT_LINE @RECORD_ID", pid).ToList();
                   return Json(new { data = list }, JsonRequestBehavior.AllowGet);

                    //var list = db.IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS.ToList();

                    //if (begin_date != "" && end_date != "")
                    //{
                    //    DateTime dt_begin = DateTime.ParseExact(begin_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //    DateTime dt_end = DateTime.ParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //    list = list.Where(a => Convert.ToDateTime(a.DATE_) >= dt_begin.Date && Convert.ToDateTime(a.DATE_) <= dt_end.Date).ToList();

                    //}

                    //if (warehouses != "")
                    //{
                    //    List<string> arr_slsm = warehouses.Split(new char[] { ',' }).ToList();
                    //    list = list.Where(l => arr_slsm.Contains(l.WH_CODE.ToString())).ToList();
                    //}

                   // return Json(new { success = true, data = list }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception )
            {
                return Json(new { success = false, responseText = "Göndərilən məlumatlarda problem yarandı.Filtrasıya edilmədi!" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Details()
        {
            return PartialView();
        }

        public ActionResult DetailsTesdiqleMallar()
        {
            return PartialView();
        }

        public ActionResult GetDataDetails(int id )
        {
            using (IDEContext db = new IDEContext())
            {
              //  try
            //    {
                    var pid = new SqlParameter("@RECORD_ID", id.ToString());             
                    var list = db.IDE_PROCEDURE_WAREHOUSE_COUNT_LINE.SqlQuery("IDE_PROCEDURE_WAREHOUSE_COUNT_LINE @RECORD_ID", pid).ToList();
                    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
              //  }
              //  catch (Exception ex)
              //  {
                    //return null;
               // }

            }
        }
    }
}