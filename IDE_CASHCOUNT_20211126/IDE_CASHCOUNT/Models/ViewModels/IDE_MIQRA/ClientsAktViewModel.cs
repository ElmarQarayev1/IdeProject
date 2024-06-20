using IDE_CASHCOUNT.Models.Entities.Procedurs;
using System.Collections.Generic;


namespace IDE_CASHCOUNT.Models.ViewModels.IDE_MIQRA
{
    public class ClientsAktViewModel
    {
        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }
        public List<IDE_PROCEDURE_CLIENTS_AKT> IDE_PROCEDURE_CLIENTS_AKT { get; set; }
    }
}