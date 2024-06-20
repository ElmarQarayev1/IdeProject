using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;



namespace IDE_CASHCOUNT.Models.Entities
{


    public partial class IDE_ITEM_BARCODES
    {
        [Key]
        [IgnoreDataMember]
        public int RECORD_ID { get; set; }

        [StringLength(50)]
        public string ITEM_CODE { get; set; }

        [StringLength(50)]
        public string BARCODE { get; set; }

        [StringLength(25)]
        public string UNIT { get; set; }

        public DateTime? CREATE_DATE { get; set; }

    }
}
