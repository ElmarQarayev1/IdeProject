using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace IDE_CASHCOUNT.Models.Entities
{
    public class IDE_STLINE
    {
        [Key]
        [IgnoreDataMember]
        public int RECORD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string STOCK_CODE { get; set; }
        [IgnoreDataMember]
        public byte TRCODE { get; set; }

        [Required]
        [IgnoreDataMember]
        public int INV_REC_ID { get; set; }

        [StringLength(50)]
        [IgnoreDataMember]
        public string CLIENT_CODE { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal PRICE { get; set; }
        public decimal NETPRICE { get; set; }
        public decimal TOTAL { get; set; }
        public decimal NETTOTAL { get; set; }

        [IgnoreDataMember]
        public byte SIGN { get; set; }

        [IgnoreDataMember]
        public int LINENR { get; set; }

        public byte LINETYPE { get; set; }
        [IgnoreDataMember]
        public bool CANCELED { get; set; }



    }
}