using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDBSample.Domain;
using MongoDBSample.Repository;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoDBSample.AcceptanceTest.ApiTests
{
    [TestFixture]
    public class SampleControllerTests : WebApplicationFactory<TestStartup>
    {
        private readonly WebApplicationFactory<TestStartup> factory;
        public IDbContext<SampleModel> context { get; set; }
        public FilterDefinition<SampleModel> filter { get; set; }


        public SampleControllerTests()
        {
            this.factory = this;
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return
              WebHost
              .CreateDefaultBuilder()
              .UseStartup<TestStartup>()
              .UseKestrel()
              .UseEnvironment("Test")
              .ConfigureServices(services =>
              {
                  services.AddSingleton(StubDbContext());
              });
        }

        [SetUp]
        public void SetUp()
        {
            this.filter = Builders<SampleModel>.Filter.Eq(_ => _.Id, "1");
        }

        [Test]
        public async Task Should_BeSuccessfully_When_GetCalled()
        {
            // ARRANGE
            var client = this.factory.CreateClient();
            var url = "/api/sample/1";

            // ACT
            var response = client.GetAsync(url).Result;
            var actual = await response.Content.ReadAsStringAsync();

            // ASSERT
            Assert.AreEqual(true, response.IsSuccessStatusCode);

            var filter = Builders<SampleModel>.Filter.Eq(_ => _.Id, "1");
            await this.context
                .Received(1)
                .Entity.Find(this.filter)
                       .FirstOrDefaultAsync();
        }

        private IDbContext<SampleModel> StubDbContext()
        {
            this.context = Substitute.For<IDbContext<SampleModel>>();

            var data = new List<SampleModel>
            {
                new SampleModel
                {
                    Id = "1",
                    InternalId = new ObjectId("5dc1039a1521eaa36835e540"),
                    Name = "Test"
                }
            };

            var cursorMock = Substitute.For<IAsyncCursor<SampleModel>>();
            cursorMock.MoveNextAsync().Returns(Task.FromResult(true), Task.FromResult(false));
            cursorMock.Current.Returns(data);

            var ff = Substitute.For<IFindFluent<SampleModel, SampleModel>>();
             ff.ToCursorAsync().Returns(Task.FromResult(cursorMock));
             ff.Limit(1).Returns(ff);

            this.context.Entity.Find(this.filter).Returns(ff);

            return this.context;
        }
    }
}
