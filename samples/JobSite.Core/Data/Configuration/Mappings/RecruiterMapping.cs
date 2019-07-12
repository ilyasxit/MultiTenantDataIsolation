using JobSite.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSite.Core.Data.Configuration.Mappings
{
    class RecruiterMapping : IEntityTypeConfiguration<Domain.Recruiter>
    {
        public void Configure(EntityTypeBuilder<Recruiter> entity)
        {
            entity.HasKey(p => p.RecruiterId)
                .HasName("PK_Recruiters");

            entity.Property(e => e.RecruiterId).ValueGeneratedNever();

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            entity.Property(e => e.RecruiterName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
