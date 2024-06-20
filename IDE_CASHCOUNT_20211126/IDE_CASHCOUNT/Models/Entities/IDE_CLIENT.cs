using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;





namespace IDE_CASHCOUNT.Models.Entities
{

    public partial class IDE_CLIENT
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

        [StringLength(200)]
        public string NAME2 { get; set; }

        [StringLength(300)]
        public string ADRESS { get; set; }

        [StringLength(300)]
        public string ADRESS2 { get; set; }

        [StringLength(50)]
        public string LOCATION_X { get; set; }

        [StringLength(50)]
        public string LOCATION_Y { get; set; }

        [StringLength(50)]
        public string GROUP_CODE { get; set; }

        [StringLength(50)]
        public string TYPE { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public DateTime? MODFY_DATE { get; set; }
    }
}
