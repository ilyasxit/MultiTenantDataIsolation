using JobSite.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobSite.Core.Data.MainContext
{
    public class SiteContext : DbContext
    {

        public SiteContext(DbContextOptions<SiteContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        
        //entities
        public virtual DbSet<Site> Sites { get; set; }
      
    }
}
