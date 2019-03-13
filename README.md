# Asp.Net Core MongoDB async generic repository

A sample of async generic repository implementation using  MongoDB driver 

Usage
-----
We created base repository and all custom repositories will implement that base repository as shown below

```csharp
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

        // add custom methods
    }
}
```

Dependencies
------------
* .NET Core 2.2
* MongoDB.Driver 2.7.3
* NUnit 3.11.0

