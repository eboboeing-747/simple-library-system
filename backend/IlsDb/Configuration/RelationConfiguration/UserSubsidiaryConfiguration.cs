using IlsDb.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IlsDb.Configuration.RelationConfiguration
{
    public class UserSubsidiaryConfiguration : IEntityTypeConfiguration<UserSubsidiaryEntity>
    {
        public void Configure(EntityTypeBuilder<UserSubsidiaryEntity> builder)
        {
            builder.HasKey(userSubsidiary => userSubsidiary.Id);
        }
    }
}
