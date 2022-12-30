using EnocaChallenge.Application.Repositories;
using EnocaChallenge.Domain.Entities.Common;
using EnocaChallenge.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        readonly private ShopDbContext _context;

        public WriteRepository(ShopDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        /// <summary>
        /// Adds the given entity to database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            SaveAsync();
            return entityEntry.State == EntityState.Added;
        }

        /// <summary>
        /// Updates the given model in database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            Save();
            return entityEntry.State == EntityState.Modified;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        } 
    }
}
