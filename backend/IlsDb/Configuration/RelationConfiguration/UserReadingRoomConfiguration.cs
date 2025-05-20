using IlsDb.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IlsDb.Configuration.RelationConfiguration
{
    public class UserReadingRoomConfiguration : IEntityTypeConfiguration<UserReadingRoomEntity>
    {
        public void Configure(EntityTypeBuilder<UserReadingRoomEntity> builder)
        {
            builder.HasKey(userReadingRoom => userReadingRoom.Id);
        }
    }
}
