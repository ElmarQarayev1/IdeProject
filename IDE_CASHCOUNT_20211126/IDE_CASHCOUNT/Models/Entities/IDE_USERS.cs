using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDE_CASHCOUNT.Models.Entities
{
    public class IDE_USERS
    {
        [Key]
        public int RECORD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME_SURNAME { get; set; }

        [StringLength(50)]
        public string USER_NAME { get; set; }

        public string PASSWORD { get; set; }

        [StringLength(10)]
        public string ROLE { get; set; }

        public string API_KEY { get; set; }
        public bool ACTIVE { get; set; }

        public bool STATUS { get; set; }

        public DateTime? CREATE_DATETIME { get; set; }
        public DateTime? UPDATE_DATETIME { get; set; }
        public DateTime? LAST_LOGIN_TIME { get; set; }
        public DateTime? LAST_ACTITY_TIME { get; set; }
    }
}