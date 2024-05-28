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
        private ProductoDto productoDtoA;
        private ProductoDto productoDtoB;

        [SetUp]
        public void Setup()
        {
            _mockGenericService = new Mock<IGenericService<ProductoDto>>();
            _productoController = new ProductoController(_mockGenericService.Object);
            productoDtoA = new ProductoDto
            {
                Prod_Id = 1,
                Prod_Descripcion = "Gorras",
                Prod_Costo = 5,
                Prod_Precio = 15,
                Prod_UM = "Unidad"
            };
            productoDtoB = new ProductoDto
            {
                Prod_Id = 2,
                Prod_Descripcion = "Camisas",
                Prod_Costo = 15,
                Prod_Precio = 25,
                Prod_UM = "Unidad"
            };
        }

        [Test]
        public async Task GetProductosAsync_ReturnsListOfProductoDto()
        {
            // Arrange
            var productosDto = new List<ProductoDto> { productoDtoA, productoDtoB };
            var responseDto = ResponseDto<IEnumerable<ProductoDto>>.Success(productosDto);
            _mockGenericService.Setup(s => s.GetAll()).ReturnsAsync(responseDto);

            // Act
            var actionResult = await _productoController.GetProductosAsync();

            // Assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var responseData = okResult.Value as ResponseDto<IEnumerable<ProductoDto>>;
            Assert.IsNotNull(responseData);
            Assert.That(responseData.Value, Is.EqualTo(productosDto));
        }

        [Test]
        public async Task GetProductoAsync_ExistingId_ReturnsProductoDto()
        {
            // Arrange

            var responseDto = ResponseDto<ProductoDto>.Success(productoDtoA);
            _mockGenericService.Setup(s => s.Get(productoDtoA.Prod_Id)).ReturnsAsync(responseDto);

            // Act
            var actionResult = await _productoController.GetProductoAsync(productoDtoA.Prod_Id);

            // Assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var responseData = okResult.Value as ResponseDto<ProductoDto>;
            Assert.IsNotNull(responseData);
            Assert.That(responseData.Value, Is.EqualTo(productoDtoA));
        }

        [Test]
        public async Task GetProductoAsync_NonExistingId_ReturnsNotFound()
        {
            // Arrange;
            var nonExistingProductId = 3;
            var responseDto = ResponseDto<ProductoDto>.Failure("No existe el producto");
            _mockGenericService.Setup(s => s.Get(nonExistingProductId)).ReturnsAsync(responseDto);

            // Act
            var actionResult = await _productoController.GetProductoAsync(nonExistingProductId);

            // Assert
            Assert.That(actionResult.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task AddAProductoAsync_ValidProducto_ReturnsCreatedResponse()
        {
            // Arrange

            var responseDto = ResponseDto<bool>.Success(true);
            _mockGenericService.Setup(s => s.Add(productoDtoA)).ReturnsAsync(responseDto);

            // Act
            var actionResult = await _productoController.AddAProductoAsync(productoDtoA);

            // Assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

        }



        [Test]
        public async Task AddAProductoAsync_InvalidProducto_ReturnsBadRequest()
        {
            // Arrange
            var responseDto = ResponseDto<bool>.Failure("Error al agregar el producto");
            _mockGenericService.Setup(s => s.Add(productoDtoA)).ReturnsAsync(responseDto);

            // Act
            var actionResult = await _productoController.AddAProductoAsync(productoDtoA);

            // Assert
            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Null);
    
            Assert.That(responseDto.MessageError, Is.EqualTo("Error al agregar el producto"));
        }
        [Test]
        public async Task PutProductoAsync_ValidIdAndProducto_ReturnsOkResponse()
        {
            // Arrange
            var mockService = new Mock<IGenericService<ProductoDto>>();
            var controller = new ProductoController(mockService.Object);
            var productId = 1; // ID válido de un producto existente
            var productoDto = new ProductoDto { Prod_Id = productId /* Configura los datos del producto DTO */ };
            var responseDto = ResponseDto<bool>.Success(true);
            mockService.Setup(s => s.Update(productoDto)).ReturnsAsync(responseDto);

            // Act
            var actionResult = await controller.PutProductoAsync(productId, productoDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var responseData = okResult.Value as ResponseDto<bool>;
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.IsSuccess);
            Assert.AreEqual(true, responseData.Value);
        }

        [Test]
        public async Task PutProductoAsync_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<IGenericService<ProductoDto>>();
            var controller = new ProductoController(mockService.Object);
            var productId = 1; // ID válido de un producto existente
            var productoDto = new ProductoDto { Prod_Id = 2 /* ID diferente al esperado */ };

            // Act
            var actionResult = await controller.PutProductoAsync(productId, productoDto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(actionResult.Result);
            var badRequestResult = actionResult.Result as BadRequestResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public async Task PutProductoAsync_MismatchedId_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<IGenericService<ProductoDto>>();
            var controller = new ProductoController(mockService.Object);
            var productId = 1; // ID válido de un producto existente
            var productoDto = new ProductoDto { Prod_Id = productId /* Mismo ID pero diferente al del parámetro */ };

            // Act
            var actionResult = await controller.PutProductoAsync(productId + 1, productoDto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(actionResult.Result);
            var badRequestResult = actionResult.Result as BadRequestResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }
        [Test]
        public async Task DeleteProductoAsync_ExistingProducto_ReturnsOkResponse()
        {
            // Arrange
            var mockService = new Mock<IGenericService<ProductoDto>>();
            var controller = new ProductoController(mockService.Object);
            var productId = 1; // ID válido de un producto existente
            var productoDto = new ProductoDto { Prod_Id = productId /* Configura los datos del producto DTO */ };
            var responseDto = ResponseDto<bool>.Success(true);
            mockService.Setup(s => s.Delete(productoDto)).ReturnsAsync(responseDto);

            // Act
            var actionResult = await controller.DeleteProductoAsync(productoDto);

            // Assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var responseData = okResult.Value as ResponseDto<bool>;
            Assert.That(responseData, Is.Not.Null);
            Assert.That(responseData.IsSuccess, Is.True);
            Assert.That(responseData.Value, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteProductoAsync_NonExistingProducto_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<IGenericService<ProductoDto>>();
            var controller = new ProductoController(mockService.Object);
            var nonExistingProductoDto = new ProductoDto { /* Configura los datos de un producto DTO que no existe */ };
            var responseDto = ResponseDto<bool>.Failure("No existe el producto");
            mockService.Setup(s => s.Delete(nonExistingProductoDto)).ReturnsAsync(responseDto);

            // Act
            var actionResult = await controller.DeleteProductoAsync(nonExistingProductoDto);

            // Assert
            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            Assert.That(notFoundResult, Is.Null);

            Assert.That(responseDto.MessageError, Is.EqualTo("No existe el producto"));
        }

    }
}
