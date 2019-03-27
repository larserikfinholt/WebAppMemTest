namespace WebAppMemTest
{
    using Microsoft.EntityFrameworkCore;

    public class MemtestDbContext : DbContext
    {

        public string ConnectionString { get; set; }

        public DbSet<MemtestModel> MemTestItems { get; set; }

        public MemtestDbContext()
        {
        }

        public MemtestDbContext(DbContextOptions<MemtestDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    options
                        .UseSqlServer(ConnectionString);
                }
                else
                {
                    var connString = "Server=tcp:whatever.database.windows.net,1433;Initial Catalog=MemTestDb;Persist Security Info=False;User ID=whatever;Password=whatever;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    options.UseSqlServer(connString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MemtestModel>().HasIndex(i => i.Created).HasName("CreatedIndex");
        }
    }
}
