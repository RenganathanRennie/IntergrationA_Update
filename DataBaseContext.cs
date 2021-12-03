
using Microsoft.EntityFrameworkCore;

namespace  WebApi
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        { }

          public DbSet<WebApi.Models.testmodel.SLBAdminUser> SLBAdminUser { get; set; }
    }
}