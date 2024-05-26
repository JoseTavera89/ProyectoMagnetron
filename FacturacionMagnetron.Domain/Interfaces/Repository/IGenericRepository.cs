using FacturacionMagnetron.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Interfaces.Repository
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<bool> Add(TEntity data);
        Task<bool> Update(TEntity data);
        Task<bool> Delete(TEntity data);

    }
}
