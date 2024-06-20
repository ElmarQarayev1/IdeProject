using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IDE_CASHCOUNT.Models.Entities.Views
{
    public class IDE_VIEW_SALECONTROL_INVOICE
    {
        [Key]
        public int RECORD_ID { get; set; }
        public string FICHENO { get; set; }
        
        public Int16 TRCODE { get; set; }
        public DateTime DATE_ { get; set; }
        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }
        public string SALESMAN_CODE { get; set; }
      
        public string SALESMAN_NAME { get; set; }
        public Int16 WH_CODE { get; set; }
        public string WH_NAME { get; set; }
        public double GROSSTOTAL { get; set; }
        public double TOTALDISCOUNT { get; set; }
        public double NETTOTAL { get; set; }
        public int CONFIRM_STATUS { get; set; }
        public int CONFIRM_COUNT { get; set; }
        public string CONFIRM_DATETIME { get; set; }
    }
}