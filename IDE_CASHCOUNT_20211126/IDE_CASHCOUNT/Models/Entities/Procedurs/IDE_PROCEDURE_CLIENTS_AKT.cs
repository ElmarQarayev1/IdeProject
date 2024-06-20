using System.ComponentModel.DataAnnotations;

namespace IDE_CASHCOUNT.Models.Entities.Procedurs
{
    public class IDE_PROCEDURE_CLIENTS_AKT
    {
        [Key]
        public string STOCK_CODE { get; set; }
        public string STOCK_NAME { get; set; }
        public string BEFORE_ONHAND { get; set; }
        public string BEFORE_ONHAND_TOTAL { get; set; }
        public string SALE_AMOUNT { get; set; }
        public string AFTER_ONHAND { get; set; }
        public string AFTER_ONHAND_TOTAL { get; set; }
        public string PRICE { get; set; }
        public string TOTAL { get; set; }
    }
}