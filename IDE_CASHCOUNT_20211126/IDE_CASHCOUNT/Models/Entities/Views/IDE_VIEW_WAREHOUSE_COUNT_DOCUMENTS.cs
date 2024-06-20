using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDE_CASHCOUNT.Models.Entities.Views
{
    public class IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS
    {
        [Key]
        public int RECORD_ID { get; set; }
        public DateTime TERMINAL_DATETIME { get; set; }
        public string FICHENO { get; set; }       
        public DateTime DATE_ { get; set; }  
        public string SALESMAN_CODE { get; set; }
        public string SALESMAN_NAME { get; set; }
        public Int16 WH_CODE { get; set; }
        public string WH_NAME { get; set; }     
        public double  TOTAL { get; set; }
       
    }
}