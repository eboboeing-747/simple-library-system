﻿using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace IlsDb.Repository
{
    public class SubsidiaryRepository
    {
        public readonly LibraryDbContext _dbContext;

        public SubsidiaryRepository(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Create(SubsidiaryEntity subsidiary)
        {
            await this._dbContext.Subsidiaries.AddAsync(subsidiary);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsEmpty()
        {
            return !await this._dbContext.Subsidiaries.AnyAsync(library => true);
        }
    }
}
