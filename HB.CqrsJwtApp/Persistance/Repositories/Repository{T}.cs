using HB.CqrsJwtApp.Core.Application.Interfaces;
using HB.CqrsJwtApp.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HB.CqrsJwtApp.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> dbset;
        public Repository(AppDbContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
        }

        
        public async Task CreateAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task<List<T>> GetAllAsync()
        {
            return dbset.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await dbset.AsNoTracking().SingleOrDefaultAsync(filter); 
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbset.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
