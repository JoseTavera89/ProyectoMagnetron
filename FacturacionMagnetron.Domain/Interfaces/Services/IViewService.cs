using FacturacionMagnetron.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Interfaces.Services
{

    public interface IViewService<TEntity>
    {
        Task<ResponseDto<IEnumerable<TEntity>>> GetAll();
    }
}
