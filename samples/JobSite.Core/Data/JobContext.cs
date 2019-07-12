using Finbuckle.MultiTenant;
using JobSite.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobSite.Core.Data
{
    public class JobContext : MultiTenantIdentityDbContext
    {
        public JobContext(TenantInfo tenantInfo, DbContextOptions<JobContext> options)
            : base(tenantInfo, options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString, opt =>
            {
                opt.MigrationsAssembly("JobSite.Core");
            });
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Recruiter> Recruiters { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
    }
}
