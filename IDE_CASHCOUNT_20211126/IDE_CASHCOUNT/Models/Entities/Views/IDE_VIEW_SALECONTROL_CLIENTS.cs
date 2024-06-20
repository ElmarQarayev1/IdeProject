using System.ComponentModel.DataAnnotations;

namespace IDE_CASHCOUNT.Models.Entities.Views
{
    public class IDE_VIEW_SALECONTROL_CLIENTS
    {
        [Key]
        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }
        public string CLIENT_NAME2 { get; set; }
    }
}