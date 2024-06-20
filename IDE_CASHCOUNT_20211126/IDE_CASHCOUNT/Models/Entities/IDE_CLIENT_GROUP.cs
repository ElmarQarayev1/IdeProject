using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;




namespace IDE_CASHCOUNT.Models.Entities
{


    public partial class IDE_CLIENT_GROUP
    {
        [Key]
        [IgnoreDataMember]
        public int RECORD_ID { get; set; }

        [StringLength(50)]
        [Required]
        public string CODE { get; set; }

        [StringLength(50)]
        public string NAME_ { get; set; }

        public DateTime? CREATE_DATE { get; set; }
        public DateTime? MODFY_DATE { get; set; }
    }
}
