using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;



namespace IDE_CASHCOUNT.Models.Entities
{

    public partial class IDE_ITEMS_GROUP
    {
        [Key]
        [Column(Order = 0)]

        [IgnoreDataMember]
        public int RECORD_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string CODE { get; set; }

        [StringLength(50)]
        public string NAME_ { get; set; }

        public DateTime? CREATE_DATE { get; set; }
        public DateTime? MODFY_DATE { get; set; }


    }
}
