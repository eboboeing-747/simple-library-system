using ils_database.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.RelationConfiguration
{
    public class UserSubsidiaryConfiguration : IEntityTypeConfiguration<UserSubsidiaryEntity>
    {
        public void Configure(EntityTypeBuilder<UserSubsidiaryEntity> builder)
        {
            builder.HasKey(userSubsidiary => userSubsidiary.Id);
        }
    }
}
