using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDBSample.Api;
using MongoDBSample.Repository;

namespace MongoDBSample.AcceptanceTest.ApiTests
{
    public class TestStartup
    {
        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment CurrentEnvironment;

        public TestStartup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
            this.CurrentEnvironment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISampleRepository, SampleRepository>();

            services
              .AddMvc()
              .AddApplicationPart(typeof(Startup).Assembly)
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
