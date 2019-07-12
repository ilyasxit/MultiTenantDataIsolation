using JobSite.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace JobSite.Core.Data.Configuration.Mappings
{
    public class JobMapping : IEntityTypeConfiguration<Domain.Job>
    {
        public void Configure(EntityTypeBuilder<Job> entity)
        {
            entity.HasKey(p => p.JobId)
                .HasName("PK_Jobs");

            entity.Property(e => e.JobId).ValueGeneratedNever();

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            entity.Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Recruiter)
                .WithMany(p => p.RecruiterJobs)
                .HasForeignKey(d => d.RecruiterId)
                .HasConstraintName("FK_Recruiters_Jobs");
        }
    }
}
