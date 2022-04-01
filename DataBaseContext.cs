
using IntergrationA.Models;
using Microsoft.EntityFrameworkCore;
using static IntergrationA.Models.barcodemodel;
using static IntergrationA.Models.categorymodel;
using static IntergrationA.Models.inventorymodel;
using static IntergrationA_Update.Models.domodel;
using static IntergrationA_Update.Models.somodel;

namespace WebApi
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        { }

        public DbSet<WebApi.Models.testmodel.SLBAdminUser> SLBAdminUser { get; set; }
        public DbSet<settings> settings { get; set; }
        public DbSet<userinfoschema> userinfoschema { get; set; }
        public DbSet<Barcode> Barcode { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<DeliveryOrderDetails> OrderDetails { get; set; }

        public DbSet<DeliveryOrderHeader> OrderHeader { get; set; }

        public DbSet<SalesOrderDetails> SalesOrderDetails { get; set; }

        public DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barcode>()
                .HasKey(c => new { c.seq, c.U8CUSTDEF_0001_E001_F003 });

                 modelBuilder.Entity<DeliveryOrderDetails>()
                .HasKey(c => new { c.OrderNo, c.ProductId });
        }
    }
}