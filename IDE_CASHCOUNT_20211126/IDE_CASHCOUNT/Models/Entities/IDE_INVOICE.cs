using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace IDE_CASHCOUNT.Models.Entities
{
    public class IDE_INVOICE
    {
        [Key]
        [IgnoreDataMember]
        public int RECORD_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string FICHENO { get; set; }
        [Required]
        public byte TRCODE { get; set; }
        [StringLength(50)]
        [Required]
        public string CLIENT_CODE { get; set; }
        [StringLength(50)]
        public string SALESMAN_CODE { get; set; }
        [StringLength(20)]
        public string PROJECT_CODE { get; set; }   
        [StringLength(20)]
        public string RESPONSIBILITY_CODE { get; set; }   
        [StringLength(20)]
        public string SPECODE { get; set; }
        public double TOTAL { get; set; }
        public double NETTOTAL { get; set; }
        [StringLength(1000)]
        public string NOTE { get; set; }
        [StringLength(200)]
        public string LOCATION_X { get; set; }
        [StringLength(200)]
        public string LOCATION_Y { get; set; }
        public DateTime? CREATE_DATE { get; set; }

        [IgnoreDataMember]
        public DateTime? MODFY_DATE { get; set; }



    }
}