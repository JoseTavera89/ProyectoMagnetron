using FacturacionMagnetron.Application.Services;
using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using FacturacionMagnetron.Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Test
{
    [TestFixture]
    public class FacturaServiceTests
    {
        private FacturaService _facturaService;
        private PersonaService _personaService;
        private ProductoService _productoService;

        private Mock<IUowMagnetron> _mockUowMagnetron;
        private Persona persona;
        private Producto producto;
        private FacturaEncabezadoDto facturaEncabezado;
        private FacturaDetalleDto facturaDetalle;

        private DbContextOptions<MagnetronDBContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MagnetronDBContext>()
                .UseInMemoryDatabase(databaseName: "Tem_MagnetronDb").Options;

            _mockUowMagnetron = new Mock<IUowMagnetron>();
            _facturaService = new FacturaService(_mockUowMagnetron.Object);
            _personaService = new PersonaService(_mockUowMagnetron.Object);
            _productoService = new ProductoService(_mockUowMagnetron.Object);

            persona = new Persona
            {
                Per_Id = 1,
                Per_Nombre = "Pedro",
                Per_Apellido = "Perez",
                Per_TipoDocumento = "CC",
                Per_Documento = 12345678
            };
            producto = new Producto
            {
                Prod_Id = 1,
                Prod_Descripcion = "Gorras",
                Prod_Costo = 5,
                Prod_Precio = 15,
                Prod_UM = "Unidad"
            };
            facturaEncabezado = new FacturaEncabezadoDto
            {
                FEnc_Id = 1,
                FEnc_Numero = 2001,
                FEnc_Fecha = DateTime.Now,
                Per_Id = 1
            };
            facturaDetalle = new FacturaDetalleDto
            {
                FDet_Id = 1,
                FDet_Linea = 1,
                FDet_Cantidad = 2,
                Prod_Id = 1,
                FEnc_Id = 1
            };

        }
        [Test]
        public async Task Add_ValidFacturaDto_ReturnsSuccessResponse()
        {
            // Arrange

            var contex = new MagnetronDBContext(options);

            await contex.Set<Producto>().AddAsync(producto);
            await contex.Set<Persona>().AddAsync(persona);
            await contex.SaveChangesAsync();

            var facturaDto = new FacturaDto
            {
                FacturaEncabezado = facturaEncabezado,
                FacturaDetalle = facturaDetalle
            };

            _mockUowMagnetron.Setup(u => u.FacturaEncabezado.Add(It.IsAny<FacturaEncabezado>()));
            _mockUowMagnetron.Setup(u => u.FacturaDetalle.Add(It.IsAny<FacturaDetalle>()));
            _mockUowMagnetron.Setup(u => u.Persona.Get(It.IsAny<int>())).ReturnsAsync(new Persona());
            _mockUowMagnetron.Setup(u => u.Producto.Get(It.IsAny<int>())).ReturnsAsync(new Producto());

            // Act
            var response = await _facturaService.Add(facturaDto);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(true, response.IsSuccess);
        }
    }
}
