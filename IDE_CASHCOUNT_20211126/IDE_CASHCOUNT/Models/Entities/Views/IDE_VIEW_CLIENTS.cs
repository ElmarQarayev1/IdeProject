using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDE_CASHCOUNT.Models.Entities.Views
{
    public class IDE_VIEW_CLIENTS
    {
        [Key]
        public int RECORD_ID { get; set; }
        public string SALESMAN_CODE { get; set; }
        public string SALESMAN_NAME { get; set; }
        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }
        public string CLIENT_NAME2 { get; set; }
        public string DEBT { get; set; }
    }
}