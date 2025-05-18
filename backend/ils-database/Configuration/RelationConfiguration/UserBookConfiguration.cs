using ils_database.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.RelationConfiguration
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBookEntity>
    {
        public void Configure(EntityTypeBuilder<UserBookEntity> builder)
        {
            builder.HasKey(userBook => userBook.Id);
        }
    }
}
