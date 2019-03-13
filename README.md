# Asp.Net Core MongoDB async generic repository

An example of async generic repository implementation using  MongoDB driver 

usage
-----
```
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
```

Dependencies
------------
* .NET Core 2.2
* MongoDB.Driver 2.7.3
* NUnit 3.11.0

