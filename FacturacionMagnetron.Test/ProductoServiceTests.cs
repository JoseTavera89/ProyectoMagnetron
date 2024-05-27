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
    public class ProductoServiceTests
    {
        private Mock<IUowMagnetron> _mockUowMagnetron;
        private ProductoDto producto;
        private DbContextOptions<MagnetronDBContext> options;
        private ProductoService _productoService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MagnetronDBContext>()
                .UseInMemoryDatabase(databaseName: "Tem_MagnetronDb").Options;

            _mockUowMagnetron = new Mock<IUowMagnetron>();
            _productoService = new ProductoService(_mockUowMagnetron.Object);


            producto = new ProductoDto
            {
                Prod_Id = 1,
                Prod_Descripcion = "Gorras",
                Prod_Costo = 5,
                Prod_Precio = 15,
                Prod_UM = "Unidad"
            };
        }

        [Test]
        public async Task Add_Person_ResponseTrue()
        {
            // Arrange

           // var contex = new MagnetronDBContext(options);
           // await contex.Set<Persona>().AddAsync(persona);
           // await contex.SaveChangesAsync();

            _mockUowMagnetron.Setup(u => u.Producto.Get(It.IsAny<int>())).ReturnsAsync(new Producto());

            // Act
            var response = await _productoService.Add(producto);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(true, response.IsSuccess);
        }
        [Test]
        public async Task Delete_ExistingProductoDto_ReturnsSuccessResponse()
        {
            // Arrange
            var existingProducto = new Producto { Prod_Id = 1};

            _mockUowMagnetron.Setup(u => u.Producto.Get(producto.Prod_Id)).ReturnsAsync(existingProducto);
            _mockUowMagnetron.Setup(u => u.Producto.Delete(It.IsAny<Producto>())).ReturnsAsync(true);

            // Act
            var response = await _productoService.Delete(producto);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(true, response.Value);
        }

    }
}
