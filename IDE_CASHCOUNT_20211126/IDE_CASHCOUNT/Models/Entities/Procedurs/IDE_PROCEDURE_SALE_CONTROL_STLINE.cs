using System.ComponentModel.DataAnnotations;

namespace IDE_CASHCOUNT.Models.Entities.Procedurs
{
    public class IDE_PROCEDURE_SALE_CONTROL_STLINE
    {
        [Key]
        public string ITM_CODE { get; set; }
        public string ITM_NAME { get; set; }
        public double AMOUNT { get; set; }
        public double PRICE { get; set; }
        public double TOTAL { get; set; }
    }
}