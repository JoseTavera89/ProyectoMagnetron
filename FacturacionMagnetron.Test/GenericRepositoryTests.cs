using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using FacturacionMagnetron.Infrastructure.Persistense;
using FacturacionMagnetron.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Test
{
    [TestFixture]
    public class GenericRepositoryTests
    {

        private MagnetronDBContext _context;
        private IGenericRepository<TestEntity> _repository;
        private TestEntity entity;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MagnetronDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new MagnetronDBContext(options);
            _repository = new GenericRepository<TestEntity>(_context);
            entity = new TestEntity() { Id = 1, Name = "prueba1" };

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Entity1" },
                new TestEntity { Id = 2, Name = "Entity2" }
            };

            await _context.Set<TestEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Get_ShouldReturnEntityById()
        {
            // Arrange
            await _context.Set<TestEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Get(1);

            // Assert
            Assert.AreEqual(entity.Name, result.Name);
        }

        [Test]
        public async Task Add_ShouldAddEntity()
        {
            // Arrange

            // Act
            var result = await _repository.Add(entity);
            await _context.SaveChangesAsync();

            // Assert
            var addedEntity = await _context.Set<TestEntity>().FindAsync(1);
            Assert.NotNull(addedEntity);
            Assert.AreEqual(entity.Name, addedEntity.Name);
        }

        [Test]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            await _context.Set<TestEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            entity.Name = "UpdatedEntity";

            // Act
            var result = await _repository.Update(entity);

            // Assert
            var updatedEntity = await _context.Set<TestEntity>().FindAsync(1);
            Assert.AreEqual("UpdatedEntity", updatedEntity.Name);
        }

        [Test]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            await _context.Set<TestEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Delete(entity);
            await _context.SaveChangesAsync();

            // Assert
            var deletedEntity = await _context.Set<TestEntity>().FindAsync(1);
            Assert.Null(deletedEntity);
        }
    }
}
