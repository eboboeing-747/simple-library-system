using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IlsDb.Configuration.BaseConfiguration
{
    public class LibraryConfiguration : IEntityTypeConfiguration<LibraryEntity>
    {
        public void Configure(EntityTypeBuilder<LibraryEntity> builder)
        {
            builder.HasKey(library => library.Id);
        }
    }
}
