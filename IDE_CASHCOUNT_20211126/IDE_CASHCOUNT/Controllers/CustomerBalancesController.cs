using IDE_CASHCOUNT.Common.Filters;
using IDE_CASHCOUNT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IDE_CASHCOUNT.Controllers
{
    [IDEAuthorize(roles: "Admin,User")]
    public class CustomerBalancesController : Controller
    {
        // GET: CustomerBalances
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (IDEContext db = new IDEContext())
            {
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
         
                var list = (from stlines in db.IDE_STLINE
                            join items in db.IDE_ITEMS
                            on stlines.STOCK_CODE equals items.CODE

                            join mr in db.IDE_ITEMS_MARK
                            on items.MARK_CODE equals mr.CODE
                            join gr in db.IDE_ITEMS_GROUP
                            on items.GROUP_CODE equals gr.CODE
                            join inv in db.IDE_INVOICE
                            on stlines.INV_REC_ID equals inv.RECORD_ID
                            join client in db.IDE_CLIENT
                            on inv.CLIENT_CODE equals client.CODE
                            group new { stlines, client, items,gr, inv } by new
                            {
                                STOCK_CODE = stlines.STOCK_CODE,
                         
                                TR_CODE = inv.TRCODE,
                                CLIENT_CODE = inv.CLIENT_CODE,
                                STOCK_NAME = items.NAME_,
                                GROUP_NAME = gr.NAME_,
                                MARKA= mr.NAME_,
                                CLIENT_NAME = client.NAME_
                            }
                            into temp
                            select new
                            {
                                CLIENT_CODE = temp.Key.CLIENT_CODE,
                                TR_CODE = temp.Key.TR_CODE,
                                CLIENT_NAME = temp.Key.CLIENT_NAME,
                                STOCK_CODE = temp.Key.STOCK_CODE,
                                STOCK_NAME = temp.Key.STOCK_NAME,
                                MARKA = temp.Key.MARKA,
                                GROUP = temp.Key.GROUP_NAME,
                                ONHAND = db.IDE_STLINE.Where(s1 => s1.STOCK_CODE == temp.Key.STOCK_CODE && s1.CLIENT_CODE == temp.Key.CLIENT_CODE && s1.SIGN == 0).Select(a => a.AMOUNT).DefaultIfEmpty(0).Sum() - db.IDE_STLINE.Where(s2 => s2.STOCK_CODE == temp.Key.STOCK_CODE && s2.CLIENT_CODE == temp.Key.CLIENT_CODE && s2.SIGN == 1).Select(a => a.AMOUNT).DefaultIfEmpty(0).Sum()
                                //OHAND = (from st1 in db.IDE_STLINE where st1.STOCK_CODE ==stlines.STOCK_CODE  st1.SIGN=0 select st1.AMOUNT).Sum()

                            }).Where(l=> l.TR_CODE == 7 || l.TR_CODE == 1).ToList();

                var test = list;

                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFilterData(string clients)
        {
            try
            {
                using (IDEContext db = new IDEContext())
                {

                    var list = (from stlines in db.IDE_STLINE
                                join items in db.IDE_ITEMS
                                on stlines.STOCK_CODE equals items.CODE

                                join mr in db.IDE_ITEMS_MARK
                                on items.MARK_CODE equals mr.CODE
                                join gr in db.IDE_ITEMS_GROUP
                                on items.GROUP_CODE equals gr.CODE
                                join inv in db.IDE_INVOICE
                                on stlines.INV_REC_ID equals inv.RECORD_ID
                                join client in db.IDE_CLIENT
                                on inv.CLIENT_CODE equals client.CODE
                                 
                                group new { stlines, client, items, gr, inv } by new
                                {
                                    STOCK_CODE = stlines.STOCK_CODE,

                                    TR_CODE = inv.TRCODE,
                                    CLIENT_CODE = inv.CLIENT_CODE,
                                    STOCK_NAME = items.NAME_,
                                    GROUP_NAME = gr.NAME_,
                                    MARKA = mr.NAME_,
                                    CLIENT_NAME = client.NAME_
                                }
                                into temp
                                select new
                                {
                                    CLIENT_CODE = temp.Key.CLIENT_CODE,
                                    TR_CODE = temp.Key.TR_CODE,
                                    CLIENT_NAME = temp.Key.CLIENT_NAME,
                                    STOCK_CODE = temp.Key.STOCK_CODE,
                                    STOCK_NAME = temp.Key.STOCK_NAME,
                                    MARKA = temp.Key.MARKA,
                                    GROUP = temp.Key.GROUP_NAME,
                                    ONHAND = db.IDE_STLINE.Where(s1 => s1.STOCK_CODE == temp.Key.STOCK_CODE && s1.CLIENT_CODE == temp.Key.CLIENT_CODE && s1.SIGN == 0).Select(a => a.AMOUNT).DefaultIfEmpty(0).Sum() - db.IDE_STLINE.Where(s2 => s2.STOCK_CODE == temp.Key.STOCK_CODE && s2.CLIENT_CODE == temp.Key.CLIENT_CODE && s2.SIGN == 1).Select(a => a.AMOUNT).DefaultIfEmpty(0).Sum()
                                    //OHAND = (from st1 in db.IDE_STLINE where st1.STOCK_CODE ==stlines.STOCK_CODE  st1.SIGN=0 select st1.AMOUNT).Sum()

                                }).Where(l => l.TR_CODE== 7 ||  l.TR_CODE == 1).ToList();


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
    }

}