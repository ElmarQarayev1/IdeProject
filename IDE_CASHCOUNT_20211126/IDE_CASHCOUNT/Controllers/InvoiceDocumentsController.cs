using IDE_CASHCOUNT.Common.Filters;
using IDE_CASHCOUNT.Models.Entities;
using IDE_CASHCOUNT.Models.ViewModels.IDE_MIQRA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace IDE_CASHCOUNT.Controllers
{
    [IDEAuthorize(roles: "Admin,User")]
    public class InvoiceDocumentsController : Controller
    {
        IDEContext db = new IDEContext();
        // GET: CountingDocuments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (IDEContext db = new IDEContext())
            {

                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var list = (from invoice in db.IDE_INVOICE
                           join client in db.IDE_CLIENT
                           on invoice.CLIENT_CODE equals client.CODE
                           join salesman in db.IDE_SLSMAN
                           on invoice.SALESMAN_CODE equals salesman.CODE
                           select new
                           {
                               RECORD_ID = invoice.RECORD_ID,
                               DOCUMENT_NUMBER = invoice.FICHENO,
                               CREATE_DATETIME = invoice.CREATE_DATE,
                               CLIENT_CODE = invoice.CLIENT_CODE,
                               CLIENT_NAME = client.NAME_,
                               SALESMAN_CODE = invoice.SALESMAN_CODE,
                               SALESMAN_NAME = salesman.NAME,
                               TOTAL = invoice.TOTAL,
                               NOTE = invoice.NOTE
                           }).Where(l=> DbFunctions.TruncateTime(l.CREATE_DATETIME) == dtTo).ToList();

                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFilterData(string begin_date,string end_date,string salesmans,string clients)
        {
            try
            {
                DateTime dt_begin = DateTime.ParseExact(begin_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dt_end = DateTime.ParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                using (IDEContext db = new IDEContext())
                {
                    var list = (from invoice in db.IDE_INVOICE
                                join client in db.IDE_CLIENT
                                on invoice.CLIENT_CODE equals client.CODE
                                join salesman in db.IDE_SLSMAN
                                on invoice.SALESMAN_CODE equals salesman.CODE
                                select new
                                {
                                    RECORD_ID = invoice.RECORD_ID,
                                    DOCUMENT_NUMBER = invoice.FICHENO,
                                    CREATE_DATETIME = invoice.CREATE_DATE,
                                    CLIENT_CODE = invoice.CLIENT_CODE,
                                    CLIENT_NAME = client.NAME_,
                                    SALESMAN_CODE = invoice.SALESMAN_CODE,
                                    SALESMAN_NAME = salesman.NAME,
                                    TOTAL = invoice.TOTAL,
                                    NOTE = invoice.NOTE
                                }).Where(a => DbFunctions.TruncateTime(a.CREATE_DATETIME) >= dt_begin.Date &&  DbFunctions.TruncateTime(a.CREATE_DATETIME) <= dt_end.Date).ToList();

                    if (salesmans != "")
                    {
                        List<string> arr_slsm = salesmans.Split(new char[] { ',' }).ToList();
                        list = list.Where(l => arr_slsm.Contains(l.SALESMAN_CODE)).ToList();
                    }

                    if (clients != "")
                    {
                        List<string> arr_clients = clients.Split(new char[] { ',' }).ToList();
                        list = list.Where(l => arr_clients.Contains(l.CLIENT_CODE)).ToList();
                    }

                    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = "Göndərilən məlumatlarda problem yarandı.Filtrasıya edilmədi!" }, JsonRequestBehavior.AllowGet);
            }
           
        }


        public ActionResult InvoiceDetails() {
            return PartialView();
        }
        public ActionResult GetDataDetails(int id)
        {
            using (IDEContext db = new IDEContext())
            {

                var list = (from invoice in db.IDE_INVOICE
                            join stline in db.IDE_STLINE
                            on invoice.RECORD_ID equals stline.INV_REC_ID
                            join client in db.IDE_CLIENT
                            on invoice.CLIENT_CODE equals client.CODE
                            join stock in db.IDE_ITEMS
                            on stline.STOCK_CODE equals stock.CODE
                            select new
                            {
                                RECORD_ID = invoice.RECORD_ID,
                                STOCK_NAME = stock.NAME_,
                                CLIENT_CODE = invoice.CLIENT_CODE,
                                CLIENT_NAME = client.NAME_,
                                AMOUNT = stline.AMOUNT,
                                PRICE = stline.PRICE,
                                TOTAL = stline.TOTAL
                            }).Where(l=>l.RECORD_ID==id).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShowMapPartal(int id)
        {
            var data = (from invoice in db.IDE_INVOICE
                         join stline in db.IDE_STLINE
                         on invoice.RECORD_ID equals stline.INV_REC_ID
                         join client in db.IDE_CLIENT
                         on invoice.CLIENT_CODE equals client.CODE
                         select new
                         {
                             RECORD_ID = invoice.RECORD_ID,
                             INVOICE_LOACATION_X = invoice.LOCATION_X,
                             INVOICE_LOACATION_Y = invoice.LOCATION_Y,
                             CLIENT_LOCATION_X = client.LOCATION_X,
                             CLIENT_LOCATION_Y = client.LOCATION_Y,
                         }).Where(l => l.RECORD_ID == id).FirstOrDefault();
            MapViewModel vm = new MapViewModel()
            {
                LOCATION_X = data.INVOICE_LOACATION_X,
                LOCATION_Y = data.INVOICE_LOACATION_Y,
                CLIENT_LOCATION_X = data.CLIENT_LOCATION_X,
                CLIENT_LOCATION_Y = data.CLIENT_LOCATION_Y
            };
            return PartialView(vm);
        }

    }
}