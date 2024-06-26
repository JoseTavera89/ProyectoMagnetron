﻿using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IGenericService<PersonaDto> _genericService;

        public PersonaController(IGenericService<PersonaDto> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet("GetPersonasAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<PersonaDto>>>> GetPersonasAsync()
        {

            var response = await _genericService.GetAll();
            return Ok(response);

        }

        [HttpGet("GetPersonasAsync/{id}")]
        public async Task<ActionResult<ResponseDto<PersonaDto>>> GetPersonaAsync(int id)
        {
            var response = await _genericService.Get(id);
            if (response.Value == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("AddAPersonaAsync")]
        public async Task<ActionResult<ResponseDto<PersonaDto>>> AddAPersonaAsync(PersonaDto persona)
        {

            var response = await _genericService.Add(persona);
            return Ok(response);
        }

        [HttpPut("UpdatePersonaAsync/{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> UpdatePersonaAsync(int id, PersonaDto persona)
        {
            if (id != persona.Per_Id)
            {
                return BadRequest();
            }
            var response = await _genericService.Update(persona);
            return Ok(response);
        }

        [HttpDelete("DeletePersonaAsync")]
        public async Task<ActionResult<ResponseDto<bool>>> DeletePersonaAsync(PersonaDto persona)
        {
            var response = await _genericService.Delete(persona);
            return Ok(response);
        }
    }
}
