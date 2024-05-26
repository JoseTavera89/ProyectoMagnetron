using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly MagnetronDBContext _context;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(MagnetronDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        async Task<IEnumerable<TEntity>> IGenericRepository<TEntity>.GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        async Task<bool> IGenericRepository<TEntity>.Add(TEntity data)
        {
            await _dbSet.AddAsync(data);
            return true;
        }

        async Task<bool> IGenericRepository<TEntity>.Update(TEntity data)
        {
            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IGenericRepository<TEntity>.Delete(TEntity data)
        {
            _dbSet.Remove(data);
            return true;
        }
    }
}
