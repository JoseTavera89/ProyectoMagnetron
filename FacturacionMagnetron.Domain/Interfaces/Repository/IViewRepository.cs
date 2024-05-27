using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Interfaces.Repository
{
    public interface IViewRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
    }
}
