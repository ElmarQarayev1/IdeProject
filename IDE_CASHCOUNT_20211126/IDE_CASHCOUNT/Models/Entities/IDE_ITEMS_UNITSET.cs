using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;




namespace IDE_CASHCOUNT.Models.Entities
{


    public partial class IDE_ITEMS_UNITSET
    {
        [Key]
        [IgnoreDataMember]
        public int RECORD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ITEM_CODE { get; set; }

        [Required]
        [StringLength(20)]
        public string UNIT { get; set; }

        public double UNITF { get; set; }

        public int LINENR { get; set; }

        public DateTime? CREATE_DATE { get; set; }
        public DateTime? MODFY_DATE { get; set; }
    }
}
