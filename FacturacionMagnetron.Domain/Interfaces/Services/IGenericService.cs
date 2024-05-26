using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Interfaces.Services
{
    public interface IGenericService<TEntity>
    {
        Task<ResponseDto<bool>> Add(TEntity obj);
        Task<ResponseDto<bool>> Update(TEntity obj);
        Task<ResponseDto<IEnumerable<TEntity>>> GetAll();
        Task<ResponseDto<TEntity>> Get(int id);
        Task<ResponseDto<bool>> Delete(TEntity obj);
    }
}
