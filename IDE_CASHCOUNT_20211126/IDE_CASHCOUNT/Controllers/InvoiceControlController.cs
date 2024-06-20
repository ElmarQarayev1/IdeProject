using IDE_CASHCOUNT.Common.Filters;
using IDE_CASHCOUNT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace IDE_CASHCOUNT.Controllers
{
    [IDEAuthorize(roles: "Admin,User")]
    public class InvoiceControlController : Controller
    {
        // GET: InvoiceControl
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
               // var list = db.IDE_VIEW_SALECONTROL_INVOICE.ToList();
                var list = db.IDE_VIEW_SALECONTROL_INVOICE.Where(l => l.DATE_ >= dtTo).ToList();
                var jsonResult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }


        public ActionResult GetFilterData(string begin_date, string end_date, string confirm_begin_date, string confirm_end_date, string warehouses, string clientsCode, string clientsName,int controlCount)
        {
            try
            {
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                using (IDEContext db = new IDEContext())
                {
                    var list = db.IDE_VIEW_SALECONTROL_INVOICE.ToList();

                    if (begin_date != "" && end_date != "")
                    {
                        DateTime dt_begin = DateTime.ParseExact(begin_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dt_end = DateTime.ParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        list = list.Where(a => Convert.ToDateTime(a.DATE_) >= dt_begin.Date && Convert.ToDateTime(a.DATE_) <= dt_end.Date).ToList();

                    }
                    //2-control true 1-control false
                    if (controlCount == 2)
                    {
                        list = list.Where(a => a.CONFIRM_STATUS == 1).ToList();
                    }
                    else if (controlCount == 1)
                    {
                        list = list.Where(a => a.CONFIRM_STATUS == 0).ToList();
                    }

                    if (confirm_begin_date != "" && confirm_end_date != "")
                    {
                        DateTime confirm_dt_begin = DateTime.ParseExact(confirm_begin_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime confirm_dt_end = DateTime.ParseExact(confirm_end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        list = list.Where(a => Convert.ToDateTime(a.CONFIRM_DATETIME) >= confirm_dt_begin.Date && Convert.ToDateTime(a.CONFIRM_DATETIME) <= confirm_dt_end).ToList();
                    }

                    if (warehouses != "")
                    {
                        List<string> arr_slsm = warehouses.Split(new char[] { ',' }).ToList();
                        list = list.Where(l => arr_slsm.Contains(l.WH_CODE.ToString())).ToList();
                    }

                    if (clientsCode != "")
                      {
                        list = list.Where(l => l.CLIENT_CODE.Contains(clientsCode)).ToList();
                    }
                    if (clientsName != "")
                    {
                        list = list.Where(l => l.CLIENT_NAME.Contains(clientsName)).ToList();
                    }
                    return Json(new { success = true, data = list }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Göndərilən məlumatlarda problem yarandı.Filtrasıya edilmədi!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details()
        {
            return PartialView();
        }
        public ActionResult GetDataDetails(int id, int trCode)
        {
            using (IDEContext db = new IDEContext())
            {
                try
                {
                    var pid = new SqlParameter("@RECORD_ID", id.ToString());
                    var ptrCode = new SqlParameter("@TRCODE", trCode);

                    var list = db.IDE_PROCEDURE_SALE_CONTROL_STLINE.SqlQuery("IDE_PROCEDURE_SALE_CONTROL_STLINE @RECORD_ID, @TRCODE", pid, ptrCode ).ToList();
                    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception ex)
                {
                    return null;
                }

            }
        }


    }
}