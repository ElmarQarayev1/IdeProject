using IDE_CASHCOUNT.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IDE_CASHCOUNT.Controllers.WebApi
{

    [RoutePrefix("api/Operations")]
    public class OperationsController : ApiController
    {
        IDEContext db = new IDEContext();
        [HttpPost]
        [Route("insert_client")]
        public HttpResponseMessage insert_client([FromUri]string apiKey, [FromBody]List<Clients> items)
        {
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "apiKey_faild");
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    string code = item.clients[0].CODE;
                    if (!string.IsNullOrWhiteSpace(code))
                    {

                        var model = db.IDE_CLIENT.Where(i => i.CODE == code).FirstOrDefault();
                        if (model != null)
                        {
                            model.CODE = item.clients[0].CODE;
                            model.NAME_ = item.clients[0].NAME_;
                            model.NAME2 = item.clients[0].NAME2;
                            model.GROUP_CODE = item.clients[0].GROUP_CODE;
                            model.ADRESS = item.clients[0].ADRESS;
                            model.ADRESS2 = item.clients[0].ADRESS2;
                            model.LOCATION_X = item.clients[0].LOCATION_X;
                            model.LOCATION_Y = item.clients[0].LOCATION_Y;
                            model.TYPE = item.clients[0].TYPE;

                            model.MODFY_DATE = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            item.clients[0].CREATE_DATE = DateTime.Now;
                            item.clients[0].MODFY_DATE = DateTime.Now;
                            db.IDE_CLIENT.Add(item.clients[0]);
                            db.SaveChanges();
                        }
                    }
                }

                return Request.CreateResponse(HttpStatusCode.Created, "operation_ok");
            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage + ",";
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild :" + errors);
            }
        }

        [HttpPost]
        [Route("insert_client_group")]
        public HttpResponseMessage insert_client_group([FromUri]string apiKey, [FromBody]List<ClientGroups> items)
        {
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "apiKey_faild");
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    string code = item.groups[0].CODE;
                    if (!string.IsNullOrWhiteSpace(code))
                    {

                        var model = db.IDE_CLIENT_GROUP.Where(i => i.CODE == code).FirstOrDefault();
                        if (model != null)
                        {
                            model.CODE = item.groups[0].CODE;
                            model.NAME_ = item.groups[0].NAME_;
                            model.MODFY_DATE = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            item.groups[0].CREATE_DATE = DateTime.Now;
                            item.groups[0].MODFY_DATE = DateTime.Now;
                            db.IDE_CLIENT_GROUP.Add(item.groups[0]);
                            db.SaveChanges();
                        }
                    }

                }

                return Request.CreateResponse(HttpStatusCode.Created, "operation_ok");

            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage + ",";
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild :" + errors);
            }

        }

        [HttpPost]
        [Route("insert_product")]
        public HttpResponseMessage insert_product([FromUri]string apiKey, [FromBody]List<Products> items)
        {
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "apiKey_faild");
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    string code = item.products[0].CODE;
                    if (!string.IsNullOrWhiteSpace(code))
                    {

                        var model = db.IDE_ITEMS.Where(i => i.CODE == code).FirstOrDefault();
                        if (model != null)
                        {
                            model.CODE = item.products[0].CODE;
                            model.NAME_ = item.products[0].NAME_;
                            model.NAME2 = item.products[0].NAME2;
                            model.GROUP_CODE = item.products[0].GROUP_CODE;
                            model.MARK_CODE = item.products[0].MARK_CODE;
                            model.MODFY_DATE = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            item.products[0].CREATE_DATE = DateTime.Now;
                            item.products[0].MODFY_DATE = DateTime.Now;
                            db.IDE_ITEMS.Add(item.products[0]);
                            db.SaveChanges();
                        }
                    }

                }

                //string code = items.products[0].CODE;
                //var model = db.IDE_ITEMS.Where(i => i.CODE == code).FirstOrDefault();
                //if (model != null) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_dublicate"); }

                //db.IDE_ITEMS.Add(items.products[0]);
                //db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "operation_ok");
            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage + ",";
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild :" + errors);
            }
        }


        [HttpPost]
        [Route("insert_product_group")]
        public HttpResponseMessage insert_product_group([FromUri]string apiKey, [FromBody]List<ProductGroup> items)
        {
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "apiKey_faild");
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (ModelState.IsValid)
            {

                foreach (var item in items)
                {
                    string code = item.group[0].CODE;
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        var model = db.IDE_ITEMS_GROUP.Where(i => i.CODE == code).FirstOrDefault();
                        if (model != null)
                        {
                            model.CODE = item.group[0].CODE;
                            model.NAME_ = item.group[0].NAME_;
                            model.MODFY_DATE = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            item.group[0].CREATE_DATE = DateTime.Now;
                            item.group[0].MODFY_DATE = DateTime.Now;
                            db.IDE_ITEMS_GROUP.Add(item.group[0]);
                            db.SaveChanges();
                        }
                    }

                }
                //string code = items.group[0].CODE;
                //var model = db.IDE_ITEMS_GROUP.Where(i => i.CODE == code).FirstOrDefault();
                //if (model != null) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_dublicate"); }

                //db.IDE_ITEMS_GROUP.Add(items.group[0]);
                //db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "operation_ok");
            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage + ",";
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild :" + errors);
            }

        }


        [HttpPost]
        [Route("insert_product_mark")]
        public HttpResponseMessage insert_product_mark([FromUri]string apiKey, [FromBody]List<ProductMarks> items)
        {
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "apiKey_faild");
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    string code = item.marks[0].CODE;
                    if (!string.IsNullOrWhiteSpace(code))
                    {

                        var model = db.IDE_ITEMS_MARK.Where(i => i.CODE == code).FirstOrDefault();
                        if (model != null)
                        {
                            model.CODE = item.marks[0].CODE;
                            model.NAME_ = item.marks[0].NAME_;
                            model.MODFY_DATE = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            item.marks[0].CREATE_DATE = DateTime.Now;
                            item.marks[0].MODFY_DATE = DateTime.Now;
                            db.IDE_ITEMS_MARK.Add(item.marks[0]);
                            db.SaveChanges();
                        }
                    }

                }
                //string code = items.marks[0].CODE;
                //var model = db.IDE_ITEMS_MARK.Where(i => i.CODE == code).FirstOrDefault();
                //if (model != null) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_dublicate"); }

                //db.IDE_ITEMS_MARK.Add(items.marks[0]);
                //db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "operation_ok");
            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage + ",";
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild :" + errors);
            }

        }



        [HttpPost]
        [Route("insert_product_unitset")]
        public HttpResponseMessage insert_product_unitset([FromUri]string apiKey, [FromBody]List<ProductUnisets> items)
        {
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "apiKey_faild");
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    try
                    {
                        var code = item.unit[0].ITEM_CODE;
                        if (!string.IsNullOrWhiteSpace(code))
                        {
                            var linenr = item.unit[0].LINENR;
                            var model = db.IDE_ITEMS_UNITSET.Where(x => x.ITEM_CODE == code && x.LINENR == linenr).FirstOrDefault();
                            if (model != null)
                            {
                                model.MODFY_DATE = DateTime.Now;
                                model.UNIT = item.unit[0].UNIT;
                                model.UNITF = item.unit[0].UNITF;

                                db.SaveChanges();
                            }
                            else
                            {
                                item.unit[0].CREATE_DATE = DateTime.Now;
                                item.unit[0].MODFY_DATE = DateTime.Now;
                                db.IDE_ITEMS_UNITSET.Add(item.unit[0]);
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
                //db.IDE_ITEMS_UNITSET.Add(items.unit[0]);
                //db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "operation_ok");
            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage + ",";
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild :" + errors);
            }

        }



        [HttpPost]
        [Route("insert_product_barcodes")]
        public HttpResponseMessage insert_product_barcodes([FromUri]string apiKey, [FromBody]List<ProductBarcodes> items)
        {
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "apiKey_faild");
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (ModelState.IsValid)
            {

                foreach (var item in items)
                {
                    if (item.barcodes[0].ITEM_CODE != "")
                    {
                        item.barcodes[0].CREATE_DATE = DateTime.Now;
                        db.IDE_ITEM_BARCODES.Add(item.barcodes[0]);
                        db.SaveChanges();
                    }

                }


                //var model = db.IDE_ITEM_BARCODES.Add(items.barcodes[0]);
                //db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "operation_ok");
            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage + ",";
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild :" + errors);
            }

        }


        [HttpPost]
        [Route("insert_invoice")]
        public HttpResponseMessage insert_invoice([FromUri]string apiKey, [FromBody]List<Invoices> items)
        {
            var error = new IDE_INVOICE();

            //checking the validity of the api key
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "apiKey_faild"); }
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }

            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    foreach (var item in items)
                    {
                        //invoice should only contain 3 and 7 trcode
                        string code = item.invoice[0].FICHENO;
                        if (!new[] { 3, 7 }.Contains(item.invoice[0].TRCODE))
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild");
                        }

                        //check for invoice in the database
                        var model = db.IDE_INVOICE.Where(i => i.FICHENO == code).FirstOrDefault();
                        if (model == null)
                        {
                            //save invoice data
                            var invoice = db.IDE_INVOICE.Add(item.invoice[0]);
                            db.SaveChanges();

                            //save invoice line data
                            for (int i = 0; i < item.stline.Count; i++)
                            {
                                item.stline[i].CLIENT_CODE = invoice.CLIENT_CODE;
                                item.stline[i].INV_REC_ID = invoice.RECORD_ID;
                                if (invoice.TRCODE == 7)
                                {
                                    item.stline[i].SIGN = 0;
                                }
                                else if (invoice.TRCODE == 3)
                                {
                                    item.stline[i].SIGN = 1;
                                }
                                item.stline[i].CANCELED = false;
                                item.stline[i].TRCODE = invoice.TRCODE;
                                item.stline[i].LINENR = i+1;
                                db.SaveChanges();
                            }

                            //add saving invoice to numbering table
                            GT_RECORD_NUMBERING recNum = new GT_RECORD_NUMBERING();
                            recNum.SALESMAN_CODE = item.invoice[0].SALESMAN_CODE;
                            recNum.DOCUMENT_NUMBER = item.invoice[0].FICHENO;
                            recNum.DOCUMENT_TYPE = item.invoice[0].TRCODE;
                            recNum.CREATE_DATETIME = DateTime.Now;
                            recNum.VALUE_ = JsonConvert.SerializeObject(item.stline).ToString();
                            db.GT_RECORD_NUMBERING.Add(recNum);
                            db.SaveChanges();

                        }
                        else
                        {
                            //update invoice data
                            model.MODFY_DATE = item.invoice[0].CREATE_DATE;
                            model.CLIENT_CODE = item.invoice[0].CLIENT_CODE;
                            model.LOCATION_X = item.invoice[0].LOCATION_X;
                            model.LOCATION_Y = item.invoice[0].LOCATION_Y;
                            model.NETTOTAL = item.invoice[0].NETTOTAL;
                            model.NOTE = item.invoice[0].NOTE;
                            model.PROJECT_CODE = item.invoice[0].PROJECT_CODE;
                            model.RESPONSIBILITY_CODE = item.invoice[0].RESPONSIBILITY_CODE;
                            model.SALESMAN_CODE = item.invoice[0].SALESMAN_CODE;
                            model.SPECODE = item.invoice[0].SPECODE;
                            model.TOTAL = item.invoice[0].TOTAL;
                            model.NETTOTAL = item.invoice[0].NETTOTAL;
                            model.TRCODE = item.invoice[0].TRCODE;
                            error = model;
                            db.SaveChanges();


                            int k = 1;
                            var lines = db.IDE_STLINE.Where(x => x.INV_REC_ID == model.RECORD_ID).ToList().OrderBy(x => x.LINENR).ThenBy(x => x.LINENR).ToList();
                            if (lines.Count != 0) { k = lines.Last().LINENR + 1; }

                            List<int> update_id = new List<int>();
                            foreach (var line in item.stline)
                            {
                                bool update = false;
                                foreach (var line1 in lines)
                                {
                                    if (line.STOCK_CODE == line1.STOCK_CODE)
                                    {
                                        var lineid = line1.RECORD_ID;
                                        var data = db.IDE_STLINE.Where(x => x.RECORD_ID == lineid).FirstOrDefault();
                                        data.AMOUNT = line.AMOUNT;
                                        data.CANCELED = false;
                                        data.CLIENT_CODE = model.CLIENT_CODE;
                                        data.NETPRICE = line.NETPRICE;
                                        data.NETTOTAL = line.NETTOTAL;
                                        data.PRICE = line.PRICE;
                                        if (model.TRCODE == 7)
                                        {
                                            data.SIGN = 0;
                                        }
                                        if (model.TRCODE == 3)
                                        {
                                            data.SIGN = 1;
                                        }
                                        data.LINETYPE = line.LINETYPE;
                                        data.LINENR = k;
                                        data.STOCK_CODE = line.STOCK_CODE;
                                        data.TOTAL = line.TOTAL;
                                        data.TRCODE = model.TRCODE;
                                        data.CANCELED = false;
                                        db.SaveChanges();
                                        update = true;
                                        update_id.Add(data.RECORD_ID);
                                    }
                                }

                                if (update == false)
                                {
                                    line.CLIENT_CODE = model.CLIENT_CODE;
                                    line.INV_REC_ID = model.RECORD_ID;

                                    if (model.TRCODE == 7)
                                    {
                                        line.SIGN = 0;
                                    }
                                    if (model.TRCODE == 3)
                                    {
                                        line.SIGN = 1;
                                    }
                                    line.CANCELED = false;
                                    line.TRCODE = model.TRCODE;
                                    line.LINENR = k;     
                                    db.IDE_STLINE.Add(line);
                                    db.SaveChanges();
                                    k++;
                                }
                            }

                            //deleting rows from the database based on new data
                            foreach (var line in lines)
                            {
                                if (!update_id.Contains(line.RECORD_ID))
                                {
                                    line.CANCELED = true;
                                    db.SaveChanges();
                                }
                            }


                            GT_RECORD_NUMBERING_UPDATE recNum = new GT_RECORD_NUMBERING_UPDATE();
                            recNum.SALESMAN_CODE = item.invoice[0].SALESMAN_CODE;
                            recNum.DOCUMENT_NUMBER = item.invoice[0].FICHENO;
                            recNum.DOCUMENT_TYPE = item.invoice[0].TRCODE;
                            recNum.CREATE_DATETIME = DateTime.Now;
                            recNum.VALUE_ = JsonConvert.SerializeObject(item.stline).ToString();
                            db.GT_RECORD_NUMBERING_UPDATE.Add(recNum);
                            db.SaveChanges();

                           
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "operation_ok");

                }
                catch (Exception e)
                {
                    var error_value = error;
                    GT_DLL_ERROR_LOGS log = new GT_DLL_ERROR_LOGS();
                    log.FICHE_NO = items[0].invoice[0].FICHENO;
                    log.FICHE_TYPE = items[0].invoice[0].TRCODE.ToString();
                    log.SLSMAN = items[0].invoice[0].SALESMAN_CODE;
                    log.VALUE_ = JsonConvert.SerializeObject(items[0].stline).ToString();
                    log.EXCEPTION = e.Message;
                    db.GT_DLL_ERROR_LOGS.Add(log);
                    db.SaveChanges();
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,e.ToString()+ "operation_faild1");
                }
            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage + ",";
                }
                GT_DLL_ERROR_LOGS log = new GT_DLL_ERROR_LOGS();
                log.FICHE_NO = items[0].invoice[0].FICHENO;
                log.FICHE_TYPE = items[0].invoice[0].TRCODE.ToString();
                log.SLSMAN = items[0].invoice[0].SALESMAN_CODE;
                // log.VALUE_ = JsonConvert.SerializeObject(items[0].stline).ToString();
                log.VALUE_ = JsonConvert.SerializeObject(items).ToString();
                log.EXCEPTION = errors;
                db.GT_DLL_ERROR_LOGS.Add(log);
                db.SaveChanges();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errors+ "operation_faild3");
            }

        }

        [HttpPost]
        [Route("insert_finance")]
        public HttpResponseMessage insert_finance([FromUri]string apiKey, [FromBody]List<Finances> items)
        {
            var apiCheck = db.IDE_USERS.Where(x => x.API_KEY == apiKey).FirstOrDefault();
            if (apiCheck == null) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (items.Count == 0) { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild"); }
            if (ModelState.IsValid)
            {
                try
                {
                    List<IDE_CLFLINE> update_model = new List<IDE_CLFLINE>();
                    List<IDE_CLFLINE> create_model = new List<IDE_CLFLINE>();
                    List<Dublicate> dublicates = new List<Dublicate>();
                    foreach (var item in items)
                    {
                        string code = item.finance[0].FICHENO;
                        var model = db.IDE_CLFLINE.Where(i => i.FICHENO == code).FirstOrDefault();
                        if (model != null)
                        {
                            model.CLIENT_CODE = item.finance[0].CLIENT_CODE;
                            model.CREATE_DATE = item.finance[0].CREATE_DATE;
                            model.SALESMAN_CODE = item.finance[0].SALESMAN_CODE;
                            model.SIGN = item.finance[0].SIGN;
                            model.TOTAL = item.finance[0].TOTAL;
                            model.TRCODE = item.finance[0].TRCODE;
                            db.SaveChanges();
                            update_model.Add(item.finance[0]);

                        }
                        else
                        {
                            db.IDE_CLFLINE.Add(item.finance[0]);
                            db.SaveChanges();
                            create_model.Add(item.finance[0]);
                        }
                        if (update_model.Count > 0)
                        {
                            GT_RECORD_NUMBERING_UPDATE recNum = new GT_RECORD_NUMBERING_UPDATE();
                            recNum.SALESMAN_CODE = item.finance[0].SALESMAN_CODE;
                            recNum.DOCUMENT_NUMBER = item.finance[0].FICHENO;
                            recNum.DOCUMENT_TYPE = item.finance[0].TRCODE;
                            recNum.CREATE_DATETIME = DateTime.Now;
                            recNum.VALUE_ = JsonConvert.SerializeObject(update_model).ToString();
                            db.GT_RECORD_NUMBERING_UPDATE.Add(recNum);
                            db.SaveChanges();
                        }

                        if (update_model.Count > 0)
                        {
                            GT_RECORD_NUMBERING recNum = new GT_RECORD_NUMBERING();
                            recNum.SALESMAN_CODE = item.finance[0].SALESMAN_CODE;
                            recNum.DOCUMENT_NUMBER = item.finance[0].FICHENO;
                            recNum.DOCUMENT_TYPE = item.finance[0].TRCODE;
                            recNum.CREATE_DATETIME = DateTime.Now;
                            recNum.VALUE_ = JsonConvert.SerializeObject(create_model).ToString();
                            db.GT_RECORD_NUMBERING.Add(recNum);
                            db.SaveChanges();

                        }

                    }

                    return Request.CreateResponse(HttpStatusCode.OK, "operation_ok");

                }
                catch (Exception e)
                {

                    string errors = "";
                    foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        errors += item.ErrorMessage;
                    }
                    GT_DLL_ERROR_LOGS log = new GT_DLL_ERROR_LOGS();
                    log.FICHE_NO = items[0].finance[0].FICHENO;
                    log.FICHE_TYPE = items[0].finance[0].TRCODE.ToString();
                    log.SLSMAN = items[0].finance[0].SALESMAN_CODE;
                    log.VALUE_ = JsonConvert.SerializeObject(items[0].finance).ToString();
                    log.EXCEPTION = e.Message;
                    db.GT_DLL_ERROR_LOGS.Add(log);
                    db.SaveChanges();
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild");
                }

            }
            else
            {
                string errors = "";
                foreach (var item in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errors += item.ErrorMessage;
                }
                GT_DLL_ERROR_LOGS log = new GT_DLL_ERROR_LOGS();
                log.FICHE_NO = items[0].finance[0].FICHENO;
                log.FICHE_TYPE = items[0].finance[0].TRCODE.ToString();
                log.SLSMAN = items[0].finance[0].SALESMAN_CODE;
                log.VALUE_ = JsonConvert.SerializeObject(items[0].finance).ToString();
                log.EXCEPTION = errors;
                db.GT_DLL_ERROR_LOGS.Add(log);
                db.SaveChanges();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "operation_faild");
            }


        }

    }


    public class Dublicate
    {
        public string value { get; set; }
        public string message { get; set; }
        public Dublicate(string _value, string _message)
        {
            value = _value;
            message = _message;
        }
    }

    public class Clients
    {
        [JsonProperty("fa_header")]
        public List<IDE_CLIENT> clients { get; set; }
    }

    public class ClientGroups
    {
        [JsonProperty("fa_header")]
        public List<IDE_CLIENT_GROUP> groups { get; set; }
    }

    public class Products
    {
        [JsonProperty("fa_header")]
        public List<IDE_ITEMS> products { get; set; }
    }

    public class ProductGroup
    {
        [JsonProperty("fa_header")]
        public List<IDE_ITEMS_GROUP> group { get; set; }
    }

    public class ProductMarks
    {
        [JsonProperty("fa_header")]
        public List<IDE_ITEMS_MARK> marks { get; set; }
    }

    public class ProductUnisets
    {
        [JsonProperty("fa_header")]
        public List<IDE_ITEMS_UNITSET> unit { get; set; }
    }

    public class ProductBarcodes
    {
        [JsonProperty("fa_header")]
        public List<IDE_ITEM_BARCODES> barcodes { get; set; }
    }

    public class Invoices
    {
        [JsonProperty("fa_header")]
        public List<IDE_INVOICE> invoice { get; set; }
        [JsonProperty("fa_line")]
        public List<IDE_STLINE> stline { get; set; }
    }

    public class Finances
    {
        [JsonProperty("fa_header")]
        public List<IDE_CLFLINE> finance { get; set; }

    }

}
