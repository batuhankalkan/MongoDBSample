using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDBSample.Domain;
using MongoDBSample.Repository;

namespace MongoDBSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleRepository sampleRepository;
        public SampleController(ISampleRepository sampleRepository)
        {
            this.sampleRepository = sampleRepository;
        }
        // GET api/sample
        [HttpGet]
        public async Task<IEnumerable<SampleModel>> Get()
        {
            return await sampleRepository.GetAllAsync();
        }

        // GET api/sample/1
        [HttpGet("{id}")]
        public async Task<SampleModel> Get(string id)
        {
            return await sampleRepository.GetAsync(id);
        }

        // POST api/sample
        [HttpPost]
        public void Post([FromBody] SampleModel item)
        {
            sampleRepository.SaveAsync(item);
        }

        // PUT api/sample/1
        [HttpPut("{id}")]
        public async void Put(string id, [FromBody] string name)
        {
            var item = await sampleRepository.GetAsync(id);
            item.Name = name;
            await sampleRepository.UpdateAsync(item);
        }

        // DELETE api/sample
        [HttpDelete("{id}")]
        public void Delete()
        {
            sampleRepository.DeleteAllAsync();
        }

        // DELETE api/sample/1
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            var item = await sampleRepository.GetAsync(id);
            await sampleRepository.DeleteAsync(item);
        }
    }
}
