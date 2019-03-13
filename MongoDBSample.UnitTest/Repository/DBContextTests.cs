using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBSample.Domain;
using MongoDBSample.Domain.Settings;
using MongoDBSample.Repository;
using NUnit.Framework;
using System;

namespace MongoDBSample.UnitTest.Repository
{
    [TestFixture]
    public class DBContextTests
    {
        [Test]
        public void Should_BeSampleModelInstance_When_DBContextCreated()
        {
            // Arrange
            var configuration = new DbConfiguration
            {
                Database = "example",
                ConnectionString = "mongodb://test"
            };

            // Act
            var context = new DbContext<SampleModel>(Options.Create(configuration));

            // Assert
            Assert.IsInstanceOf<IMongoCollection<SampleModel>>(context.Entity);
        }

        [Test]
        public void Should_ThrowAnException_When_ConnectionStringIsNull()
        {
            // Arrange
            var configuration = new DbConfiguration { };

            //Act
            var ex = Assert.Throws<ArgumentNullException>(() => new DbContext<SampleModel>(Options.Create(configuration)));

            //Assert
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: connectionString"));
        }
    }
}
