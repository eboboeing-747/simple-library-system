using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IlsDb.Configuration.BaseConfiguration
{
    public class SubsidiaryConfiguration : IEntityTypeConfiguration<SubsidiaryEntity>
    {
        public void Configure(EntityTypeBuilder<SubsidiaryEntity> builder)
        {
            builder.HasKey(subsid => subsid.Id);
        }
    }
}
