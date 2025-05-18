using ils_database.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.BaseConfiguration
{
    public class LibraryConfiguration : IEntityTypeConfiguration<LibraryEntity>
    {
        public void Configure(EntityTypeBuilder<LibraryEntity> builder)
        {
            builder.HasKey(library => library.Id);
        }
    }
}
