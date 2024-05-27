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
    public class ViewRepository<TEntity> : IViewRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _dbSet;

        public ViewRepository(MagnetronDBContext context)
        {
            _dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
