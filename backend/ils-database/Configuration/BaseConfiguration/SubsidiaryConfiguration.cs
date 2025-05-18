using ils_database.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.BaseConfiguration
{
    public class SubsidiaryConfiguration : IEntityTypeConfiguration<SubsidiaryEntity>
    {
        public void Configure(EntityTypeBuilder<SubsidiaryEntity> builder)
        {
            builder.HasKey(subsid => subsid.Id);
        }
    }
}
