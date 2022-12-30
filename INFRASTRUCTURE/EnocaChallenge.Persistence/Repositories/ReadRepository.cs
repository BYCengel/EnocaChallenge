using EnocaChallenge.Application.Repositories;
using EnocaChallenge.Domain.Entities.Common;
using EnocaChallenge.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ShopDbContext _context;

        public ReadRepository(ShopDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        /// <summary>
        /// Gets all records of T.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return Table;
        }

        /// <summary>
        /// Gets the T with given id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
