using Dotnet8_EFCore.DataAccess;
using Dotnet8_EFCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8_EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController(ILogger<CountryController> logger, ILocationDb locationDb) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Country>> Get()
        {
            return await locationDb.CountryRepository.ToListAsync(o => true);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Country>> Get(long id)
        {
            return await locationDb.CountryRepository.GetAsync(o => o.Id == id);
        }

        [HttpGet("{id}/states")]
        public async Task<IEnumerable<Country>> GetStates(long id)
        {
            var countries =  await locationDb.CountryRepository.ToListAsync(q => q.Id == id, new List<string> { "states"});
            return countries;
        }

        [HttpPost]
        public async Task<Country> Post([FromBody] Country country)
        {
            return await locationDb.CountryRepository.CreateAsync(country);
        }

        [HttpPut("{id}")]
        public async Task<Country> Put(long id, [FromBody] Country country)
        {
            country.Id = id;
            return await locationDb.CountryRepository.UpdateAsync(country);
        }

        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            var countries =  await locationDb.CountryRepository.GetAsync(o => o.Id == id);
            countries.First().Id = id;
            await locationDb.CountryRepository.DeleteAsync(countries.First());
        }
    }
}
