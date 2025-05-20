using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IlsDb.Configuration.BaseConfiguration
{
    class ReadingRoomConfiguration : IEntityTypeConfiguration<ReadingRoomEntity>
    {
        public void Configure(EntityTypeBuilder<ReadingRoomEntity> builder)
        {
            builder.HasKey(readingRoom => readingRoom.Id);
        }
    }
}
