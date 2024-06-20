using IDE_CASHCOUNT.Models.Entities.Procedurs;
using IDE_CASHCOUNT.Models.Entities.Views;
using System.Data.Entity;


namespace IDE_CASHCOUNT.Models.Entities
{
    public partial class IDEContext : DbContext
    {
        public IDEContext()
            : base("name=IDEContext")
        {
        }


        public virtual DbSet<IDE_USERS> IDE_USERS { get; set; }

        public virtual DbSet<IDE_ITEM_BARCODES> IDE_ITEM_BARCODES { get; set; }
        public virtual DbSet<IDE_ITEMS_UNITSET> IDE_ITEMS_UNITSET { get; set; }
        public virtual DbSet<IDE_CLIENT> IDE_CLIENT { get; set; }
        public virtual DbSet<IDE_CLIENT_GROUP> IDE_CLIENT_GROUP { get; set; }
        public virtual DbSet<IDE_ITEMS> IDE_ITEMS { get; set; }
        public virtual DbSet<IDE_ITEMS_GROUP> IDE_ITEMS_GROUP { get; set; }
        public virtual DbSet<IDE_ITEMS_MARK> IDE_ITEMS_MARK { get; set; }
        public virtual DbSet<IDE_INVOICE> IDE_INVOICE { get; set; }
        public virtual DbSet<IDE_SLSMAN> IDE_SLSMAN { get; set; }
        public virtual DbSet<IDE_STLINE> IDE_STLINE { get; set; }
        public virtual DbSet<GT_RECORD_NUMBERING> GT_RECORD_NUMBERING { get; set; }
        public virtual DbSet<GT_RECORD_NUMBERING_UPDATE> GT_RECORD_NUMBERING_UPDATE { get; set; }
        public virtual DbSet<GT_DLL_ERROR_LOGS> GT_DLL_ERROR_LOGS { get; set; }
        public virtual DbSet<IDE_CLFLINE> IDE_CLFLINE { get; set; }

        //Views
        public virtual DbSet<IDE_VIEW_SALECONTROL_INVOICE> IDE_VIEW_SALECONTROL_INVOICE { get; set; }
        public virtual DbSet<IDE_VIEW_WAREHOUSES> IDE_VIEW_WAREHOUSES { get; set; }
        public virtual DbSet<IDE_VIEW_SALECONTROL_CLIENTS> IDE_VIEW_SALECONTROL_CLIENTS { get; set; }
        public virtual DbSet<IDE_VIEW_CLIENTS> IDE_VIEW_CLIENTS { get; set; }
        public virtual DbSet<IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS> IDE_VIEW_WAREHOUSE_COUNT_DOCUMENTS { get; set; }

        //Procedurs
        public virtual DbSet<IDE_PROCEDURE_SALE_CONTROL_STLINE> IDE_PROCEDURE_SALE_CONTROL_STLINE { get; set; }
        public virtual DbSet<IDE_PROCEDURE_WAREHOUSE_COUNT_LINE> IDE_PROCEDURE_WAREHOUSE_COUNT_LINE { get; set; }       
        public virtual DbSet<IDE_PROCEDURE_CLIENTS_AKT> IDE_PROCEDURE_CLIENTS_AKT { get; set; }
        public virtual DbSet<IDE_PROCEDURE_WAREHOUSE_COUNT_CALC_ITEMS> IDE_PROCEDURE_WAREHOUSE_COUNT_CALC_ITEMS { get; set; }
        public virtual DbSet<IDE_PROCEDURE_WAREHOUSE_COUNT_SAVE> IDE_PROCEDURE_WAREHOUSE_COUNT_SAVE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }

    }
}
