using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace IDE_CASHCOUNT.Models.Entities
{


    public partial class IDE_ITEMS
    {
        [Key]
        [Column(Order = 0)]
        [IgnoreDataMember]
        public int RECORD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string CODE { get; set; }

        [StringLength(200)]
        public string NAME_ { get; set; }

        [StringLength(50)]
        public string NAME2 { get; set; }

        [StringLength(10)]
        public string GROUP_CODE { get; set; }

        [StringLength(10)]
        public string MARK_CODE { get; set; }

        public DateTime? CREATE_DATE { get; set; }
        public DateTime? MODFY_DATE { get; set; }
    }
}
