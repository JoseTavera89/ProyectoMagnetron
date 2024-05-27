using FacturacionMagnetron.Api.Controllers;
using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Test
{
    [TestFixture]
    public class ProductoControllerTests
    {
        private ProductoController _productoController;
        private Mock<IGenericService<ProductoDto>> _mockGenericService;

        [SetUp]
        public void Setup()
        {
            _mockGenericService = new Mock<IGenericService<ProductoDto>>();
            _productoController = new ProductoController(_mockGenericService.Object);
        }

        [Test]
        public async Task GetFacturasAsync_ReturnsOkResponseWithListOfProductos()
        {
            // Arrange
            var productos = new List<ProductoDto> { /* crea una lista de productos simulados */ };
            var responseDto = ResponseDto<IEnumerable<ProductoDto>>.Success(productos);
            _mockGenericService.Setup(service => service.GetAll()).ReturnsAsync(responseDto);

            // Act
            var result = await _productoController.GetProductosAsync();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(responseDto, okResult.Value);
        }
        [Test]
        public async Task AddAFacturaAsync_ValidProductoDto_ReturnsOkResponseWithSuccessResponse()
        {
            // Arrange
            var productoDto = new ProductoDto { /* Proporciona los datos necesarios para el producto */ };
            var responseDto = ResponseDto<bool>.Success(true);
            _mockGenericService.Setup(service => service.Add(productoDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _productoController.AddAProductoAsync(productoDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(responseDto, okResult.Value);
        }

    }
}
