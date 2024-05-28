using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Services;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using Mapster;


namespace FacturacionMagnetron.Application.Services
{
    public class PersonaService : IGenericService<PersonaDto>
    {
        private readonly IUowMagnetron _uowMagnetron;

        public PersonaService(IUowMagnetron uowMagnetron)
        {
            _uowMagnetron = uowMagnetron;
        }

        public async Task<ResponseDto<bool>> Add(PersonaDto obj)
        {
            var data = obj.Adapt<Persona>();
            var response = await _uowMagnetron.Persona.Add(data);
            SaveChanges();

            return ResponseDto<bool>.Success(response);
        }

        public async Task<ResponseDto<bool>> Delete(PersonaDto obj)
        {
            var data = await _uowMagnetron.Persona.Get(obj.Per_Id);
            if (data != null)
            {
                await _uowMagnetron.Persona.Delete(data);
                SaveChanges();
                return ResponseDto<bool>.Success(true);
            }
            return ResponseDto<bool>.Failure("No existe la persona");
        }

        public async Task<ResponseDto<PersonaDto>> Get(int id)
        {
            var response = await _uowMagnetron.Persona.Get(id);
            return ResponseDto<PersonaDto>.Success(response.Adapt<PersonaDto>());
        }

        public async Task<ResponseDto<IEnumerable<PersonaDto>>> GetAll()
        {
            var response = await _uowMagnetron.Persona.GetAll();
            return ResponseDto<IEnumerable<PersonaDto>>.Success(response.Adapt<IEnumerable<PersonaDto>>());
        }

        public async Task<ResponseDto<bool>> Update(PersonaDto obj)
        {

            var data = await _uowMagnetron.Persona.Get(obj.Per_Id);
            if (data != null)
            {
                data = obj.Adapt(data);
                await _uowMagnetron.Persona.Update(data);
                SaveChanges();
                return ResponseDto<bool>.Success(true);
            }
            return ResponseDto<bool>.Failure("No existe la persona");
        }
        private void SaveChanges()
        {
            _uowMagnetron.SaveChanges();
        }
    }
}
