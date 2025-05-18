using ils_database.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.BaseConfiguration
{
    class ReadingRoomConfiguration : IEntityTypeConfiguration<ReadingRoomEntity>
    {
        public void Configure(EntityTypeBuilder<ReadingRoomEntity> builder)
        {
            builder.HasKey(readingRoom => readingRoom.Id);
        }
    }
}
