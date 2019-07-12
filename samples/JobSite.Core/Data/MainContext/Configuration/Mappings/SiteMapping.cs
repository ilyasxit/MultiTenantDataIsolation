using JobSite.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSite.Core.Data.MainContext. Configuration.Mappings
{
    public class SiteMapping : IEntityTypeConfiguration<Domain.Site>
    {
        public void Configure(EntityTypeBuilder<Site> entity)
        {
            entity.HasKey(p => p.SiteId);

            entity.Property(p => p.SiteName)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(p => p.ThemeName)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasData(
                new Site { SiteId = 1, SiteName = "InterNurse", ThemeName = "internurse" },
                new Site { SiteId = 2, SiteName = "UkVets", ThemeName = "ukvets" },
                new Site { SiteId = 3, SiteName = "RhineGold", ThemeName = "rhinegold" });
        }
    }
}
