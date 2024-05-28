using FacturacionMagnetron.Application.Services;
using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using Mapster;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Test
{
    [TestFixture]
    public class PersonaServiceTest
    {
        private Mock<IUowMagnetron> _mockUowMagnetron;
        private PersonaDto personaDto;
        private PersonaService _personaService;

        [SetUp]
        public void Setup()
        {
            _mockUowMagnetron = new Mock<IUowMagnetron>();
            _personaService = new PersonaService(_mockUowMagnetron.Object);

            personaDto = new PersonaDto
            {
               Per_Id= 1,
               Per_Nombre="juan",
               Per_Apellido="perez",
               Per_TipoDocumento="cc",
               Per_Documento=123456
            };
        }

        [Test]
        public async Task Add_Persona_ResponseTrue()
        {
            // Arrange
            var persona = personaDto.Adapt<Persona>();
            _mockUowMagnetron.Setup(u => u.Persona.Add(It.IsAny<Persona>())).ReturnsAsync(true);

            // Act
            var response = await _personaService.Add(personaDto);

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }


        [Test]
        public async Task Delete_ExistingPersonaDto_ReturnsTrue()
        {
            // Arrange
            var existingPersona = new Persona
            {
                Per_Id = 1,
                Per_Nombre = "juan",
                Per_Apellido = "perez",
                Per_TipoDocumento = "cc",
                Per_Documento = 123456
            };
            _mockUowMagnetron.Setup(u => u.Persona.Get(It.IsAny<int>())).ReturnsAsync(existingPersona);
            _mockUowMagnetron.Setup(u => u.Persona.Delete(It.IsAny<Persona>())).ReturnsAsync(true);

            // Act
            var response = await _personaService.Delete(personaDto);

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
        [Test]
        public async Task Delete_NonExistingpersona_ReturnsFailure()
        {
            // Arrange
            var mockUow = new Mock<IUowMagnetron>();
            var service = new PersonaService(mockUow.Object);
            var nonExistingPersonId = 2;
            mockUow.Setup(u => u.Persona.Get(nonExistingPersonId)).ReturnsAsync((Persona)null);

            // Act
            var response = await service.Delete(new PersonaDto { Per_Id = nonExistingPersonId });

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(false));
            Assert.That(response.MessageError, Is.EqualTo("No existe la persona"));
        }

        [Test]
        public async Task Get_ValidId_ReturnsPersonaDto()
        {
            // Arrange
            var existingpersona = personaDto.Adapt<Persona>();
            _mockUowMagnetron.Setup(u => u.Persona.Get(personaDto.Per_Id)).ReturnsAsync(existingpersona);

            // Act
            var response = await _personaService.Get(personaDto.Per_Id);

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public async Task GetAll_ReturnsListOfPersonaDto()
        {
            // Arrange
            var personas = new List<Persona>
            {
                new Persona {  Per_Id= 1,Per_Nombre="juana",Per_Apellido="perez",Per_TipoDocumento="CC",Per_Documento=123456},
                new Persona {  Per_Id= 1,Per_Nombre="juan",Per_Apellido="gomez",Per_TipoDocumento="CC",Per_Documento=789654}
            };
            var personasDto = personas.Adapt<IEnumerable<PersonaDto>>();
            _mockUowMagnetron.Setup(u => u.Persona.GetAll()).ReturnsAsync(personas);

            // Act
            var response = await _personaService.GetAll();

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
            Assert.IsNotNull(response.Value);
            var resultpersonasDto = response.Value.ToList();
            Assert.That(resultpersonasDto.Count, Is.EqualTo(personasDto.Count()));
        }

        [Test]
        public async Task Update_ExistingPersona_ReturnsSuccessResponse()
        {
            // Arrange

            var existingPersona = new Persona
            {
                Per_Id = 1,
                Per_Nombre = "juan",
                Per_Apellido = "perez",
                Per_TipoDocumento = "cc",
                Per_Documento = 123456
            };

            _mockUowMagnetron.Setup(u => u.Persona.Get(personaDto.Per_Id)).ReturnsAsync(existingPersona);
            _mockUowMagnetron.Setup(u => u.Persona.Update(It.IsAny<Persona>())).ReturnsAsync(true);

            // Act
            var response = await _personaService.Update(personaDto);

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
    }
}
