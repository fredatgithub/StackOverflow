using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        public DbSet<MyDataModel> MyDataModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MyDataModel>()
                .HasKey(c => c.Id);
        }
    }
}
