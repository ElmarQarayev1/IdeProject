using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IDE_CASHCOUNT.Models.Entities
{
    public class IDE_CLFLINE
    {
        [Key]
        [IgnoreDataMember]
        public int RECORD_ID { get; set; }
        [StringLength(50)]
        [Required]
        public string FICHENO { get; set; }
        [Required]
        public DateTime CREATE_DATE { get; set; }
        [Required]
        public byte TRCODE { get; set; }
        [Required]
        [StringLength(50)]
        public string CLIENT_CODE { get; set; }
        public byte SIGN { get; set; }
        [StringLength(50)]
        public string SALESMAN_CODE { get; set; }
        public double TOTAL { get; set; }
    }
}