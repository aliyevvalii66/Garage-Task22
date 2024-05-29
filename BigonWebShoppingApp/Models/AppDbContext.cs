using BigonWebShoppingApp.EFConfigurations;
using BigonWebShoppingApp.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using System.Reflection;

namespace BigonWebShoppingApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opt) : base(opt) { }


        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var baseEntries = ChangeTracker.Entries<BaseEntity>();
            foreach (var item in baseEntries)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        item.Entity.UpdatedDate = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        item.Entity.DeletedDate = DateTime.Now;
                        break;
                }
            }



            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
