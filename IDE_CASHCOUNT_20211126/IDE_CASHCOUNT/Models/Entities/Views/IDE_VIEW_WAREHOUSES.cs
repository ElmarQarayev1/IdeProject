using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDE_CASHCOUNT.Models.Entities.Views
{
    public class IDE_VIEW_WAREHOUSES
    {
        [Key]
        public Int16 WH_CODE { get; set; }
        public string WH_NAME { get; set; }
    }
}