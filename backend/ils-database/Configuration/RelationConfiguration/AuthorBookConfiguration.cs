using ils_database.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.RelationConfiguration
{
    public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBookEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorBookEntity> builder)
        {
            builder.HasKey(authorBook => authorBook.Id);
        }
    }
}
