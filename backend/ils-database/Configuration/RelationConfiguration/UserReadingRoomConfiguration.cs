using ils_database.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.RelationConfiguration
{
    public class UserReadingRoomConfiguration : IEntityTypeConfiguration<UserReadingRoomEntity>
    {
        public void Configure(EntityTypeBuilder<UserReadingRoomEntity> builder)
        {
            builder.HasKey(userReadingRoom => userReadingRoom.Id);
        }
    }
}
