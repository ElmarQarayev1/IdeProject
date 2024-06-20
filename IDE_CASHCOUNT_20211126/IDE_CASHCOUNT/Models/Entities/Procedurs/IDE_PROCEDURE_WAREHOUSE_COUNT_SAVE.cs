using System.ComponentModel.DataAnnotations;

namespace IDE_CASHCOUNT.Models.Entities.Procedurs
{
    public class IDE_PROCEDURE_WAREHOUSE_COUNT_SAVE
    {
        [Key]
        public string ERROR_STATUS { get; set; }
        public string MESSAGE { get; set; }

    }
}