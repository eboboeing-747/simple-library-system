using ils_database.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.BaseConfiguration
{
    class UserTypeConfiguration : IEntityTypeConfiguration<UserTypeEntity>
    {
        public void Configure(EntityTypeBuilder<UserTypeEntity> builder)
        {
            builder.HasKey(userType => userType.Id);
        }
    }
}
