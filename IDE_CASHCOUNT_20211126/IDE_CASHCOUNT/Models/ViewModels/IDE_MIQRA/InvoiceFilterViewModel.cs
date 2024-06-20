using IDE_CASHCOUNT.Models.Entities;
using System;
using System.Collections.Generic;


namespace IDE_CASHCOUNT.Models.ViewModels.IDE_MIQRA
{
    public class InvoiceFilterViewModel
    {
        public List<IDE_CLIENT> Clients_List { get; set; }
        public List<IDE_SLSMAN> Salesmans_List { get; set; }
        public DateTime Begin_Date { get; set; }
        public DateTime End_Date { get; set; }
    }
}