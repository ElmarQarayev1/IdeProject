using System;
using System.ComponentModel.DataAnnotations;


namespace IDE_CASHCOUNT.Models.Entities
{
    public class GT_RECORD_NUMBERING_UPDATE
    {
        [Key]
        public int RECORD_ID { get; set; }
        [StringLength(25)]
        public string SALESMAN_CODE { get; set; }
        [StringLength(25)]
        public string DOCUMENT_NUMBER { get; set; }
        public int DOCUMENT_TYPE { get; set; }
        public DateTime? CREATE_DATETIME { get; set; }
        [StringLength(50)]
        public string LOCATION { get; set; }
        public string VALUE_ { get; set; }
        public DateTime? TERMINAL_DATETIME { get; set; }
    }
}