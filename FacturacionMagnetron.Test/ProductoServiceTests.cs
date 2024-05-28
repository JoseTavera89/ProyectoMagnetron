using FacturacionMagnetron.Application.Services;
using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using Mapster;
using Moq;

namespace FacturacionMagnetron.Test
{
    [TestFixture]
    public class ProductoServiceTests
    {
        private Mock<IUowMagnetron> _mockUowMagnetron;
        private ProductoDto productoDto;
        private ProductoService _productoService;

        [SetUp]
        public void Setup()
        {
            _mockUowMagnetron = new Mock<IUowMagnetron>();
            _productoService = new ProductoService(_mockUowMagnetron.Object);

            productoDto = new ProductoDto
            {
                Prod_Id = 1,
                Prod_Descripcion = "Gorras",
                Prod_Costo = 5,
                Prod_Precio = 15,
                Prod_UM = "Unidad"
            };
        }

        [Test]
        public async Task Add_Product_ResponseTrue()
        {
            // Arrange
            var product = productoDto.Adapt<Producto>();
            _mockUowMagnetron.Setup(u => u.Producto.Add(It.IsAny<Producto>())).ReturnsAsync(true);

            // Act
            var response = await _productoService.Add(productoDto);

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }


        [Test]
        public async Task Delete_ExistingProductoDto_ReturnsTrue()
        {
            // Arrange
            var existingProducto = new Producto
            {
                Prod_Id = 1,
                Prod_Descripcion = "Gorras",
                Prod_Costo = 5,
                Prod_Precio = 15,
                Prod_UM = "Unidad"
            };
            _mockUowMagnetron.Setup(u => u.Producto.Get(It.IsAny<int>())).ReturnsAsync(existingProducto);
            _mockUowMagnetron.Setup(u => u.Producto.Delete(It.IsAny<Producto>())).ReturnsAsync(true);

            // Act
            var response = await _productoService.Delete(productoDto);

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
        [Test]
        public async Task Delete_NonExistingProducto_ReturnsFailure()
        {
            // Arrange
            var mockUow = new Mock<IUowMagnetron>();
            var service = new ProductoService(mockUow.Object);
            var nonExistingProductId = 2; 
            mockUow.Setup(u => u.Producto.Get(nonExistingProductId)).ReturnsAsync((Producto)null); 

            // Act
            var response = await service.Delete(new ProductoDto { Prod_Id = nonExistingProductId });

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(false));
            Assert.That(response.MessageError, Is.EqualTo("No existe el producto"));
        }

        [Test]
        public async Task Get_ValidId_ReturnsProductoDto()
        {
            // Arrange
            var existingProducto = productoDto.Adapt<Producto>();
            _mockUowMagnetron.Setup(u => u.Producto.Get(productoDto.Prod_Id)).ReturnsAsync(existingProducto);

            // Act
            var response = await _productoService.Get(productoDto.Prod_Id);

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public async Task GetAll_ReturnsListOfProductoDto()
        {
            // Arrange
            var productos = new List<Producto>
            {
                new Producto { Prod_Id = 1,Prod_Descripcion = "Gorras", Prod_Costo = 5,Prod_Precio = 15,Prod_UM = "Unidad"},
                new Producto {  Prod_Id = 2,Prod_Descripcion = "Balon", Prod_Costo = 15,Prod_Precio = 25,Prod_UM = "Unidad" },
            };
            var productosDto = productos.Adapt<IEnumerable<ProductoDto>>();
            _mockUowMagnetron.Setup(u => u.Producto.GetAll()).ReturnsAsync(productos);

            // Act
            var response = await _productoService.GetAll();

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
            Assert.IsNotNull(response.Value);
            var resultProductosDto = response.Value.ToList();
            Assert.That(resultProductosDto.Count, Is.EqualTo(productosDto.Count()));
        }

        [Test]
        public async Task Update_ExistingProducto_ReturnsSuccessResponse()
        {
            // Arrange

            var existingProducto = new Producto
            {
                Prod_Id = 1,
                Prod_Descripcion = "Gorras",
                Prod_Costo = 20,
                Prod_Precio = 15,
                Prod_UM = "Unidad"
            };

            _mockUowMagnetron.Setup(u => u.Producto.Get(productoDto.Prod_Id)).ReturnsAsync(existingProducto);
            _mockUowMagnetron.Setup(u => u.Producto.Update(It.IsAny<Producto>())).ReturnsAsync(true);

            // Act
            var response = await _productoService.Update(productoDto);

            // Assert
            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }

    }
}
