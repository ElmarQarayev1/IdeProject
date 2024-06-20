using System.ComponentModel.DataAnnotations;

namespace IDE_CASHCOUNT.Models.Entities.Procedurs
{
    public class IDE_PROCEDURE_WAREHOUSE_COUNT_CALC_ITEMS
    {
        [Key]
        public string ITM_CODE { get; set; }
        public string ITM_NAME { get; set; }
        public double AMOUNT { get; set; }
        public double ONHAND { get; set; }
        public double COUNT_PLUS { get; set; }
        public double COUNT_MINUS { get; set; }
        public double PRICE { get; set; }
        public double TOTAL { get; set; }
    }
}