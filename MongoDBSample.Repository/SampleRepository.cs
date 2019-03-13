using Microsoft.Extensions.Options;
using MongoDBSample.Domain;
using MongoDBSample.Domain.Settings;

namespace MongoDBSample.Repository
{
    public class SampleRepository : BaseRepository<SampleModel>, ISampleRepository
    { 
        private readonly DbContext<SampleModel> context = null;

        public SampleRepository(IOptions<DbConfiguration> configuration) : base(configuration)
        {
            this.context = new DbContext<SampleModel>(configuration);
        }
    }
}
