using MongoDBSample.Domain;

namespace MongoDBSample.Repository
{
    public class SampleRepository : BaseRepository<SampleModel>, ISampleRepository
    { 
        private readonly IDbContext<SampleModel> context;

        public SampleRepository(IDbContext<SampleModel> context) : base(context)
        {
            this.context = context;
        }
    }
}
