using MongoDBSample.Domain;

namespace MongoDBSample.Repository
{
    public interface ISampleRepository : IBaseRepository<SampleModel, string>
    {
    }
}
