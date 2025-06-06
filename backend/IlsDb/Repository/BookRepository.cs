﻿using IlsDb.Entity.BaseEntity;

namespace IlsDb.Repository
{
    public class BookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Create(BookEntity book)
        {
            await this._dbContext.Books.AddAsync(book);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
