using System;
using System.ComponentModel.DataAnnotations;



namespace IDE_CASHCOUNT.Models.Entities
{
    public class GT_DLL_ERROR_LOGS
    {
        [Key]
        public int RECORD_ID { get; set; }
        [StringLength(25)]
        public string FICHE_NO { get; set; }

        public DateTime? DATE_TIME { get; set; }
        [StringLength(50)]
        public string FICHE_TYPE { get; set; }
        [StringLength(50)]
        public string SLSMAN { get; set; }
        [StringLength(50)]
        public string ERROR_NO { get; set; }
        public string EXCEPTION { get; set; }
        public string VALUE_ { get; set; }
    }
}