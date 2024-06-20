using System.ComponentModel.DataAnnotations;


namespace IDE_CASHCOUNT.Models.Entities
{
    public class IDE_SLSMAN
    {
        [Key]
        public int RECORD_ID { get; set; }
        [Required]
        [StringLength(10)]
        public string CODE { get; set; }
        [StringLength(50)]
        public string NAME { get; set; }

    }
}