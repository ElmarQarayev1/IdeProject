using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDE_CASHCOUNT.Models.ViewModels.IDE_MIQRA
{
    public class InvoiceViewModel
    {
        [Key]
        public int RECORD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FICHENO { get; set; }
        public DateTime? CREATE_DATE { get; set; } 
        [StringLength(50)]
        public string CLIENT_CODE { get; set; }
        [StringLength(50)]
        public string CLIENT_NAME { get; set; }
        [StringLength(50)]
        public string SALESMAN_CODE { get; set; }
        public string SALESMAN_NAME { get; set; }
        //[StringLength(20)]
        //public int TRCODE { get; set; }
        //public string PROJECT_CODE { get; set; }
        //[StringLength(20)]
        //public string RESPONSIBILITY_CODE { get; set; }
        //[StringLength(20)]
        //public string SPECODE { get; set; }

        //public double TOTAL { get; set; }
        //public double NETTOTAL { get; set; }

        //public DateTime? MODFY_DATE { get; set; }
    }
}