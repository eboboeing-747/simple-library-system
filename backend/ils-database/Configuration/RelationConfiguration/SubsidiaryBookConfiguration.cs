using ils_database.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ils_database.Configuration.RelationConfiguration
{
    public class SubsidiaryBookConfiguration : IEntityTypeConfiguration<SubsidiaryBookEntity>
    {
        public void Configure(EntityTypeBuilder<SubsidiaryBookEntity> builder)
        {
            builder.HasKey(subsidiaryBook => subsidiaryBook.Id);
        }
    }
}
