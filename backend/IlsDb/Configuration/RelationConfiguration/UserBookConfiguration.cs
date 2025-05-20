using IlsDb.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IlsDb.Configuration.RelationConfiguration
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBookEntity>
    {
        public void Configure(EntityTypeBuilder<UserBookEntity> builder)
        {
            builder.HasKey(userBook => userBook.Id);
        }
    }
}
