using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDE_CASHCOUNT.Models.ViewModels
{
    public class ProfilSettingsViewModel
    {
        public int RECORD_ID { get; set; }
        public DateTime? CREATE_DATETIME { get; set; }
        public DateTime? UPDATE_DATETIME { get; set; }

        [DisplayName("Ad Soyad")]
        [Required(ErrorMessage = "Ad Soyad boş buraxıla bilməz!")]
        public string NAME_SURNAME { get; set; }
        [DisplayName("İstifadəçi Adı")]
        [Required(ErrorMessage = "Login boş buraxıla bilməz!")]
        public string USER_NAME { get; set; }
        [DisplayName("Şifrə")]
        [Required(ErrorMessage = "Şifrə boş buraxıla bilməz!")]
        public string PASSWORD { get; set; }
        public string ROLE { get; set; }


        //////
        [DataType(DataType.Upload)]
        public string LogoImage { get; set; }
    }
}