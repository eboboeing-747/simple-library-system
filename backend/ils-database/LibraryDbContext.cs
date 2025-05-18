using ils_database.Configuration.BaseConfiguration;
using ils_database.Configuration.RelationConfiguration;
using ils_database.Entity.BaseEntity;
using ils_database.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;

namespace ils_database
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        // base entities
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<LibraryEntity> Libraries { get; set; }
        public DbSet<ReadingRoomEntity> ReadingRooms { get; set; }
        public DbSet<SubsidiaryEntity> Subsidiaries { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserTypeEntity> UserTypes { get; set; }

        // relational entities
        // links many authors with many books
        public DbSet<AuthorBookEntity> AuthorBookEntities { get; set; }
        // amount or presense of a book in a subsidiary
        public DbSet<SubsidiaryBookEntity> SubsidiaryBookEntities { get; set; }
        // books in use by a user
        public DbSet<UserBookEntity> UserBookEntities { get; set; }
        // reading rooms contains users
        public DbSet<UserReadingRoomEntity> UserReadingRoomEntities { get; set; }
        // represents librarians
        public DbSet<UserSubsidiaryEntity> UserSubsidiaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply base configurations
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryConfiguration());
            modelBuilder.ApplyConfiguration(new ReadingRoomConfiguration());
            modelBuilder.ApplyConfiguration(new SubsidiaryConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());

            // apply ralation configurations
            modelBuilder.ApplyConfiguration(new AuthorBookConfiguration());
            modelBuilder.ApplyConfiguration(new SubsidiaryConfiguration());
            modelBuilder.ApplyConfiguration(new UserBookConfiguration());
            modelBuilder.ApplyConfiguration(new UserReadingRoomConfiguration());
            modelBuilder.ApplyConfiguration(new UserSubsidiaryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
