using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IlsDb.Configuration.BaseConfiguration
{
    class UserTypeConfiguration : IEntityTypeConfiguration<UserTypeEntity>
    {
        public void Configure(EntityTypeBuilder<UserTypeEntity> builder)
        {
            builder.HasKey(userType => userType.Id);
        }
    }
}
